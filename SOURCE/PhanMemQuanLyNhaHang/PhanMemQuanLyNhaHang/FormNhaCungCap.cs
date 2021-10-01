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
    public partial class FormNhaCungCap : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idNCC = 0;
        public FormNhaCungCap()
        {
            InitializeComponent();
            loadDataGridView();
            txtTurnOff();
        }

        public void loadDataGridView()
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from ncc in db.NHACUNGCAPs
                                           select new
                                           {
                                               MaNCC = ncc.MaNCC,
                                               TenNCC = ncc.TenNCC,
                                               DiaChi = ncc.DiaChi,
                                               SoDT = ncc.SoDT,
                                               Email = ncc.Email
                                           };
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idNCC = Int32.Parse(row.Cells[0].Value.ToString());
                txt_tenNCC.Text = row.Cells[1].Value.ToString();
                txt_diaChi.Text = row.Cells[2].Value.ToString();
                txt_sdt.Text = row.Cells[3].Value.ToString();
                txt_email.Text = row.Cells[4].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false && btnSua.Enabled == true)
            {
                var ktTrung = db.NHACUNGCAPs.Where(a => a.TenNCC == txt_tenNCC.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Nhà cung cấp này đã tồn tại !");
                    return;
                }

                NHACUNGCAP x = new NHACUNGCAP();
                x.TenNCC = txt_tenNCC.Text;
                x.DiaChi = txt_diaChi.Text;
                x.SoDT = txt_sdt.Text;
                x.Email = txt_email.Text;
                db.NHACUNGCAPs.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThem.Enabled == true && btnSua.Enabled == false)
            {
                NHACUNGCAP x = db.NHACUNGCAPs.Where(t => t.MaNCC == idNCC).FirstOrDefault();
                x.TenNCC = txt_tenNCC.Text;
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
            txt_tenNCC.Enabled = txt_email.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = true;
        }

        private void txtTurnOff()
        {
            txt_tenNCC.Enabled = txt_email.Enabled = txt_diaChi.Enabled = txt_sdt.Enabled = false;
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
            txt_tenNCC.ResetText();
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
            txt_tenNCC.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            NHACUNGCAP x = new NHACUNGCAP();
            x = db.NHACUNGCAPs.Where(s => s.MaNCC == idNCC).Single();
            db.NHACUNGCAPs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataGridView();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            var list = from ncc in db.NHACUNGCAPs
                       where ncc.TenNCC.Contains(txtTimKiem.Text)
                       select new
                       {
                           MaNCC = ncc.MaNCC,
                           TenNCC = ncc.TenNCC,
                           DiaChi = ncc.DiaChi,
                           SoDT = ncc.SoDT,
                           Email = ncc.Email
                       };
            gunaDataGridView1.DataSource = list;
        }
    }
}
