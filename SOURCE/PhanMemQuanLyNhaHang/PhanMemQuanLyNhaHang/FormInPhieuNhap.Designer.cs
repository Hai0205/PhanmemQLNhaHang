namespace PhanMemQuanLyNhaHang
{
    partial class frm_xemPhieu
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
            this.crptView = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crptView
            // 
            this.crptView.ActiveViewIndex = -1;
            this.crptView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crptView.Cursor = System.Windows.Forms.Cursors.Default;
            this.crptView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crptView.Location = new System.Drawing.Point(0, 0);
            this.crptView.Name = "crptView";
            this.crptView.Size = new System.Drawing.Size(1412, 803);
            this.crptView.TabIndex = 1;
            // 
            // frm_xemPhieu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 803);
            this.Controls.Add(this.crptView);
            this.Name = "frm_xemPhieu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "In phiếu nhập";
            this.Load += new System.EventHandler(this.frm_xemPhieu_load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crptView;
    }
}