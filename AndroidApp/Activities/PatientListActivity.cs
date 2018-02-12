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
    [Activity(Label = "����¼�б�")]
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
            //Android 5.0��Ҫʹ�������ʱ�ĺ�����SetColorSchemeColors��Ч��
            swipeRefreshLayout.SetColorScheme(Android.Resource.Color.HoloRedLight);
            //����ˢ��
            swipeRefreshLayout.Refresh += (s, e) => RefreshStudyList();

            MyAdapter = new PatientListAdapter(this, MyStudyList.StudyList);
            LsvStudy = FindViewById<ListView>(Resource.Id.lsvStudy);
            LsvStudy.Adapter = MyAdapter;
            LsvStudy.ItemClick += LsvStudy_ItemClick;
        }

        //����ˢ��
        private async void RefreshStudyList()
        {
            //��ʾ����¼�б�
            string msg = await GetStudyListAsync();

            Toast.MakeText(this, msg, ToastLength.Long).Show();

            swipeRefreshLayout.Refreshing = false;
        }

        //�������¼
        private void LsvStudy_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            MyStudyList.SelectedIndex = e.Position;

            StartActivity(typeof(PatientDetailActivity));
        }

        //��ʾ����¼�б�
        private async Task<string> GetStudyListAsync()
        {
            string msg = "";

            try
            {
                //��ȡ����¼�б�
                MyStudyList.StudyList = await MyStudyWebClient.GetStudyListByPatientName(MyConfig.Config.CurrentUserInfo.Account);

                msg = (MyStudyList.StudyList.Count == 0) ? "û�м���¼" : "�Ѿ�����������";

                //�����б�����
                MyAdapter.NotifyDataSetChanged();
            }
            catch (Exception ex)
            {
                msg = $"��ȡ����¼ʧ��{System.Environment.NewLine}{ex.Message}";
            }
            return msg;
        }
    }

    //����¼�б�������
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
                case "����":
                    imgID = Resource.Drawable.good;
                    break;

                case "����쳣":
                    imgID = Resource.Drawable.ill;
                    break;

                case "�����쳣":
                    imgID = Resource.Drawable.die;
                    break;

                default:
                    break;
            }

            string title = $"���ʱ��{item.SampleTime.ToLocalTime():HH:mm:ss}";
            string detail = $"��Ͻ��: {item.Diagnose}";

            //��ȡUI�ؼ�����������
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