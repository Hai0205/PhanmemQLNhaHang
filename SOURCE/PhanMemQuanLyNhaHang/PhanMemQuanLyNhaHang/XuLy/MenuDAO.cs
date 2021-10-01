using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set { MenuDAO.instance = value; }
        }

        private MenuDAO() { }

        public List<Menu> GetListMenuByTable(int id)
        {
            List<Menu> listMenu = new List<Menu>();

            string query = "select ma.TenMonAn, ct.SoLuong, ma.GiaTien, ma.GiaTien*ct.SoLuong as totalPrice from dbo.ChiTietHoaDon as ct, dbo.HoaDon as hd, dbo.MonAn as ma where hd.MaHoaDon = ct.MaHoaDon and ct.MaMonAn = ma.MaMonAn and hd.TrangThai = 0 and hd.MaBan = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Menu menu = new Menu(item);
                listMenu.Add(menu);
            }

            return listMenu;
        }
    }
}
