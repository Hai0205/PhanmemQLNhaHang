using PhanMemQuanLyNhaHang.XuLy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhanMemQuanLyNhaHang
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConnecDB db = new ConnecDB();
            try
            {
                db.myconnect();
                db.myclose();
                Application.Run(new FormLogin());
            }
            catch
            {
                Application.Run(new FormKetNoi());
            }
        }
    }
}
