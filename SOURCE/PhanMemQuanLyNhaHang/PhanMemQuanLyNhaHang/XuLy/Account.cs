using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class Account
    {
        public Account(int maNV, string tenNV, string tenDangNhap, DateTime? ngaySinh, DateTime? ngayVaoLam, string hinh,
            string gioiTinh, string soCMND, int maQuyen, string email, string diaChi, string soDT, string matKhau = null)
        {
            this.MaNV = maNV;
            this.TenNV = tenNV;
            this.TenDangNhap = tenDangNhap;
            this.MatKhau = matKhau;
            this.Email = email;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.CMND = soCMND;
            this.DiaChi = diaChi;
            this.SoDT = soDT;
            this.NgayVaoLam = ngayVaoLam;
            this.MaQuyen = maQuyen;
            this.Hinh = hinh;
        }

        public Account(DataRow row)
        {
            this.MaNV = (int)row["MaNV"];
            if (row["TenNV"].ToString() != "")
                this.TenNV = row["TenNV"].ToString();
            if (row["TenDangNhap"].ToString() != "")
                this.TenDangNhap = row["TenDangNhap"].ToString();
            if (row["Email"].ToString() != "")
                this.Email = row["Email"].ToString();
            if (row["NgaySinh"].ToString() != "")
                this.NgaySinh = (DateTime?)row["NgaySinh"];
            if (row["NgayVaoLam"].ToString() != "")
                this.NgayVaoLam = (DateTime?)row["NgayVaoLam"];
            if (row["GioiTinh"].ToString() != "")
                this.GioiTinh = row["GioiTinh"].ToString();
            //if (row["LuongThang"].ToString() != "")
            //    this.LuongThang = (float)Convert.ToDouble(row["LuongThang"]);
            if (row["CMND"].ToString() != "")
                this.CMND = row["CMND"].ToString();
            if (row["DiaChi"].ToString() != "")
                this.DiaChi = row["DiaChi"].ToString();
            if (row["SoDT"].ToString() != "")
                this.SoDT = row["SoDT"].ToString();
            if (row["Hinh"].ToString() != "")
                this.Hinh = row["Hinh"].ToString();
            this.MaQuyen = (int)row["MaQuyen"];
            this.MatKhau = row["MatKhau"].ToString();
        }

        private string hinh;

        public string Hinh
        {
            get { return hinh; }
            set { hinh = value; }
        }

        private int maQuyen;

        public int MaQuyen
        {
            get { return maQuyen; }
            set { maQuyen = value; }
        }

        private DateTime? ngayVaoLam;

        public DateTime? NgayVaoLam
        {
            get { return ngayVaoLam; }
            set { ngayVaoLam = value; }
        }

        private string soDT;

        public string SoDT
        {
            get { return soDT; }
            set { soDT = value; }
        }

        private string diaChi;

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        private string cmnd;

        public string CMND
        {
            get { return cmnd; }
            set { cmnd = value; }
        }

        private string gioiTinh;

        public string GioiTinh
        {
            get { return gioiTinh; }
            set { gioiTinh = value; }
        }

        private DateTime? ngaySinh;

        public DateTime? NgaySinh
        {
            get { return ngaySinh; }
            set { ngaySinh = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string matKhau;

        public string MatKhau
        {
            get { return matKhau; }
            set { matKhau = value; }
        }

        private string tenDangNhap;

        public string TenDangNhap
        {
            get { return tenDangNhap; }
            set { tenDangNhap = value; }
        }

        private string tenNV;

        public string TenNV
        {
            get { return tenNV; }
            set { tenNV = value; }
        }

        private int maNV;

        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }
    }
}
