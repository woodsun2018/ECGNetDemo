namespace DoctorStation
{
    partial class frmDoctorStation
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.labDoctorName = new System.Windows.Forms.Label();
            this.btnAutoAnalyse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.cboDiagnose = new System.Windows.Forms.ComboBox();
            this.txbPatientName = new System.Windows.Forms.TextBox();
            this.btnShowAllStudy = new System.Windows.Forms.Button();
            this.btnShowForDiagnose = new System.Windows.Forms.Button();
            this.grpArchives = new System.Windows.Forms.GroupBox();
            this.btnShowMyDiagnose = new System.Windows.Forms.Button();
            this.lsbStudys = new System.Windows.Forms.ListBox();
            this.labPatientName = new System.Windows.Forms.Label();
            this.labDiagnose = new System.Windows.Forms.Label();
            this.picBioBuf = new System.Windows.Forms.PictureBox();
            this.labBioWave = new System.Windows.Forms.Label();
            this.grpStudyDetail = new System.Windows.Forms.GroupBox();
            this.txbDoctorName = new System.Windows.Forms.TextBox();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.grpArchives.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBioBuf)).BeginInit();
            this.grpStudyDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // labDoctorName
            // 
            this.labDoctorName.AutoSize = true;
            this.labDoctorName.Location = new System.Drawing.Point(443, 236);
            this.labDoctorName.Name = "labDoctorName";
            this.labDoctorName.Size = new System.Drawing.Size(53, 12);
            this.labDoctorName.TabIndex = 16;
            this.labDoctorName.Text = "医生姓名";
            // 
            // btnAutoAnalyse
            // 
            this.btnAutoAnalyse.Location = new System.Drawing.Point(34, 248);
            this.btnAutoAnalyse.Name = "btnAutoAnalyse";
            this.btnAutoAnalyse.Size = new System.Drawing.Size(75, 23);
            this.btnAutoAnalyse.TabIndex = 15;
            this.btnAutoAnalyse.Text = "自动分析";
            this.btnAutoAnalyse.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(692, 252);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "完成诊断";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(796, 252);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "打印报告";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // cboDiagnose
            // 
            this.cboDiagnose.FormattingEnabled = true;
            this.cboDiagnose.Location = new System.Drawing.Point(189, 251);
            this.cboDiagnose.Name = "cboDiagnose";
            this.cboDiagnose.Size = new System.Drawing.Size(200, 20);
            this.cboDiagnose.TabIndex = 11;
            // 
            // txbPatientName
            // 
            this.txbPatientName.Location = new System.Drawing.Point(91, 31);
            this.txbPatientName.Name = "txbPatientName";
            this.txbPatientName.Size = new System.Drawing.Size(200, 21);
            this.txbPatientName.TabIndex = 10;
            // 
            // btnShowAllStudy
            // 
            this.btnShowAllStudy.Location = new System.Drawing.Point(343, 33);
            this.btnShowAllStudy.Name = "btnShowAllStudy";
            this.btnShowAllStudy.Size = new System.Drawing.Size(130, 23);
            this.btnShowAllStudy.TabIndex = 3;
            this.btnShowAllStudy.Text = "显示全部记录";
            this.btnShowAllStudy.UseVisualStyleBackColor = true;
            // 
            // btnShowForDiagnose
            // 
            this.btnShowForDiagnose.Location = new System.Drawing.Point(28, 33);
            this.btnShowForDiagnose.Name = "btnShowForDiagnose";
            this.btnShowForDiagnose.Size = new System.Drawing.Size(130, 23);
            this.btnShowForDiagnose.TabIndex = 2;
            this.btnShowForDiagnose.Text = "显示待诊断记录";
            this.btnShowForDiagnose.UseVisualStyleBackColor = true;
            // 
            // grpArchives
            // 
            this.grpArchives.Controls.Add(this.btnShowMyDiagnose);
            this.grpArchives.Controls.Add(this.btnShowAllStudy);
            this.grpArchives.Controls.Add(this.btnShowForDiagnose);
            this.grpArchives.Controls.Add(this.lsbStudys);
            this.grpArchives.Location = new System.Drawing.Point(12, 12);
            this.grpArchives.Name = "grpArchives";
            this.grpArchives.Size = new System.Drawing.Size(897, 168);
            this.grpArchives.TabIndex = 8;
            this.grpArchives.TabStop = false;
            this.grpArchives.Text = "检查记录表";
            // 
            // btnShowMyDiagnose
            // 
            this.btnShowMyDiagnose.Location = new System.Drawing.Point(187, 33);
            this.btnShowMyDiagnose.Name = "btnShowMyDiagnose";
            this.btnShowMyDiagnose.Size = new System.Drawing.Size(130, 23);
            this.btnShowMyDiagnose.TabIndex = 4;
            this.btnShowMyDiagnose.Text = "显示我诊断的记录";
            this.btnShowMyDiagnose.UseVisualStyleBackColor = true;
            // 
            // lsbStudys
            // 
            this.lsbStudys.FormattingEnabled = true;
            this.lsbStudys.ItemHeight = 12;
            this.lsbStudys.Location = new System.Drawing.Point(28, 62);
            this.lsbStudys.Name = "lsbStudys";
            this.lsbStudys.Size = new System.Drawing.Size(841, 88);
            this.lsbStudys.TabIndex = 1;
            // 
            // labPatientName
            // 
            this.labPatientName.AutoSize = true;
            this.labPatientName.Location = new System.Drawing.Point(32, 34);
            this.labPatientName.Name = "labPatientName";
            this.labPatientName.Size = new System.Drawing.Size(53, 12);
            this.labPatientName.TabIndex = 9;
            this.labPatientName.Text = "个人姓名";
            // 
            // labDiagnose
            // 
            this.labDiagnose.AutoSize = true;
            this.labDiagnose.Location = new System.Drawing.Point(187, 236);
            this.labDiagnose.Name = "labDiagnose";
            this.labDiagnose.Size = new System.Drawing.Size(53, 12);
            this.labDiagnose.TabIndex = 7;
            this.labDiagnose.Text = "诊断结果";
            // 
            // picBioBuf
            // 
            this.picBioBuf.BackColor = System.Drawing.Color.White;
            this.picBioBuf.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBioBuf.Location = new System.Drawing.Point(30, 80);
            this.picBioBuf.Name = "picBioBuf";
            this.picBioBuf.Size = new System.Drawing.Size(841, 133);
            this.picBioBuf.TabIndex = 6;
            this.picBioBuf.TabStop = false;
            // 
            // labBioWave
            // 
            this.labBioWave.AutoSize = true;
            this.labBioWave.Location = new System.Drawing.Point(28, 65);
            this.labBioWave.Name = "labBioWave";
            this.labBioWave.Size = new System.Drawing.Size(41, 12);
            this.labBioWave.TabIndex = 5;
            this.labBioWave.Text = "心电图";
            // 
            // grpStudyDetail
            // 
            this.grpStudyDetail.Controls.Add(this.txbDoctorName);
            this.grpStudyDetail.Controls.Add(this.labDoctorName);
            this.grpStudyDetail.Controls.Add(this.btnAutoAnalyse);
            this.grpStudyDetail.Controls.Add(this.btnSave);
            this.grpStudyDetail.Controls.Add(this.btnPrint);
            this.grpStudyDetail.Controls.Add(this.cboDiagnose);
            this.grpStudyDetail.Controls.Add(this.txbPatientName);
            this.grpStudyDetail.Controls.Add(this.labPatientName);
            this.grpStudyDetail.Controls.Add(this.labDiagnose);
            this.grpStudyDetail.Controls.Add(this.picBioBuf);
            this.grpStudyDetail.Controls.Add(this.labBioWave);
            this.grpStudyDetail.Location = new System.Drawing.Point(10, 200);
            this.grpStudyDetail.Name = "grpStudyDetail";
            this.grpStudyDetail.Size = new System.Drawing.Size(899, 291);
            this.grpStudyDetail.TabIndex = 7;
            this.grpStudyDetail.TabStop = false;
            this.grpStudyDetail.Text = "检查记录详情";
            // 
            // txbDoctorName
            // 
            this.txbDoctorName.Location = new System.Drawing.Point(445, 254);
            this.txbDoctorName.Name = "txbDoctorName";
            this.txbDoctorName.Size = new System.Drawing.Size(200, 21);
            this.txbDoctorName.TabIndex = 17;
            // 
            // rtbInfo
            // 
            this.rtbInfo.Location = new System.Drawing.Point(12, 509);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(897, 171);
            this.rtbInfo.TabIndex = 9;
            this.rtbInfo.Text = "";
            // 
            // frmDoctorStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 692);
            this.Controls.Add(this.rtbInfo);
            this.Controls.Add(this.grpArchives);
            this.Controls.Add(this.grpStudyDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmDoctorStation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "医生工作站";
            this.Load += new System.EventHandler(this.frmDoctorStation_Load);
            this.grpArchives.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBioBuf)).EndInit();
            this.grpStudyDetail.ResumeLayout(false);
            this.grpStudyDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labDoctorName;
        private System.Windows.Forms.Button btnAutoAnalyse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.ComboBox cboDiagnose;
        private System.Windows.Forms.TextBox txbPatientName;
        private System.Windows.Forms.Button btnShowAllStudy;
        private System.Windows.Forms.Button btnShowForDiagnose;
        private System.Windows.Forms.GroupBox grpArchives;
        private System.Windows.Forms.ListBox lsbStudys;
        private System.Windows.Forms.Label labPatientName;
        private System.Windows.Forms.Label labDiagnose;
        private System.Windows.Forms.PictureBox picBioBuf;
        private System.Windows.Forms.Label labBioWave;
        private System.Windows.Forms.GroupBox grpStudyDetail;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.TextBox txbDoctorName;
        private System.Windows.Forms.Button btnShowMyDiagnose;
    }
}

