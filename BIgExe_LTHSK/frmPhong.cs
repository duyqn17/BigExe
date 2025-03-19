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
    public partial class frmPhong : Form
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
        public frmPhong()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        
        private void HienMaKhach()
        {
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetKhachHang", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string ma = reader.GetString(1);
                            cboMaKhach.Items.Add(ma);
                        }
                    }
                }
            }

        }

        

        private void HienMaNhanVien()
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
                            cboMaNV.Items.Add(ma);
                        }
                    }
                }
            }

        }

        private void frmPhong_Load(object sender, EventArgs e)
        {
            
            LoadPhong();
            LoadDatPhong();
            HienMaKhach();
            
            HienMaNhanVien();
        }

        private void HienDanhSachDatPhong()
        {
            //using (SqlConnection conn = Connection.getConnection())
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = new SqlCommand("sp_HienDSDP", conn))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        using (SqlDataReader reader = cmd.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                int ma = reader.GetInt32(0);
            //                cbo.Items.Add(ma);
            //            }

            //        }
            //    }
            //}
        }
        
        private void LoadPhong()
        {
            lvPhong.Items.Clear();
            Modify modify = new Modify();

            List<Phong> phongs = modify.getPhongs();

            foreach (Phong phong in phongs)
            {
                ListViewItem lvi = new ListViewItem(phong.maPhong);
                lvi.SubItems.Add(phong.loaiPhong);
                lvi.SubItems.Add(phong.giaPhong.ToString());

                lvi.SubItems.Add(phong.tinhTrang);

                lvPhong.Items.Add(lvi);
            }
        }

        private void LoadDatPhong()
        {
            lvDatPhong.Items.Clear();
            Modify modify = new Modify();
            List<DangKy> dangkys  = modify.getDangKys();
            foreach(DangKy dk in dangkys)
            {
                ListViewItem lvi = new ListViewItem(dk.maDK);
                lvi.SubItems.Add(dk.maKH);
                lvi.SubItems.Add(dk.maPhong);
                lvi.SubItems.Add(dk.maNV);
                lvi.SubItems.Add(dk.ngayNhan.ToString());
                lvi.SubItems.Add(dk.ngayTra.ToString());
                lvi.SubItems.Add(dk.trangThai);

                lvDatPhong.Items.Add(lvi);
            }
        }

        private void lvDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void lvPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvPhong.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvi = lvPhong.SelectedItems[0];
            txtMaPhong.Text = lvi.SubItems[0].Text;
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            if (dtpNgayTra.Value.Date < DateTime.Today ||
                string.IsNullOrWhiteSpace(txtMaPhong.Text) ||
                cboMaKhach.SelectedIndex == -1 ||
                cboMaNV.SelectedIndex == -1||
                cboTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("sp_ThemDangKyDatPhong", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@makh", cboMaKhach.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@maphong", txtMaPhong.Text);
                    cmd.Parameters.AddWithValue("@manv", cboMaNV.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ngaynhan", dtpNgayNhan.Value);
                    cmd.Parameters.AddWithValue("@ngaytra", dtpNgayTra.Value);
                    cmd.Parameters.AddWithValue("@trangthai", cboTrangThai.SelectedItem.ToString());


                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        dinhNghiaThongBao("Thêm thành công", "Thông báo", 2000);
                        LoadDatPhong();

                    }
                    else
                    {
                        dinhNghiaThongBao("Thêm thất bại", "Thông báo", 2000);
                    }
                }
                conn.Close();
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = Connection.getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_TimKiemDP", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    
                    if (cboMaKhach.SelectedItem == null || string.IsNullOrWhiteSpace(cboMaKhach.Text))
                        command.Parameters.AddWithValue("@makh", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@makh", cboMaKhach.SelectedItem.ToString());


                    // Mã phòng
                    command.Parameters.AddWithValue("@maphong", string.IsNullOrWhiteSpace(txtMaPhong.Text) ? (object)DBNull.Value : txtMaPhong.Text);

                    // Mã nhân viên
                    if (cboMaNV.SelectedItem == null || string.IsNullOrWhiteSpace(cboMaNV.Text))
                        command.Parameters.AddWithValue("@manv", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@manv", cboMaNV.SelectedItem.ToString());

                    //// Ngày nhận
                    //if (dtpNgayNhan.Checked)
                    //    command.Parameters.AddWithValue("@ngaynhan", dtpNgayNhan.Value.Date);
                    //else
                    //    command.Parameters.AddWithValue("@ngaynhan", DBNull.Value);

                    //// Ngày trả
                    //if (dtpNgayTra.Checked)
                    //    command.Parameters.AddWithValue("@ngaytra", dtpNgayTra.Value.Date);
                    //else
                    //    command.Parameters.AddWithValue("@ngaytra", DBNull.Value);
                    if (chkLocNgayNhan.Checked)
                        command.Parameters.AddWithValue("@ngaynhan", dtpNgayNhan.Value.Date);
                    else
                        command.Parameters.AddWithValue("@ngaynhan", DBNull.Value);

                    if (chkLocNgayTra.Checked)
                        command.Parameters.AddWithValue("@ngaytra", dtpNgayTra.Value.Date);
                    else
                        command.Parameters.AddWithValue("@ngaytra", DBNull.Value);


                    // Trạng thái
                    if (string.IsNullOrWhiteSpace(cboTrangThai.Text))
                        command.Parameters.AddWithValue("@trangthai", DBNull.Value);
                    else
                        command.Parameters.AddWithValue("@trangthai", cboTrangThai.Text);

                    // Đọc dữ liệu
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var items = new List<ListViewItem>();

                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["sMaDangKy"].ToString());
                            item.SubItems.Add(reader["FK_sMaKhach"].ToString());
                            item.SubItems.Add(reader["FK_sMaPhong"].ToString());
                            item.SubItems.Add(reader["FK_sMaNhanVien"].ToString());
                            item.SubItems.Add(Convert.ToDateTime(reader["dNgayNhan"]).ToString("dd/MM/yyyy"));
                            item.SubItems.Add(Convert.ToDateTime(reader["dNgayTra"]).ToString("dd/MM/yyyy"));
                            item.SubItems.Add(reader["sTrangThai"].ToString());

                            items.Add(item);
                        }

                        lvDatPhong.BeginUpdate();
                        lvDatPhong.Items.Clear();
                        lvDatPhong.Items.AddRange(items.ToArray());
                        lvDatPhong.EndUpdate();
                    }
                }
            }
        }



        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDatPhong();
        }

        private void cboMaKhach_TextChanged(object sender, EventArgs e)
        {
            btnTim_Click(sender, e);
        }

        private void txtMaPhong_TextChanged(object sender, EventArgs e)
        {
            btnTim_Click(sender, e);
        }

        private void cboMaNV_TextChanged(object sender, EventArgs e)
        {
            btnTim_Click(sender, e);
        }

        private void cboTrangThai_TextChanged(object sender, EventArgs e)
        {
            btnTim_Click(sender, e);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cboMaKhach.SelectedIndex = -1;
            cboMaNV.SelectedIndex = -1;
            txtMaPhong.Text = "";
            dtpNgayNhan.Checked = false;
            dtpNgayTra.Checked = false;

        }
    }
}
