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
    public partial class FormNguyenLieu : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idNguyenLieu = 0;
        public string fileHinhData = "";

        DataTable dt = new DataTable();
        public FormNguyenLieu()
        {
            InitializeComponent();
            loadDataNguyenLieu();
            loadComboNSX();
            txtTurnOff();
            loadDuongDan();
            btn_Thongkenguyenlieu.Enabled = true;
           
        }

        private void loadDuongDan()
        {
            fileHinhData = db.DUONGDANs.FirstOrDefault().DuongDan1.ToString().Trim();
        }

        public void loadDataNguyenLieu()
        {
            gunaDataGridView1.DataSource = from nsx in db.NHASANXUATs
                                           from nl in db.NGUYENLIEUs
                                           where nsx.MaNSX == nl.MaNSX
                                           select new
                                           {
                                               MaNguyenLieu = nl.MaNguyenLieu,
                                               TenNguyenLieu = nl.TenNguyenLieu,
                                               GiaNhap = nl.GiaNhap,
                                               SoLuong = nl.SoLuong,
                                               Hinh = nl.Hinh,
                                               TenNSX = nsx.TenNSX,
                                               CheBien = nl.CheBien,
                                               DonVi = nl.DonVi
                                           };

            
        }

      
        private void loadComboNSX()
        {
            var list = from x in db.NHASANXUATs
                       select new
                       {
                           MaNSX = x.MaNSX,
                           TenNSX = x.TenNSX
                       };
            comboNSX.DataSource = list;
            comboNSX.DisplayMember = "TenNSX";
            comboNSX.ValueMember = "MaNSX";
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idNguyenLieu = Int32.Parse(row.Cells[0].Value.ToString());
                txtTen.Text = row.Cells[1].Value.ToString();
                txtGiaNhap.Text = row.Cells[2].Value.ToString();
                txtSoLuong.Text = row.Cells[3].Value.ToString();
                if (row.Cells[4].Value.ToString().Trim() != "")
                    pictureBox1.Image = Image.FromFile(row.Cells[4].Value.ToString());
                else
                    pictureBox1.Image = Image.FromFile(fileHinhData.ToString().Trim() + @"\macdinh1.png");
                comboNSX.Text = row.Cells[5].Value.ToString();
                txtCheBien.Text = row.Cells[6].Value.ToString();
                txtDonVi.Text = row.Cells[7].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false && btnSua.Enabled == true)
            {
                var ktTrung = db.NGUYENLIEUs.Where(b => b.TenNguyenLieu == txtTen.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Nguyên liệu này đã tồn tại !");
                    return;
                }

                int soNgauNhien;
                Random rd = new Random();
                soNgauNhien = rd.Next(1, 100000000);
                string fname = soNgauNhien.ToString() + txtGiaNhap.Text + ".jpg";
                string folder = fileHinhData.ToString().Trim();
                string pathstring = System.IO.Path.Combine(folder, fname);
                Image a = pictureBox1.Image;
                a.Save(pathstring);

                NGUYENLIEU x = new NGUYENLIEU();
                x.TenNguyenLieu = txtTen.Text;
                x.GiaNhap = Int32.Parse(txtGiaNhap.Text);
                x.SoLuong = Int32.Parse(txtSoLuong.Value.ToString().Trim());
                if (pictureBox1.Image == null)
                    x.Hinh = "";
                else
                    x.Hinh = pathstring;
                x.MaNSX = Int32.Parse(comboNSX.SelectedValue.ToString());
                x.CheBien = txtCheBien.Text;
                x.DonVi = txtDonVi.Text;
                db.NGUYENLIEUs.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThem.Enabled == true && btnSua.Enabled == false)
            {
                int soNgauNhien;
                Random rd = new Random();
                soNgauNhien = rd.Next(1, 100000000);
                string fname = soNgauNhien.ToString() + txtGiaNhap.Text + ".jpg";
                string folder = fileHinhData.ToString().Trim();
                string pathstring = System.IO.Path.Combine(folder, fname);
                Image a = pictureBox1.Image;
                a.Save(pathstring);

                NGUYENLIEU x = db.NGUYENLIEUs.Where(t => t.MaNguyenLieu == idNguyenLieu).FirstOrDefault();
                x.TenNguyenLieu = txtTen.Text;
                x.GiaNhap = Int32.Parse(txtGiaNhap.Text);
                x.SoLuong = Int32.Parse(txtSoLuong.Value.ToString().Trim());
                if (pictureBox1.Image == null)
                    x.Hinh = "";
                else
                    x.Hinh = pathstring;
                x.MaNSX = Int32.Parse(comboNSX.SelectedValue.ToString());
                x.CheBien = txtCheBien.Text;
                x.DonVi = txtDonVi.Text;
                db.SubmitChanges();
            }
            txtTurnOff();
            btnThem.Enabled = btnSua.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = false;
            loadDataNguyenLieu();
        }

        private void txtTurnOn()
        {
            txtTen.Enabled = txtGiaNhap.Enabled = txtSoLuong.Enabled = pictureBox1.Enabled = comboNSX.Enabled = txtCheBien.Enabled = txtDonVi.Enabled = true;
        }

        private void txtTurnOff()
        {
            txtTen.Enabled = txtGiaNhap.Enabled = txtSoLuong.Enabled = pictureBox1.Enabled = comboNSX.Enabled = txtCheBien.Enabled = txtDonVi.Enabled = false;
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
            txtTen.ResetText();
            txtGiaNhap.ResetText();
            txtSoLuong.ResetText();
            pictureBox1.ResetText();
            comboNSX.ResetText();
            txtCheBien.ResetText();
            txtDonVi.ResetText();
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
            NGUYENLIEU x = new NGUYENLIEU();
            x = db.NGUYENLIEUs.Where(s => s.MaNguyenLieu == idNguyenLieu).Single();
            db.NGUYENLIEUs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataNguyenLieu();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.DataSource = from nsx in db.NHASANXUATs
                                           from nl in db.NGUYENLIEUs
                                           where nsx.MaNSX == nl.MaNSX
                                           where nl.TenNguyenLieu.Contains(txtTim.Text)
                                           select new
                                           {
                                               MaNguyenLieu = nl.MaNguyenLieu,
                                               TenNguyenLieu = nl.TenNguyenLieu,
                                               GiaNhap = nl.GiaNhap,
                                               SoLuong = nl.SoLuong,
                                               Hinh = nl.Hinh,
                                               TenNSX = nsx.TenNSX,
                                               CheBien = nl.CheBien,
                                               DonVi = nl.DonVi
                                           };
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
        //Cải tiến:
        // Kiểm tra nguyên liệu trong kho, các nguyên liệu gần hết sẽ được chuyển màu để nhân viên kho nhập thêm

        private void loadThongKe()
        {
            gunaDataGridView1.DataSource = from nsx in db.NHASANXUATs
                                           from nl in db.NGUYENLIEUs
                                           where nsx.MaNSX == nl.MaNSX && nl.SoLuong < 10
                                           select new
                                           {
                                               MaNguyenLieu = nl.MaNguyenLieu,
                                               TenNguyenLieu = nl.TenNguyenLieu,
                                               GiaNhap = nl.GiaNhap,
                                               SoLuong = nl.SoLuong,
                                               Hinh = nl.Hinh,
                                               TenNSX = nsx.TenNSX,
                                               CheBien = nl.CheBien,
                                               DonVi = nl.DonVi
                                           };
        }
        private void btn_Thongkenguyenlieu_Click(object sender, EventArgs e)
        {
            if (btn_Thongkenguyenlieu.Text == "SẢN PHẨM SẮP HẾT")
            {
                loadThongKe();
                btn_Thongkenguyenlieu.Text = "QUAY LẠI";
                if (gunaDataGridView1.Rows.Count > 0 && gunaDataGridView1.Columns.Count > 0)
                {
                    foreach (DataGridViewRow row in gunaDataGridView1.Rows)
                    {
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            else
            {
                loadDataNguyenLieu();
                btn_Thongkenguyenlieu.Text = "SẢN PHẨM SẮP HẾT";
            }
        }
    

        private void gunaDataGridView1_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (gunaDataGridView1.Rows.Count > 0 && gunaDataGridView1.Columns.Count > 0)
            {
                foreach (DataGridViewRow row in gunaDataGridView1.Rows)
                {
                    if (Convert.ToInt32(row.Cells[3].Value) < 10)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;

                      
                    }
                }
            }
        }
    }
}
