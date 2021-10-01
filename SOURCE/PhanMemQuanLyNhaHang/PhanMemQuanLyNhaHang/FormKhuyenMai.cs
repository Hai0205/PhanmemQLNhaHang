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
    public partial class FormKhuyenMai : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idMaKhuyenMai = 0;
        public FormKhuyenMai()
        {
            InitializeComponent();
            loadDataGridView();
            txtTurnOff();
        }

        public void loadDataGridView()
        {
            gunaDataGridView1.Rows.Clear();
            var list = from b in db.MAKHUYENMAIs
                       select new
                       {
                           ID = b.ID,
                           MaKhuyenMai = b.MaKhuyenMai1,
                           GioiHanSuDung = b.GioiHanSuDung,
                           HoaDonToiThieu = b.HoaDonToiThieu,
                           NgayTaoMa = b.NgayTaoMa,
                           NgayHetHan = b.NgayHetHan,
                           GiamGia = b.GiamGia
                       };
            gunaDataGridView1.DataSource = list;
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idMaKhuyenMai = Int32.Parse(row.Cells[0].Value.ToString());
                txtMaKhuyenMai.Text = row.Cells[1].Value.ToString();
                txtGioiHanSuDung.Text = row.Cells[2].Value.ToString();
                txtHoaDonToiThieu.Text = row.Cells[3].Value.ToString();
                txtNgayTao.Text = row.Cells[4].Value.ToString();
                txtNgayHetHan.Text = row.Cells[5].Value.ToString();
                txtGiamGia.Text = row.Cells[6].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false && btnSua.Enabled == true)
            {
                var ktTrung = db.MAKHUYENMAIs.Where(a => a.MaKhuyenMai1 == txtMaKhuyenMai.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Mã khuyến mãi này đã tồn tại !");
                    return;
                }

                MAKHUYENMAI x = new MAKHUYENMAI();
                x.MaKhuyenMai1 = txtMaKhuyenMai.Text;
                x.GioiHanSuDung = Int32.Parse(txtGioiHanSuDung.Value.ToString().Trim());
                x.HoaDonToiThieu = Double.Parse(txtHoaDonToiThieu.Value.ToString().Trim());
                x.NgayTaoMa = DateTime.Now;
                x.NgayHetHan = txtNgayHetHan.Value;
                x.GiamGia = Int32.Parse(txtGiamGia.Value.ToString().Trim());
                db.MAKHUYENMAIs.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThem.Enabled == true && btnSua.Enabled == false)
            {
                MAKHUYENMAI x = db.MAKHUYENMAIs.Where(t => t.ID == idMaKhuyenMai).FirstOrDefault();
                x.MaKhuyenMai1 = txtMaKhuyenMai.Text;
                x.GioiHanSuDung = Int32.Parse(txtGioiHanSuDung.Value.ToString().Trim());
                x.HoaDonToiThieu = Double.Parse(txtHoaDonToiThieu.Value.ToString().Trim());
                x.NgayTaoMa = DateTime.Now;
                x.NgayHetHan = txtNgayHetHan.Value;
                x.GiamGia = Int32.Parse(txtGiamGia.Value.ToString().Trim());
                db.SubmitChanges();
            }
            txtTurnOff();
            btnThem.Enabled = btnSua.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = false;
            loadDataGridView();
        }

        private void txtTurnOn()
        {
            txtMaKhuyenMai.Enabled = txtGioiHanSuDung.Enabled = txtHoaDonToiThieu.Enabled = txtNgayHetHan.Enabled = txtGiamGia.Enabled = true;
        }

        private void txtTurnOff()
        {
            txtMaKhuyenMai.Enabled = txtGioiHanSuDung.Enabled = txtHoaDonToiThieu.Enabled = txtNgayHetHan.Enabled = txtGiamGia.Enabled = txtNgayTao.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtTurnOff();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTurnOn();
            clearTextBox();
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = true;
            txtNgayTao.Value = DateTime.Today;
        }

        private void clearTextBox()
        {
            txtMaKhuyenMai.ResetText();
            txtGioiHanSuDung.ResetText();
            txtHoaDonToiThieu.ResetText();
            txtNgayHetHan.ResetText();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTurnOn();
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            MAKHUYENMAI x = new MAKHUYENMAI();
            x = db.MAKHUYENMAIs.Where(s => s.ID == idMaKhuyenMai).Single();
            db.MAKHUYENMAIs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataGridView();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            var list = from b in db.MAKHUYENMAIs
                       where b.MaKhuyenMai1.Contains(txtTim.Text)
                       select new
                       {
                           ID = b.ID,
                           MaKhuyenMai = b.MaKhuyenMai1,
                           GioiHanSuDung = b.GioiHanSuDung,
                           HoaDonToiThieu = b.HoaDonToiThieu,
                           NgayTaoMa = b.NgayTaoMa,
                           NgayHetHan = b.NgayHetHan,
                           GiamGia = b.GiamGia
                       };
            gunaDataGridView1.DataSource = list;
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            FormLichSuKhuyenMai frm = new FormLichSuKhuyenMai();
            frm.ShowDialog();
        }
    }
}
