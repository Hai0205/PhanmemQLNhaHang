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
    public partial class FormNhanVien : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idNhanVien = 0;
        public string fileHinhData = "";
        public FormNhanVien()
        {
            InitializeComponent();
            loadComboGioiTinh();
            loadComboQuyen();
            loadDuongDan();
        }

        private void loadDuongDan()
        {
            fileHinhData = db.DUONGDANs.FirstOrDefault().DuongDan1.ToString().Trim();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            loadDataGridView();
        }

        public void loadDataGridView()
        {
            gunaDataGridView1.Rows.Clear();
            var list = from nv in db.NHANVIENs
                       from quyen in db.QUYENs
                       where nv.MaQuyen == quyen.MaQuyen
                       select new
                       {
                           MaNV = nv.MaNV,
                           TenNV = nv.TenNV,
                           TenDangNhap = nv.TenDangNhap,
                           MatKhau = nv.MatKhau,
                           Email = nv.Email,
                           NgaySinh = nv.NgaySinh,
                           GioiTinh = nv.GioiTinh,
                           CMND = nv.CMND,
                           DiaChi = nv.DiaChi,
                           SoDT = nv.SoDT,
                           NgayVaoLam = nv.NgayVaoLam,
                           TenQuyen = quyen.TenQuyen,
                           Hinh = nv.Hinh
                       };
            gunaDataGridView1.DataSource = list;
        }

        private void loadComboGioiTinh()
        {
            combo_gioiTinh.Items.Add("Nam");
            combo_gioiTinh.Items.Add("Nữ");
        }

        private void loadComboQuyen()
        {
            var list = from q in db.QUYENs
                       select new
                       {
                           MaQuyen = q.MaQuyen,
                           TenQuyen = q.TenQuyen
                       };
            combo_quyen.DataSource = list;
            combo_quyen.DisplayMember = "TenQuyen";
            combo_quyen.ValueMember = "MaQuyen";
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                if (row.Cells[0].Value.ToString().Trim() != "")
                idNhanVien = Int32.Parse(row.Cells[0].Value.ToString());
                if (row.Cells[1].Value.ToString().Trim() != "")
                txt_hoTen.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value.ToString().Trim() != "")
                txt_taiKhoan.Text = row.Cells[2].Value.ToString();
                if (row.Cells[3].Value.ToString().Trim() != "")
                txt_matKhau.Text = row.Cells[3].Value.ToString();
                if (row.Cells[4].Value.ToString().Trim() != "")
                txt_email.Text = row.Cells[4].Value.ToString();
                if (row.Cells[5].Value.ToString().Trim() != "")
                txt_ngaysinh.Text = row.Cells[5].Value.ToString();
                if (row.Cells[6].Value.ToString().Trim() != "")
                combo_gioiTinh.Text = row.Cells[6].Value.ToString().Trim();
                if (row.Cells[7].Value.ToString().Trim() != "")
                txt_cmnd.Text = row.Cells[7].Value.ToString();
                if (row.Cells[8].Value.ToString().Trim() != "")
                txt_diaChi.Text = row.Cells[8].Value.ToString();
                if (row.Cells[9].Value.ToString().Trim() != "")
                txt_sdt.Text = row.Cells[9].Value.ToString();
                if (row.Cells[10].Value.ToString().Trim() != "")
                txt_ngayVaoLam.Text = row.Cells[10].Value.ToString();
                if (row.Cells[11].Value.ToString().Trim() != "")
                combo_quyen.Text = row.Cells[11].Value.ToString();
                if (row.Cells[12].Value.ToString().Trim() != "")
                    pictureBox1.Image = Image.FromFile(row.Cells[12].Value.ToString());
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
                var ktTrung = db.NHANVIENs.Where(b => b.TenDangNhap == txt_taiKhoan.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Tài khoản này đã tồn tại !");
                    return;
                }

                int soNgauNhien;
                Random rd = new Random();
                soNgauNhien = rd.Next(1, 100000000);
                string fname = soNgauNhien.ToString() + txt_cmnd.Text + ".jpg";
                string folder = fileHinhData.ToString().Trim();
                string pathstring = System.IO.Path.Combine(folder, fname);
                Image a = pictureBox1.Image;
                a.Save(pathstring);

                NHANVIEN nv = new NHANVIEN();
                nv.TenNV = txt_hoTen.Text;
                nv.TenDangNhap = txt_taiKhoan.Text;
                nv.MatKhau = txt_matKhau.Text;
                nv.Email = txt_email.Text;
                nv.NgaySinh = txt_ngaysinh.Value;
                nv.GioiTinh = combo_gioiTinh.SelectedItem.ToString().Trim();
                nv.CMND = txt_cmnd.Text;
                nv.DiaChi = txt_diaChi.Text;
                nv.SoDT = txt_sdt.Text;
                nv.NgayVaoLam = txt_ngayVaoLam.Value;
                nv.MaQuyen = Int32.Parse(combo_quyen.SelectedValue.ToString());
                if (pictureBox1.Image == null)
                    nv.Hinh = " ";
                else
                    nv.Hinh = pathstring;
                db.NHANVIENs.InsertOnSubmit(nv);
                db.SubmitChanges();
            }

            else if(btnThem.Enabled == true && btnSua.Enabled == false)
            {
                int soNgauNhien;
                Random rd = new Random();
                soNgauNhien = rd.Next(1, 100000000);
                string fname = soNgauNhien.ToString() + txt_cmnd.Text + ".jpg";
                string folder = fileHinhData.ToString().Trim();
                string pathstring = System.IO.Path.Combine(folder, fname);
                Image a = pictureBox1.Image;
                a.Save(pathstring); 

                NHANVIEN nv = db.NHANVIENs.Where(t => t.MaNV == idNhanVien).FirstOrDefault();
                nv.TenNV = txt_hoTen.Text;
                //nv.TenDangNhap = txt_taiKhoan.Text;
                nv.MatKhau = txt_matKhau.Text;
                nv.Email = txt_email.Text;
                nv.NgaySinh = txt_ngaysinh.Value;
                nv.GioiTinh = combo_gioiTinh.SelectedItem.ToString().Trim();
                nv.CMND = txt_cmnd.Text;
                nv.DiaChi = txt_diaChi.Text;
                nv.SoDT = txt_sdt.Text;
                nv.NgayVaoLam = txt_ngayVaoLam.Value;
                nv.MaQuyen = Int32.Parse(combo_quyen.SelectedValue.ToString());
                if (pictureBox1.Image == null)
                    nv.Hinh = " ";
                else
                    nv.Hinh = pathstring;
                db.SubmitChanges();
            }
            txtTurnOff();
            btnThem.Enabled = btnSua.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = false;
            FormNhanVien_Load(sender, e);
        }
        private void txtTurnOn()
        {
            txt_hoTen.Enabled = txt_taiKhoan.Enabled = txt_matKhau.Enabled = txt_email.Enabled = txt_ngaysinh.Enabled = combo_gioiTinh.Enabled
                = txt_cmnd.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = txt_ngayVaoLam.Enabled = combo_quyen.Enabled = true;
        }

        private void txtTurnOff()
        {
            txt_hoTen.Enabled = txt_taiKhoan.Enabled = txt_matKhau.Enabled = txt_email.Enabled = txt_ngaysinh.Enabled = combo_gioiTinh.Enabled
                = txt_cmnd.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = txt_ngayVaoLam.Enabled = combo_quyen.Enabled = false;
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
            txt_ngayVaoLam.ResetText();
            combo_quyen.ResetText();
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
            NHANVIEN nv = new NHANVIEN();
            nv = db.NHANVIENs.Where(s => s.MaNV == idNhanVien).Single();
            db.NHANVIENs.DeleteOnSubmit(nv);
            db.SubmitChanges();
            FormNhanVien_Load(sender, e);
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
            var list = from nv in db.NHANVIENs
                       from quyen in db.QUYENs
                       where nv.MaQuyen == quyen.MaQuyen
                       where nv.TenNV.Contains(txt_timKiem.Text)
                       select new
                       {
                           MaNV = nv.MaNV,
                           TenNV = nv.TenNV,
                           TenDangNhap = nv.TenDangNhap,
                           MatKhau = nv.MatKhau,
                           Email = nv.Email,
                           NgaySinh = nv.NgaySinh,
                           GioiTinh = nv.GioiTinh,
                           CMND = nv.CMND,
                           DiaChi = nv.DiaChi,
                           SoDT = nv.SoDT,
                           NgayVaoLam = nv.NgayVaoLam,
                           TenQuyen = quyen.TenQuyen,
                           Hinh = nv.Hinh
                       };
            gunaDataGridView1.DataSource = list;
        }
    }
}
