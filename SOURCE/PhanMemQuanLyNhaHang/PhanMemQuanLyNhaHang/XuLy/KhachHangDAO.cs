using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class KhachHangDAO
    {
        private static KhachHangDAO instance;

        public static KhachHangDAO Instance
        {
            get { if (instance == null) instance = new KhachHangDAO(); return KhachHangDAO.instance; }
            private set { KhachHangDAO.instance = value; }
        }

        private KhachHangDAO() { }

        public List<KhachHang> loadKhachList()
        {
            List<KhachHang> khachList = new List<KhachHang>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetKhachHangList");
            foreach (DataRow item in data.Rows)
            {
                KhachHang khach = new KhachHang(item);
                khachList.Add(khach);
            }
            return khachList;
        }
    }
}
