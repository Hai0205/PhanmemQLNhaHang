namespace PhanMemQuanLyNhaHang
{
    partial class FormTienTe
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
            this.cbb2 = new System.Windows.Forms.ComboBox();
            this.cbb1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKetQua = new System.Windows.Forms.TextBox();
            this.txtGiaTri = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbQuảnLýKH = new System.Windows.Forms.Label();
            this.btnDoiChieu = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnChuyenDoi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbb2
            // 
            this.cbb2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbb2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cbb2.ForeColor = System.Drawing.Color.Black;
            this.cbb2.FormattingEnabled = true;
            this.cbb2.Items.AddRange(new object[] {
            "USA",
            "EUR",
            "JPY"});
            this.cbb2.Location = new System.Drawing.Point(396, 119);
            this.cbb2.Margin = new System.Windows.Forms.Padding(4);
            this.cbb2.Name = "cbb2";
            this.cbb2.Size = new System.Drawing.Size(160, 28);
            this.cbb2.TabIndex = 17;
            // 
            // cbb1
            // 
            this.cbb1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbb1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbb1.ForeColor = System.Drawing.Color.Black;
            this.cbb1.FormattingEnabled = true;
            this.cbb1.Items.AddRange(new object[] {
            "VND"});
            this.cbb1.Location = new System.Drawing.Point(119, 119);
            this.cbb1.Margin = new System.Windows.Forms.Padding(4);
            this.cbb1.Name = "cbb1";
            this.cbb1.Size = new System.Drawing.Size(160, 28);
            this.cbb1.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(25, 122);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Từ :";
            // 
            // txtKetQua
            // 
            this.txtKetQua.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKetQua.ForeColor = System.Drawing.Color.Red;
            this.txtKetQua.Location = new System.Drawing.Point(118, 157);
            this.txtKetQua.Margin = new System.Windows.Forms.Padding(4);
            this.txtKetQua.Name = "txtKetQua";
            this.txtKetQua.ReadOnly = true;
            this.txtKetQua.Size = new System.Drawing.Size(438, 26);
            this.txtKetQua.TabIndex = 19;
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiaTri.ForeColor = System.Drawing.Color.Black;
            this.txtGiaTri.Location = new System.Drawing.Point(119, 81);
            this.txtGiaTri.Margin = new System.Windows.Forms.Padding(4);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.Size = new System.Drawing.Size(437, 26);
            this.txtGiaTri.TabIndex = 14;
            this.txtGiaTri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGiaTri_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(25, 160);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Kết quả :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(25, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Giá trị : ";
            // 
            // lbQuảnLýKH
            // 
            this.lbQuảnLýKH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbQuảnLýKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbQuảnLýKH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbQuảnLýKH.Image = global::PhanMemQuanLyNhaHang.Properties.Resources.iconTienTe;
            this.lbQuảnLýKH.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbQuảnLýKH.Location = new System.Drawing.Point(0, 0);
            this.lbQuảnLýKH.Name = "lbQuảnLýKH";
            this.lbQuảnLýKH.Size = new System.Drawing.Size(663, 39);
            this.lbQuảnLýKH.TabIndex = 40;
            this.lbQuảnLýKH.Text = "CHUYỂN ĐỔI NGOẠI TỆ";
            this.lbQuảnLýKH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDoiChieu
            // 
            this.btnDoiChieu.BackColor = System.Drawing.Color.White;
            this.btnDoiChieu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDoiChieu.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDoiChieu.Image = global::PhanMemQuanLyNhaHang.Properties.Resources.arrows1;
            this.btnDoiChieu.Location = new System.Drawing.Point(288, 118);
            this.btnDoiChieu.Margin = new System.Windows.Forms.Padding(4);
            this.btnDoiChieu.Name = "btnDoiChieu";
            this.btnDoiChieu.Size = new System.Drawing.Size(100, 31);
            this.btnDoiChieu.TabIndex = 22;
            this.btnDoiChieu.Text = "==>";
            this.btnDoiChieu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDoiChieu.UseVisualStyleBackColor = false;
            this.btnDoiChieu.Click += new System.EventHandler(this.btnDoiChieu_Click);
            // 
            // btnDong
            // 
            this.btnDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDong.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDong.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.ForeColor = System.Drawing.Color.Black;
            this.btnDong.Image = global::PhanMemQuanLyNhaHang.Properties.Resources.exit24;
            this.btnDong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDong.Location = new System.Drawing.Point(329, 191);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(140, 41);
            this.btnDong.TabIndex = 21;
            this.btnDong.Text = "Đóng";
            this.btnDong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnChuyenDoi
            // 
            this.btnChuyenDoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChuyenDoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChuyenDoi.ForeColor = System.Drawing.Color.Black;
            this.btnChuyenDoi.Image = global::PhanMemQuanLyNhaHang.Properties.Resources.dollar24;
            this.btnChuyenDoi.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChuyenDoi.Location = new System.Drawing.Point(153, 191);
            this.btnChuyenDoi.Margin = new System.Windows.Forms.Padding(4);
            this.btnChuyenDoi.Name = "btnChuyenDoi";
            this.btnChuyenDoi.Size = new System.Drawing.Size(168, 41);
            this.btnChuyenDoi.TabIndex = 20;
            this.btnChuyenDoi.Text = "Chuyển đổi";
            this.btnChuyenDoi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChuyenDoi.UseVisualStyleBackColor = true;
            this.btnChuyenDoi.Click += new System.EventHandler(this.btnChuyenDoi_Click);
            // 
            // FormTienTe
            // 
            this.AcceptButton = this.btnChuyenDoi;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CancelButton = this.btnDong;
            this.ClientSize = new System.Drawing.Size(662, 274);
            this.Controls.Add(this.lbQuảnLýKH);
            this.Controls.Add(this.btnDoiChieu);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnChuyenDoi);
            this.Controls.Add(this.cbb2);
            this.Controls.Add(this.cbb1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKetQua);
            this.Controls.Add(this.txtGiaTri);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTienTe";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tiền tệ";
            this.Load += new System.EventHandler(this.FormTienIch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDoiChieu;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnChuyenDoi;
        private System.Windows.Forms.ComboBox cbb2;
        private System.Windows.Forms.ComboBox cbb1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKetQua;
        private System.Windows.Forms.TextBox txtGiaTri;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbQuảnLýKH;
    }
}