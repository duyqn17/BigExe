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
    public partial class frmNhanVien : Form
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
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void lvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvNhanVien.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvi = lvNhanVien.SelectedItems[0];
            txtTenNV.Text = lvi.SubItems[1].Text;
            txtSoDienThoai.Text = lvi.SubItems[2].Text;
            txtEmail.Text = lvi.SubItems[3].Text;
            txtChucVu.Text = lvi.SubItems[4].Text;
            txtLuong.Text = lvi.SubItems[5].Text;


        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        private void LoadNhanVien()
        {
            lvNhanVien.Items.Clear();
            Modify modify = new Modify();
            List<NhanVien> nhanViens = modify.getNhanViens();

            foreach(NhanVien nhanVien in nhanViens)
            {
                ListViewItem lvi = new ListViewItem(nhanVien.maNV.ToString());
                lvi.SubItems.Add(nhanVien.tenNV);
                lvi.SubItems.Add(nhanVien.chucVu);
                lvi.SubItems.Add(nhanVien.sdt);
                lvi.SubItems.Add(nhanVien.email);
                lvi.SubItems.Add(nhanVien.luong.ToString());

                lvNhanVien.Items.Add(lvi);
            }

        }

        private void btnThemNV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNV.Text) ||
                string.IsNullOrWhiteSpace(txtChucVu.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtLuong.Text) ||
                string.IsNullOrWhiteSpace(txtSoDienThoai.Text)
                )
            {
                MessageBox.Show("Vui lòng nhập đầy đủ", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            try
            {
                using (SqlConnection conn = Connection.getConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ThemNhanVien", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@hoten", txtTenNV.Text);
                        cmd.Parameters.AddWithValue("@sdt", txtSoDienThoai.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@chucvu", txtChucVu.Text);
                        cmd.Parameters.AddWithValue("@luong", txtLuong.Text);

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Thêm thành công", "Thông báo", 2000);
                            ClearFields();
                            LoadNhanVien();
                        }
                        else
                        {
                            dinhNghiaThongBao("Thêm thất bại", "Thông báo", 2000);
                        }
                    }
                }
            }
            catch { }
        }

        private void ClearFields()
        {
            txtSoDienThoai.Text = "";
            txtLuong.Text = "";
            txtEmail.Text = "";
            txtTenNV.Text = "";
            txtChucVu.Text = "";
        }
        string ma = "";
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            ma = lvNhanVien.SelectedItems[0].SubItems[0].Text;
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin nhân viên có mã:" + ma + "không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                using (SqlConnection connection = Connection.getConnection())
                {

                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SuaNV", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@manv", ma);
                        cmd.Parameters.AddWithValue("@hoten", txtTenNV.Text);
                        cmd.Parameters.AddWithValue("@sdt", txtSoDienThoai.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        
                        cmd.Parameters.AddWithValue("@chucvu", txtChucVu.Text);
                        cmd.Parameters.AddWithValue("@luong", txtLuong.Text);

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Sửa thành công", "Thông báo", 2000);
                            ClearFields();
                            LoadNhanVien();
                        }
                        else
                        {
                            dinhNghiaThongBao("Sửa thất bại", "Thông báo", 2000);
                        }
                    }
                    connection.Close();
                }
            }
            else
            {

            }
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muón xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            string ma = lvNhanVien.SelectedItems[0].SubItems[0].Text;
            using (SqlConnection conn = Connection.getConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_XoaNV", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@manv", ma);

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Xóa thành công", "Thông báo", 2000);
                            ClearFields();
                            LoadNhanVien();
                        }
                        else
                        {
                            dinhNghiaThongBao("Xóa thất bại", "Thông báo", 2000);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa nhân viên này vì vẫn còn dữ liệu liên quan trong hệ thống.",
                                        "Lỗi ràng buộc dữ liệu",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}
