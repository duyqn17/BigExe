using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIgExe_LTHSK
{
    public partial class frmChiTietDichVu : Form
    {
        public frmChiTietDichVu()
        {
            InitializeComponent();
        }

        private DataTable dtTietDichVu()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Mã chi tiết dv", typeof(string));
            dt.Columns.Add("Mã đăng ký", typeof(string));
            dt.Columns.Add("Mã dịch vụ", typeof(string));
            dt.Columns.Add("Số lượng", typeof(int));
            dt.Columns.Add("Ngày sử dụng", typeof(DateTime));
            dt.Columns.Add("Ghi chú", typeof(string));


            return dt;
        }
        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            loadCTDV();
            
        }


        private void loadCTDV()
        {
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                DataTable dt = dtTietDichVu();
                using (SqlCommand cmd = new SqlCommand("sp_GetCTDV", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dt.Rows.Add(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), reader.GetDateTime(5), reader.GetString(6));
                        }
                        dgvChiTietDichVu.DataSource = dt;
                    }
                }
            }
        }

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            loadCTDV();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            using(SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using(SqlCommand cmd = new SqlCommand("sp_XoaCTDV", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mact", dgvChiTietDichVu.SelectedRows[0].Cells[0].Value);
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand("sp_TimKiemCTDV", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@mact", txtMaCT.Text); 
                    using(SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dgvChiTietDichVu.DataSource = dt;
                    }
                }
            }
        }

        private void txtMaCT_TextChanged(object sender, EventArgs e)
        {
            btnTimKiem_Click(sender, e);
        }
    }
}
