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
    public partial class FormBan : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idBan = 0;
        public FormBan()
        {
            InitializeComponent();
            loadDataGridView();
            txtTurnOff();
        }

        public void loadDataGridView()
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from b in db.BANs
                                           select new
                                           {
                                               MaBan = b.MaBan,
                                               Ten = b.Ten,
                                               TrangThai = b.TrangThai
                                           };
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.gunaDataGridView1.Rows[e.RowIndex];
                idBan = Int32.Parse(row.Cells[0].Value.ToString());
                txt_tenBan.Text = row.Cells[1].Value.ToString();
                txt_trangThai.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                var ktTrung = db.BANs.Where(a => a.Ten == txt_tenBan.Text.Trim()).FirstOrDefault();
                if (ktTrung != null)
                {
                    MessageBox.Show("Bàn này đã tồn tại !");
                    return;
                }

                BAN x = new BAN();
                x.Ten = txt_tenBan.Text;
                x.TrangThai = "Trống";
                db.BANs.InsertOnSubmit(x);
                db.SubmitChanges();
            }
            txtTurnOff();
            btnThem.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = false;
            loadDataGridView();
        }

        private void txtTurnOn()
        {
            txt_tenBan.Enabled = true;
        }

        private void txtTurnOff()
        {
            txt_tenBan.Enabled = txt_trangThai.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            txtTurnOff();
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTurnOn();
            clearTextBox();
            btnThem.Enabled = false;
            btnLuu.Enabled = btnBoQua.Enabled = true;
        }

        private void clearTextBox()
        {
            txt_tenBan.ResetText();
            txt_trangThai.ResetText();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtTurnOn();
            btnThem.Enabled = true;
            btnLuu.Enabled = btnBoQua.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            BAN x = new BAN();
            x = db.BANs.Where(s => s.MaBan == idBan).Single();
            db.BANs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataGridView();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            var list = from b in db.BANs
                       where b.Ten.Contains(txtTim.Text)
                       select new
                       {
                           MaBan = b.MaBan,
                           Ten = b.Ten,
                           TrangThai = b.TrangThai
                       };
            gunaDataGridView1.DataSource = list;
        }
    }
}
