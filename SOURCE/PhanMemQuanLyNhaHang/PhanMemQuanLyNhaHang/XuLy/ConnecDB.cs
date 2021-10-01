using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class ConnecDB
    {
        public SqlConnection sqlcon = new SqlConnection(PhanMemQuanLyNhaHang.Properties.Settings.Default.strConnect);

        public void myconnect()
        {
            sqlcon.Open();
        }
            
        public void myclose()
        {
            sqlcon.Close();
            sqlcon.Dispose();
            sqlcon = null;
        }
    }
}
