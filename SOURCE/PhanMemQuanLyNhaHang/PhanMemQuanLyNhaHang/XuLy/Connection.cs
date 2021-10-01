using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyNhaHang.XuLy
{
    public class Connection
    {
        public SqlConnection ketNoi;
        string sql;

        public Connection()
        {
            sql = "Data Source=(local); Initial Catalog=DBNhaHang;User ID = sa; Password=sa2012";
            ketNoi = new SqlConnection(sql);
        }

        public void moKetNoi()
        {
            if (ketNoi.State.ToString() == "Closed")
            {
                ketNoi.Open();
            }
        }

        public void dongKetNoi()
        {
            if (ketNoi.State.ToString() == "Open")
                ketNoi.Close();
        }

        public DataTable kiemTraTaiKhoan()
        {
            DataTable data = new DataTable();
            SqlCommand command = new SqlCommand();
            command.Connection = ketNoi;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "sp_load_account";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(data);
            ketNoi.Close();
            return data;
        }
    }
}
