using ECGWeb.DB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShareCode;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECGWeb
{
    //ECG设备TCP服务器
    public class DeviceTcpServer
    {
        private IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        private TcpListener tcpListener = null;
        private int ServerPort = 6000;

        public DeviceTcpServer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetRequiredService<ILogger<DeviceTcpServer>>();
        }

        public void ShowInfo(string info)
        {
            _logger.LogDebug($"{DateTime.Now}, {info}");
        }

        //建立TCP服务器，接入ECG设备
        public async Task StartServer()
        {
            try
            {
                //创建TCP服务器
                tcpListener = new TcpListener(IPAddress.Any, ServerPort);

                //开始侦听
                tcpListener.Start();

                ShowInfo($"StartServer, 开始侦听{tcpListener.LocalEndpoint}……");

                while (true)
                {
                    //等待客户端连接
                    TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();

                    ShowInfo($"StartServer, 接入客户端{tcpClient.Client.RemoteEndPoint}");

                    //读取ECG设备发送的检查记录
                    ReadTcpClient(tcpClient);
                }
            }
            catch (Exception ex)
            {
                ShowInfo($"StartServer, {Environment.NewLine}{ex}");
            }
        }

        //读取ECG设备发送的检查记录
        private async Task ReadTcpClient(TcpClient tcpClient)
        {
            try
            {
                using (NetworkStream netStream = tcpClient.GetStream())
                {
                    while (true)
                    {
                        byte[] buf = new byte[1024];

                        int readLen = await netStream.ReadAsync(buf, 0, buf.Length);

                        //如果客户端TCP关闭，会马上返回0，从这里跳出
                        if (readLen == 0)
                            break;

                        //只取接收的有效部分去解包
                        buf = buf.Take(readLen).ToArray();

                        Array2Study(buf);
                    }

                    ShowInfo($"ReadTcpClient, 客户端{tcpClient.Client.RemoteEndPoint}, 通讯结束了");
                }
            }
            catch (Exception ex)
            {
                ShowInfo($"ReadTcpClient, 客户端{tcpClient.Client.RemoteEndPoint}, {Environment.NewLine}{ex}");
            }

            tcpClient.Dispose();
        }

        //解包
        private void Array2Study(byte[] buf)
        {
            Study study = new Study();

            int readIndex = 0;

            study.DeviceID = BitConverter.ToInt32(buf, readIndex);
            readIndex += 4;

            study.SampleID = new Guid(buf.Skip(readIndex).Take(16).ToArray());
            readIndex += 16;

            study.SampleTime = DateTime.FromBinary(BitConverter.ToInt64(buf, readIndex));
            readIndex += 8;

            study.DeviceID = BitConverter.ToInt32(buf, readIndex);
            readIndex += 4;

            study.FrameNo = BitConverter.ToInt32(buf, readIndex);
            readIndex += 4;

            study.BioBuf = buf.Skip(readIndex).Take(Study.BioBufLen).ToArray();
            readIndex += Study.BioBufLen;

            study.PatientName = Encoding.Unicode.GetString(buf, readIndex, Study.PersonNameMaxLen * 2);
            study.PatientName = study.PatientName.Trim();
            readIndex += Study.PersonNameMaxLen * 2;

            study.Diagnose = Encoding.Unicode.GetString(buf, readIndex, Study.DiagnoseMaxLen * 2);
            study.Diagnose = study.Diagnose.Trim();
            readIndex += Study.DiagnoseMaxLen * 2;

            study.DoctorName = Encoding.Unicode.GetString(buf, readIndex, Study.PersonNameMaxLen * 2);
            study.DoctorName = study.DoctorName.Trim();
            readIndex += Study.PersonNameMaxLen * 2;

            study.ModifyTime = DateTime.FromBinary(BitConverter.ToInt64(buf, readIndex));
            readIndex += 8;

            //增加检查记录到数据库
            AddStudyAsync(study);
        }

        //增加检查记录到数据库
        private async Task AddStudyAsync(Study study)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<StudyDBContext>();

                context.studies.Add(study);
                await context.SaveChangesAsync();
            }

            ShowInfo($"AddStudyAsync, 添加检查记录, 序号{study.FrameNo}");
        }

        //关闭
        public void StopServer()
        {
            try
            {
                tcpListener.Stop();
            }
            catch (Exception ex)
            {
                ShowInfo($"StopServer, {Environment.NewLine}{ex}");
            }
        }

    }//DeviceTcpServer
}
