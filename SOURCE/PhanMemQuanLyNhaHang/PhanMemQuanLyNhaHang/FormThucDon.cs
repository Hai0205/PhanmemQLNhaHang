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
    public partial class FormThucDon : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idDM = 0;
        public int idMA = 0;
        public FormThucDon()
        {
            InitializeComponent();
            loadDataDanhMuc();
            loadDataMonAn();
            txtTurnOffDM();
            txtTurnOffMA();
            loadComboDanhMuc();
        }

        public void loadDataDanhMuc()
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from dm in db.DANHMUCs
                                           select new
                                           {
                                               MaDanhMuc = dm.MaDanhMuc,
                                               TenDanhMuc = dm.TenDanhMuc
                                           };
        }

        public void loadDataMonAn()
        {
            gunaDataGridView2.Rows.Clear();
            gunaDataGridView2.DataSource = from ma in db.MONANs
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

        private void loadComboDanhMuc()
        {
            var list = from dm in db.DANHMUCs
                       select new
                       {
                           MaDanhMuc = dm.MaDanhMuc,
                           TenDanhMuc = dm.TenDanhMuc
                       };
            comboDanhMuc.DataSource = list;
            comboDanhMuc.DisplayMember = "TenDanhMuc";
            comboDanhMuc.ValueMember = "MaDanhMuc";
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idDM = Int32.Parse(row.Cells[0].Value.ToString());
                txtTenDanhMuc.Text = row.Cells[1].Value.ToString();
            }
        }

        private void gunaDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView2.Rows[e.RowIndex];
                idMA = Int32.Parse(row.Cells[0].Value.ToString());
                txtTenMonAn.Text = row.Cells[1].Value.ToString();
                txtGiaNhap.Text = row.Cells[2].Value.ToString();
                comboDanhMuc.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnLuuDM_Click(object sender, EventArgs e)
        {
            if (btnThemDM.Enabled == false)
            {
                var ktTrung = db.DANHMUCs.Where(a => a.TenDanhMuc == txtTenDanhMuc.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Danh mục này đã tồn tại !");
                    return;
                }

                DANHMUC x = new DANHMUC();
                x.TenDanhMuc = txtTenDanhMuc.Text;
                db.DANHMUCs.InsertOnSubmit(x);
                db.SubmitChanges();
            }
            txtTurnOffDM();
            btnThemDM.Enabled = true;
            btnLuuDM.Enabled = btnBoQuaDM.Enabled = false;
            loadDataDanhMuc();
            loadComboDanhMuc();
        }

        private void txtTurnOnDM()
        {
            txtTenDanhMuc.Enabled = true;
        }

        private void txtTurnOffDM()
        {
            txtTenDanhMuc.Enabled = false;
        }

        private void btnBoQuaDM_Click(object sender, EventArgs e)
        {
            txtTurnOffDM();
            btnThemDM.Enabled = true;
            btnLuuDM.Enabled = false;
            btnBoQuaDM.Enabled = false;
        }

        private void btnThemDM_Click(object sender, EventArgs e)
        {
            txtTurnOnDM();
            clearTextBoxDM();
            btnThemDM.Enabled = false;
            btnLuuDM.Enabled = btnBoQuaDM.Enabled = true;
        }

        private void clearTextBoxDM()
        {
            txtTenDanhMuc.ResetText();
        }

        private void btnThoatMA_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoaDM_Click(object sender, EventArgs e)
        {
            DANHMUC x = new DANHMUC();
            x = db.DANHMUCs.Where(s => s.MaDanhMuc == idDM).Single();
            db.DANHMUCs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataDanhMuc();
        }

        private void btnTimDM_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from dm in db.DANHMUCs
                                           where dm.TenDanhMuc.Contains(txtTimDM.Text)
                                           select new
                                           {
                                               MaDanhMuc = dm.MaDanhMuc,
                                               TenDanhMuc = dm.TenDanhMuc
                                           };
        }

        private void btbLuuMA_Click(object sender, EventArgs e)
        {
            if (btnThemMA.Enabled == false && btnSuaMA.Enabled == true)
            {
                var ktTrung = db.MONANs.Where(a => a.TenMonAn == txtTenMonAn.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Món ăn này đã tồn tại !");
                    return;
                }

                MONAN x = new MONAN();
                x.TenMonAn = txtTenMonAn.Text;
                x.GiaTien = Double.Parse(txtGiaNhap.Text);
                x.MaDanhMuc = Int32.Parse(comboDanhMuc.SelectedValue.ToString());
                db.MONANs.InsertOnSubmit(x);
                db.SubmitChanges();

                DONGIA y = new DONGIA();
                y.MaMonAn = db.MONANs.LastOrDefault().MaMonAn;
                y.GiaTien = Double.Parse(txtGiaNhap.Text);
                y.NgayCapNhat = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                db.DONGIAs.InsertOnSubmit(y);
                db.SubmitChanges();
            }

            else if (btnThemMA.Enabled == true && btnSuaMA.Enabled == false)
            {
                MONAN x = db.MONANs.Where(t => t.MaMonAn == idMA).FirstOrDefault();
                x.TenMonAn = txtTenMonAn.Text;
                x.GiaTien = Double.Parse(txtGiaNhap.Text);
                x.MaDanhMuc = Int32.Parse(comboDanhMuc.SelectedValue.ToString());
                db.SubmitChanges();

                DONGIA y = new DONGIA();
                y.MaMonAn = idMA;
                y.GiaTien = Double.Parse(txtGiaNhap.Text);
                y.NgayCapNhat = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
                db.DONGIAs.InsertOnSubmit(y);
                db.SubmitChanges();
            }
            txtTurnOffMA();
            btnThemMA.Enabled = btnSuaMA.Enabled = true;
            btnLuuMA.Enabled = btnBoQuaMA.Enabled = false;
            loadDataMonAn();
        }

        private void txtTurnOnMA()
        {
            txtTenMonAn.Enabled = txtGiaNhap.Enabled = true;
        }

        private void txtTurnOffMA()
        {
            txtTenMonAn.Enabled = txtGiaNhap.Enabled = false;
        }

        private void btnBoQuaMA_Click(object sender, EventArgs e)
        {
            txtTurnOffMA();
            btnThemMA.Enabled = true;
            btnSuaMA.Enabled = true;
            btnLuuMA.Enabled = false;
            btnBoQuaMA.Enabled = false;
        }

        private void btnThemMA_Click(object sender, EventArgs e)
        {
            txtTurnOnMA();
            clearTextBoxMA();
            btnThemMA.Enabled = false;
            btnSuaMA.Enabled = true;
            btnLuuMA.Enabled = btnBoQuaMA.Enabled = true;
        }

        private void clearTextBoxMA()
        {
            txtTenMonAn.ResetText();
            txtGiaNhap.ResetText();
            comboDanhMuc.ResetText();
        }

        private void btnSuaMA_Click(object sender, EventArgs e)
        {
            txtTurnOnMA();
            btnSuaMA.Enabled = false;
            btnThemMA.Enabled = true;
            btnLuuMA.Enabled = btnBoQuaMA.Enabled = true;
            txtTenMonAn.Enabled = false;
        }

        private void btnXoaMA_Click(object sender, EventArgs e)
        {
            MONAN x = new MONAN();
            x = db.MONANs.Where(s => s.MaMonAn == idMA).Single();
            db.MONANs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataMonAn();
        }

        private void btnTimMA_Click(object sender, EventArgs e)
        {
            gunaDataGridView2.Rows.Clear();
            gunaDataGridView2.DataSource = from ma in db.MONANs
                                           from dm in db.DANHMUCs
                                           where ma.MaDanhMuc == dm.MaDanhMuc
                                           where ma.TenMonAn.Contains(txtTimMA.Text)
                                           select new
                                           {
                                               MaMonAn = ma.MaMonAn,
                                               TenMonAn = ma.TenMonAn,
                                               GiaTien = ma.GiaTien,
                                               TenDanhMuc = dm.TenDanhMuc
                                           };
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            gunaDataGridView2.Rows.Clear();
            gunaDataGridView2.DataSource = from ma in db.MONANs
                                           from dm in db.DANHMUCs
                                           where ma.MaDanhMuc == dm.MaDanhMuc
                                           where dm.TenDanhMuc.Contains(comboDanhMuc.Text.ToString().Trim())
                                           select new
                                           {
                                               MaMonAn = ma.MaMonAn,
                                               TenMonAn = ma.TenMonAn,
                                               GiaTien = ma.GiaTien,
                                               TenDanhMuc = dm.TenDanhMuc
                                           };
        }
    }
}
