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
    public partial class FormPhanQuyen : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idManHinh = 0;
        public int idManHinhDuocChon = 0;
        public int idPhanQuyen = 0;
        public int idQuyen = 0;
        public FormPhanQuyen()
        {
            InitializeComponent();
            loadComboQuyen();
        }

        private void loadComboQuyen()
        {
            var list = from x in db.QUYENs
                       select new
                       {
                           MaQuyen = x.MaQuyen,
                           TenQuyen = x.TenQuyen
                       };
            comboQuyen.DataSource = list;
            comboQuyen.DisplayMember = "TenQuyen";
            comboQuyen.ValueMember = "MaQuyen";
        }

        private void loadDataManHinh()
        {
            guna2DataGridView1.Rows.Clear();
            var list = from x in db.MANHINHs
                       select new
                       {
                           ID = x.ID,
                           MaManHinh = x.MaManHinh,
                           TenManHinh = x.TenManHinh
                       };
            guna2DataGridView1.DataSource = list;
        }

        private void loadDataManHinhNguoiDung(int id)
        {
            guna2DataGridView2.Rows.Clear();
            var list = from pq in db.PHANQUYENs
                       from mh  in db.MANHINHs
                       where pq.MaQuyen == id
                       where mh.ID == pq.MaMH
                       select new
                       {
                           ID = pq.MaPhanQuyen,
                           MaManHinh = pq.MaMH,
                           TenManHinh = mh.TenManHinh
                       };
            guna2DataGridView2.DataSource = list;
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            guna2GroupBox3.Enabled = true;
            guna2GroupBox2.Enabled = true;
            guna2GroupBox1.Enabled = false;
            idQuyen = Int32.Parse(comboQuyen.SelectedValue.ToString());
            loadDataManHinh();
            loadDataManHinhNguoiDung(idQuyen);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var ktTRUNG = from a in db.PHANQUYENs
                          where a.MaQuyen == idQuyen
                          where a.MaMH == idManHinh
                          select new
                          {
                              MaPhanQuyen = a.MaPhanQuyen
                          };
            foreach(var kt in ktTRUNG)
            {
                if(kt.MaPhanQuyen.ToString().Trim() != null)
                {
                    MessageBox.Show("Người dùng đã thêm màn hình này rồi !");
                    return;
                }
            }

            PHANQUYEN x = new PHANQUYEN();
            x.MaQuyen = Int32.Parse(comboQuyen.SelectedValue.ToString());
            x.MaMH = idManHinh;
            db.PHANQUYENs.InsertOnSubmit(x);
            db.SubmitChanges();
            loadDataManHinhNguoiDung(Int32.Parse(comboQuyen.SelectedValue.ToString()));
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                idManHinh = Int32.Parse(row.Cells[0].Value.ToString());
                txtManHinhDS.Text = row.Cells[2].Value.ToString();
            }
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView2.Rows[e.RowIndex];
                idPhanQuyen = Int32.Parse(row.Cells[0].Value.ToString());
                txtManHinhSuDung.Text = row.Cells[2].Value.ToString();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            PHANQUYEN x = new PHANQUYEN();
            x = db.PHANQUYENs.Where(s => s.MaPhanQuyen == idPhanQuyen).Single();
            db.PHANQUYENs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataManHinhNguoiDung(Int32.Parse(comboQuyen.SelectedValue.ToString()));
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHoanTat_Click(object sender, EventArgs e)
        {
            guna2GroupBox3.Enabled = false;
            guna2GroupBox2.Enabled = false;
            guna2GroupBox1.Enabled = true;
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView2.Rows.Clear();
            txtManHinhDS.ResetText();
            txtManHinhSuDung.ResetText();
        }

        private void btnThemQuyen_Click(object sender, EventArgs e)
        {
            var ktTrung = db.QUYENs.Where(b => b.TenQuyen == comboQuyen.Text.Trim()).FirstOrDefault();
            if (ktTrung != null)
            {
                MessageBox.Show("Quyền này đã tồn tại, không thể thêm !");
                return;
            }

            QUYEN q = new QUYEN();
            q.TenQuyen = comboQuyen.Text.Trim();
            db.QUYENs.InsertOnSubmit(q);
            db.SubmitChanges();
            loadComboQuyen();
            MessageBox.Show("Thêm quyền thành công !");
        }

        private void btnXoaQuyen_Click(object sender, EventArgs e)
        {
            QUYEN x = new QUYEN();
            x = db.QUYENs.Where(s => s.TenQuyen == comboQuyen.Text.Trim()).Single();
            db.QUYENs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadComboQuyen();
            MessageBox.Show("Xóa quyền thành công !");
        }
    }
}
