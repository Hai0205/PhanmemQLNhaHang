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
    public partial class FormNhaSanXuat : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idNSX = 0;
        public FormNhaSanXuat()
        {
            InitializeComponent();
            loadDataGridView();
            txtTurnOff();
        }

        public void loadDataGridView()
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from nsx in db.NHASANXUATs
                                           select new
                                           {
                                               MaNSX = nsx.MaNSX,
                                               TenNSX = nsx.TenNSX,
                                               DiaChi = nsx.DiaChi,
                                               SoDT = nsx.SoDT,
                                               Email = nsx.Email
                                           };
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idNSX = Int32.Parse(row.Cells[0].Value.ToString());
                txt_tenNSX.Text = row.Cells[1].Value.ToString();
                txt_diaChi.Text = row.Cells[2].Value.ToString();
                txt_sdt.Text = row.Cells[3].Value.ToString();
                txt_email.Text = row.Cells[4].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false && btnSua.Enabled == true)
            {
                var ktTrung = db.NHASANXUATs.Where(a => a.TenNSX == txt_tenNSX.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Nhà sản xuất này đã tồn tại !");
                    return;
                }

                NHASANXUAT x = new NHASANXUAT();
                x.TenNSX = txt_tenNSX.Text;
                x.DiaChi = txt_diaChi.Text;
                x.SoDT = txt_sdt.Text;
                x.Email = txt_email.Text;
                db.NHASANXUATs.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThem.Enabled == true && btnSua.Enabled == false)
            {
                NHASANXUAT x = db.NHASANXUATs.Where(t => t.MaNSX == idNSX).FirstOrDefault();
                x.TenNSX = txt_tenNSX.Text;
                x.DiaChi = txt_diaChi.Text;
                x.SoDT = txt_sdt.Text;
                x.Email = txt_email.Text;
                db.SubmitChanges();
            }
            txtTurnOff();
            btnThem.Enabled = btnSua.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = false;
            loadDataGridView();
        }

        private void txtTurnOn()
        {
            txt_tenNSX.Enabled = txt_email.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = true;
        }

        private void txtTurnOff()
        {
            txt_tenNSX.Enabled = txt_email.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtTurnOff();
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTurnOn();
            clearTextBox();
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = true;
        }

        private void clearTextBox()
        {
            txt_tenNSX.ResetText();
            txt_email.ResetText();
            txt_diaChi.ResetText();
            txt_sdt.ResetText();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTurnOn();
            btnSua.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = true;
            txt_tenNSX.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            NHASANXUAT x = new NHASANXUAT();
            x = db.NHASANXUATs.Where(s => s.MaNSX == idNSX).Single();
            db.NHASANXUATs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataGridView();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            var list = from nsx in db.NHASANXUATs
                       where nsx.TenNSX.Contains(txtTimKiem.Text)
                       select new
                       {
                           MaNSX = nsx.MaNSX,
                           TenNSX = nsx.TenNSX,
                           DiaChi = nsx.DiaChi,
                           SoDT = nsx.SoDT,
                           Email = nsx.Email
                       };
            gunaDataGridView1.DataSource = list;
        }
    }
}
