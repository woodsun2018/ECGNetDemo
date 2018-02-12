using CoreGraphics;
using Foundation;
using ShareCode;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace iOSApp
{
    [Register("DiagnoseDetailViewController")]
    public class DiagnoseDetailViewController : UIViewController
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private List<string> DiagnoseList;
        private UIAlertController DiagnoseAlertController;
        private UIButton btnSelectDiagnose;

        private DrawBioWave MyDrawBioWave;

        //完成诊断事件
        public event EventHandler HandlerSaveStudy;

        public DiagnoseDetailViewController()
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view

            //一定要设置frame，否则显示空白
            this.View.Frame = UIScreen.MainScreen.Bounds;

            //如果不设置视图背景，默认是黑色
            this.View.BackgroundColor = UIColor.White;

            string[] studyInfo = new string[] {
                $"检查时间: {MyStudyList.SelectedStudy.SampleTime.ToLocalTime():HH:mm:ss}",
                $"个人姓名: {MyStudyList.SelectedStudy.PatientName}",
            };
            for (int i = 0; i < studyInfo.Length; i++)
            {
                this.View.CreateLabel(10, 90 + i * 30, studyInfo[i]);
            }

            //初始化绘制ECG波形对象
            MyDrawBioWave = new DrawBioWave(MyStudyList.SelectedStudy.BioBuf);
            MyDrawBioWave.Frame = new CGRect(0, 200, this.View.Bounds.Width, 200);
            MyDrawBioWave.BackgroundColor = UIColor.White;
            this.View.AddSubview(MyDrawBioWave);

            UIButton btnAutoAnalyse = this.View.CreateButton(10, 420, "自动分析");
            btnAutoAnalyse.TouchUpInside += (s1, e1) => AutoAnalyse();

            this.View.CreateLabel(10, 450, "诊断结果");

            //诊断结果列表
            DiagnoseList = new List<string>() { "正常", "轻度异常", "严重异常" };
            List<UIAlertAction> alertActions = new List<UIAlertAction>();
            foreach (string diagnose in DiagnoseList)
            {
                //所有选项共用一个回调函数
                UIAlertAction alertAction = UIAlertAction.Create(diagnose, UIAlertActionStyle.Default, SelectDiagnose);
                alertActions.Add(alertAction);
            }

            btnSelectDiagnose = this.View.CreateButton(10, 480, DiagnoseList[0]);
            btnSelectDiagnose.TouchUpInside += (s1, e1) =>
            {
                //必须沿用同一个UIAlertController，否则仅在第一次选择有效，因为ShowSheetBox每次重新创建UIAlertController
                if (DiagnoseAlertController == null)
                {
                    DiagnoseAlertController = this.ShowSheetBox("诊断", "请选择诊断结果", alertActions.ToArray());
                }
                else
                {
                    //显示弹出窗口
                    this.PresentViewController(DiagnoseAlertController, true, null);
                }
            };

            UIButton btnSave = this.View.CreateButton(10, 510, "完成诊断");
            btnSave.TouchUpInside += (s1, e1) => SaveStudy();
        }

        //选择诊断结果的回调函数
        private void SelectDiagnose(UIAlertAction aa)
        {
            btnSelectDiagnose.SetTitle(aa.Title, UIControlState.Normal);
        }

        //自动分析
        private void AutoAnalyse()
        {
            //计算基线
            MyDrawBioWave.BioBaselien = Analyse.AnalyseBaseline(MyStudyList.SelectedStudy.BioBuf);

            //绘制ECG波形
            MyDrawBioWave.SetNeedsDisplay();
        }

        //完成诊断
        private async Task SaveStudy()
        {
            MyStudyList.SelectedStudy.Diagnose = btnSelectDiagnose.CurrentTitle;
            MyStudyList.SelectedStudy.DoctorName = MyConfig.Config.CurrentUserInfo.Account;
            MyStudyList.SelectedStudy.ModifyTime = DateTime.Now;

            try
            {
                //修改记录
                await MyStudyWebClient.Modify(MyStudyList.SelectedStudy);

                HandlerSaveStudy?.Invoke(this, EventArgs.Empty);

                //如果保存成功，关闭页面
                this.NavigationController.PopViewController(true);
            }
            catch (Exception ex)
            {
                //如果保存失败，提示错误信息
                this.ShowMessageBox("信息", $"{ex.Message}");
            }
        }
    }
}