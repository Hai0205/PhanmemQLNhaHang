using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class BillInfoDao
    {
        private static BillInfoDao instance;

        public static BillInfoDao Instance
        {
            get { if (instance == null) instance = new BillInfoDao(); return BillInfoDao.instance; }
            private set { BillInfoDao.instance = value; }
        }

        private BillInfoDao() { }

        public void DeleteBillInfoByFoodID(int id)
        {
            DataProvider.Instance.ExecuteQuery("delete dbo.ChiTietHoaDon where MaMonAn = " + id);
        }

        public List<BillInfo> GetListBillInfo(int id)
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from dbo.ChiTietHoaDon where MaHoaDon = " + id);
            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }
            return listBillInfo;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @mahoadon , @mamonan , @soluong", new object[] { idBill, idFood, count });
        }
    }
}
