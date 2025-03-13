using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class Connection
    {
        static string connStr = "Data Source=admin\\SQLEXPRESS;Initial Catalog=QuanLyThuePhongKhachSan1;Integrated Security=True;TrustServerCertificate=True";
        public static SqlConnection getConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}
