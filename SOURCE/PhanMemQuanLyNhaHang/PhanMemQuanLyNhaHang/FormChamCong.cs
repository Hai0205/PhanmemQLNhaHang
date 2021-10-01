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
    public partial class FormChamCong : Form
    {
        DataNhaHangDataContext db = new DataNhaHangDataContext();
        public FormChamCong()
        {
            InitializeComponent();
            loadComboNV();
            loadDataDiemDanh();
        }

        private void loadComboNV()
        {
            var list = from x in db.NHANVIENs
                       select new
                       {
                           MaNV = x.MaNV,
                           TenNV = x.TenNV
                       };
            comboNV.DataSource = list;
            comboNV.DisplayMember = "TenNV";
            comboNV.ValueMember = "MaNV";
        }

        private void loadDataDiemDanh()
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from dd in db.DIEMDANHs
                                           from nv in db.NHANVIENs
                                           where nv.MaNV == dd.MaNV
                                           where dd.NgayDiemDanh == DateTime.Today
                                           select new
                                           {
                                               MaDiemDanh = dd.MaDiemDanh,
                                               MaNV = nv.TenNV
                                           };
        }

        private void btnDiemDanh_Click(object sender, EventArgs e)
        {
            int idKiemTra = 0;
            var ketQua = from dd in db.DIEMDANHs
                         where dd.MaNV == Int32.Parse(comboNV.SelectedValue.ToString())
                         where dd.NgayDiemDanh == DateTime.Today
                         select new
                         {
                             ID = dd.MaDiemDanh
                         };
            foreach (var data in ketQua)
            {
                idKiemTra = data.ID;
            }

            if (idKiemTra != 0)
            {
                MessageBox.Show("Nhân viên " + comboNV.Text.Trim() + " đã điểm danh hôm nay rồi!");
                return;
            }

            DIEMDANH x = new DIEMDANH();
            x.MaNV = Int32.Parse(comboNV.SelectedValue.ToString());
            x.NgayDiemDanh = DateTime.Today;
            db.DIEMDANHs.InsertOnSubmit(x);
            db.SubmitChanges();
            MessageBox.Show("Nhân viên " + comboNV.Text.Trim() + " đã điểm danh thành công!");

            loadDataDiemDanh();
        }

        private void btnLocDD_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from dd in db.DIEMDANHs
                                           from nv in db.NHANVIENs
                                           where nv.MaNV == dd.MaNV
                                           where dd.NgayDiemDanh == txtDiemDanh.Value
                                           select new
                                           {
                                               MaDiemDanh = dd.MaDiemDanh,
                                               MaNV = nv.TenNV
                                           };
        }

        private void btnTimDD_Click(object sender, EventArgs e)
        {
            gunaDataGridView1.Rows.Clear();
            gunaDataGridView1.DataSource = from dd in db.DIEMDANHs
                                           from nv in db.NHANVIENs
                                           where nv.MaNV == dd.MaNV
                                           where nv.TenNV.Contains(txtTimDD.Text.Trim())
                                           select new
                                           {
                                               MaDiemDanh = dd.MaDiemDanh,
                                               MaNV = nv.TenNV
                                           };
        }

        private bool namNhuan(int nam)
        {
            if ((nam % 4 == 0 && nam % 100 != 0) || nam % 400 == 0)
                return true;
            return false;
        }

        private int soNgay(int thang, int nam)
        {
            switch(thang)
            {
                case 1: case 3: case 5: case 7: case 8: case 10: case 12: 
                    return 31;
                case 4: case 6: case 9: case 11:
                    return 30;
                case 2:
                    if (namNhuan(nam))
                        return 29;
                    else
                        return 28;
            }
            return 0;
        }

        private void btnLocThang_Click(object sender, EventArgs e)
        {
            int ngay = 0;
            int thang = txtThangLuong.Value.Month;
            int nam = txtThangLuong.Value.Year;
            ngay = soNgay(thang, nam);

            //delete
            var dataCu = from cc in db.CHAMCONGs
                         where cc.Thang == thang
                         where cc.Nam == nam
                         select new
                         {
                             ID = cc.MaChamCong
                         };
            foreach (var dataXoa in dataCu)
            {
                CHAMCONG x = new CHAMCONG();
                x = db.CHAMCONGs.Where(s => s.MaChamCong == dataXoa.ID).Single();
                db.CHAMCONGs.DeleteOnSubmit(x);
                db.SubmitChanges();
            }

            //insert

            DateTime dauThang = DateTime.Parse(nam+"-"+thang+"-01");
            DateTime cuoiThang = DateTime.Parse(nam + "-" + thang + "-" + ngay);

            var ketqua1 = from dd in db.DIEMDANHs
                           where dd.NgayDiemDanh >= dauThang
                           where dd.NgayDiemDanh <= cuoiThang
                           group dd by dd.MaNV;

            foreach(var group1 in ketqua1)
            {
                foreach(var data1 in group1)
                {
                    var ketqua2 = from dd in db.DIEMDANHs
                                        where dd.NgayDiemDanh >= dauThang
                                        where dd.NgayDiemDanh <= cuoiThang
                                        where dd.MaNV == data1.MaNV
                                        select new
                                        {
                                            MaDiemDanh = dd.MaDiemDanh
                                        };

                    //so ngay lam
                    int soNgayLam = 0;
                    foreach(var data2 in ketqua2)
                    {
                        soNgayLam++;
                    }

                    //luong co ban
                    var ketqua3 = from cv in db.CHUCVUs
                                  from nv in db.NHANVIENs
                                  from cvnv in db.ChucVuNhanViens
                                  where cvnv.MaNV == nv.MaNV
                                  where cvnv.MaCV == cv.MaChucVu
                                  where nv.MaNV == data1.MaNV
                                  select new
                                  {
                                      LuongCoBan = cv.LuongCoBan
                                  };

                    float LuongCoBan = 0;
                    foreach(var data3 in ketqua3)
                    {
                        LuongCoBan = float.Parse(data3.LuongCoBan.ToString());
                    }

                    CHAMCONG cc = db.CHAMCONGs.Where(x1 => x1.MaNV == data1.MaNV).Where(y1 => y1.Thang == thang)
                        .Where(z1 => z1.Nam == nam).FirstOrDefault();
                    int id = 0;
                    if(cc != null)
                        id = cc.MaChamCong;

                    if (id == 0)
                    {
                        //insert cham cong
                        CHAMCONG cc1 = new CHAMCONG();
                        cc1.MaNV = data1.MaNV;
                        cc1.SoNgayLam = soNgayLam;
                        cc1.SoNgayNghi = (ngay - soNgayLam);
                        cc1.Thang = thang;
                        cc1.Nam = nam;
                        cc1.Luong = Math.Round((LuongCoBan / ngay * soNgayLam), 1);
                        db.CHAMCONGs.InsertOnSubmit(cc1);
                        db.SubmitChanges();
                    }
                    loadDataChamCong(thang, nam);
                }    
            }    
        }

        private void loadDataChamCong(int thang, int nam)
        {

            gunaDataGridView2.Rows.Clear();
            gunaDataGridView2.DataSource = from cc in db.CHAMCONGs
                                           from cv in db.CHUCVUs
                                           from cvnv in db.ChucVuNhanViens
                                           from nv in db.NHANVIENs
                                           where cvnv.MaNV == nv.MaNV
                                           where cvnv.MaCV == cv.MaChucVu
                                           where nv.MaNV == cc.MaNV
                                           where cc.Thang == thang
                                           where cc.Nam == nam
                                           select new
                                           {
                                               MaChamCong = cc.MaChamCong,
                                               TenNV = nv.TenNV,
                                               TenCV = cv.TenChucVu,
                                               LuongCoBan = cv.LuongCoBan,
                                               SoNgayLam = cc.SoNgayLam,
                                               SoNgayNghi = cc.SoNgayNghi,
                                               Luong = cc.Luong
                                           };
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            new FormInPhieuLuong(txtThangLuong.Value.Month,txtThangLuong.Value.Year).Show();
        }
    }
}
