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
    public partial class FormDonGia : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idMaMonAn;
        public FormDonGia()
        {
            InitializeComponent();
            loadDataMonAn();
        }

        public void loadDataMonAn()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from ma in db.MONANs
                                           from dm in db.DANHMUCs
                                           where ma.MaDanhMuc == dm.MaDanhMuc
                                           select new
                                           {
                                               MaMonAn = ma.MaMonAn,
                                               TenMonAn = ma.TenMonAn,
                                               GiaTien = ma.GiaTien,
                                               TenDanhMuc = dm.TenDanhMuc
                                           };
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                idMaMonAn = Int32.Parse(row.Cells[0].Value.ToString());
                loadDataDonGia(idMaMonAn);
            }
        }

        public void loadDataDonGia(int id)
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView2.DataSource = from ma in db.MONANs
                                            from dg in db.DONGIAs
                                            where ma.MaMonAn == dg.MaMonAn
                                            where ma.MaMonAn == id
                                            select new
                                            {
                                                GiaTien = dg.GiaTien,
                                                NgayCapNhat = dg.NgayCapNhat
                                            };
        }
    }
}
