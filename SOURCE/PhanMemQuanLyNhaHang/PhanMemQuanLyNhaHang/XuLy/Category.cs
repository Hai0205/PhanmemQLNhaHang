using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class Category
    {
        public Category(int maDanhMuc, string tenDanhMuc)
        {
            this.MaDanhMuc = maDanhMuc;
            this.TenDanhMuc = tenDanhMuc;
        }

        public Category(DataRow row)
        {
            this.MaDanhMuc = (int)row["MaDanhMuc"];
            this.TenDanhMuc = row["TenDanhMuc"].ToString();
        }

        private string tenDanhMuc;

        public string TenDanhMuc
        {
            get { return tenDanhMuc; }
            set { tenDanhMuc = value; }
        }

        private int maDanhMuc;

        public int MaDanhMuc
        {
            get { return maDanhMuc; }
            set { maDanhMuc = value; }
        }
    }
}
