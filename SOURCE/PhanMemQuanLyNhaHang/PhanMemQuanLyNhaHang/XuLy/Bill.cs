using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class Bill
    {
        public Bill(int maHoaDon, DateTime? gioVao, DateTime? gioRa, int trangThai, int maNV, int maKH, int giamGia = 0)
        {
            this.MaHoaDon = maHoaDon;
            this.GioVao = gioVao;
            this.GioRa = gioRa;
            this.TrangThai = trangThai;
            this.MaNV = maNV;
            this.MaKH = maKH;
            this.GiamGia = giamGia;
        }

        public Bill(DataRow row)
        {
            this.MaHoaDon = (int)row["MaHoaDon"];
            this.GioVao = (DateTime?)row["GioVao"];
            var dateCheckOutTemp = row["GioRa"];
            if (dateCheckOutTemp.ToString() != "")
            {
                this.GioRa = (DateTime?)dateCheckOutTemp;
            }
            this.TrangThai = (int)row["TrangThai"];
            this.MaNV = (int)row["MaNV"];
            this.MaKH = (int)row["MaKH"];
            if(row["GiamGia"].ToString() != "")
                this.GiamGia = (int)row["GiamGia"];
        }

        private int giamGia;

        public int GiamGia
        {
            get { return giamGia; }
            set { giamGia = value; }
        }

        private int maKH;

        public int MaKH
        {
            get { return maKH; }
            set { maKH = value; }
        }

        private int maNV;

        public int MaNV
        {
            get { return maNV; }
            set { maNV = value; }
        }

        private int trangThai;

        public int TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }

        private DateTime? gioRa;

        public DateTime? GioRa
        {
            get { return gioRa; }
            set { gioRa = value; }
        }

        private DateTime? gioVao;

        public DateTime? GioVao
        {
            get { return gioVao; }
            set { gioVao = value; }
        }

        private int maHoaDon;

        public int MaHoaDon
        {
            get { return maHoaDon; }
            set { maHoaDon = value; }
        }
    }
}
