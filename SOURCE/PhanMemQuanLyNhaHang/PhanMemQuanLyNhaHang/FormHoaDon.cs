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
    public partial class FormHoaDon : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idHD = 0;
        public int status = 0;
        public FormHoaDon()
        {
            InitializeComponent();
            loadDataHoaDon();
        }

        private void loadDataHoaDon()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from hd in db.HOADONs
                                            from kh in db.KHACHHANGs
                                            from nv in db.NHANVIENs
                                            from b in db.BANs
                                            where hd.MaKH == kh.MaKH
                                            where hd.MaNV == nv.MaNV
                                            where hd.MaBan == b.MaBan
                                            select new
                                            {
                                                MaHoaDon = hd.MaHoaDon,
                                                GioVao = hd.GioVao,
                                                GioRa = hd.GioRa,
                                                MaBan = b.Ten,
                                                TrangThai = hd.TrangThai,
                                                MaKH = kh.TenKH,
                                                MaNV = nv.TenNV,
                                                GiamGia = hd.GiamGia,
                                                TongTien = hd.TongTien
                                            };
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbThanhToan.Checked)
            {
                guna2DataGridView1.Rows.Clear();
                guna2DataGridView1.DataSource = from hd in db.HOADONs
                                                from kh in db.KHACHHANGs
                                                from nv in db.NHANVIENs
                                                from b in db.BANs
                                                where hd.MaKH == kh.MaKH
                                                where hd.MaNV == nv.MaNV
                                                where hd.MaBan == b.MaBan
                                                where hd.TrangThai == 1
                                                select new
                                                {
                                                    MaHoaDon = hd.MaHoaDon,
                                                    GioVao = hd.GioVao,
                                                    GioRa = hd.GioRa,
                                                    MaBan = b.Ten,
                                                    TrangThai = hd.TrangThai,
                                                    MaKH = kh.TenKH,
                                                    MaNV = nv.TenNV,
                                                    GiamGia = hd.GiamGia,
                                                    TongTien = hd.TongTien
                                                };
            }
            else
                loadDataHoaDon();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                idHD = Int32.Parse(row.Cells[0].Value.ToString());
                loadDataChiTiet(idHD);
                status = Int32.Parse(row.Cells[4].Value.ToString().Trim());
            }
        }

        private void loadDataChiTiet(int id)
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView2.DataSource = from hd in db.HOADONs
                                            from ct in db.CHITIETHOADONs
                                            from ma in db.MONANs
                                            where hd.MaHoaDon == ct.MaHoaDon
                                            where ct.MaMonAn == ma.MaMonAn
                                            where ct.MaHoaDon == idHD
                                            select new
                                            {
                                                MaChiTietHoaDon = ct.MaChiTietHoaDon,
                                                MaMonAn = ma.TenMonAn,
                                                SoLuong = ct.SoLuong,
                                                DonGia = ma.GiaTien
                                            };
        }

        private void inHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (status == 0)
            {
                MessageBox.Show("Hóa đơn này chưa thanh toán! Vui lòng thanh toán để in hóa đơn");
                return;
            }
            if (idHD != -1)
            {
                new frm_XemIn(idHD).Show();
            }
            else
                return;
        }
    }
}
