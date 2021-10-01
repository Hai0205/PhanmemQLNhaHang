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
    public partial class FormInPhieuLuong : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int t = 0;
        public int n = 0;

        public FormInPhieuLuong(int thang, int nam)
        {
            InitializeComponent();
            t = thang;
            n = nam;
        }

        private void FormInPhieuLuong_Load(object sender, EventArgs e)
        {
            MyLuong rpt = new MyLuong();
            string query = "select * from InPhieuLuong where Thang = " + t.ToString().Trim() + " and Nam = " +n.ToString().Trim();
            rpt.SetDataSource(DataProvider.Instance.ExecuteQuery(query));
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
