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
    public partial class FormTrangChu : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.MaQuyen); }
        }

        void ChangeAccount(int type)
        {
            lb_idUser.Text = "Xin chào: " + LoginAccount.TenNV;
            loadPhanQuyen(type);
            //if(type == 2)
            //{
            //    btnQuanLyThongTin.Visible = btnQuanLyMonAn.Visible = btnQuanLyKho.Visible = btnDoanhThu.Visible = false;
            //}
            //else if (type == 3)
            //{
            //    btnQuanLyThongTin.Visible = btnQuanLyMonAn.Visible = btnPhucVuKhachHang.Visible = btnDoanhThu.Visible = false;
            //}
        }

        private void loadPhanQuyen(int id)
        {
            var list = from pq in db.PHANQUYENs
                       from mh in db.MANHINHs
                       where pq.MaMH == mh.ID
                       where pq.MaQuyen == id
                       select new
                       {
                           MaMH = mh.MaManHinh
                       };

            if(list != null)
            {
                string maMH_KT = "";
                foreach (Control item in panelSideMenu.Controls)
                {
                    if(item is Button)
                    {
                        maMH_KT = item.Tag.ToString();
                        foreach (var dataKT in list)
                            if (dataKT.MaMH.ToString().Trim() == maMH_KT.ToString().Trim())
                                item.Visible = true;
                    }
                }
            }
        }

        public FormTrangChu(Account acc)
        {
            InitializeComponent();
            customizeDesing();
            this.LoginAccount = acc;
            lbl_ngayThang.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void customizeDesing()
        {
            panelQLThongTinSubMenu.Visible = false;
            panelQLMonAnSubMenu.Visible = false;
            panelQLKhoSubMenu.Visible = false;
            panelDoanhThuSubMenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panelQLThongTinSubMenu.Visible == true)
                panelQLThongTinSubMenu.Visible = false;
            if (panelQLMonAnSubMenu.Visible == true)
                panelQLMonAnSubMenu.Visible = false;
            if (panelQLKhoSubMenu.Visible == true)
                panelQLKhoSubMenu.Visible = false;
            if (panelDoanhThuSubMenu.Visible == true)
                panelDoanhThuSubMenu.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnQLThongTin_Click(object sender, EventArgs e)
        {
            showSubMenu(panelQLThongTinSubMenu);
        }

        #region QLThongTin
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            openChildForm(new FormNhanVien());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new FormKhachHang());
        }

        private void btnNhaCungCap_Click(object sender, EventArgs e)
        {
            openChildForm(new FormNhaCungCap());
        }

        private void btnNhaSanXuat_Click(object sender, EventArgs e)
        {
            openChildForm(new FormNhaSanXuat());
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            openChildForm(new FormBan());
        }

        #endregion

        private void btnQuanLyMonAn_Click(object sender, EventArgs e)
        {
            showSubMenu(panelQLMonAnSubMenu);
        }

        private void btnPhucVuKhachHang_Click(object sender, EventArgs e)
        {
            openChildForm(new FormPhucVuKhachHang(loginAccount));
        }

        private void btnQuanLyKho_Click(object sender, EventArgs e)
        {
            showSubMenu(panelQLKhoSubMenu);
        }

        private void btnNguyenLieu_Click(object sender, EventArgs e)
        {
            openChildForm(new FormNguyenLieu());
        }

        private void btnDoanhThu_Click(object sender, EventArgs e)
        {
            showSubMenu(panelDoanhThuSubMenu);
        }

        private void btnThucDon_Click(object sender, EventArgs e)
        {
            openChildForm(new FormThucDon());
        }

        private void btnCheBien_Click(object sender, EventArgs e)
        {
            openChildForm(new FormCheBien());
        }

        private void btnDonGia_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDonGia());
        }

        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            openChildForm(new FormTaoDon(loginAccount));
        }

        private void btnDonDatHang_Click(object sender, EventArgs e)
        {
            openChildForm(new FormDonDatHang());
        }

        private void btnPhieuNhapHang_Click(object sender, EventArgs e)
        {
            openChildForm(new FormPhieuNhapHang());
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            openChildForm(new FormHoaDon());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            openChildForm(new FormBaoCaoThongKe());
        }

        private void btnTaoPhieu_Click(object sender, EventArgs e)
        {
            openChildForm(new FormTaoPhieu(loginAccount));
        }

        private void btnChucVu_Click(object sender, EventArgs e)
        {
            openChildForm(new FormChucVu());
        }

        private void btnChamCong_Click(object sender, EventArgs e)
        {
            openChildForm(new FormChamCong());
        }

        private void btnMaKhuyenMai_Click(object sender, EventArgs e)
        {
            openChildForm(new FormKhuyenMai());
        }

        private void btnPhanQuyen_Click(object sender, EventArgs e)
        {
            openChildForm(new FormPhanQuyen());
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_gio.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                this.Hide();
                FormLogin frmLogin = new FormLogin();
                frmLogin.ShowDialog();
                this.Close();
            }
        }

        private void btnKhoaManHinh_Click(object sender, EventArgs e)
        {
            FormKhoa addF = new FormKhoa("admin", loginAccount.TenNV, loginAccount.MatKhau);
            addF.ShowDialog();
            this.Show();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau frmcn = new FormDoiMatKhau(loginAccount);
            frmcn.ShowDialog();
        }
    }
}
