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
    public partial class frm_XemIn : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idPhieu = 0;

        public frm_XemIn(int id)
        {
            InitializeComponent();
            idPhieu = id;
        }

        private void frm_XemIn_Load(object sender, EventArgs e)
        {
            MyHoaDon rpt = new MyHoaDon();
            string query = "select * from InHoaDon where MaHoaDon = " + idPhieu.ToString().Trim();
            rpt.SetDataSource(DataProvider.Instance.ExecuteQuery(query));
            crystalReportViewer1.ReportSource = rpt;
        }
    }
}
