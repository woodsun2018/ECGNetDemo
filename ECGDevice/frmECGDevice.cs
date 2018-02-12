using ShareCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ECGDevice
{
    public partial class frmECGDevice : Form
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private SampleECG MySampleECG = SampleECG.Instance;
        private TcpNetClient MyTcpNetClient = TcpNetClient.Instance;

        private Int32 DeviceID = 100;
        private string PatientName = "张飞";

        private Int32 FrameNo = 1;

        private enum LinkTypes { Http = 0, Tcp };
        private LinkTypes LinkType = LinkTypes.Http;

        public frmECGDevice()
        {
            InitializeComponent();
        }

        private void frmECGDevice_Load(object sender, EventArgs e)
        {
            MyConfig.Init(MyConfig.Filename);
            MyConfig.ReadConfig();

            this.txbDeviceID.Text = DeviceID.ToString();

            this.txbPatientName.Text = PatientName;

            //最多输入字符数不超过个人姓名最大长度
            this.txbPatientName.MaxLength = Study.PersonNameMaxLen;

            List<string> linkTypes = Enum.GetNames(typeof(LinkTypes)).ToList();
            linkTypes.ForEach(x => cboLinkType.Items.Add(x));
            cboLinkType.SelectedIndex = 0;

            this.txbWebServerUrl.Text = MyConfig.Config.WebServerUrl;

            this.txbTcpServerIP.Text = MyTcpNetClient.TcpServerIP;
            this.txbTcpServerPort.Text = MyTcpNetClient.TcpServerPort.ToString();

            //开始检测
            btnStart.Click += (s1, e1) => StartStudy();

            ShowInfo($"点击开始检测按钮采集心电图数据");
        }

        private void ShowInfo(string msg)
        {
            this.rtbInfo.AppendText($"{DateTime.Now}, {msg}{Environment.NewLine}");
        }

        //开始检测
        private void StartStudy()
        {
            DeviceID = int.Parse(this.txbDeviceID.Text);
            PatientName = this.txbPatientName.Text.Trim();

            LinkType = (LinkTypes)(cboLinkType.SelectedIndex);

            MyConfig.Config.WebServerUrl = this.txbWebServerUrl.Text.Trim();
            MyConfig.SaveConfig();

            MyTcpNetClient.TcpServerIP = this.txbTcpServerIP.Text.Trim();
            MyTcpNetClient.TcpServerPort = int.Parse(this.txbTcpServerPort.Text);

            //采集心电图
            byte[] sampleBuf = MySampleECG.SampleECGData();

            //创建检查记录
            Study study = CreateStudy(sampleBuf);

            //发送检查记录到服务器
            SendStudyToServerAsync(study);
        }

        //创建检查记录
        private Study CreateStudy(byte[] sampleBuf)
        {
            //创建一个新的检查记录
            Study study = new Study();

            study.DeviceID = DeviceID;
            study.PatientName = PatientName;
            study.FrameNo = FrameNo;
            study.BioBuf = sampleBuf;

            FrameNo++;

            return study;
        }

        //发送检查记录到服务器
        private async Task SendStudyToServerAsync(Study study)
        {
            try
            {
                if (LinkType == LinkTypes.Http)
                {
                    //通过Http方式发送检查记录到服务器

                    //初始化检查记录档案客户端
                    MyStudyWebClient.Init(MyConfig.Config.WebServerUrl);

                    //添加记录
                    await MyStudyWebClient.AddStudy(study);
                }
                else
                {
                    //通过Tcp方式发送检测记录到服务器
                    await MyTcpNetClient.SendStudyAsync(study);
                }

                ShowInfo($"SendStudyToServerAsync, {LinkType}发送检测数据序号{study.FrameNo}成功");
            }
            catch (Exception ex)
            {
                ShowInfo($"SendStudyToServerAsync, {LinkType}发送检测数据序号{study.FrameNo}失败{Environment.NewLine}{ex}");
            }
        }

    }
}
