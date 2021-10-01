using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class BillInfo
    {
        public BillInfo(int maChiTietHoaDon, int maHoaDon, int maMonAn, int soLuong)
        {
            this.MaChiTietHoaDon = maChiTietHoaDon;
            this.MaHoaDon = maHoaDon;
            this.MaMonAn = maMonAn;
            this.SoLuong = soLuong;
        }

        public BillInfo(DataRow row)
        {
            this.MaChiTietHoaDon = (int)row["MaChiTietHoaDon"];
            this.MaHoaDon = (int)row["MaHoaDon"];
            this.MaMonAn = (int)row["MaMonAn"];
            this.SoLuong = (int)row["SoLuong"];
        }

        private int soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }

        private int maMonAn;

        public int MaMonAn
        {
            get { return maMonAn; }
            set { maMonAn = value; }
        }

        private int maHoaDon;

        public int MaHoaDon
        {
            get { return maHoaDon; }
            set { maHoaDon = value; }
        }

        private int maChiTietHoaDon;

        public int MaChiTietHoaDon
        {
            get { return maChiTietHoaDon; }
            set { maChiTietHoaDon = value; }
        }
    }
}
