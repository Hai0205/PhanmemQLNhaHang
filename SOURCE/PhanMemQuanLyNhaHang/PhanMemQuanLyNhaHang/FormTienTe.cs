using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyNhaHang
{
    public partial class FormTienTe : Form
    {
        const double USA = 22606.11;
        const double EUR = 24158.2955;
        const double JPY = 207.68;

        public FormTienTe()
        {
            InitializeComponent();
        }

        private void FormTienIch_Load(object sender, EventArgs e)
        {
            btnChuyenDoi.Enabled = false;
            MaximizeBox = false;
        }

        private double doiTien()
        {
            double kq = 0;
            double giaTri = double.Parse(txtGiaTri.Text);
            if (cbb1.SelectedIndex == 0)
            {
                if (cbb2.SelectedIndex == 0)
                    kq = giaTri / USA;
                else if (cbb2.SelectedIndex == 1)
                    kq = giaTri / EUR;
                else if (cbb2.SelectedIndex == 2)
                    kq = giaTri / JPY;
            }
            return kq;
        }

        private double doiNguocLai()
        {
            double kq = 0;
            double giaTri = double.Parse(txtGiaTri.Text);
            if (cbb1.SelectedIndex == 0)
            {
                if (cbb2.SelectedIndex == 0)
                    kq = giaTri * USA;
                else if (cbb2.SelectedIndex == 1)
                    kq = giaTri * EUR;
                else if (cbb2.SelectedIndex == 2)
                    kq = giaTri * JPY;
            }
            return kq;
        }

        private void btnChuyenDoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnDoiChieu.Text == "==>")
                    txtKetQua.Text = doiTien().ToString().Trim();
                else if (btnDoiChieu.Text == "<==")
                    txtKetQua.Text = doiNguocLai().ToString().Trim();
            }
            catch
            {
                MessageBox.Show("Bạn chưa nhập giá trị để chuyển đổi", "Thông báo", MessageBoxButtons.OK);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtGiaTri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtGiaTri.Text != null)
            {
                btnChuyenDoi.Enabled = true;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Giá tiền là kí tự số ", "Thông Báo ", MessageBoxButtons.OK);
            }
        }

        private void btnDoiChieu_Click(object sender, EventArgs e)
        {
            txtKetQua.Text = "";
            if (btnDoiChieu.Text == "==>")
            {
                btnDoiChieu.Text = "<==";
                btnDoiChieu.Image = Properties.Resources.arrows2;
            }
            else
            {
                btnDoiChieu.Text = "==>";
                btnDoiChieu.Image = Properties.Resources.arrows1;
            }
        }
    }
}
