namespace DoctorStation
{
    partial class frmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txbWebServerUrl = new System.Windows.Forms.TextBox();
            this.labWebServerUrl = new System.Windows.Forms.Label();
            this.txbAccount = new System.Windows.Forms.TextBox();
            this.labAccount = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txbPassword = new System.Windows.Forms.TextBox();
            this.labPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txbWebServerUrl
            // 
            this.txbWebServerUrl.Location = new System.Drawing.Point(159, 39);
            this.txbWebServerUrl.Name = "txbWebServerUrl";
            this.txbWebServerUrl.Size = new System.Drawing.Size(209, 21);
            this.txbWebServerUrl.TabIndex = 28;
            // 
            // labWebServerUrl
            // 
            this.labWebServerUrl.AutoSize = true;
            this.labWebServerUrl.Location = new System.Drawing.Point(73, 42);
            this.labWebServerUrl.Name = "labWebServerUrl";
            this.labWebServerUrl.Size = new System.Drawing.Size(83, 12);
            this.labWebServerUrl.TabIndex = 27;
            this.labWebServerUrl.Text = "Web服务器地址";
            // 
            // txbAccount
            // 
            this.txbAccount.Location = new System.Drawing.Point(159, 100);
            this.txbAccount.Name = "txbAccount";
            this.txbAccount.Size = new System.Drawing.Size(209, 21);
            this.txbAccount.TabIndex = 30;
            // 
            // labAccount
            // 
            this.labAccount.AutoSize = true;
            this.labAccount.Location = new System.Drawing.Point(73, 103);
            this.labAccount.Name = "labAccount";
            this.labAccount.Size = new System.Drawing.Size(29, 12);
            this.labAccount.TabIndex = 29;
            this.labAccount.Text = "帐号";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(75, 202);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(133, 23);
            this.btnLogin.TabIndex = 31;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(235, 202);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(133, 23);
            this.btnExit.TabIndex = 32;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // txbPassword
            // 
            this.txbPassword.Location = new System.Drawing.Point(159, 140);
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.Size = new System.Drawing.Size(209, 21);
            this.txbPassword.TabIndex = 34;
            // 
            // labPassword
            // 
            this.labPassword.AutoSize = true;
            this.labPassword.Location = new System.Drawing.Point(73, 143);
            this.labPassword.Name = "labPassword";
            this.labPassword.Size = new System.Drawing.Size(29, 12);
            this.labPassword.TabIndex = 33;
            this.labPassword.Text = "密码";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 280);
            this.ControlBox = false;
            this.Controls.Add(this.txbPassword);
            this.Controls.Add(this.labPassword);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txbAccount);
            this.Controls.Add(this.labAccount);
            this.Controls.Add(this.txbWebServerUrl);
            this.Controls.Add(this.labWebServerUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbWebServerUrl;
        private System.Windows.Forms.Label labWebServerUrl;
        private System.Windows.Forms.TextBox txbAccount;
        private System.Windows.Forms.Label labAccount;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txbPassword;
        private System.Windows.Forms.Label labPassword;
    }
}