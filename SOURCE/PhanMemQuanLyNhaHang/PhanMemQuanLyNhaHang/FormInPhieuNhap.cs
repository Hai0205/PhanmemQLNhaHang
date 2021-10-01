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
    public partial class frm_xemPhieu : Form
    {
        public int idBill = 0;

        public frm_xemPhieu(int id)
        {
            InitializeComponent();
            idBill = id;
        }

        private void frm_xemPhieu_load(object sender, EventArgs e)
        {
            MyPhieuNhap rpt2 = new MyPhieuNhap();
            string query = "select * from InPhieuNhap where MaNhap = "+idBill.ToString().Trim();
            rpt2.SetDataSource(DataProvider.Instance.ExecuteQuery(query));
            crptView.ReportSource = rpt2;
        }
    }
}
