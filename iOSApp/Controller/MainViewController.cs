using Foundation;
using ShareCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UIKit;

namespace iOSApp
{

    [Register("MainViewController")]
    public class MainViewController : UIViewController
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private List<string> RoleTypes;

        private UITextField txtWebServerUrl;
        private UIAlertController RoleTypeAlertController;
        private UIButton btnSelectRole;
        private UITextField txtAccount;

        public MainViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view

            //初始化配置信息
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FilePath = Path.Combine(documentsPath, MyConfig.Filename);
            MyConfig.Init(FilePath);
            MyConfig.ReadConfig();

            //一定要设置frame，否则显示空白
            this.View.Frame = UIScreen.MainScreen.Bounds;

            //如果不设置视图背景，默认是黑色
            this.View.BackgroundColor = UIColor.White;

            this.View.CreateLabel(10, 90, "服务器");

            txtWebServerUrl = this.View.CreateTextField(10, 120, "请输入服务器地址");
            txtWebServerUrl.Text = MyConfig.Config.WebServerUrl;
            //进入页面后获取输入焦点
            txtWebServerUrl.BecomeFirstResponder();

            this.View.CreateLabel(10, 150, "用户身份");

            //用户身份列表
            RoleTypes = Enum.GetNames(typeof(UserInfo.RoleType)).ToList();
            List<UIAlertAction> alertActions = new List<UIAlertAction>();
            foreach (string roleType in RoleTypes)
            {
                //所有选项共用一个回调函数
                UIAlertAction alertAction = UIAlertAction.Create(roleType, UIAlertActionStyle.Default, SelectRoleType);
                alertActions.Add(alertAction);
            }

            btnSelectRole = this.View.CreateButton(10, 180, MyConfig.Config.CurrentUserInfo.Role.ToString());
            btnSelectRole.TouchUpInside += (s1, e1) =>
            {
                //必须沿用同一个UIAlertController，否则仅在第一次选择有效，因为ShowSheetBox每次重新创建UIAlertController
                if (RoleTypeAlertController == null)
                {
                    RoleTypeAlertController = this.ShowSheetBox("用户身份", "请选择用户身份", alertActions.ToArray());
                }
                else
                {
                    //显示弹出窗口
                    this.PresentViewController(RoleTypeAlertController, true, null);
                }
            };

            this.View.CreateLabel(10, 210, "姓名");

            txtAccount = this.View.CreateTextField(10, 240, "请输入姓名");
            txtAccount.Text = MyConfig.Config.CurrentUserInfo.Account;

            UIButton btnLogin = this.View.CreateButton(10, 270, "登录");
            btnLogin.TouchUpInside +=(s1,e1)=> Login();
        }

        //选择诊断结果的回调函数
        private void SelectRoleType(UIAlertAction aa)
        {
            btnSelectRole.SetTitle(aa.Title, UIControlState.Normal);
        }

        //登录
        private void Login()
        {
            //消除键盘
            this.View.EndEditing(true);

            //检查帐号密码……

            //保存配置信息
            MyConfig.Config.WebServerUrl = txtWebServerUrl.Text.Trim();
            MyConfig.Config.CurrentUserInfo.Role = (UserInfo.RoleType)Enum.Parse(typeof(UserInfo.RoleType), btnSelectRole.CurrentTitle);
            MyConfig.Config.CurrentUserInfo.Account = txtAccount.Text.Trim();

            MyConfig.SaveConfig();

            //初始化检查记录档案客户端
            MyStudyWebClient.Init(MyConfig.Config.WebServerUrl);

            //显示检查记录列表
            GetStudyListAsync();
        }

        //显示检查记录列表
        private async Task GetStudyListAsync()
        {
            try
            {
                if (MyConfig.Config.CurrentUserInfo.Role == UserInfo.RoleType.Doctor)
                {
                    //获取检查记录列表
                    MyStudyList.StudyList = await MyStudyWebClient.GetStudyListByType(StudyListType.ForDiagnose, MyConfig.Config.CurrentUserInfo.Account);

                    string msg = (MyStudyList.StudyList.Count == 0) ? "没有检查记录" : "已经是最新数据";

                    //显示检查记录列表
                    DiagnoseListViewController diagnoseListVC = new DiagnoseListViewController();
                    this.NavigationController.PushViewController(diagnoseListVC, true);
                }
                else
                {
                    //获取检查记录列表
                    MyStudyList.StudyList = await MyStudyWebClient.GetStudyListByPatientName(MyConfig.Config.CurrentUserInfo.Account);

                    string msg = (MyStudyList.StudyList.Count == 0) ? "没有检查记录" : "已经是最新数据";

                    //显示检查记录列表
                    PatientListViewController patientListVC = new PatientListViewController();
                    this.NavigationController.PushViewController(patientListVC, true);
                }
            }
            catch (Exception ex)
            {
                string msg = $"获取检查记录失败{System.Environment.NewLine}{ex.Message}";
                this.ShowMessageBox("错误", msg);

                return;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

    }
}