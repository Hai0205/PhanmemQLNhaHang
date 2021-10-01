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
    public partial class FormSoMon : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public FormSoMon()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from ctn in db.COTHENAUs
                                            from ma in db.MONANs
                                            where ctn.MaMon == ma.MaMonAn
                                            select new
                                            {
                                                ID = ctn.ID,
                                                MaMon = ma.TenMonAn,
                                                SoLuongMon = ctn.SoLuongMon
                                            };
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from ctn in db.COTHENAUs
                                            from ma in db.MONANs
                                            where ctn.MaMon == ma.MaMonAn
                                            where ma.TenMonAn.Contains(txt_timKiem.Text)
                                            select new
                                            {
                                                ID = ctn.ID,
                                                MaMon = ma.TenMonAn,
                                                SoLuongMon = ctn.SoLuongMon
                                            };
        }
    }
}
