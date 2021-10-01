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
    public partial class FormDonDatHang : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idDDH = 0;
        public FormDonDatHang()
        {
            InitializeComponent();
            loadDataDonDatHang();
        }

        private void loadDataDonDatHang()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from nv in db.NHANVIENs
                                            from ncc in db.NHACUNGCAPs
                                            from ddh in db.DONDATHANGs
                                            where ddh.MaNV == nv.MaNV
                                            where ddh.MaNCC == ncc.MaNCC
                                            select new
                                            {
                                                MaDatHang = ddh.MaDatHang,
                                                TenNV = nv.TenNV,
                                                TenNCC = ncc.TenNCC,
                                                NgayDatHang = ddh.NgayDatHang,
                                                TinhTrang = ddh.TinhTrang
                                            };
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2DataGridView1.Rows.Clear();
                guna2DataGridView1.DataSource = from nv in db.NHANVIENs
                                                from ncc in db.NHACUNGCAPs
                                                from ddh in db.DONDATHANGs
                                                where ddh.MaNV == nv.MaNV
                                                where ddh.MaNCC == ncc.MaNCC
                                                where ddh.TinhTrang == "Đã xác nhận"
                                                select new
                                                {
                                                    MaDatHang = ddh.MaDatHang,
                                                    TenNV = nv.TenNV,
                                                    TenNCC = ncc.TenNCC,
                                                    NgayDatHang = ddh.NgayDatHang,
                                                    TinhTrang = ddh.TinhTrang
                                                };
            }
            else
                loadDataDonDatHang();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                if (row.Cells[0].Value.ToString() != "")
                {
                    idDDH = Int32.Parse(row.Cells[0].Value.ToString());
                    loadDataChiTiet(idDDH);
                }
            }
        }

        private void loadDataChiTiet(int id)
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView2.DataSource = from nl in db.NGUYENLIEUs
                                            from ctddh in db.CHITIETDONDATHANGs
                                            where ctddh.MaNL == nl.MaNguyenLieu
                                            where ctddh.MaDDH == id
                                            select new
                                            {
                                                MaChiMaChiTietDatHang = ctddh.MaChiTietDatHang,
                                                TenNL = nl.TenNguyenLieu,
                                                SoLuong = ctddh.SoLuong,
                                                DonGia = nl.GiaNhap
                                            };
        }

        private void btnXacNhanDat_Click(object sender, EventArgs e)
        {
            DONDATHANG x = db.DONDATHANGs.Where(t => t.MaDatHang == idDDH).FirstOrDefault();
            x.TinhTrang = "Đã xác nhận";
            db.SubmitChanges();
            MessageBox.Show("Đơn đặt hàng đã được xác nhận!");
            loadDataDonDatHang();
        }
    }
}
