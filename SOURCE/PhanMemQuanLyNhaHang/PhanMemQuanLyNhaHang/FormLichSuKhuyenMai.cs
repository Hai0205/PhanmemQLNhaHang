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
    public partial class FormLichSuKhuyenMai : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public FormLichSuKhuyenMai()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from mkm in db.MAKHUYENMAIs
                                            from kh in db.KHACHHANGs
                                            from lskm in db.LICHSUKHUYENMAIs
                                            where lskm.MaKH == kh.MaKH
                                            where lskm.MaKM == mkm.ID
                                            select new
                                            {
                                                ID = lskm.MaLichSuKhuyenMai,
                                                TenKH = kh.TenKH,
                                                MaKM = mkm.MaKhuyenMai1,
                                                Ngay = lskm.NgaySuDung
                                            };
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from mkm in db.MAKHUYENMAIs
                                            from kh in db.KHACHHANGs
                                            from lskm in db.LICHSUKHUYENMAIs
                                            where lskm.MaKH == kh.MaKH
                                            where lskm.MaKM == mkm.ID
                                            where kh.TenKH.Contains(txt_timKiem.Text.Trim())
                                            select new
                                            {
                                                ID = lskm.MaLichSuKhuyenMai,
                                                TenKH = kh.TenKH,
                                                MaKM = mkm.MaKhuyenMai1,
                                                Ngay = lskm.NgaySuDung
                                            };
        }
    }
}
