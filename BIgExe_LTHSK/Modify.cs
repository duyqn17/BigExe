using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public List<Phong> getPhongs()
        {
            List<Phong> phongs = new List<Phong>();
            using(SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetPhong", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            phongs.Add(new Phong(
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetFloat(3),
                                reader.GetString(4)));
                        }
                    }
                }
                conn.Close() ;
                return phongs;
            }
        }
        public List<NhanVien> getNhanViens()
        {
            List<NhanVien> nhanViens = new List<NhanVien>();
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetNhanVien", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nhanViens.Add(new NhanVien(
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                reader.GetString(5),
                                float.Parse(reader["fLuong"].ToString())
                                ));
                        }
                    }
                }
                conn.Close() ;
                return nhanViens;
            }
        }

        public List<ChiTietDichVu> getChiTietDichVus()
        {
            List<ChiTietDichVu> chiTietDichVus = new List<ChiTietDichVu>();
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetCTDV", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chiTietDichVus.Add(new ChiTietDichVu(
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetInt32(4),
                                reader.GetDateTime(5),
                                reader.GetString(6)
                                ));
                        }
                    }
                }
                conn.Close();
                return chiTietDichVus;
            }
        }
        public List<KhachHang> getKhachHangs()
        {

            List<KhachHang> khachHangs = new List<KhachHang>();

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetKhachHang", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            khachHangs.Add(new KhachHang(
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetString(3),
                            reader.GetString(4),
                            reader.GetBoolean(5),
                            reader.GetString(6),
                            reader.GetString(7)
                            ));
                        }

                    }
                    conn.Close() ;
                }
                return khachHangs;
            }
        }

        public List<DangKy> getDangKys()
        {
            List<DangKy> dangKys = new List<DangKy>();
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetDangKy", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dangKys.Add(new DangKy(
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetString(4),
                                DateTime.Parse(reader["dNgayNhan"].ToString()),
                                DateTime.Parse(reader["dNgayTra"].ToString()),
                                reader.GetString(7)
                                ));
                        }
                    }
                }
                conn.Close() ;
                return dangKys;
            }
        }

        public List<DichVu> getDichVus()
        {
            List<DichVu> dichVus = new List<DichVu>();
            using(SqlConnection conn = Connection.getConnection())
            {
                conn.Open() ;
                using (SqlCommand cmd = new SqlCommand("sp_GetDichVu", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dichVus.Add(new DichVu(
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetFloat(3)
                                ));
                        }
                    }
                }
                return dichVus;
                
            }
        }

        public List<HoaDon> getHoaDons(string query)
        {

            List<HoaDon> hoaDons = new List<HoaDon>();

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hoaDons.Add(new HoaDon(
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3),
                            reader.GetFloat(4)
                            ));
                        }

                    }
                    conn.Close();
                }
                return hoaDons;
            }
        }
    }
}
