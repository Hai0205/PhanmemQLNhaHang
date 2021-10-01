using PhanMemQuanLyNhaHang.XuLy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyNhaHang
{
    public partial class FormPhucVuKhachHang : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public static string maQR = "";
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }

        public FormPhucVuKhachHang(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            loadTable();
            LoadCategory();
            LoadComboboxTable(cbo_ban);
            LoadComboboxKhachHang(cboKhachHang);
        }

        void ChangeAccount(Account acc)
        {
            cboNhanVien.Text = LoginAccount.TenNV;
        }

        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbo_danhMuc.DataSource = listCategory;
            cbo_danhMuc.DisplayMember = "TenDanhMuc";
        }

        public void LoadFoodListByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbo_MonAn.DataSource = listFood;
            cbo_MonAn.DisplayMember = "TenMonAn";
        }

        public void loadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tableList = TableDAO.Instance.loadBanList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = "         "+item.TenBan + Environment.NewLine + item.TrangThai;
                btn.Click += btn_Click;
                btn.Tag = item;
                switch (item.TrangThai)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }

        float giaTriHoaDon = 0;
        public void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<PhanMemQuanLyNhaHang.XuLy.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalPrice = 0;
            foreach (PhanMemQuanLyNhaHang.XuLy.Menu item in listBillInfo)
            {
                ListViewItem lstItem = new ListViewItem(item.TenMonAn.ToString());
                lstItem.SubItems.Add(item.SoLuong.ToString());
                lstItem.SubItems.Add(item.Gia.ToString());
                lstItem.SubItems.Add(item.ThanhTien.ToString());
                totalPrice += item.ThanhTien;
                lsvBill.Items.Add(lstItem);
            }

            //cải tiến: ngoài sử dụng mã QR khi thanh toán để được giảm giá 
            //thì bàn đó chỉ cần có giá trị trên 2.000.000 VND sẽ tự động được giảm 10%
            if (totalPrice >= 2000000)
                //totalPrice *= (float)0.8;
                 nmDisCount.Value = 10;
            else
                nmDisCount.Value = 0;
            giaTriHoaDon = totalPrice;
            CultureInfo culture = new CultureInfo("vi-VN");
            txt_tongTien.Text = totalPrice.ToString("c", culture);

            
        }

        void LoadComboboxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.loadBanList();
            cb.DisplayMember = "TenBan";
        }

        //Load comboKhachHang
        void LoadComboboxKhachHang(ComboBox cb)
        {
            cb.DataSource = KhachHangDAO.Instance.loadKhachList();
            cb.DisplayMember = "TenKH";
        }

        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).MaBan;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
            lblChonBan.Text = "Bạn đang chọn bàn: " + tableID;

            //chọn bàn hiển thị nhân viên và khách hàng
            string tinhTrangBan = db.BANs.Where(t => t.MaBan == tableID).FirstOrDefault().TrangThai.ToString().Trim();
            if (tinhTrangBan.ToString().Trim() == "Có người")
            {
                int maHoaDon = db.HOADONs.Where(a => a.MaBan == tableID).Where(b => b.TrangThai == 0).FirstOrDefault().MaHoaDon;
                int maNV = db.HOADONs.Where(c => c.MaHoaDon == maHoaDon).FirstOrDefault().MaNV;
                int maKH = db.HOADONs.Where(d => d.MaHoaDon == maHoaDon).FirstOrDefault().MaKH;
                string tenNV = db.NHANVIENs.Where(g => g.MaNV == maNV).FirstOrDefault().TenNV;
                string tenKH = db.KHACHHANGs.Where(f => f.MaKH == maKH).FirstOrDefault().TenKH;
                cboNhanVien.Text = tenNV.ToString().Trim();
                cboKhachHang.Text = tenKH.ToString().Trim();
            }
        }

        private void cbo_danhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
                return;
            Category selected = cb.SelectedItem as Category;
            id = selected.MaDanhMuc;
            LoadFoodListByCategoryID(id);
        }

        private void btn_ThemMon_Click(object sender, EventArgs e)
        {
            int maKH, maNV;
            maKH = (int)DataProvider.Instance.ExecuteScalar("exec USP_GetIDKhachHang @tenKH", new object[] { cboKhachHang.Text.Trim() });
            maNV = (int)DataProvider.Instance.ExecuteScalar("exec USP_GetIDNhanVien @tenNV", new object[] { cboNhanVien.Text.Trim() });
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn trước khi thêm món");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillByTableID(table.MaBan);
            int foodID = (cbo_MonAn.SelectedItem as Food).MaMonAn;
            int count = (int)nmFoodCount.Value;

            //check món có quá số lượng
            var checkFood = db.COTHENAUs.Where(a => a.MaMon == foodID).FirstOrDefault();
            if (checkFood == null)
            {
                MessageBox.Show("Món chưa được chế biến !");
                return;
            }
            if (checkFood.SoLuongMon < count)
            {
                MessageBox.Show("Số lượng món bạn đặt vượt quá số lượng khả dụng của nhà hàng!");
                return;
            }

            //thay đổi số lượng nguyên liệu
            var nlCTM = db.CHITIETMONs.Where(b => b.MaMon == foodID).ToList();
            foreach(var dataNL_CTM in nlCTM)
            {
                NGUYENLIEU x = db.NGUYENLIEUs.Where(t => t.MaNguyenLieu == dataNL_CTM.MaNL).FirstOrDefault();
                x.SoLuong = x.SoLuong - (dataNL_CTM.SoLuong * count);
                db.SubmitChanges();
            }

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.MaBan, maKH, maNV);
                BillInfoDao.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDao.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.MaBan);
            loadTable();
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUncheckBillByTableID(table.MaBan);
            int discount = (int)nmDisCount.Value;

            double totalPrice = Convert.ToDouble(txt_tongTien.Text.Split(',')[0].Replace(".", ""));
            double finalTotalPrice = totalPrice - (totalPrice / 100) * discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(string.Format("Bạn có chắc chắn muốn thanh toán hóa đơn cho bàn {0}\n Tổng tiền - (Tổng tiền / 100) x Giảm giá\n => {1} - ({1} / 100) x {2} = {3}", table.TenBan, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, (float)finalTotalPrice);
                    ShowBill(table.MaBan);
                    loadTable();
                    //insert xài mã
                    if(nmDisCount.Value != 0)
                    {
                        int idKH = db.HOADONs.Where(t => t.MaHoaDon == idBill).FirstOrDefault().MaKH;
                        int idKM = db.MAKHUYENMAIs.Where(t => t.MaKhuyenMai1 == maQR).FirstOrDefault().ID;
                        LICHSUKHUYENMAI x = new LICHSUKHUYENMAI();
                        x.MaKH = idKH;
                        x.MaKM = idKM;
                        x.NgaySuDung = DateTime.Today;
                        db.LICHSUKHUYENMAIs.InsertOnSubmit(x);
                        db.SubmitChanges();
                    }
                    //reset giảm giá
                    nmDisCount.Value = 0;
                    maQR = "";
                    //in hóa đơn
                   // new frm_XemIn(idBill).Show();
                }
            }
        }

        private void btn_ChuyenBan_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).MaBan;
            string ten1 = (lsvBill.Tag as Table).TenBan.ToString().Trim();
            int id2 = (cbo_ban.SelectedItem as Table).MaBan;
            string ten2 = (cbo_ban.SelectedItem as Table).TenBan.ToString().Trim();

            if (MessageBox.Show(string.Format("Bạn có muốn chuyển bàn {0} qua bàn {1}", ten1, ten2), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);
                loadTable();
            }
        }

        private void btnDoiTienTe_Click(object sender, EventArgs e)
        {
            FormTienTe frm = new FormTienTe();
            frm.ShowDialog();
        }

        private void resetData()
        {
            db.COTHENAUs.DeleteAllOnSubmit(db.COTHENAUs);
            db.SubmitChanges();

            var listMonAn = from ctm in db.CHITIETMONs
                            group ctm by ctm.MaMon;

            foreach (var dataMonAn in listMonAn)
            {
                var listNL = db.CHITIETMONs.Where(a => a.MaMon == dataMonAn.Key);
                int min = 9000000;
                foreach (var dataNL in listNL)
                {
                    float soLuongYeuCau = float.Parse(dataNL.SoLuong.ToString());
                    float soLuongTon = float.Parse(db.NGUYENLIEUs.Where(b => b.MaNguyenLieu == dataNL.MaNL).FirstOrDefault().SoLuong.ToString());

                    int soLuongNau = (int)(soLuongTon / soLuongYeuCau);
                    if (min > soLuongNau)
                        min = soLuongNau;
                }

                COTHENAU x = new COTHENAU();
                x.MaMon = Int32.Parse(dataMonAn.Key.ToString());
                x.SoLuongMon = min;
                db.COTHENAUs.InsertOnSubmit(x);
                db.SubmitChanges();
            }
        }

        private void btnSoMon_Click(object sender, EventArgs e)
        {
            //reset
            resetData();
            FormSoMon frm = new FormSoMon();
            frm.ShowDialog();
        }
        
        private void btnQuetMa_Click(object sender, EventArgs e)
        {
            FormQuetMa frm = new FormQuetMa();
            frm.ShowDialog();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            //kiểm tra không xài được
            var x = db.MAKHUYENMAIs.Where(t => t.MaKhuyenMai1 == maQR).FirstOrDefault();
            if(x == null || x.NgayHetHan < DateTime.Today)
            {
                MessageBox.Show("Mã khuyến mãi không thể sử dụng được !");
                return;
            }

            //kiểm tra khách hàng đã xài quá số lần chưa
            int maKH = db.KHACHHANGs.Where(kh => kh.TenKH == cboKhachHang.Text.ToString().Trim()).FirstOrDefault().MaKH;
            var duLieuDemSoLan = from ls in db.LICHSUKHUYENMAIs
                                 where ls.MaKH == maKH
                                 where ls.MaKM == x.ID
                                 group ls by ls.MaKH;
            int dem = 0;
            foreach(var data in duLieuDemSoLan)
            {
                foreach(var demSL in data)
                {
                    dem++;
                }
            }

            if (dem >= x.GioiHanSuDung)
            {
                MessageBox.Show("Quý khách đã vượt quá lần sử dụng mã khuyến mãi này !");
                return;
            }

            //kiểm tra giá trị hóa đơn
            if (giaTriHoaDon < Double.Parse(x.HoaDonToiThieu.ToString().Trim()))
            {
                MessageBox.Show("Hóa đơn không đủ điều kiện (Yêu cầu: tối thiểu: "+x.HoaDonToiThieu.ToString().Trim()+"đ - giảm giá: "+x.GiamGia.ToString().Trim()+"%");
                return;
            }

            MessageBox.Show("Thành công! Mã khuyến mãi: "+x.MaKhuyenMai1.ToString().Trim()+" giảm giá: "+x.GiamGia.ToString().Trim()+"% cho hóa đơn!");
            nmDisCount.Value = x.GiamGia.Value;
        }

        private void btnThemKhach_Click(object sender, EventArgs e)
        {
            var ktTrung = db.KHACHHANGs.Where(b => b.TenKH == cboKhachHang.Text.Trim()).FirstOrDefault();
            if (ktTrung != null)
            {
                MessageBox.Show("Tên đã trùng bạn nên kèm theo SĐT kế bên tên !");
                return;
            }

            int soNgauNhien;
            Random rd = new Random();
            soNgauNhien = rd.Next(10000, 100000000);
            string fname = soNgauNhien.ToString();

            KHACHHANG kh = new KHACHHANG();
            kh.TenKH = cboKhachHang.Text.Trim();
            kh.TenDangNhap = "TK"+fname.ToString().Trim();
            kh.MatKhau = "MK" + fname.ToString().Trim();
            kh.Email = "Chưa nhập";
            kh.NgaySinh = DateTime.Today;
            kh.GioiTinh = "Nam";
            kh.CMND = "Chưa nhập";
            kh.DiaChi = "Chưa nhập";
            kh.SoDT = "Chưa nhập";
            kh.Hinh = " ";
            db.KHACHHANGs.InsertOnSubmit(kh);
            db.SubmitChanges();
            LoadComboboxKhachHang(cboKhachHang);
            MessageBox.Show("Thêm khách hàng thành công !");
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            int maKH, maNV;
            maKH = (int)DataProvider.Instance.ExecuteScalar("exec USP_GetIDKhachHang @tenKH", new object[] { cboKhachHang.Text.Trim() });
            maNV = (int)DataProvider.Instance.ExecuteScalar("exec USP_GetIDNhanVien @tenNV", new object[] { cboNhanVien.Text.Trim() });
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Hãy chọn bàn trước khi xoá món");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillByTableID(table.MaBan);
            int foodID = (cbo_MonAn.SelectedItem as Food).MaMonAn;
            int count = (int)nmFoodCount.Value * -1;

            //check món có quá số lượng
            var checkFood = db.COTHENAUs.Where(a => a.MaMon == foodID).FirstOrDefault();
            if (checkFood == null)
            {
                MessageBox.Show("Món chưa được chế biến !");
                return;
            }
            if (checkFood.SoLuongMon < count)
            {
                MessageBox.Show("Số lượng món bạn đặt vượt quá số lượng khả dụng của nhà hàng!");
                return;
            }

            //thay đổi số lượng nguyên liệu
            var nlCTM = db.CHITIETMONs.Where(b => b.MaMon == foodID).ToList();
            foreach (var dataNL_CTM in nlCTM)
            {
                NGUYENLIEU x = db.NGUYENLIEUs.Where(t => t.MaNguyenLieu == dataNL_CTM.MaNL).FirstOrDefault();
                x.SoLuong = x.SoLuong - (dataNL_CTM.SoLuong * count);
                db.SubmitChanges();
            }

            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.MaBan, maKH, maNV);
                BillInfoDao.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count);
            }
            else
            {
                BillInfoDao.Instance.InsertBillInfo(idBill, foodID, count);
            }
            ShowBill(table.MaBan);
            loadTable();
        }
    }
}