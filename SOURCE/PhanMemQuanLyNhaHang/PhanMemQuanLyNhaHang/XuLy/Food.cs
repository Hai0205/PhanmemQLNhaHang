using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class Food
    {
        public Food(int maMonAn, string tenMonAn, int maDanhMuc, float giaTien)
        {
            this.MaMonAn = maMonAn;
            this.TenMonAn = tenMonAn;
            this.MaDanhMuc = maDanhMuc;
            this.GiaTien = giaTien;
        }

        public Food(DataRow row)
        {
            this.MaMonAn = (int)row["MaMonAn"];
            this.TenMonAn = row["TenMonAn"].ToString();
            this.MaDanhMuc = (int)row["MaDanhMuc"];
            this.GiaTien = (float)Convert.ToDouble(row["GiaTien"].ToString());
        }

        private float giaTien;

        public float GiaTien
        {
            get { return giaTien; }
            set { giaTien = value; }
        }

        private int maDanhMuc;

        public int MaDanhMuc
        {
            get { return maDanhMuc; }
            set { maDanhMuc = value; }
        }

        private string tenMonAn;

        public string TenMonAn
        {
            get { return tenMonAn; }
            set { tenMonAn = value; }
        }

        private int maMonAn;

        public int MaMonAn
        {
            get { return maMonAn; }
            set { maMonAn = value; }
        }
    }
}
