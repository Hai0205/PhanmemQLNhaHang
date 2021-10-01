using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace PhanMemQuanLyNhaHang
{
    public partial class FormKetNoi : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public FormKetNoi()
        {
            InitializeComponent();
        }

        private void btnKetNoi_Click(object sender, EventArgs e)
        {
            string strConnect = "";
            strConnect = "Server=" + txtTenMay.Text + ";Database ="+txtTenCSDL.Text+" ;User Id=" + txtUser.Text + ";Password = " + txtPassword.Text + "; ";
            SqlConnection sqlcon = new SqlConnection(strConnect);
            try
            {
                sqlcon.Open();
                PhanMemQuanLyNhaHang.Properties.Settings.Default.strConnect = strConnect;
                PhanMemQuanLyNhaHang.Properties.Settings.Default.Save();
                MessageBox.Show("Kết nối thành công, vui lòng khởi động lại chương trình sau tiếng beep!");
                Console.Beep();
                luuFileHinh(txtFileHinh.Text.Trim());
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("Kết nối không thành công, xin kiểm tra lại!");
                sqlcon.Close();
            }
        }

        private void luuFileHinh(string duongDan)
        {
            DUONGDAN x = new DUONGDAN();
            x.DuongDan1 = duongDan.ToString().Trim();
            db.DUONGDANs.InsertOnSubmit(x);
            db.SubmitChanges();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FromKetNoi_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void FromKetNoi_Load(object sender, EventArgs e)
        {
            txtTenMay.ForeColor = Color.RoyalBlue;
            txtTenCSDL.ForeColor = Color.RoyalBlue;
            txtUser.ForeColor = Color.RoyalBlue;
            txtPassword.ForeColor = Color.RoyalBlue;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
