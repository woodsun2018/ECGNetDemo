
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ShareCode;
using static Android.Views.ViewTreeObserver;

namespace AndroidApp
{
    [Activity(Label = "详细检查记录")]
    public class PatientDetailActivity : Activity, IOnGlobalLayoutListener
    {
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private ViewTreeObserver observer;

        private DrawBioWave MyDrawBioWave;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.StudyDetail);

            string[] studyInfo = new string[] {
                $"检查时间: {MyStudyList.SelectedStudy.SampleTime.ToLocalTime():HH:mm:ss}",
                $"诊断结果: {MyStudyList.SelectedStudy.Diagnose}",
                $"诊断医生: {MyStudyList.SelectedStudy.DoctorName}",
            };
            ListView lsvStudyInfo = FindViewById<ListView>(Resource.Id.lsvStudyInfo);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, studyInfo);
            lsvStudyInfo.Adapter = adapter;

            ImageView imgWave = FindViewById<ImageView>(Resource.Id.imgWave);
            //初始化绘制ECG波形对象
            MyDrawBioWave = new DrawBioWave(imgWave, MyStudyList.SelectedStudy.BioBuf);

            //onCreate时控件未显示，大小为0，不能直接初始化绘图对象,onStart,onResume都不行
            //System.NullReferenceException: Object reference not set to an instance of an object.
            observer = imgWave.ViewTreeObserver;
            observer.AddOnGlobalLayoutListener(this);
        }

        public void OnGlobalLayout()
        {
            //绘图
            MyDrawBioWave.DrawWave();
        }

    }

}