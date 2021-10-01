
namespace PhanMemQuanLyNhaHang
{
    partial class FormQuetMa
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
            this.components = new System.ComponentModel.Container();
            this.comboCamera = new System.Windows.Forms.ComboBox();
            this.gunaLabel11 = new Guna.UI.WinForms.GunaLabel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDongY = new Guna.UI2.WinForms.Guna2Button();
            this.txtQRCode = new Guna.UI.WinForms.GunaTextBox();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.btnBatDau = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.guna2Panel1.SuspendLayout();
            this.guna2Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboCamera
            // 
            this.comboCamera.FormattingEnabled = true;
            this.comboCamera.Location = new System.Drawing.Point(105, 28);
            this.comboCamera.Name = "comboCamera";
            this.comboCamera.Size = new System.Drawing.Size(233, 24);
            this.comboCamera.TabIndex = 45;
            // 
            // gunaLabel11
            // 
            this.gunaLabel11.AutoSize = true;
            this.gunaLabel11.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel11.Location = new System.Drawing.Point(29, 29);
            this.gunaLabel11.Name = "gunaLabel11";
            this.gunaLabel11.Size = new System.Drawing.Size(60, 20);
            this.gunaLabel11.TabIndex = 44;
            this.gunaLabel11.Text = "Camera";
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.White;
            this.guna2Panel1.Controls.Add(this.btnDongY);
            this.guna2Panel1.Controls.Add(this.txtQRCode);
            this.guna2Panel1.Controls.Add(this.gunaLabel2);
            this.guna2Panel1.Controls.Add(this.btnBatDau);
            this.guna2Panel1.Controls.Add(this.comboCamera);
            this.guna2Panel1.Controls.Add(this.gunaLabel11);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.ShadowDecoration.Parent = this.guna2Panel1;
            this.guna2Panel1.Size = new System.Drawing.Size(1055, 76);
            this.guna2Panel1.TabIndex = 46;
            // 
            // btnDongY
            // 
            this.btnDongY.BackColor = System.Drawing.Color.White;
            this.btnDongY.BorderRadius = 15;
            this.btnDongY.CheckedState.Parent = this.btnDongY;
            this.btnDongY.CustomImages.Parent = this.btnDongY;
            this.btnDongY.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDongY.ForeColor = System.Drawing.Color.White;
            this.btnDongY.HoverState.Parent = this.btnDongY;
            this.btnDongY.Location = new System.Drawing.Point(881, 17);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.ShadowDecoration.Parent = this.btnDongY;
            this.btnDongY.Size = new System.Drawing.Size(135, 45);
            this.btnDongY.TabIndex = 49;
            this.btnDongY.Text = "ĐỒNG Ý";
            this.btnDongY.Click += new System.EventHandler(this.btnDongY_Click);
            // 
            // txtQRCode
            // 
            this.txtQRCode.BaseColor = System.Drawing.Color.White;
            this.txtQRCode.BorderColor = System.Drawing.Color.Silver;
            this.txtQRCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtQRCode.FocusedBaseColor = System.Drawing.Color.White;
            this.txtQRCode.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txtQRCode.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txtQRCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtQRCode.Location = new System.Drawing.Point(648, 24);
            this.txtQRCode.Name = "txtQRCode";
            this.txtQRCode.PasswordChar = '\0';
            this.txtQRCode.Size = new System.Drawing.Size(227, 32);
            this.txtQRCode.TabIndex = 48;
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.BackColor = System.Drawing.Color.Transparent;
            this.gunaLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel2.Location = new System.Drawing.Point(609, 32);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(33, 20);
            this.gunaLabel2.TabIndex = 47;
            this.gunaLabel2.Text = "Mã:";
            // 
            // btnBatDau
            // 
            this.btnBatDau.BackColor = System.Drawing.Color.White;
            this.btnBatDau.BorderRadius = 15;
            this.btnBatDau.CheckedState.Parent = this.btnBatDau;
            this.btnBatDau.CustomImages.Parent = this.btnBatDau;
            this.btnBatDau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBatDau.ForeColor = System.Drawing.Color.White;
            this.btnBatDau.HoverState.Parent = this.btnBatDau;
            this.btnBatDau.Location = new System.Drawing.Point(344, 18);
            this.btnBatDau.Name = "btnBatDau";
            this.btnBatDau.ShadowDecoration.Parent = this.btnBatDau;
            this.btnBatDau.Size = new System.Drawing.Size(135, 45);
            this.btnBatDau.TabIndex = 46;
            this.btnBatDau.Text = "BẮT ĐẦU";
            this.btnBatDau.Click += new System.EventHandler(this.btnBatDau_Click);
            // 
            // guna2Panel2
            // 
            this.guna2Panel2.Controls.Add(this.pictureBox1);
            this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel2.Location = new System.Drawing.Point(0, 76);
            this.guna2Panel2.Name = "guna2Panel2";
            this.guna2Panel2.ShadowDecoration.Parent = this.guna2Panel2;
            this.guna2Panel2.Size = new System.Drawing.Size(1055, 536);
            this.guna2Panel2.TabIndex = 47;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1055, 536);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormQuetMa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 612);
            this.Controls.Add(this.guna2Panel2);
            this.Controls.Add(this.guna2Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormQuetMa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quét mã QR";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormQuetMa_FormClosing);
            this.Load += new System.EventHandler(this.FormQuetMa_Load);
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            this.guna2Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboCamera;
        private Guna.UI.WinForms.GunaLabel gunaLabel11;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnBatDau;
        private Guna.UI.WinForms.GunaTextBox txtQRCode;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;
        private Guna.UI2.WinForms.Guna2Button btnDongY;
        private System.Windows.Forms.Timer timer1;
    }
}