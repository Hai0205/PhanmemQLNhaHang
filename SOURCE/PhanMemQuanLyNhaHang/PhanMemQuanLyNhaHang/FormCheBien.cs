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
    public partial class FormCheBien : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public int idNguyenLieu = 0;
        public int idMoTa = 0;
        public FormCheBien()
        {
            InitializeComponent();
            loadComboMonAn();
            loadComboNguyenLieu();
            txtTurnOffNL();
            txtTurnOffCT();
        }

        private void loadComboMonAn()
        {
            var list = from x in db.MONANs
                       select new
                       {
                           MaMonAn = x.MaMonAn,
                           TenMonAn = x.TenMonAn
                       };
            comboMonAn.DataSource = list;
            comboMonAn.DisplayMember = "TenMonAn";
            comboMonAn.ValueMember = "MaMonAn";
        }

        private void loadComboNguyenLieu()
        {
            var list = from x in db.NGUYENLIEUs
                       select new
                       {
                           MaNguyenLieu = x.MaNguyenLieu,
                           TenNguyenLieu = x.TenNguyenLieu
                       };
            comboNguyenLieu.DataSource = list;
            comboNguyenLieu.DisplayMember = "TenNguyenLieu";
            comboNguyenLieu.ValueMember = "MaNguyenLieu";
        }

        private void comboMonAn_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboMonAn.Text.ToString().Trim() != "")
            {
                loadDataNguyenLieu();
                loadDataMoTa();
            }
        }

        public void loadDataNguyenLieu()
        {
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView1.DataSource = from ctm in db.CHITIETMONs
                                            from nl in db.NGUYENLIEUs
                                            from ma in db.MONANs
                                            where ma.MaMonAn == ctm.MaMon
                                            where nl.MaNguyenLieu == ctm.MaNL
                                            where ma.TenMonAn.Contains(comboMonAn.Text.ToString().Trim())
                                            select new
                                            {
                                                MaChiTietMon = ctm.MaChiTietMon,
                                                TenNguyenLieu = nl.TenNguyenLieu,
                                                SoLuong = ctm.SoLuong,
                                                CheBien = nl.CheBien,
                                            };
        }

        public void loadDataMoTa()
        {
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView2.DataSource = from mtm in db.MOTAMONs
                                            from ma in db.MONANs
                                            where ma.MaMonAn == mtm.MaMon
                                            where ma.TenMonAn.Contains(comboMonAn.Text.ToString().Trim())
                                            select new
                                            {
                                                MaMoTa = mtm.MaMon,
                                                ChiTiet = mtm.ChiTiet
                                            };
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
                idNguyenLieu = Int32.Parse(row.Cells[0].Value.ToString());
                comboNguyenLieu.Text = row.Cells[1].Value.ToString();
                txtSoLuong.Text = row.Cells[2].Value.ToString();
            }
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView2.Rows[e.RowIndex];
            idMoTa = Int32.Parse(row.Cells[0].Value.ToString());
            txtNoiDung.Text = row.Cells[1].Value.ToString();
        }

        private void btnLuuNL_Click(object sender, EventArgs e)
        {
            if (btnThemNL.Enabled == false && btnSuaNL.Enabled == true)
            {
                var trungNL = db.CHITIETMONs.Where(a => a.MaMon == Int32.Parse(comboMonAn.SelectedValue.ToString()))
                                            .Where(b => b.MaNL == Int32.Parse(comboNguyenLieu.SelectedValue.ToString()))
                                            .FirstOrDefault();

                if (trungNL != null)
                {
                    MessageBox.Show("Nguyên liệu đã có trong chế biến món!");
                    return;
                }

                CHITIETMON x = new CHITIETMON();
                x.MaMon = Int32.Parse(comboMonAn.SelectedValue.ToString());
                x.MaNL = Int32.Parse(comboNguyenLieu.SelectedValue.ToString());
                x.SoLuong = double.Parse(txtSoLuong.Text.Trim());
                db.CHITIETMONs.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThemNL.Enabled == true && btnSuaNL.Enabled == false)
            {
                CHITIETMON x = db.CHITIETMONs.Where(t => t.MaChiTietMon == idNguyenLieu).FirstOrDefault();
                x.MaMon = Int32.Parse(comboMonAn.SelectedValue.ToString());
                x.MaNL = Int32.Parse(comboNguyenLieu.SelectedValue.ToString());
                x.SoLuong = double.Parse(txtSoLuong.Text.Trim());
                db.SubmitChanges();
            }
            txtTurnOffNL();
            btnThemNL.Enabled = btnSuaNL.Enabled = true;
            btnLuuNL.Enabled = btnBoQuaNL.Enabled = false;
            loadDataNguyenLieu();
        }

        private void txtTurnOnNL()
        {
            comboNguyenLieu.Enabled = txtSoLuong.Enabled = true;
        }

        private void txtTurnOffNL()
        {
            comboNguyenLieu.Enabled = txtSoLuong.Enabled = false;
        }

        private void btnBoQuaNL_Click(object sender, EventArgs e)
        {
            txtTurnOffNL();
            btnThemNL.Enabled = true;
            btnSuaNL.Enabled = true;
            btnLuuNL.Enabled = false;
            btnBoQuaNL.Enabled = false;
        }

        private void btnThemNL_Click(object sender, EventArgs e)
        {
            txtTurnOnNL();
            clearTextBoxNL();
            btnThemNL.Enabled = false;
            btnSuaNL.Enabled = true;
            btnLuuNL.Enabled = btnBoQuaNL.Enabled = true;
        }

        private void clearTextBoxNL()
        {
            comboNguyenLieu.ResetText();
            txtSoLuong.ResetText();
        }

        private void btnSuaNL_Click(object sender, EventArgs e)
        {
            txtTurnOnNL();
            btnSuaNL.Enabled = false;
            btnThemNL.Enabled = true;
            btnLuuNL.Enabled = btnBoQuaNL.Enabled = true;
        }

        private void btnXoaNL_Click(object sender, EventArgs e)
        {
            CHITIETMON x = new CHITIETMON();
            x = db.CHITIETMONs.Where(s => s.MaChiTietMon == idNguyenLieu).Single();
            db.CHITIETMONs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataNguyenLieu();
        }

        private void btnLuuCT_Click(object sender, EventArgs e)
        {
            if (btnThemCT.Enabled == false && btnSuaCT.Enabled == true)
            {
                MOTAMON x = new MOTAMON();
                x.MaMon = Int32.Parse(comboMonAn.SelectedValue.ToString());
                x.ChiTiet = txtNoiDung.Text;
                db.MOTAMONs.InsertOnSubmit(x);
                db.SubmitChanges();
            }

            else if (btnThemCT.Enabled == true && btnSuaCT.Enabled == false)
            {
                MOTAMON x = db.MOTAMONs.Where(t => t.MaMoTa == idMoTa).FirstOrDefault();
                x.ChiTiet = txtNoiDung.Text;
                db.SubmitChanges();
            }
            txtTurnOffCT();
            btnThemCT.Enabled = btnSuaCT.Enabled = true;
            btnLuuCT.Enabled = btnBoQuaCT.Enabled = false;
            loadDataMoTa();
        }

        private void txtTurnOnCT()
        {
            txtNoiDung.Enabled = true;
        }

        private void txtTurnOffCT()
        {
            txtNoiDung.Enabled = false;
        }

        private void btnBoQuaCT_Click(object sender, EventArgs e)
        {
            txtTurnOffCT();
            btnThemCT.Enabled = true;
            btnSuaCT.Enabled = true;
            btnLuuCT.Enabled = false;
            btnBoQuaCT.Enabled = false;
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            txtTurnOnCT();
            clearTextBoxCT();
            btnThemCT.Enabled = false;
            btnSuaCT.Enabled = true;
            btnLuuCT.Enabled = btnBoQuaCT.Enabled = true;
        }

        private void clearTextBoxCT()
        {
            txtNoiDung.ResetText();
        }

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            txtTurnOnCT();
            btnSuaCT.Enabled = false;
            btnThemCT.Enabled = true;
            btnLuuCT.Enabled = btnBoQuaCT.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            MOTAMON x = new MOTAMON();
            x = db.MOTAMONs.Where(s => s.MaMoTa == idMoTa).Single();
            db.MOTAMONs.DeleteOnSubmit(x);
            db.SubmitChanges();
            loadDataMoTa();
        }
    }
}
