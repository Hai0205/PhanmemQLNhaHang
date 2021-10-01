using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class NhanVienDAO
    {
        private static NhanVienDAO instance;

        public static NhanVienDAO Instance
        {
            get { if (instance == null) instance = new NhanVienDAO(); return NhanVienDAO.instance; }
            private set { NhanVienDAO.instance = value; }
        }

        private NhanVienDAO() { }

        public List<NhanVien> loadNhanVienList()
        {
            List<NhanVien> nhanVienList = new List<NhanVien>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetNhanVienList");
            foreach (DataRow item in data.Rows)
            {
                NhanVien nhanVien = new NhanVien(item);
                nhanVienList.Add(nhanVien);
            }
            return nhanVienList;
        }
    }
}
