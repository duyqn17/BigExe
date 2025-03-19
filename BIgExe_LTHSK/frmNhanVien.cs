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
            if (re == DialogResult.Yes)
            {
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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnTimNV_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = Connection.getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_TimKiemNV", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    string hoTen = txtTenNV.Text.Trim();
                    command.Parameters.AddWithValue("@hoten", string.IsNullOrWhiteSpace(hoTen) ? (object)DBNull.Value : hoTen);
                    command.Parameters.AddWithValue("@sdt", string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ? (object)DBNull.Value : txtSoDienThoai.Text);
                    command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                    command.Parameters.AddWithValue("@chucvu", string.IsNullOrWhiteSpace(txtChucVu.Text) ? (object)DBNull.Value : txtChucVu.Text);

                    // Chuyển đổi lương sang số nếu có giá trị
                    float luong;
                    command.Parameters.AddWithValue("@luong_min", float.TryParse(txtLuongMin.Text, out luong) ? (object)luong : DBNull.Value);
                    command.Parameters.AddWithValue("@luong_max", float.TryParse(txtLuongMax.Text, out luong) ? (object)luong : DBNull.Value);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var items = new List<ListViewItem>(); // Danh sách tạm để chứa dữ liệu mới

                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["sMaNhanVien"].ToString()); // Cột ID
                            item.SubItems.Add(reader["sHoTen"].ToString());
                            item.SubItems.Add(reader["sSoDienThoai"].ToString());
                            item.SubItems.Add(reader["sEmail"].ToString());
                            item.SubItems.Add(reader["sChucVu"].ToString());

                            // Kiểm tra NULL trước khi lấy lương
                            float luongNhanVien = reader.IsDBNull(reader.GetOrdinal("fLuong"))
                                ? 0
                                : (float)Math.Round(reader.GetFloat(reader.GetOrdinal("fLuong")), 2); // Chuyển đổi & làm tròn

                            // Định dạng lương để không hiển thị 1.2E
                            string luongFormatted = luongNhanVien.ToString("N2"); // Hiển thị 2 số thập phân

                            item.SubItems.Add(luongFormatted);
                            items.Add(item);
                        }

                        lvNhanVien.BeginUpdate();
                        lvNhanVien.Items.Clear();
                        lvNhanVien.Items.AddRange(items.ToArray()); // Thêm tất cả vào một lần
                        lvNhanVien.EndUpdate();
                    }
                }
            }
        }

        private void txtTenNV_TextChanged(object sender, EventArgs e)
        {
            //btnTimNV_Click(sender, e);
        }

        private void txtSoDienThoai_TextChanged(object sender, EventArgs e)
        {
            //btnTimNV_Click(sender, e);
        }

        private void txtChucVu_TextChanged(object sender, EventArgs e)
        {
            //btnTimNV_Click(sender, e);
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            //btnTimNV_Click(sender, e);
        }

        private void txtLuong_TextChanged(object sender, EventArgs e)
        {
            //btnTimNV_Click(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadNhanVien();
        }

        private void txtTenNV_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenNV.Text))
            {

                errorProvider1.SetError(txtTenNV, "Vui lòng nhập tên nhân viên");
            }
            else
            {
                errorProvider1.SetError(txtTenNV, null);

            }
        }

        private void txtSoDienThoai_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoDienThoai.Text))
            {

                errorProvider1.SetError(txtSoDienThoai, "Vui lòng nhập tên khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtSoDienThoai, null);

            }
        }

        private void txtChucVu_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtChucVu.Text))
            {

                errorProvider1.SetError(txtChucVu, "Vui lòng nhập tên khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtChucVu, null);

            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {

                errorProvider1.SetError(txtEmail, "Vui lòng nhập tên khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);

            }
        }

        private void txtLuong_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLuong.Text))
            {

                errorProvider1.SetError(txtLuong, "Vui lòng nhập tên khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtLuong, null);

            }
        }

        private void txtLuongMin_VisibleChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLuongMin.Text))
            {

                errorProvider1.SetError(txtLuongMin, "Vui lòng nhập khoảng lương bắt đầu");
            }
            else
            {
                errorProvider1.SetError(txtLuongMin, null);

            }
        }

        private void txtLuongMax_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtLuongMax.Text))
            {

                errorProvider1.SetError(txtLuongMax, "Vui lòng nhập khoảng lương kết thúc");
            }
            else
            {
                errorProvider1.SetError(txtLuongMax, null);

            }
        }
    }
}
