using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class KhachHang
    {
        public KhachHang(int maKH, string tenKH, string tenDangNhap, string matKhau, string email, DateTime? ngaySinh, string gioiTinh, string cmnd, string diaChi, string soDT)
        {
            this.MaKH = maKH;
            this.TenKH = tenKH;
            this.TenDangNhap = tenDangNhap;
            this.MatKhau = matKhau;
            this.Email = email;
            this.NgaySinh = ngaySinh;
            this.GioiTinh = gioiTinh;
            this.CMND = cmnd;
            this.DiaChi = diaChi;
            this.SoDT = soDT;
        }

        public KhachHang(DataRow row)
        {
            this.MaKH = (int)row["MaKH"];
            if (row["TenKH"].ToString() != "")
                this.TenKH = row["TenKH"].ToString();
            if (row["TenDangNhap"].ToString() != "")
                this.TenDangNhap = row["TenDangNhap"].ToString();
            if (row["MatKhau"].ToString() != "")
                this.MatKhau = row["MatKhau"].ToString();
            if (row["Email"].ToString() != "")
                this.Email = row["Email"].ToString();
            if (row["NgaySinh"].ToString() != "")
                this.NgaySinh = (DateTime?)row["NgaySinh"];
            if (row["GioiTinh"].ToString() != "")
                this.GioiTinh = row["GioiTinh"].ToString();
            if (row["CMND"].ToString() != "")
                this.CMND = row["CMND"].ToString();
            if (row["DiaChi"].ToString() != "")
                this.DiaChi = row["DiaChi"].ToString();
            if (row["SoDT"].ToString() != "")
                this.SoDT = row["SoDT"].ToString();
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

        private string tenKH;

        public string TenKH
        {
            get { return tenKH; }
            set { tenKH = value; }
        }

        private int maKH;

        public int MaKH
        {
            get { return maKH; }
            set { maKH = value; }
        }
    }
}
