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
    public partial class FormKhachHang : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idKhachHang = 0;
        public string fileHinhData = "";
        public FormKhachHang()
        {
            InitializeComponent();
            loadComboGioiTinh();
            txtTurnOff();
            loadDuongDan();
        }

        private void loadDuongDan()
        {
            fileHinhData = db.DUONGDANs.FirstOrDefault().DuongDan1.ToString().Trim();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        public void loadDataGridView()
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from kh in db.KHACHHANGs
                                           select new
                                           {
                                               MaKH = kh.MaKH,
                                               TenKH = kh.TenKH,
                                               TenDangNhap = kh.TenDangNhap,
                                               MatKhau = kh.MatKhau,
                                               Email = kh.Email,
                                               NgaySinh = kh.NgaySinh,
                                               GioiTinh = kh.GioiTinh,
                                               CMND = kh.CMND,
                                               DiaChi = kh.DiaChi,
                                               SoDT = kh.SoDT,
                                               Hinh = kh.Hinh
                                           };
        }

        private void loadComboGioiTinh()
        {
            combo_gioiTinh.Items.Add("Nam");
            combo_gioiTinh.Items.Add("Nữ");
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idKhachHang = Int32.Parse(row.Cells[0].Value.ToString());
                txt_hoTen.Text = row.Cells[1].Value.ToString();
                txt_taiKhoan.Text = row.Cells[2].Value.ToString();
                txt_matKhau.Text = row.Cells[3].Value.ToString();
                txt_email.Text = row.Cells[4].Value.ToString();
                txt_ngaysinh.Text = row.Cells[5].Value.ToString();
                combo_gioiTinh.Text = row.Cells[6].Value.ToString().Trim();
                txt_cmnd.Text = row.Cells[7].Value.ToString();
                txt_diaChi.Text = row.Cells[8].Value.ToString();
                txt_sdt.Text = row.Cells[9].Value.ToString();
                if (row.Cells[10].Value.ToString().Trim() != "")
                    pictureBox1.Image = Image.FromFile(row.Cells[10].Value.ToString());
                else
                    pictureBox1.Image = Image.FromFile(fileHinhData.ToString().Trim() + @"\macdinh.png");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            var text = txt_matKhau.Text.Trim();
            if (text.Length < 6)
            {
                MessageBox.Show("Mật khẩu không đủ 6 kí tự !");
                return;
            }

            if (btnThem.Enabled == false && btnSua.Enabled == true)
            {
                var ktTrung = db.KHACHHANGs.Where(b => b.TenDangNhap == txt_taiKhoan.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Tài khoản này đã tồn tại !");
                    return;
                }

                string fname = txt_cmnd.Text + ".jpg";
                string folder = fileHinhData.ToString().Trim();
                string pathstring = System.IO.Path.Combine(folder, fname);
                Image a = pictureBox1.Image;
                a.Save(pathstring);

                KHACHHANG kh = new KHACHHANG();
                kh.TenKH = txt_hoTen.Text;
                kh.TenDangNhap = txt_taiKhoan.Text;
                kh.MatKhau = txt_matKhau.Text;
                kh.Email = txt_email.Text;
                kh.NgaySinh = txt_ngaysinh.Value;
                kh.GioiTinh = combo_gioiTinh.SelectedItem.ToString().Trim();
                kh.CMND = txt_cmnd.Text;
                kh.DiaChi = txt_diaChi.Text;
                kh.SoDT = txt_sdt.Text;
                if (pictureBox1.Image == null)
                    kh.Hinh = "";
                else
                    kh.Hinh = pathstring;
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
            }

            else if (btnThem.Enabled == true && btnSua.Enabled == false)
            {
                int soNgauNhien;
                Random rd = new Random();
                soNgauNhien = rd.Next(1, 100000000);
                string fname = soNgauNhien.ToString() + txt_cmnd.Text + ".jpg";
                string folder = fileHinhData.ToString().Trim();
                string pathstring = System.IO.Path.Combine(folder, fname);
                Image a = pictureBox1.Image;
                a.Save(pathstring);

                KHACHHANG kh = db.KHACHHANGs.Where(t => t.MaKH == idKhachHang).FirstOrDefault();
                kh.TenKH = txt_hoTen.Text;
                kh.TenDangNhap = txt_taiKhoan.Text;
                kh.MatKhau = txt_matKhau.Text;
                kh.Email = txt_email.Text;
                kh.NgaySinh = txt_ngaysinh.Value;
                kh.GioiTinh = combo_gioiTinh.SelectedItem.ToString().Trim();
                kh.CMND = txt_cmnd.Text;
                kh.DiaChi = txt_diaChi.Text;
                kh.SoDT = txt_sdt.Text;
                if (pictureBox1.Image == null)
                    kh.Hinh = "";
                else
                    kh.Hinh = pathstring;
                db.SubmitChanges();
            }
            txtTurnOff();
            btnThem.Enabled = btnSua.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = false;
            FormKhachHang_Load(sender, e);
        }

        private void txtTurnOn()
        {
            txt_hoTen.Enabled = txt_taiKhoan.Enabled = txt_matKhau.Enabled = txt_email.Enabled = txt_ngaysinh.Enabled = combo_gioiTinh.Enabled
                = txt_cmnd.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = true;
        }

        private void txtTurnOff()
        {
            txt_hoTen.Enabled = txt_taiKhoan.Enabled = txt_matKhau.Enabled = txt_email.Enabled = txt_ngaysinh.Enabled = combo_gioiTinh.Enabled
                = txt_cmnd.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = false;
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
        }

        private void clearTextBox()
        {
            txt_hoTen.ResetText();
            txt_taiKhoan.ResetText();
            txt_matKhau.ResetText();
            txt_email.ResetText();
            txt_ngaysinh.ResetText();
            combo_gioiTinh.ResetText();
            txt_cmnd.ResetText();
            txt_diaChi.ResetText();
            txt_sdt.ResetText();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTurnOn();
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = true;
            txt_taiKhoan.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            KHACHHANG kh = new KHACHHANG();
            kh = db.KHACHHANGs.Where(s => s.MaKH == idKhachHang).Single();
            db.KHACHHANGs.DeleteOnSubmit(kh);
            db.SubmitChanges();
            FormKhachHang_Load(sender, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            PictureBox p = sender as PictureBox;
            if (p != null)
            {
                open.Filter = "(*.jpg;*.jpeg;*.bmp;*.png;)| *.jpg; *.jpeg; *.bmp; *.png;";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    p.Image = Image.FromFile(open.FileName);
                }
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            var list = from kh in db.KHACHHANGs
                       where kh.TenKH.Contains(txt_timKiem.Text)
                       select new
                       {
                           MaKH = kh.MaKH,
                           TenKH = kh.TenKH,
                           TenDangNhap = kh.TenDangNhap,
                           MatKhau = kh.MatKhau,
                           Email = kh.Email,
                           NgaySinh = kh.NgaySinh,
                           GioiTinh = kh.GioiTinh,
                           CMND = kh.CMND,
                           DiaChi = kh.DiaChi,
                           SoDT = kh.SoDT,
                           Hinh = kh.Hinh
                       };
            gunaDataGridView1.DataSource = list;
        }
    }
}
