using CoreGraphics;
using Foundation;
using ShareCode;
using UIKit;

namespace iOSApp
{
    [Register("PatientDetailViewController")]
    public class PatientDetailViewController : UIViewController
    {
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private DrawBioWave MyDrawBioWave;

        public PatientDetailViewController()
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
                $"检查时间: {MyStudyList.SelectedStudy.SampleTime:HH:mm:ss}",
                $"诊断结果: {MyStudyList.SelectedStudy.Diagnose}",
                $"诊断医生: {MyStudyList.SelectedStudy.DoctorName}",
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
        }

    }
}