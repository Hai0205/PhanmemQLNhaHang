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
    public partial class FormChucVu : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idCV = 0;
        public int idNV = 0;
        public FormChucVu()
        {
            InitializeComponent();
            loadDataCV();
            loadDataNV();
            txtTurnOffCV();
            txtTurnOffNV();
            loadComboCV();
            loadComboNV();
        }

        public void loadDataCV()
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from x in db.CHUCVUs
                                           select new
                                           {
                                               MaChucVu = x.MaChucVu,
                                               TenChucVu = x.TenChucVu,
                                               LuongCoBan = x.LuongCoBan
                                           };
        }

        public void loadDataNV()
        {
            gunaDataGridView2.Rows.Clear();
            gunaDataGridView2.DataSource = from cv in db.CHUCVUs
                                           from nv in db.NHANVIENs
                                           from nvcv in db.ChucVuNhanViens
                                           where nvcv.MaCV == cv.MaChucVu
                                           where nvcv.MaNV == nv.MaNV
                                           select new
                                           {
                                               MaChucVuNhanVien = nvcv.MaChucVuNhanVien,
                                               TenNV = nv.TenNV,
                                               TenCV = cv.TenChucVu
                                           };
        }

        private void loadComboCV()
        {
            var list = from x in db.CHUCVUs
                       select new
                       {
                           MaChucVu = x.MaChucVu,
                           TenChucVu = x.TenChucVu
                       };
            comboCV.DataSource = list;
            comboCV.DisplayMember = "TenChucVu";
            comboCV.ValueMember = "MaChucVu";
        }

        private void loadComboNV()
        {
            var list = from x in db.NHANVIENs
                       select new
                       {
                           MaNV = x.MaNV,
                           TenNV = x.TenNV
                       };
            comboNV.DataSource = list;
            comboNV.DisplayMember = "TenNV";
            comboNV.ValueMember = "MaNV";
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idCV = Int32.Parse(row.Cells[0].Value.ToString());
                txtChucVu.Text = row.Cells[1].Value.ToString();
                txtLuongThang.Text = row.Cells[2].Value.ToString();
            }
        }

        private void gunaDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView2.Rows[e.RowIndex];
                idNV = Int32.Parse(row.Cells[0].Value.ToString());
                comboNV.Text = row.Cells[1].Value.ToString();
                comboCV.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btnLuuCV_Click(object sender, EventArgs e)
        {
            if(txtLuongThang.Value <= 0)
            {
                MessageBox.Show("TIền lương vui lòng lớn hơn 0đ !");
                return;
            }

            if (btnThemCV.Enabled == false && btnSuaCV.Enabled == true)
            {
                var ktTrung = db.CHUCVUs.Where(a => a.TenChucVu == txtChucVu.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Chức vụ này đã tồn tại !");
                    return;
                }

                CHUCVU x = new CHUCVU();
                x.TenChucVu = txtChucVu.Text;
                x.LuongCoBan = Double.Parse(txtLuongThang.Text);
                db.CHUCVUs.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThemCV.Enabled == true && btnSuaCV.Enabled == false)
            {
                CHUCVU x = db.CHUCVUs.Where(t => t.MaChucVu == idCV).FirstOrDefault();
                x.TenChucVu = txtChucVu.Text;
                x.LuongCoBan = Double.Parse(txtLuongThang.Text);
                db.SubmitChanges();
            }
            txtTurnOffCV();
            btnThemCV.Enabled = btnSuaCV.Enabled = true;
            btnLuuCV.Enabled = btnBoQuaCV.Enabled = false;
            loadDataCV();
        }

        private void txtTurnOnCV()
        {
            txtChucVu.Enabled = txtLuongThang.Enabled = true;
        }

        private void txtTurnOffCV()
        {
            txtChucVu.Enabled = txtLuongThang.Enabled = false;
        }

        private void btnBoQuaCV_Click(object sender, EventArgs e)
        {
            txtTurnOffCV();
            btnThemCV.Enabled = true;
            btnSuaCV.Enabled = true;
            btnLuuCV.Enabled = false;
            btnBoQuaCV.Enabled = false;
        }

        private void btnThemCV_Click(object sender, EventArgs e)
        {
            txtTurnOnCV();
            clearTextBoxCV();
            btnThemCV.Enabled = false;
            btnSuaCV.Enabled = true;
            btnLuuCV.Enabled = btnBoQuaCV.Enabled = true;
        }

        private void clearTextBoxCV()
        {
            txtChucVu.ResetText();
            txtLuongThang.ResetText();
        }

        private void btnSuaCV_Click(object sender, EventArgs e)
        {
            txtTurnOnCV();
            btnSuaCV.Enabled = false;
            btnThemCV.Enabled = true;
            btnLuuCV.Enabled = btnBoQuaCV.Enabled = true;
            txtChucVu.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoaCV_Click(object sender, EventArgs e)
        {
            CHUCVU x = new CHUCVU();
            x = db.CHUCVUs.Where(s => s.MaChucVu == idCV).Single();
            db.CHUCVUs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataCV();
        }

        private void btnTimCV_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from x in db.CHUCVUs
                                           where x.TenChucVu.Contains(txtTimCV.Text.Trim())
                                           select new
                                           {
                                               MaChucVu = x.MaChucVu,
                                               TenChucVu = x.TenChucVu,
                                               LuongCoBan = x.LuongCoBan
                                           };
        }

        private void btnLuuNV_Click(object sender, EventArgs e)
        {
            if (btnThemNV.Enabled == false && btnSuaNV.Enabled == true)
            {
                var ktTrung = db.ChucVuNhanViens.Where(a => a.MaNV == Int32.Parse(comboNV.SelectedValue.ToString())).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Nhân viên này đã có chức vụ !");
                    return;
                }

                ChucVuNhanVien x = new ChucVuNhanVien();
                x.MaNV = Int32.Parse(comboNV.SelectedValue.ToString());
                x.MaCV = Int32.Parse(comboCV.SelectedValue.ToString());
                db.ChucVuNhanViens.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThemNV.Enabled == true && btnSuaNV.Enabled == false)
            {
                ChucVuNhanVien x = db.ChucVuNhanViens.Where(t => t.MaChucVuNhanVien == idNV).FirstOrDefault();
                x.MaNV = Int32.Parse(comboNV.SelectedValue.ToString());
                x.MaCV = Int32.Parse(comboCV.SelectedValue.ToString());
                db.SubmitChanges();
            }
            txtTurnOffNV();
            btnThemNV.Enabled = btnSuaNV.Enabled = true;
            btnLuuNV.Enabled = btnBoQuaNV.Enabled = false;
            loadDataNV();
        }

        private void txtTurnOnNV()
        {
            comboNV.Enabled =  true;
        }

        private void txtTurnOffNV()
        {
            comboNV.Enabled = false;
        }

        private void btnBoQuaNV_Click(object sender, EventArgs e)
        {
            txtTurnOffNV();
            btnThemNV.Enabled = true;
            btnSuaNV.Enabled = true;
            btnLuuNV.Enabled = false;
            btnBoQuaNV.Enabled = false;
        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            txtTurnOnNV();
            clearTextBoxNV();
            btnThemNV.Enabled = false;
            btnSuaNV.Enabled = true;
            btnLuuNV.Enabled = btnBoQuaNV.Enabled = true;
        }

        private void clearTextBoxNV()
        {
            comboCV.ResetText();
            comboNV.ResetText();
        }

        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            txtTurnOnNV();
            btnSuaNV.Enabled = false;
            btnThemNV.Enabled = true;
            btnLuuNV.Enabled = btnBoQuaNV.Enabled = true;
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            ChucVuNhanVien x = new ChucVuNhanVien();
            x = db.ChucVuNhanViens.Where(s => s.MaChucVuNhanVien == idNV).Single();
            db.ChucVuNhanViens.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataNV();
        }

        private void btnTimNV_Click(object sender, EventArgs e)
        {
            gunaDataGridView2.Rows.Clear();
            gunaDataGridView2.DataSource = from cv in db.CHUCVUs
                                           from nv in db.NHANVIENs
                                           from nvcv in db.ChucVuNhanViens
                                           where nvcv.MaCV == cv.MaChucVu
                                           where nvcv.MaNV == nv.MaNV
                                           where nv.TenNV.Contains(txtTimNV.Text.Trim())
                                           select new
                                           {
                                               MaChucVuNhanVien = nvcv.MaChucVuNhanVien,
                                               TenNV = nv.TenNV,
                                               TenCV = cv.TenChucVu
                                           };
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            gunaDataGridView2.Rows.Clear();
            gunaDataGridView2.DataSource = from cv in db.CHUCVUs
                                           from nv in db.NHANVIENs
                                           from nvcv in db.ChucVuNhanViens
                                           where nvcv.MaCV == cv.MaChucVu
                                           where nvcv.MaNV == nv.MaNV
                                           where cv.TenChucVu == comboCV.Text.Trim()
                                           select new
                                           {
                                               MaChucVuNhanVien = nvcv.MaChucVuNhanVien,
                                               TenNV = nv.TenNV,
                                               TenCV = cv.TenChucVu
                                           };
        }

        private void comboNV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboCV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel11_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {

        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel17_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaDataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTimNV_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GroupBox4_Click(object sender, EventArgs e)
        {

        }

        private void panel20_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel21_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel22_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel23_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel24_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel25_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel26_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel6_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel5_Click(object sender, EventArgs e)
        {

        }

        private void txtLuongThang_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2GroupBox3_Click(object sender, EventArgs e)
        {

        }

        private void txtChucVu_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }

        private void txtTimCV_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
