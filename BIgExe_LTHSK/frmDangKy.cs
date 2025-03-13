using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIgExe_LTHSK
{
    public partial class frmDangKy : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_CLOSE = 0x0010;

        private async void dinhNghiaThongBao(string message, string title, int timeout)
        {
            Task.Run(async () =>
            {
                await Task.Delay(timeout);
                IntPtr hWnd = FindWindow(null, title);
                if (hWnd != IntPtr.Zero)
                {
                    SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                }
            });

            MessageBox.Show(message, title, MessageBoxButtons.OK);
        }
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmDangKy_Load(object sender, EventArgs e)
        {
            HienThiMaNV();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDK.Text) ||
                string.IsNullOrWhiteSpace(txtMatKhau.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtXNMK.Text) ||
                cboTenNV.SelectedIndex == -1
                )
            {
                MessageBox.Show("Vui lòng nhập, chọn đầy đủ", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else if (txtMatKhau.Text != txtXNMK.Text)
            {
                MessageBox.Show("Mật khẩu không khớp", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            try
            {
                using (SqlConnection conn = Connection.getConnection())
                {
                    conn.Open();
                    
                    using (SqlCommand cmd = new SqlCommand("sp_ThemTaiKhoan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@manv", cboTenNV.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@tentk", txtTenDK.Text);
                        cmd.Parameters.AddWithValue("@matkhau", txtMatKhau.Text);

                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Đăng ký tài khoản thành công", "Thông báo", 2000);
                            ClearFields();
                           
                        }
                        else
                        {
                            dinhNghiaThongBao("Thêm thất bại", "Thông báo", 2000);
                            
                        }
                    }
                }
            }
            catch(SqlException ex) {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtTenDK.Text = "";
            txtEmail.Text = "";
            txtMatKhau.Text = "";
            txtXNMK.Text = "";
        }

        private void HienThiMaNV()
        {
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetNhanVien", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ma = reader.GetString(1);
                            cboTenNV.Items.Add(ma);
                        }

                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
