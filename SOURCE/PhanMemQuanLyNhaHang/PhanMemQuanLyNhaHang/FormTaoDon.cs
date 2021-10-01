using PhanMemQuanLyNhaHang.XuLy;
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
    public partial class FormTaoDon : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idDonMoi = 0;
        public int idNhanVien = 0;
        public int idChiTiet = 0;

        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }

        void ChangeAccount(Account acc)
        {
            txtTenNV.Text = LoginAccount.TenNV;
            idNhanVien = LoginAccount.MaNV;
        }

        public FormTaoDon(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            loadComboNCC();
            loadComboNL();
        }

        private void loadComboNCC()
        {
            var list = from x in db.NHACUNGCAPs
                       select new
                       {
                           MaNCC = x.MaNCC,
                           TenNCC = x.TenNCC
                       };
            comboNCC.DataSource = list;
            comboNCC.DisplayMember = "TenNCC";
            comboNCC.ValueMember = "MaNCC";
        }

        private void loadComboNL()
        {
            var list = from x in db.NGUYENLIEUs
                       select new
                       {
                           MaNguyenLieu = x.MaNguyenLieu,
                           TenNguyenLieu = x.TenNguyenLieu
                       };
            comboNguyenLieu.DataSource = list;
            comboNguyenLieu.DisplayMember = "TenNguyenLieu";
            comboNguyenLieu.ValueMember = "MaNguyenLieu";
        }

        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            guna2GroupBox2.Enabled = true;
            guna2GroupBox1.Enabled = false;
            DONDATHANG x = new DONDATHANG();
            x.MaNV = idNhanVien;
            x.MaNCC = Int32.Parse(comboNCC.SelectedValue.ToString());
            x.NgayDatHang = DateTime.Today;
            x.TinhTrang = "Không sử dụng";
            db.DONDATHANGs.InsertOnSubmit(x);
            db.SubmitChanges();
            idDonMoi = db.DONDATHANGs.ToList().LastOrDefault().MaDatHang;

            loadDataChiTiet();
        }

        private void loadDataChiTiet()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from ctddh in db.CHITIETDONDATHANGs
                                            from nl in db.NGUYENLIEUs
                                            where ctddh.MaDDH == idDonMoi
                                            where nl.MaNguyenLieu == ctddh.MaNL
                                           select new
                                           {
                                               MaChiTietDatHang = ctddh.MaChiTietDatHang,
                                               TenNguyenLieu = nl.TenNguyenLieu,
                                               SoLuong = ctddh.SoLuong,
                                               DonGia = nl.GiaNhap
                                           };
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                idChiTiet = Int32.Parse(row.Cells[0].Value.ToString());
                if(row.Cells[1].Value.ToString() != "")
                    comboNCC.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value.ToString() != "")
                    txtSoLuong.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var ktraTrung = from ctddh in db.CHITIETDONDATHANGs
                            where ctddh.MaDDH == idDonMoi
                            where ctddh.MaNL == Int32.Parse(comboNguyenLieu.SelectedValue.ToString())
                            select ctddh.MaChiTietDatHang;
            
            foreach(int idTrung in ktraTrung)
            {
                if(idTrung > 0)
                {
                    MessageBox.Show("Nguyên liệu đã có không thể thêm!");
                    return;
                }    
            }    

            CHITIETDONDATHANG x = new CHITIETDONDATHANG();
            x.MaDDH = idDonMoi;
            x.MaNL = Int32.Parse(comboNguyenLieu.SelectedValue.ToString());
            x.SoLuong = Int32.Parse(txtSoLuong.Value.ToString().Trim());
            db.CHITIETDONDATHANGs.InsertOnSubmit(x);
            db.SubmitChanges();
            loadDataChiTiet();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var ktraTrung = from ctddh in db.CHITIETDONDATHANGs
                            where ctddh.MaDDH == idDonMoi
                            where ctddh.MaNL == Int32.Parse(comboNguyenLieu.SelectedValue.ToString())
                            select ctddh.MaChiTietDatHang;

            foreach (int idTrung in ktraTrung)
            {
                if (idTrung > 0)
                {
                    MessageBox.Show("Nguyên liệu đã có không thể sửa!");
                    return;
                }
            }   

            CHITIETDONDATHANG x = db.CHITIETDONDATHANGs.Where(t => t.MaChiTietDatHang == idChiTiet).FirstOrDefault();
            x.MaNL = Int32.Parse(comboNguyenLieu.SelectedValue.ToString());
            x.SoLuong = Int32.Parse(txtSoLuong.Value.ToString().Trim());
            db.SubmitChanges();
            loadDataChiTiet();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            CHITIETDONDATHANG x = new CHITIETDONDATHANG();
            x = db.CHITIETDONDATHANGs.Where(s => s.MaChiTietDatHang == idChiTiet).Single();
            db.CHITIETDONDATHANGs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataChiTiet();
        }

        private void btnXacNhanDat_Click(object sender, EventArgs e)
        {
            DONDATHANG x = db.DONDATHANGs.Where(t => t.MaDatHang == idDonMoi).FirstOrDefault();
            x.TinhTrang = "Đã xác nhận";
            db.SubmitChanges();
            clearBox();
            guna2GroupBox2.Enabled = false;
            guna2GroupBox1.Enabled = true;
            MessageBox.Show("Tạo đơn đặt hàng thành công !");
        }

        private void btnLuuTam_Click(object sender, EventArgs e)
        {
            DONDATHANG x = db.DONDATHANGs.Where(t => t.MaDatHang == idDonMoi).FirstOrDefault();
            x.TinhTrang = "Chưa xác nhận";
            db.SubmitChanges();
            clearBox();
            guna2GroupBox2.Enabled = false;
            guna2GroupBox1.Enabled = true;
            MessageBox.Show("Tạo đơn đặt hàng tạm thành công !");
        }

        private void clearBox()
        {
            comboNCC.ResetText();
            txtSoLuong.ResetText();
            guna2DataGridView1.Rows.Clear();
        }
    }
}
