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
    public partial class FormPhieuNhapHang : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idPNH = 0;
        public FormPhieuNhapHang()
        {
            InitializeComponent();
            loadDataNhapHang();
        }

        private void loadDataNhapHang()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from nv in db.NHANVIENs
                                            from nh in db.NHAPHANGs
                                            where nv.MaNV == nh.NV_NhapHang
                                            select new
                                            {
                                                MaNhap = nh.MaNhap,
                                                MaDDH_DeNhap = nh.MaDDH_DeNhap,
                                                NgayNhap = nh.NgayNhap,
                                                LanGiao = 1,
                                                NV_NhapHang = nv.TenNV,
                                                TrangThai = nh.TrangThai
                                            };
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                if (row.Cells[0].Value.ToString() != "")
                {
                    idPNH = Int32.Parse(row.Cells[0].Value.ToString());
                    loadDataChiTiet(idPNH);
                }
            }
        }

        private void loadDataChiTiet(int id)
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView2.DataSource = from ctnh in db.CHITIETNHAPHANGs
                                            from nl in db.NGUYENLIEUs
                                            from ctdh in db.CHITIETDONDATHANGs
                                            where ctdh.MaChiTietDatHang == ctnh.MaCTDDH
                                            where ctdh.MaNL == nl.MaNguyenLieu
                                            where ctnh.MaNhap == id
                                            select new
                                            {
                                                MaChiTietDonNhapHang = ctnh.MaChiTietDonNhapHang,
                                                MaCTDDH = ctnh.MaCTDDH,
                                                TenNL = nl.TenNguyenLieu,
                                                SoLuongNhap = ctnh.SoLuongNhap
                                            };
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked)
            {
                guna2DataGridView1.Rows.Clear();
                guna2DataGridView1.DataSource = from nv in db.NHANVIENs
                                                from nh in db.NHAPHANGs
                                                where nv.MaNV == nh.NV_NhapHang
                                                where nh.TrangThai == "Đã nhập"
                                                select new
                                                {
                                                    MaNhap = nh.MaNhap,
                                                    MaDDH_DeNhap = nh.MaDDH_DeNhap,
                                                    NgayNhap = nh.NgayNhap,
                                                    LanGiao = 1,
                                                    NV_NhapHang = nv.TenNV,
                                                    TrangThai = nh.TrangThai
                                                };
            }
            else
                loadDataNhapHang();
        }
    }
}
