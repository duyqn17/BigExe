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
    public partial class frmHoaDon : Form
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
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            HienMaDangKy();
            LoadHoaDon();
        }
        private void HienMaDangKy()
        {
            using (SqlConnection conn = Connection.getConnection()){
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetDangKy", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ma = reader.GetString(1);
                            cboMaDK.Items.Add(ma);
                        }
                    }
                }
            }
        }
        private void LoadHoaDon()
        {
            lvHoaDon.Items.Clear();
            Modify modify = new Modify();
            string query = "select * from tblHoaDon";
            List<HoaDon> hoaDons = modify.getHoaDons(query);

            foreach (var hd in hoaDons)
            {
                ListViewItem item = new ListViewItem(hd.maHD.ToString());
                item.SubItems.Add(hd.maDK.ToString());
                item.SubItems.Add(hd.ngayLap.ToString());
                item.SubItems.Add(hd.tongTien.ToString());
      
                lvHoaDon.Items.Add(item);
            }
        }

        private void lvHoaDon_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLapHD_Click(object sender, EventArgs e)
        {
            if (cboMaDK.SelectedIndex == -1||
                dtpNgaylap.Value.Date <DateTime.Now)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ, hợp lệ thông tin");
                return;
            }

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_ThemHoaDon", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@madk", cboMaDK.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ngaylap", dtpNgaylap.Value);

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        dinhNghiaThongBao("Thêm thành công", "Thông báo", 2000);
                        LoadHoaDon();

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

        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            ma = lvHoaDon.SelectedItems[0].SubItems[0].Text;
            DialogResult re = MessageBox.Show("Bạn có chắc chắc muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
            {
                using (SqlConnection connection = Connection.getConnection())
                {

                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SuaHD", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@mahd", ma);
                        cmd.Parameters.AddWithValue("@madk", cboMaDK.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@ngaylap", dtpNgaylap.Value);
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Sửa thành công", "Thông báo", 2000);
                            
                            LoadHoaDon();
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

        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            ma = lvHoaDon.SelectedItems[0].SubItems[0].Text;
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn xóa không", "Thông báo", MessageBoxButtons.YesNo);
            if (re == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = Connection.getConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_XoaHD", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@mahd", ma);

                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                dinhNghiaThongBao("Xóa thành công", "Thông báo", 2000);
                                LoadHoaDon();
                            }

                        }
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa hóa đơn này vì vẫn còn dữ liệu liên quan trong hệ thống.",
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
}
