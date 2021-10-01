using PhanMemQuanLyNhaHang.XuLy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyNhaHang
{
    public partial class FormBaoCaoThongKe : Form
    {
        ConnecDB db = new ConnecDB();

        public FormBaoCaoThongKe()
        {
            InitializeComponent();
            LoadDateTimePickerBill();
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
        }

        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFormDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFormDate.Value.AddMonths(1).AddDays(-1);
        }

        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFormDate.Value, dtpkToDate.Value);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            txtPageBill.Text = "1";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFormDate.Value, dtpkToDate.Value);
            int lastPage = sumRecord / 10;
            if (sumRecord % 10 != 0)
                lastPage++;
            txtPageBill.Text = lastPage.ToString();
        }

        private void txtPageBill_TextChanged(object sender, EventArgs e)
        {
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpkFormDate.Value, dtpkToDate.Value, Convert.ToInt32(txtPageBill.Text));
        }

        private void btnPreviours_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPageBill.Text);
            if (page > 1)
                page--;
            txtPageBill.Text = page.ToString();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkFormDate.Value, dtpkToDate.Value);
            if (page < sumRecord)
                page++;
            txtPageBill.Text = page.ToString();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}