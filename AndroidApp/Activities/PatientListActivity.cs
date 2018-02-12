using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using ShareCode;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AndroidApp
{
    [Activity(Label = "检查记录列表")]
    public class PatientListActivity : Activity
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private SwipeRefreshLayout swipeRefreshLayout;
        private ListView LsvStudy;

        private PatientListAdapter MyAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DiagnoseList);

            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            //Android 5.0需要使用这个过时的函数，SetColorSchemeColors无效。
            swipeRefreshLayout.SetColorScheme(Android.Resource.Color.HoloRedLight);
            //下拉刷新
            swipeRefreshLayout.Refresh += (s, e) => RefreshStudyList();

            MyAdapter = new PatientListAdapter(this, MyStudyList.StudyList);
            LsvStudy = FindViewById<ListView>(Resource.Id.lsvStudy);
            LsvStudy.Adapter = MyAdapter;
            LsvStudy.ItemClick += LsvStudy_ItemClick;
        }

        //下拉刷新
        private async void RefreshStudyList()
        {
            //显示检查记录列表
            string msg = await GetStudyListAsync();

            Toast.MakeText(this, msg, ToastLength.Long).Show();

            swipeRefreshLayout.Refreshing = false;
        }

        //点击检查记录
        private void LsvStudy_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            MyStudyList.SelectedIndex = e.Position;

            StartActivity(typeof(PatientDetailActivity));
        }

        //显示检查记录列表
        private async Task<string> GetStudyListAsync()
        {
            string msg = "";

            try
            {
                //获取检查记录列表
                MyStudyList.StudyList = await MyStudyWebClient.GetStudyListByPatientName(MyConfig.Config.CurrentUserInfo.Account);

                msg = (MyStudyList.StudyList.Count == 0) ? "没有检查记录" : "已经是最新数据";

                //更新列表数据
                MyAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                msg = $"获取检查记录失败{System.Environment.NewLine}{ex.Message}";
            }
            return msg;
        }
    }

    //检查记录列表适配器
    public class PatientListAdapter : BaseAdapter<Study>
    {
        private Activity context;
        private List<Study> items;

        public PatientListAdapter(Activity context, List<Study> items)
        {
            this.context = context;
            this.items = items;
        }
        public override Study this[int position] { get { return items[position]; } }

        public override int Count { get { return items.Count; } }

        public override long GetItemId(int position) { return position; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var lsv = parent as ListView;
            Study item = items[position];

            int imgID = Resource.Drawable.unknow;
            switch (item.Diagnose)
            {
                case "正常":
                    imgID = Resource.Drawable.good;
                    break;

                case "轻度异常":
                    imgID = Resource.Drawable.ill;
                    break;

                case "严重异常":
                    imgID = Resource.Drawable.die;
                    break;

                default:
                    break;
            }

            string title = $"检查时间{item.SampleTime.ToLocalTime():HH:mm:ss}";
            string detail = $"诊断结果: {item.Diagnose}";

            //获取UI控件，设置数据
            View view = context.LayoutInflater.Inflate(Resource.Layout.StudyItem, null);

            ImageView imgStudy = view.FindViewById<ImageView>(Resource.Id.imgStudy);
            imgStudy.SetImageResource(imgID);

            TextView txvStudyTitle = view.FindViewById<TextView>(Resource.Id.txvStudyTitle);
            txvStudyTitle.Text = title;

            TextView txvStudyDetail = view.FindViewById<TextView>(Resource.Id.txvStudyDetail);
            txvStudyDetail.Text = detail;

            return view;
        }

    }

}