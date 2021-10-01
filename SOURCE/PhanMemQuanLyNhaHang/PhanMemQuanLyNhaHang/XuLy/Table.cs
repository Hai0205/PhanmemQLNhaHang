using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class Table
    {
        public Table(int maBan, string tenBan, string trangThai)
        {
            this.MaBan = maBan;
            this.TenBan = tenBan;
            this.TrangThai = trangThai;
        }

        public Table(DataRow row)
        {
            this.MaBan = (int)row["MaBan"];
            this.TenBan = row["Ten"].ToString();
            this.TrangThai = row["TrangThai"].ToString();
        }

        private string trangThai;

        public string TrangThai
        {
            get { return trangThai; }
            set { trangThai = value; }
        }

        private string tenBan;

        public string TenBan
        {
            get { return tenBan; }
            set { tenBan = value; }
        }

        private int maBan;

        public int MaBan
        {
            get { return maBan; }
            set { maBan = value; }
        }
    }
}
