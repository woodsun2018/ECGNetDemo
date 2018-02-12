using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using ShareCode;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Android.Views.ViewTreeObserver;

namespace AndroidApp
{
    [Activity(Label = "诊断检查记录")]
    public class DiagnoseDetailActivity : Activity, IOnGlobalLayoutListener
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private List<string> DiagnoseList;
        private ArrayAdapter<string> DiagnoseAdapter;
        private Spinner spnDiagnose;

        private ViewTreeObserver observer;

        private DrawBioWave MyDrawBioWave;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DiagnoseDetail);

            string[] studyInfo = new string[] {
                $"检查时间: {MyStudyList.SelectedStudy.SampleTime.ToLocalTime():HH:mm:ss}",
                $"患者姓名: {MyStudyList.SelectedStudy.PatientName}",
            };
            ListView lsvStudyInfo = FindViewById<ListView>(Resource.Id.lsvStudyInfo);
            lsvStudyInfo.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, studyInfo);

            ImageView imgWave = FindViewById<ImageView>(Resource.Id.imgWave);
            //初始化绘制ECG波形对象
            MyDrawBioWave = new DrawBioWave(imgWave, MyStudyList.SelectedStudy.BioBuf);

            //onCreate时控件未显示，大小为0，不能直接初始化绘图对象,onStart,onResume都不行
            //System.NullReferenceException: Object reference not set to an instance of an object.
            observer = imgWave.ViewTreeObserver;
            observer.AddOnGlobalLayoutListener(this);

            //诊断结果列表
            DiagnoseList = new List<string>() { "正常", "轻度异常", "严重异常" };
            DiagnoseAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerDropDownItem, DiagnoseList);
            spnDiagnose = FindViewById<Spinner>(Resource.Id.spnDiagnose);
            spnDiagnose.Adapter = DiagnoseAdapter;

            //自动分析
            Button btnAutoAnalyse = FindViewById<Button>(Resource.Id.btnAutoAnalyse);
            btnAutoAnalyse.Click += (s1, e1) => AutoAnalyse();

            //完成诊断
            Button btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnSave.Click += (s1, e1) => SaveStudy();
        }

        //自动分析
        private void AutoAnalyse()
        {
            //计算基线
            MyDrawBioWave.BioBaselien = Analyse.AnalyseBaseline(MyStudyList.SelectedStudy.BioBuf);

            //绘制ECG波形
            MyDrawBioWave.DrawWave();
        }

        //完成诊断
        private async Task SaveStudy()
        {
            MyStudyList.SelectedStudy.Diagnose = DiagnoseList[spnDiagnose.SelectedItemPosition];
            MyStudyList.SelectedStudy.DoctorName = MyConfig.Config.CurrentUserInfo.Account;
            MyStudyList.SelectedStudy.ModifyTime = DateTime.Now;

            try
            {
                //修改记录
                await MyStudyWebClient.Modify(MyStudyList.SelectedStudy);

                Toast.MakeText(this, "保存成功", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, $"保存失败{System.Environment.NewLine}{ex}", ToastLength.Long).Show();
            }

            this.Finish();
        }

        public void OnGlobalLayout()
        {
            //绘制生理参数波形
            MyDrawBioWave.DrawWave();
        }

    }

}