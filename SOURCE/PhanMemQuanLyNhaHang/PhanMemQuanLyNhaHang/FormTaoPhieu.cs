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
    public partial class FormTaoPhieu : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idPhieuNhap = 0;
        public int idNhanVien = 0;
        public int idChiTiet = 0;
        public int idChiTietDDH = 0;
        public int idChiTietNH = 0;
        public int soLuongMax = 0;
        public int id = 0;

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

        public FormTaoPhieu(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            loadComboDDH();
        }

        private void loadComboDDH()
        {
            var list = from x in db.DONDATHANGs
                       where x.TinhTrang == "Đã xác nhận"
                       select new
                       {
                           MaDatHang = x.MaDatHang
                       };
            comboDDH.DataSource = list;
            comboDDH.DisplayMember = "MaDatHang";
            comboDDH.ValueMember = "MaDatHang";
        }

        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            guna2GroupBox3.Enabled = true;
            guna2GroupBox2.Enabled = true;
            guna2GroupBox1.Enabled = false;
            int idMaDonDatHang = Int32.Parse(comboDDH.SelectedValue.ToString());

            NHAPHANG x = new NHAPHANG();
            x.MaDDH_DeNhap = Int32.Parse(comboDDH.SelectedValue.ToString());
            x.NgayNhap = DateTime.Today;
            x.NV_NhapHang = idNhanVien;
            x.TrangThai = "Chưa nhập";
            db.NHAPHANGs.InsertOnSubmit(x);
            db.SubmitChanges();
            idPhieuNhap = db.NHAPHANGs.ToList().LastOrDefault().MaNhap;
            loadDataChiTietDDH();
        }

        private void loadDataChiTietDDH()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from nl in db.NGUYENLIEUs
                                            from ctddh in db.CHITIETDONDATHANGs
                                            where ctddh.MaDDH == Int32.Parse(comboDDH.Text.ToString().Trim())
                                            where ctddh.MaNL == nl.MaNguyenLieu
                                            select new
                                            {
                                                MaChiTietDatHang = ctddh.MaChiTietDatHang,
                                                TenNL = nl.TenNguyenLieu,
                                                SoLuongDat = ctddh.SoLuong
                                            };
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                idChiTietDDH = Int32.Parse(row.Cells[0].Value.ToString());
                txtNguyenLieu.Text = row.Cells[1].Value.ToString();
                txtSoLuongDat.Value = Int32.Parse(row.Cells[2].Value.ToString());
                soLuongMax = Int32.Parse(row.Cells[2].Value.ToString());
            }
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            if (soLuongMax < txtSoLuongDat.Value)
            {
                MessageBox.Show("Vui lòng nhập số lượng ít hơn số lượng đặt!");
                return;
            }

            if (txtSoLuongDat.Value < 1)
            {
                MessageBox.Show("Số lượng nguyên liệu nhập phải lớn hơn 0 !");
                return;
            }

            var ktTrung = db.CHITIETNHAPHANGs.Where(a => a.MaNhap == idPhieuNhap)
                                            .Where(b => b.MaCTDDH == idChiTietDDH)
                                            .FirstOrDefault();
            if (ktTrung != null)
            {
                MessageBox.Show("Nguyên liệu này đã tồn tại trong phiếu nhập !");
                return;
            }

            CHITIETNHAPHANG x2 = new CHITIETNHAPHANG();
            x2.MaNhap = idPhieuNhap;
            x2.MaCTDDH = idChiTietDDH;
            x2.SoLuongNhap = Int32.Parse(txtSoLuongDat.Value.ToString().Trim());
            db.CHITIETNHAPHANGs.InsertOnSubmit(x2);
            db.SubmitChanges();
            loadDataChiTietPN(idPhieuNhap);
        }

        private void loadDataChiTietPN(int id)
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView2.DataSource = from ctddh in db.CHITIETDONDATHANGs
                                            from ctnh in db.CHITIETNHAPHANGs
                                            from nl in db.NGUYENLIEUs
                                            where ctnh.MaCTDDH == ctddh.MaChiTietDatHang
                                            where ctddh.MaNL == nl.MaNguyenLieu
                                            where ctnh.MaNhap == idPhieuNhap
                                            select new
                                            {
                                                MaChiTietDonNhapHang = ctnh.MaChiTietDonNhapHang,
                                                TenNL = nl.TenNguyenLieu,
                                                SoLuong = ctnh.SoLuongNhap
                                            };            
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView2.Rows[e.RowIndex];
                idChiTietNH = Int32.Parse(row.Cells[0].Value.ToString());
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            CHITIETNHAPHANG x = new CHITIETNHAPHANG();
            x = db.CHITIETNHAPHANGs.Where(s => s.MaChiTietDonNhapHang == idChiTietNH).Single();
            db.CHITIETNHAPHANGs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataChiTietPN(idPhieuNhap);
        }

        private void btnXacNhanNhap_Click(object sender, EventArgs e)
        {
            var ketQua = from ctpn in db.CHITIETNHAPHANGs
                                   from ctddh in db.CHITIETDONDATHANGs
                                   where ctddh.MaChiTietDatHang == ctpn.MaCTDDH
                                   where ctpn.MaNhap == idPhieuNhap
                                   select new
                                   {
                                       MaNL = ctddh.MaNL,
                                       SoLuong = ctpn.SoLuongNhap
                                   };

            NHAPHANG updateNH = db.NHAPHANGs.Where(nh => nh.MaNhap == idPhieuNhap).FirstOrDefault();
            updateNH.TrangThai = "Đã nhập";
            db.SubmitChanges();

            foreach(var data in ketQua)
            {
                NGUYENLIEU x = db.NGUYENLIEUs.Where(t => t.MaNguyenLieu == data.MaNL).FirstOrDefault();
                x.SoLuong = x.SoLuong + data.SoLuong;
                db.SubmitChanges();

                CHITIETDONDATHANG y = db.CHITIETDONDATHANGs.Where(t => t.MaDDH == Int32.Parse(comboDDH.SelectedValue.ToString())).Where(t => t.MaNL == data.MaNL).FirstOrDefault();
                y.SoLuong = y.SoLuong - data.SoLuong;
                db.SubmitChanges();
            }
            
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView2.Rows.Clear();
            guna2GroupBox3.Enabled = false;
            guna2GroupBox2.Enabled = false;
            guna2GroupBox1.Enabled = true;

            new frm_xemPhieu(idPhieuNhap).Show();
        }

        private void btnHuyNhap_Click(object sender, EventArgs e)
        {
            guna2GroupBox3.Enabled = false;
            guna2GroupBox2.Enabled = false;
            guna2GroupBox1.Enabled = true;
        }
    }
}
