using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class Menu
    {
        public Menu(string tenMonAn, int soLuong, float gia, float thanhTien = 0)
        {
            this.TenMonAn = tenMonAn;
            this.SoLuong = soLuong;
            this.Gia = gia;
            this.ThanhTien = thanhTien;
        }

        public Menu(DataRow row)
        {
            this.TenMonAn = row["TenMonAn"].ToString();
            this.SoLuong = (int)row["SoLuong"];
            this.Gia = (float)Convert.ToDouble(row["GiaTien"].ToString());
            this.ThanhTien = (float)Convert.ToDouble(row["totalPrice"].ToString());
        }

        private float thanhTien;

        public float ThanhTien
        {
            get { return thanhTien; }
            set { thanhTien = value; }
        }

        private float gia;

        public float Gia
        {
            get { return gia; }
            set { gia = value; }
        }

        private int soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }

        private string tenMonAn;

        public string TenMonAn
        {
            get { return tenMonAn; }
            set { tenMonAn = value; }
        }
    }
}
