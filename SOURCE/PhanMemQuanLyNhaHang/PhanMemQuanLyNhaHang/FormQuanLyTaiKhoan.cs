using PhanMemQuanLyNhaHang.XuLy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyNhaHang
{
    public partial class FormQuanLyTaiKhoan : Form
    {

        private Account loginAccount;
        private string username;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

        public FormQuanLyTaiKhoan(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
        }

        void ChangeAccount(int type)
        {
            username = loginAccount.HoTen;
        }

        public FormQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void load_dataGridView()
        {
            dgvDSTaiKhoan.DataSource = DataProvider.Instance.ExecuteQuery("select * from dbo.TaiKhoan");
        }

        private void loadComboBox()
        {
            string[] s = { "Quản lý", "Nhân viên" };
            foreach (string x in s)
            {
                cboLoaiTK.Items.Add(x);
            }
        }

        private void FormQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            load_dataGridView();
            loadComboBox();
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
            txtTenHienThi.Enabled = false;
            cboLoaiTK.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void dgvDSTaiKhoan_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDSTaiKhoan.Rows[e.RowIndex];
                txtTenHienThi.Text = row.Cells[0].Value.ToString();
                txtTaiKhoan.Text = row.Cells[1].Value.ToString();
                txtMatKhau.Text = row.Cells[2].Value.ToString();
                if ((int)row.Cells[3].Value == 1)
                {
                    cboLoaiTK.Text = "Quản lý";
                }
                else
                {
                    cboLoaiTK.Text = "Nhân viên";
                }
                
            }
        }

        private void resetAllTextBox()
        {
            txtTaiKhoan.ResetText();
            txtTenHienThi.ResetText();
            txtMatKhau.ResetText();
            cboLoaiTK.ResetText();
            txtTaiKhoan.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            txtTaiKhoan.Enabled = true;
            txtTenHienThi.Enabled = true;
            txtMatKhau.Enabled = true;
            cboLoaiTK.Enabled = true;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            txtTenHienThi.Focus();
            resetAllTextBox();
        }

        private bool kiemTraTrong()
        {
            if (txtTaiKhoan.Text.Length == 0 || txtTenHienThi.Text.Length == 0 || txtMatKhau.Text.Length == 0)
                return true;
            return false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (kiemTraTrong())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin trước khi thêm tài khoản!");
                return;
            }
            if (btnThem.Enabled == false)
            {
                try
                {
                    int loaiTK;
                    if (cboLoaiTK.Text.Trim() == "Quản lý")
                        loaiTK = 1;
                    else
                        loaiTK = 0;
                    string kiemTraTen = "select DisplayName from dbo.TaiKhoan where UserName = N'" + txtTaiKhoan.Text.Trim() + "'";
                    if (DataProvider.Instance.ExecuteScalar(kiemTraTen) != null)
                    {
                        MessageBox.Show("Tên user này đã tồn tại vui lòng sửa tên user khác!");
                        return;
                    }
                    string query = "insert into TaiKhoan values(N'" + txtTenHienThi.Text.Trim() + "','" + txtTaiKhoan.Text.Trim() + "','" + txtMatKhau.Text.Trim() + "'," + loaiTK.ToString().Trim() + ")";
                    DataProvider.Instance.ExecuteNonQuery(query);
                    MessageBox.Show("Thêm tài khoản thành công!");
                    load_dataGridView();
                    resetAllTextBox();
                }
                catch (Exception)
                {
                    MessageBox.Show("Thêm tài khoản thất bại!");
                }
            }
            else
            {
                try
                {
                    int loaiTK;
                    if (cboLoaiTK.Text.Trim() == "Quản lý")
                        loaiTK = 1;
                    else
                        loaiTK = 0;
                    string query = "update TaiKhoan set DisplayName = N'" + txtTenHienThi.Text.Trim() + "', PassWord = '" + txtMatKhau.Text.Trim() + "', Type= " + loaiTK.ToString().Trim() + " where UserName = '" + txtTaiKhoan.Text.Trim() + "'";
                    DataProvider.Instance.ExecuteNonQuery(query);
                    MessageBox.Show("Cập nhật thông tin tài khoản thành công!");
                    load_dataGridView();
                    resetAllTextBox();
                }
                catch (Exception)
                {
                    MessageBox.Show("Cập nhật thông tin tài khoản thất bại!");
                }
            }
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnSua.Enabled = false;
            btnBoQua.Enabled = false;
            btnXoa.Enabled = false;
            resetAllTextBox();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = false;
            load_dataGridView();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            txtTaiKhoan.Enabled = false;
            txtMatKhau.Enabled = false;
            txtTenHienThi.Enabled = false;
            txtTaiKhoan.ResetText();
            txtMatKhau.ResetText();
            txtTenHienThi.ResetText();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (username.Equals(txtTaiKhoan.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng đừng xoá chính bạn");
                    return;
                }
                string query = "delete from TaiKhoan where UserName = N'" + txtTaiKhoan.Text.Trim() + "'";
                DataProvider.Instance.ExecuteNonQuery(query);
                MessageBox.Show("Xóa tài khoản thành công");
                load_dataGridView();
                resetAllTextBox();
            }
            catch (Exception)
            {
                MessageBox.Show("Xóa tài khoản thất bại!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            txtTaiKhoan.Enabled = true;
            txtTenHienThi.Enabled = true;
            txtMatKhau.Enabled = true;
            cboLoaiTK.Enabled = true;
            btnLuu.Enabled = true;
            btnBoQua.Enabled = true;
            txtTenHienThi.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormQuanLyTaiKhoan_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.No)
                e.Cancel = true;
        }

        private void btnLamMoiTimKiem_Click(object sender, EventArgs e)
        {
            txtTimKiem.ResetText();
            txtTimKiem.Focus();
            load_dataGridView();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (rdoLoaiTK.Checked == false && rdoTaiKhoan.Checked == false && rdoTen.Checked == false)
            {
                MessageBox.Show("Vui lòng chọn loại tìm kiếm ở trên textbox");
                return;
            }
            if (txtTimKiem.Text.Length == 0)
            {
                load_dataGridView();
                return;
            }
            try
            {
                string sql = "";
                if (rdoLoaiTK.Checked)
                {
                    sql = string.Format("select * from dbo.TaiKhoan where Type = {0}", txtTimKiem.Text.ToString().Trim());
                }
                if (rdoTaiKhoan.Checked)
                {
                    sql = string.Format("select * from dbo.TaiKhoan where dbo.fuConvertToUnsign1(UserName) like '%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", txtTimKiem.Text.ToString().Trim());
                }
                if (rdoTen.Checked)
                {
                    sql = string.Format("select * from dbo.TaiKhoan where dbo.fuConvertToUnsign1(DisplayName) like N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", txtTimKiem.Text.ToString().Trim());
                }
                dgvDSTaiKhoan.DataSource = DataProvider.Instance.ExecuteQuery(sql);
            }
            catch (Exception)
            {
                MessageBox.Show("Tìm kiếm tài khoản thất bại!");
            }
        }

        private void dgvDSTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != 0)
            {
                btnSua.Enabled = btnXoa.Enabled = true;
            }
        }
    }
}
