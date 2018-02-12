using ShareCode;
using System;
using System.Windows.Forms;

namespace DoctorStation
{
    public partial class frmLogin : Form
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            //初始化配置信息
            MyConfig.Init(MyConfig.Filename);
            MyConfig.ReadConfig();

            txbWebServerUrl.Text = MyConfig.Config.WebServerUrl;
            txbAccount.Text = MyConfig.Config.CurrentUserInfo.Account;
            txbPassword.UseSystemPasswordChar = true;

            btnLogin.Click += (s1, e1) => Login();
            btnExit.Click += (s1, e1) => ExitApp();
        }

        //登录
        private void Login()
        {
            //检查帐号密码……

            //保存配置信息
            MyConfig.Config.WebServerUrl = txbWebServerUrl.Text.Trim();
            MyConfig.Config.CurrentUserInfo.Account = txbAccount.Text.Trim();
            MyConfig.SaveConfig();

            //初始化检查记录档案客户端
            MyStudyWebClient.Init(MyConfig.Config.WebServerUrl);

            this.Close();
        }

        //退出软件
        private void ExitApp()
        {
            Application.ExitThread();

            Environment.Exit(0);
        }

    }
}
