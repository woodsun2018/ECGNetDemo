namespace ECGDevice
{
    partial class frmECGDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmECGDevice));
            this.txbTcpServerIP = new System.Windows.Forms.TextBox();
            this.labTcpServerIP = new System.Windows.Forms.Label();
            this.txbPatientName = new System.Windows.Forms.TextBox();
            this.labPatientName = new System.Windows.Forms.Label();
            this.picDevice = new System.Windows.Forms.PictureBox();
            this.labDeviceID = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txbDeviceID = new System.Windows.Forms.TextBox();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.labLinkType = new System.Windows.Forms.Label();
            this.cboLinkType = new System.Windows.Forms.ComboBox();
            this.txbTcpServerPort = new System.Windows.Forms.TextBox();
            this.labTcpServerPort = new System.Windows.Forms.Label();
            this.txbWebServerUrl = new System.Windows.Forms.TextBox();
            this.labWebServerUrl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDevice)).BeginInit();
            this.SuspendLayout();
            // 
            // txbTcpServerIP
            // 
            this.txbTcpServerIP.Location = new System.Drawing.Point(105, 150);
            this.txbTcpServerIP.Name = "txbTcpServerIP";
            this.txbTcpServerIP.Size = new System.Drawing.Size(209, 21);
            this.txbTcpServerIP.TabIndex = 17;
            // 
            // labTcpServerIP
            // 
            this.labTcpServerIP.AutoSize = true;
            this.labTcpServerIP.Location = new System.Drawing.Point(20, 153);
            this.labTcpServerIP.Name = "labTcpServerIP";
            this.labTcpServerIP.Size = new System.Drawing.Size(71, 12);
            this.labTcpServerIP.TabIndex = 16;
            this.labTcpServerIP.Text = "TCP服务器IP";
            // 
            // txbPatientName
            // 
            this.txbPatientName.Location = new System.Drawing.Point(105, 48);
            this.txbPatientName.Name = "txbPatientName";
            this.txbPatientName.Size = new System.Drawing.Size(209, 21);
            this.txbPatientName.TabIndex = 15;
            // 
            // labPatientName
            // 
            this.labPatientName.AutoSize = true;
            this.labPatientName.Location = new System.Drawing.Point(21, 51);
            this.labPatientName.Name = "labPatientName";
            this.labPatientName.Size = new System.Drawing.Size(53, 12);
            this.labPatientName.TabIndex = 14;
            this.labPatientName.Text = "个人姓名";
            // 
            // picDevice
            // 
            this.picDevice.Image = ((System.Drawing.Image)(resources.GetObject("picDevice.Image")));
            this.picDevice.Location = new System.Drawing.Point(320, 21);
            this.picDevice.Name = "picDevice";
            this.picDevice.Size = new System.Drawing.Size(253, 227);
            this.picDevice.TabIndex = 13;
            this.picDevice.TabStop = false;
            // 
            // labDeviceID
            // 
            this.labDeviceID.AutoSize = true;
            this.labDeviceID.Location = new System.Drawing.Point(20, 21);
            this.labDeviceID.Name = "labDeviceID";
            this.labDeviceID.Size = new System.Drawing.Size(53, 12);
            this.labDeviceID.TabIndex = 12;
            this.labDeviceID.Text = "设备编号";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(105, 225);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(209, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "开始检测";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // txbDeviceID
            // 
            this.txbDeviceID.Location = new System.Drawing.Point(105, 21);
            this.txbDeviceID.Name = "txbDeviceID";
            this.txbDeviceID.Size = new System.Drawing.Size(209, 21);
            this.txbDeviceID.TabIndex = 9;
            // 
            // rtbInfo
            // 
            this.rtbInfo.Location = new System.Drawing.Point(22, 274);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.Size = new System.Drawing.Size(551, 248);
            this.rtbInfo.TabIndex = 18;
            this.rtbInfo.Text = "";
            // 
            // labLinkType
            // 
            this.labLinkType.AutoSize = true;
            this.labLinkType.Location = new System.Drawing.Point(21, 97);
            this.labLinkType.Name = "labLinkType";
            this.labLinkType.Size = new System.Drawing.Size(53, 12);
            this.labLinkType.TabIndex = 19;
            this.labLinkType.Text = "联网方式";
            // 
            // cboLinkType
            // 
            this.cboLinkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLinkType.FormattingEnabled = true;
            this.cboLinkType.Location = new System.Drawing.Point(105, 97);
            this.cboLinkType.Name = "cboLinkType";
            this.cboLinkType.Size = new System.Drawing.Size(209, 20);
            this.cboLinkType.TabIndex = 20;
            // 
            // txbTcpServerPort
            // 
            this.txbTcpServerPort.Location = new System.Drawing.Point(105, 177);
            this.txbTcpServerPort.Name = "txbTcpServerPort";
            this.txbTcpServerPort.Size = new System.Drawing.Size(209, 21);
            this.txbTcpServerPort.TabIndex = 22;
            // 
            // labTcpServerPort
            // 
            this.labTcpServerPort.AutoSize = true;
            this.labTcpServerPort.Location = new System.Drawing.Point(19, 180);
            this.labTcpServerPort.Name = "labTcpServerPort";
            this.labTcpServerPort.Size = new System.Drawing.Size(83, 12);
            this.labTcpServerPort.TabIndex = 21;
            this.labTcpServerPort.Text = "TCP服务器端口";
            // 
            // txbWebServerUrl
            // 
            this.txbWebServerUrl.Location = new System.Drawing.Point(105, 123);
            this.txbWebServerUrl.Name = "txbWebServerUrl";
            this.txbWebServerUrl.Size = new System.Drawing.Size(209, 21);
            this.txbWebServerUrl.TabIndex = 24;
            // 
            // labWebServerUrl
            // 
            this.labWebServerUrl.AutoSize = true;
            this.labWebServerUrl.Location = new System.Drawing.Point(19, 126);
            this.labWebServerUrl.Name = "labWebServerUrl";
            this.labWebServerUrl.Size = new System.Drawing.Size(83, 12);
            this.labWebServerUrl.TabIndex = 23;
            this.labWebServerUrl.Text = "Web服务器地址";
            // 
            // frmECGDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 549);
            this.Controls.Add(this.txbWebServerUrl);
            this.Controls.Add(this.labWebServerUrl);
            this.Controls.Add(this.txbTcpServerPort);
            this.Controls.Add(this.labTcpServerPort);
            this.Controls.Add(this.cboLinkType);
            this.Controls.Add(this.labLinkType);
            this.Controls.Add(this.rtbInfo);
            this.Controls.Add(this.txbTcpServerIP);
            this.Controls.Add(this.labTcpServerIP);
            this.Controls.Add(this.txbPatientName);
            this.Controls.Add(this.labPatientName);
            this.Controls.Add(this.picDevice);
            this.Controls.Add(this.labDeviceID);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txbDeviceID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmECGDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ECG检测设备";
            this.Load += new System.EventHandler(this.frmECGDevice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDevice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbTcpServerIP;
        private System.Windows.Forms.Label labTcpServerIP;
        private System.Windows.Forms.TextBox txbPatientName;
        private System.Windows.Forms.Label labPatientName;
        private System.Windows.Forms.PictureBox picDevice;
        private System.Windows.Forms.Label labDeviceID;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txbDeviceID;
        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.Label labLinkType;
        private System.Windows.Forms.ComboBox cboLinkType;
        private System.Windows.Forms.TextBox txbTcpServerPort;
        private System.Windows.Forms.Label labTcpServerPort;
        private System.Windows.Forms.TextBox txbWebServerUrl;
        private System.Windows.Forms.Label labWebServerUrl;
    }
}

