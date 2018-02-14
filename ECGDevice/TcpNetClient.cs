using Newtonsoft.Json;
using ShareCode;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ECGDevice
{
    //Tcp网络客户端
    public class TcpNetClient
    {
        //public string TcpServerIP { get; set; } = "127.0.0.1";
        public string TcpServerIP { get; set; } = "193.112.19.82";//腾讯云
        public int TcpServerPort { get; set; } = 6000;

        private static TcpNetClient _instance = new TcpNetClient();
        public static TcpNetClient Instance { get { return _instance; } }

        private TcpNetClient()
        {
        }

        #region TCP方式发送检查记录到服务器

        //通过Tcp方式发送检测记录到服务器
        public async Task<bool> SendStudyAsync(Study study)
        {
            //打包
            byte[] frameBuf = Study2Array(study);

            //发送数据包到服务器
            try
            {
                IPAddress serverIPAddress = IPAddress.Parse(TcpServerIP);

                using (TcpClient DeviceTcpClient = new TcpClient())
                {
                    await DeviceTcpClient.ConnectAsync(serverIPAddress, TcpServerPort);

                    if (DeviceTcpClient.Connected)
                    {
                        using (NetworkStream netStream = DeviceTcpClient.GetStream())
                        {
                            await netStream.WriteAsync(frameBuf, 0, frameBuf.Length);
                        }
                    }

                    //发送完毕，断开连接。
                    //使用短连接方案，可以减少对云服务器TCP并发连接数的占用，提高云服务器利用率
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
        }

        //打包
        private byte[] Study2Array(Study study)
        {
            //List<byte> buf = new List<byte>();

            //buf.AddRange(BitConverter.GetBytes(study.ID));
            //buf.AddRange(study.SampleID.ToByteArray());
            //buf.AddRange(BitConverter.GetBytes(study.SampleTime.ToBinary()));
            //buf.AddRange(BitConverter.GetBytes(study.DeviceID));
            //buf.AddRange(BitConverter.GetBytes(study.FrameNo));
            //buf.AddRange(study.BioBuf);
            //buf.AddRange(Encoding.Unicode.GetBytes(study.PatientName.PadRight(Study.PersonNameMaxLen)));
            //buf.AddRange(Encoding.Unicode.GetBytes(study.Diagnose.PadRight(Study.DiagnoseMaxLen)));
            //buf.AddRange(Encoding.Unicode.GetBytes(study.DoctorName.PadRight(Study.PersonNameMaxLen)));
            //buf.AddRange(BitConverter.GetBytes(study.ModifyTime.ToBinary()));

            //return buf.ToArray();

            string jsonStr = JsonConvert.SerializeObject(study);
            return Encoding.UTF8.GetBytes(jsonStr);
        }

        #endregion
    }
}
