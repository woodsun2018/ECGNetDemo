using ShareCode;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DoctorStation
{
    public partial class frmDoctorStation : Form
    {
        private ConfigHelper MyConfig = ConfigHelper.Instance;
        private StudyWebClient MyStudyWebClient = StudyWebClient.Instance;
        private GlobalStudyList MyStudyList = GlobalStudyList.Instance;

        private DrawBioWave MyDrawBioWave = null;

        public frmDoctorStation()
        {
            InitializeComponent();
        }

        private void frmDoctorStation_Load(object sender, EventArgs e)
        {
            //显示待诊断检查记录
            btnShowForDiagnose.Click += (s1, e1) => ShowStudyList(StudyListType.ForDiagnose);

            //显示我诊断的记录
            btnShowMyDiagnose.Click += (s1, e1) => ShowStudyList(StudyListType.All, MyConfig.Config.CurrentUserInfo.Account);

            //显示全部检查记录
            btnShowAllStudy.Click += (s1, e1) => ShowStudyList(StudyListType.All);

            lsbStudys.SelectedIndexChanged += LsbStudys_SelectedIndexChanged;

            txbPatientName.ReadOnly = true;

            //初始化绘制ECG波形对象
            MyDrawBioWave = new DrawBioWave(picBioBuf, new byte[0]);

            List<string> dagnoseList = new List<string>() { "正常", "轻度异常", "严重异常" };
            dagnoseList.ForEach(x => this.cboDiagnose.Items.Add(x));

            txbDoctorName.Text = MyConfig.Config.CurrentUserInfo.Account;
            txbDoctorName.ReadOnly = true;

            //自动分析
            btnAutoAnalyse.Click += (s1, e1) => AutoAnalyse();

            //完成诊断
            btnSave.Click += (s1, e1) => SaveStudy();

            //退出软件
            this.FormClosed += (s1, e1) => ExitApp();

            //显示登录窗口
            ShowLogin();
        }

        private void ShowInfo(string msg)
        {
            this.rtbInfo.AppendText($"{DateTime.Now}, {msg}{Environment.NewLine}");
        }

        //显示登录窗口
        private void ShowLogin()
        {
            frmLogin winLogin = new frmLogin();

            winLogin.FormClosed += (s1, e1) =>
            {
                //显示待诊断检查记录
                ShowStudyList(StudyListType.ForDiagnose);

                this.Text = $"医生工作站 - 当前用户: {MyConfig.Config.CurrentUserInfo.Account}";
            };

            winLogin.ShowDialog(this);
        }

        //显示检查记录列表
        private async void ShowStudyList(StudyListType studyType, string doctorName = "")
        {
            ClearShowStudy();

            this.lsbStudys.Items.Clear();

            try
            {
                //查询记录
                MyStudyList.StudyList = await MyStudyWebClient.GetStudyListByType(studyType, doctorName);

                if (MyStudyList.StudyList.Count == 0)
                {
                    //MessageBox.Show(this, "没有检查记录", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowInfo($"ShowStudyList, 没有检查记录");

                    return;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowInfo($"ShowStudyList, 错误{Environment.NewLine}{ex}");

                return;
            }

            foreach (Study study in MyStudyList.StudyList)
            {
                this.lsbStudys.Items.Add(GetStudyInfo(study));
            }
        }

        private string GetStudyInfo(Study study)
        {
            string studyInfo = $"{study.ID}, {study.SampleTime.ToLocalTime()}, 来自设备{study.DeviceID}, 数据包序号{study.FrameNo}, 患者姓名: {study.PatientName}, 诊断结果: {study.Diagnose}, 诊断医生: {study.DoctorName}";

            return studyInfo;
        }

        private void LsbStudys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsbStudys.SelectedIndex == -1)
                return;

            MyStudyList.SelectedIndex = lsbStudys.SelectedIndex;

            ShowStudy();
        }

        private void ShowStudy()
        {
            this.txbPatientName.Text = MyStudyList.SelectedStudy.PatientName;
            this.cboDiagnose.Text = MyStudyList.SelectedStudy.Diagnose;
            this.txbDoctorName.Text = MyStudyList.SelectedStudy.DoctorName;

            btnAutoAnalyse.Enabled = true;
            btnSave.Enabled = true;
            btnPrint.Enabled = true;

            MyDrawBioWave.BioBuf = MyStudyList.SelectedStudy.BioBuf;
            MyDrawBioWave.BioBaselien = 0;

            //绘制ECG波形
            MyDrawBioWave.DrawWave();
        }

        //清除显示
        private void ClearShowStudy()
        {
            txbPatientName.Text = "";
            cboDiagnose.Text = "";
            txbDoctorName.Text = "";

            btnAutoAnalyse.Enabled = false;
            btnSave.Enabled = false;
            btnPrint.Enabled = false;

            picBioBuf.Image = null;
        }

        //自动分析
        private void AutoAnalyse()
        {
            if (lsbStudys.SelectedIndex == -1)
                return;

            //计算基线
            MyDrawBioWave.BioBaselien = Analyse.AnalyseBaseline(MyStudyList.SelectedStudy.BioBuf);

            //绘制生理参数波形
            MyDrawBioWave.DrawWave();
        }

        //完成诊断
        private async void SaveStudy()
        {
            if (lsbStudys.SelectedIndex == -1)
                return;

            MyStudyList.SelectedStudy.Diagnose = this.cboDiagnose.Text;
            //医生姓名改为当前帐号
            MyStudyList.SelectedStudy.DoctorName = MyConfig.Config.CurrentUserInfo.Account;
            MyStudyList.SelectedStudy.ModifyTime = DateTime.Now;

            try
            {
                //修改记录
                await MyStudyWebClient.Modify(MyStudyList.SelectedStudy);

                ShowInfo($"SaveStudy, 保存成功");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowInfo($"SaveStudy, 错误{Environment.NewLine}{ex}");

                return;
            }

            //更新列表显示（赋值操作会导致原来选中的索引SelectedIndex复位为-1，要还原SelectedIndex）
            int oldSelectedIndex = lsbStudys.SelectedIndex;
            lsbStudys.Items[lsbStudys.SelectedIndex] = GetStudyInfo(MyStudyList.SelectedStudy);
            lsbStudys.SelectedIndex = oldSelectedIndex;
        }

        //退出软件
        private void ExitApp()
        {
            Application.ExitThread();

            Environment.Exit(0);
        }
    }
}
