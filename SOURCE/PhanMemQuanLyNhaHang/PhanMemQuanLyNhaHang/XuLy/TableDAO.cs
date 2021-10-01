using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if (instance == null) instance = new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }

        private TableDAO() { }

        public static int TableWidth = 135;
        public static int TableHeight = 135;

        public void SwitchTable(int id1, int id2)
        {
            int makh1, makh2, manv1, manv2;
            makh1 = (int)DataProvider.Instance.ExecuteScalar("select min(MaKH) from dbo.KHACHHANG");
            makh2 = makh1;
            manv1 = (int)DataProvider.Instance.ExecuteScalar("select min(MaNV) from dbo.NHANVIEN");
            manv2 = manv1;
            DataProvider.Instance.ExecuteQuery("USP_SwitchTable @idTable1 , @idTable2 , @makh1 , @makh2 , @manv1 , @manv2", new object[] { id1 , id2 , makh1 , makh2 , manv1 , manv2 });
        }

        public void MergeTable(int id1, int id2)
        {
            DataProvider.Instance.ExecuteQuery("USP_MergeTable @firstBill , @secondBill", new object[] { id1 , id2 });
        }

        public List<Table> loadBanList()
        {
            List<Table> banList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetTableList");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                banList.Add(table);
            }
            return banList;
        }
    }
}
