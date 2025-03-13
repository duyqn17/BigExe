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
    public partial class frmDichVu : Form
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
        public frmDichVu()
        {
            InitializeComponent();
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {

            HienThiDangKy();
            LoadDichVu();
        }

        private void LoadDichVu()
        {
            lvDichVu.Items.Clear();
            Modify modify = new Modify();
            List<DichVu> dichVus = modify.getDichVus();

            foreach (DichVu dv in dichVus)
            {
                ListViewItem lvi = new ListViewItem(dv.maDV.ToString());
                lvi.SubItems.Add(dv.tenDV);
                lvi.SubItems.Add(dv.donGia.ToString());

                lvDichVu.Items.Add(lvi);
            }
        }

        private void HienThiDangKy()
        {
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetDangKy", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ma = reader.GetString(1);
                            cboDatPhong.Items.Add(ma);
                        }

                    }
                }
            }
        }



        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDatPhong.SelectedIndex == -1)
            {
                return;
            }
            string ma = cboDatPhong.SelectedItem.ToString();

        }

        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDichVu.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvi = lvDichVu.SelectedItems[0];
            txtMaDV.Text = lvi.SubItems[0].Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDV.Text) ||
                string.IsNullOrWhiteSpace(txtDonGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_ThemDichVu", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@tendv", txtTenDV.Text);
                    cmd.Parameters.AddWithValue("@dongia", txtDonGia.Text);


                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        dinhNghiaThongBao("Thêm thành công", "Thông báo", 2000);
                        LoadDichVu();

                    }
                    else
                    {
                        dinhNghiaThongBao("Thêm thất bại", "Thông báo", 2000);
                    }
                }
                conn.Close();
            }
        }

        string ma = "";
        private void btnSua_Click(object sender, EventArgs e)
        {
            ma = lvDichVu.SelectedItems[0].SubItems[0].Text;
            DialogResult re = MessageBox.Show("Bạn có chắc chắc muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
            {
                using (SqlConnection connection = Connection.getConnection())
                {

                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SuaDV", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@madv", ma);
                        cmd.Parameters.AddWithValue("@tendv", txtTenDV.Text);
                        cmd.Parameters.AddWithValue("@dongia", txtDonGia.Text);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Sửa thành công", "Thông báo", 2000);
                            ClearFields();
                            LoadDichVu();
                        }
                        else
                        {
                            dinhNghiaThongBao("Sửa thất bại", "Thông báo", 2000);
                        }
                    }
                    connection.Close();
                }
            }
        }

        private void ClearFields()
        {
            txtTenDV.Text = "";
            txtDonGia.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            ma = lvDichVu.SelectedItems[0].SubItems[0].Text;
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa không", "Thông báo", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = Connection.getConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_XoaDV", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@madv", ma);

                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                dinhNghiaThongBao("Xóa thành công", "Thông báo", 2000);
                                LoadDichVu();
                            }

                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa dịch vụ này vì vẫn còn dữ liệu liên quan trong hệ thống.",
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

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDV.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ThemCTDV", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@madk", cboDatPhong.Text);
                    cmd.Parameters.AddWithValue("@madv", txtMaDV.Text);
                    cmd.Parameters.AddWithValue("@soluong", txtSoLuong.Text);
                    cmd.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);
                    cmd.Parameters.AddWithValue("@ngaysudung", dtpNgaySuDung.Value);

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        dinhNghiaThongBao("Thêm thành công", "Thông báo", 2000);
                        LoadDichVu();

                    }
                }
            }

        }
    }
}
