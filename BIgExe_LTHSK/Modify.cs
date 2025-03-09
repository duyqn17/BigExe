using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class Modify
    {
        public Modify() { }

        public List<TaiKhoan> taiKhoans(string query)
        {
            List<TaiKhoan> taiKhoans = new List<TaiKhoan>();

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            taiKhoans.Add(new TaiKhoan(reader.GetString(2), reader.GetString(3)));
                        }
                        conn.Close();
                    }

                }
            }
            return taiKhoans;
        }

        public List<KhachHang> getKhachHangs(string query)
        {

            List<KhachHang> khachHangs = new List<KhachHang>();

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            khachHangs.Add(new KhachHang(
                            reader.GetInt32(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetBoolean(4).ToString(),
                            reader.GetString(5),
                            reader.GetString(6)
                            ));
                        }

                    }
                    conn.Close() ;
                }
                return khachHangs;
            }
        }
    }
}
