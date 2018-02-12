using CoreGraphics;
using Foundation;
using ShareCode;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;

namespace iOSApp
{
    [Register("PatientListViewController")]
    public class PatientListViewController : UITableViewController
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private UIRefreshControl refreshCtrl;

        public PatientListViewController()
        {
        }

        //调用者要使用UITableViewStyle.Grouped初始化为分组列表视图
        public PatientListViewController(UITableViewStyle style) : base(style)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view

            //创建一个可重用的单元格
            TableView.RegisterClassForCellReuse(typeof(StudyCell), "StudyCellID");

            //下拉刷新
            refreshCtrl = new UIRefreshControl();
            refreshCtrl.AttributedTitle = new NSAttributedString("开始刷新……");
            refreshCtrl.AddTarget(RefreshStudyList, UIControlEvent.ValueChanged);

            TableView.AddSubview(refreshCtrl);
        }

        private async void RefreshStudyList(object sender, EventArgs e)
        {
            refreshCtrl.AttributedTitle = new NSAttributedString("正在努力下载中……");

            //显示检查记录列表
            string msg = await GetStudyListAsync();

            refreshCtrl.AttributedTitle = new NSAttributedString(msg);

            refreshCtrl.EndRefreshing();
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
                TableView.ReloadData();
            }
            catch (Exception ex)
            {
                msg = $"获取检查记录失败{System.Environment.NewLine}{ex.Message}";
            }

            return msg;
        }

        #region 管理列表行为

        //每组行数
        public override nint RowsInSection(UITableView tableView, nint section)
        {
            //return base.RowsInSection(tableView, section);
            return MyStudyList.StudyList.Count;
        }

        //设置行高
        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            //return base.GetHeightForRow(tableView, indexPath);
            return 60;
        }

        //每行内容
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //return base.GetCell(tableView, indexPath);
            //Debug.Print($"{DateTime.Now}, StudyListViewController.GetCell, 创建了一个UITableViewCell");

            //UITableViewCell cell = new UITableViewCell();
            StudyCell cell = tableView.DequeueReusableCell("StudyCellID", indexPath) as StudyCell;
            if (cell == null)
            {
                cell = new StudyCell(UITableViewCellStyle.Default, "StudyCellID");
                Debug.Print($"{DateTime.Now}, StudyListViewController.GetCell, 创建了一个UITableViewCell");
            }

            Study item = MyStudyList.StudyList[indexPath.Row];

            string title = $"检查时间{item.SampleTime.ToLocalTime():HH:mm:ss}";
            string detail = $"诊断结果: {item.Diagnose}";

            cell.HeadTitle = title;
            cell.SubTitle = detail;
            cell.ContentImage = GetImageForDiagnose(item.Diagnose);

            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            Debug.Print($"{DateTime.Now}, StudyListViewController.GetCell, 显示了一个UITableViewCell");
            //创建完cell，然后再调用StudyCell.LayoutSubviews

            return cell;
        }

        //根据诊断结果获取图片
        private UIImage GetImageForDiagnose(string diagnose)
        {
            string imgName = "unknow.jpg";
            switch (diagnose)
            {
                case "正常":
                    imgName = "good.jpg";
                    break;

                case "轻度异常":
                    imgName = "ill.jpg";
                    break;

                case "严重异常":
                    imgName = "die.jpg";
                    break;

                default:
                    break;
            }

            UIImage img = UIImage.FromFile(imgName);

            return img;
        }

        //点击一行
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //base.RowSelected(tableView, indexPath);
            MyStudyList.SelectedIndex = indexPath.Row;

            //显示检查报告详细视图
            PatientDetailViewController patientDetailVC = new PatientDetailViewController();
            this.NavigationController.PushViewController(patientDetailVC, true);
        }

        #endregion

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        #region StudyCell

        public class StudyCell : UITableViewCell
        {
            public UILabel HeadLabel { get; set; }
            public string HeadTitle { get; set; }
            public UILabel SubLabel { get; set; }
            public string SubTitle { get; set; }
            public UIImageView ContentImageView { get; set; }
            public UIImage ContentImage { get; set; }

            protected StudyCell(IntPtr handle) : base(handle)
            {
                // Note: this .ctor should not contain any initialization logic.
                InitCell();
            }

            public StudyCell(UITableViewCellStyle style, string reuseID) : base(style, reuseID)
            {
                InitCell();
            }

            private void InitCell()
            {
                HeadTitle = "";
                SubTitle = "";
                ContentImage = UIImage.FromFile("unknow.jpg");

                HeadLabel = new UILabel(new CGRect(10, 5, 200, 25));
                HeadLabel.TextColor = UIColor.Black;
                this.ContentView.AddSubview(HeadLabel);

                SubLabel = new UILabel(new CGRect(10, 30, 200, 25));
                SubLabel.TextColor = UIColor.Gray;
                SubLabel.Font = UIFont.SystemFontOfSize(15);
                this.ContentView.AddSubview(SubLabel);

                ContentImageView = new UIImageView(new CGRect(this.ContentView.Bounds.Width - 60, 14, 32, 32));
                ContentImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
                this.ContentView.AddSubview(ContentImageView);

                SetView();
            }

            public override void LayoutSubviews()
            {
                base.LayoutSubviews();

                SetView();

                Debug.Print($"{DateTime.Now}, StudyCell.LayoutSubviews, 显示了{HeadTitle}");
            }

            private void SetView()
            {
                this.HeadLabel.Text = this.HeadTitle;
                this.SubLabel.Text = this.SubTitle;
                this.ContentImageView.Image = this.ContentImage;
            }

        }//StudyCell

        #endregion

    }
}