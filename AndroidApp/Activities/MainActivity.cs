using Android.App;
using Android.OS;
using Android.Widget;
using ShareCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/heart")]
    public class MainActivity : Activity
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private List<string> RoleTypes;

        private EditText edtWebServerUrl;
        private Spinner spinnerRole;
        private EditText edtAccount;
        private Button btnLogin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //初始化配置信息
            MyConfig.Init(this.GetFileStreamPath(MyConfig.Filename).Path);
            MyConfig.ReadConfig();

            //显示配置信息
            edtWebServerUrl = FindViewById<EditText>(Resource.Id.edtWebServerUrl);
            edtWebServerUrl.Text = MyConfig.Config.WebServerUrl;

            RoleTypes = Enum.GetNames(typeof(UserInfo.RoleType)).ToList();
            spinnerRole = FindViewById<Spinner>(Resource.Id.spinnerRole);
            spinnerRole.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, RoleTypes);
            int spinnerRoleSelecteIndex = RoleTypes.IndexOf(MyConfig.Config.CurrentUserInfo.Role.ToString());
            spinnerRole.SetSelection(spinnerRoleSelecteIndex);

            edtAccount = FindViewById<EditText>(Resource.Id.edtAccount);
            edtAccount.Text = MyConfig.Config.CurrentUserInfo.Account;

            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnLogin.Click += (s1, e1) => Login();
        }

        //登录
        private void Login()
        {
            //检查帐号密码……

            //保存配置信息
            MyConfig.Config.WebServerUrl = edtWebServerUrl.Text.Trim();
            MyConfig.Config.CurrentUserInfo.Role = (UserInfo.RoleType)Enum.Parse(typeof(UserInfo.RoleType), RoleTypes[spinnerRole.SelectedItemPosition]);
            MyConfig.Config.CurrentUserInfo.Account = edtAccount.Text.Trim();

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
                    Toast.MakeText(this, msg, ToastLength.Long).Show();

                    //显示检查记录列表
                    StartActivity(typeof(DiagnoseListActivity));
                }
                else
                {
                    //获取检查记录列表
                    MyStudyList.StudyList = await MyStudyWebClient.GetStudyListByPatientName(MyConfig.Config.CurrentUserInfo.Account);

                    string msg = (MyStudyList.StudyList.Count == 0) ? "没有检查记录" : "已经是最新数据";
                    Toast.MakeText(this, msg, ToastLength.Long).Show();

                    //显示检查记录列表
                    StartActivity(typeof(PatientListActivity));
                }
            }
            catch (Exception ex)
            {
                string msg = $"获取检查记录失败{System.Environment.NewLine}{ex.Message}";
                Toast.MakeText(this, msg, ToastLength.Long).Show();

                return;
            }
        }
    }
}

