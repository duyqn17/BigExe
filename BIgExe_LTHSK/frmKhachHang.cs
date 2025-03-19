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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BIgExe_LTHSK
{
    public partial class frmKhachHang : Form
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
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void lvKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvKhachHang.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lvi = lvKhachHang.SelectedItems[0];
            txtMaKH.Text = lvi.SubItems[0].Text; 
            txtHoTenKH.Text = lvi.SubItems[1].Text;
            txtSdtKH.Text = lvi.SubItems[2].Text;
            txtEmailKH.Text = lvi.SubItems[3].Text;
            string gioiTinh = lvi.SubItems[4].Text;
            if(gioiTinh == "1")
            {
                rabNam.Checked = true;
                rabNu.Checked = false;
            }
            else
            {
                rabNam.Checked = false;
                rabNu.Checked = true;
            }
            txtDiaChiKH.Text = lvi.SubItems[5].Text;
            string loaiKH = lvi.SubItems[6].Text;
            cboLoaiKhach.SelectedItem = loaiKH;
        }

        private void LoadKhachHang()
        {
            lvKhachHang.Items.Clear();
            Modify modify = new Modify();
            
            List<KhachHang> khachHangs = modify.getKhachHangs();

            foreach (var kh in khachHangs)
            {
                ListViewItem item = new ListViewItem(kh.maKH);
                item.SubItems.Add(kh.hoTen);
                item.SubItems.Add(kh.soDienThoai);
                item.SubItems.Add(kh.email);
                item.SubItems.Add(Convert.ToBoolean(kh.gioiTinh) ? "Nam": "Nữ");
                item.SubItems.Add(kh.diaChi);
                item.SubItems.Add(kh.loaiKhach);
                
                lvKhachHang.Items.Add(item);
            }
        }

        
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnThemKH_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtHoTenKH.Text) ||
                string.IsNullOrWhiteSpace(txtSdtKH.Text) ||
                string.IsNullOrWhiteSpace(txtEmailKH.Text) ||
                string.IsNullOrWhiteSpace(txtDiaChiKH.Text) ||
                cboLoaiKhach.SelectedIndex == -1
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
                    using (SqlCommand cmd = new SqlCommand("sp_ThemKhachHang", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@hoten", txtHoTenKH.Text);
                        cmd.Parameters.AddWithValue("@sdt", txtSdtKH.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmailKH.Text);
                        if (rabNam.Checked)
                        {
                            bool gioiTinh = rabNam.Checked;
                            cmd.Parameters.AddWithValue("@gioitinh", gioiTinh);
                        }
                        else
                        {
                            bool gioiTinh = rabNu.Checked;
                            cmd.Parameters.AddWithValue("@gioitinh", gioiTinh);
                        }

                        cmd.Parameters.AddWithValue("@diachi", txtDiaChiKH.Text);
                        cmd.Parameters.AddWithValue("@loaikhach", cboLoaiKhach.SelectedItem.ToString());

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Thêm thành công", "Thông báo", 2000);
                            ClearFields();
                            LoadKhachHang();
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

        string ma = "";
        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            ma = lvKhachHang.SelectedItems[0].SubItems[0].Text;
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng có mã:" + ma + "không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                using (SqlConnection connection = Connection.getConnection())
                {

                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_SuaKH", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@makh", ma);
                        cmd.Parameters.AddWithValue("@hoten", txtHoTenKH.Text);
                        cmd.Parameters.AddWithValue("@sdt", txtSdtKH.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmailKH.Text);
                        if (rabNam.Checked)
                        {
                            cmd.Parameters.AddWithValue("@gioitinh", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@gioitinh", false);
                        }
                        cmd.Parameters.AddWithValue("@diachi", txtDiaChiKH.Text);
                        cmd.Parameters.AddWithValue("@loaikhach", cboLoaiKhach.SelectedItem.ToString());

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            dinhNghiaThongBao("Sửa thành công", "Thông báo", 2000);
                            ClearFields();
                            LoadKhachHang();
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

        
        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn có chắc chắn muón xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                string ma = lvKhachHang.SelectedItems[0].SubItems[0].Text;
                using (SqlConnection conn = Connection.getConnection())
                {
                    try
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("sp_XoaKH", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@makh", ma);

                            int i = cmd.ExecuteNonQuery();
                            if (i > 0)
                            {
                                dinhNghiaThongBao("Xóa thành công", "Thông báo", 2000);
                                ClearFields();
                                LoadKhachHang();
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
                            MessageBox.Show("Không thể xóa khách hàng này vì vẫn còn dữ liệu liên quan trong hệ thống.",
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


        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void txtHoTenKH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtHoTenKH.Text))
            {
               
                errorProvider1.SetError(txtHoTenKH, "Vui lòng nhập tên khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtHoTenKH, null);

            }
        }

        private void txtHoTenKH_TextChanged(object sender, EventArgs e)
        {
            //btnTim_Click(sender, e);
        }

        private void txtSdtKH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSdtKH.Text))
            {

                errorProvider1.SetError(txtSdtKH, "Vui lòng nhập số điện thoại khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtSdtKH, null);

            }
        }

        private void txtEmailKH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailKH.Text))
            {
                errorProvider1.SetError(txtEmailKH, "Vui lòng nhập email khách hàng");
            }
            else
            {
                errorProvider1.SetError (txtEmailKH, null);
            }
        }

        private void txtDiaChiKH_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDiaChiKH.Text))
            {
                errorProvider1.SetError(txtDiaChiKH, "Vui lòng nhập địa chỉ khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtDiaChiKH , null);
            }
        }

        private void groupBox2_Validating(object sender, CancelEventArgs e)
        {
            if (!rabNam.Checked && !rabNu.Checked)
            {
                errorProvider1.SetError(groupBox2, "Vui lòng chọn 1 giới tính");
            }
            else
            {
                errorProvider1.SetError(groupBox2 , null);
            }
        }

        private void txtLoaiKH_Validating(object sender, CancelEventArgs e)
        {
            
        }


        

        private void btnTim_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = Connection.getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_TimKiemKH", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    string hoTen = txtHoTenKH.Text.Trim();
                    command.Parameters.AddWithValue("@hoten", string.IsNullOrWhiteSpace(hoTen) ? (object)DBNull.Value : hoTen);
                    command.Parameters.AddWithValue("@sdt", string.IsNullOrWhiteSpace(txtSdtKH.Text) ? (object)DBNull.Value : txtSdtKH.Text);
                    command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(txtEmailKH.Text) ? (object)DBNull.Value : txtEmailKH.Text);

                    if (rabNam.Checked)
                        command.Parameters.AddWithValue("@gioitinh", true);
                    else if (rabNu.Checked)
                        command.Parameters.AddWithValue("@gioitinh", false);
                    else
                        command.Parameters.AddWithValue("@gioitinh", (object)DBNull.Value);

                    command.Parameters.AddWithValue("@diachi", string.IsNullOrWhiteSpace(txtDiaChiKH.Text) ? (object)DBNull.Value : txtDiaChiKH.Text);
                    command.Parameters.AddWithValue("@loaikhach", cboLoaiKhach.SelectedItem == null ? (object)DBNull.Value : cboLoaiKhach.SelectedItem.ToString());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        var items = new List<ListViewItem>(); // Danh sách tạm để chứa dữ liệu mới

                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["sMaKhach"].ToString()); // Cột ID
                            item.SubItems.Add(reader["sHoTen"].ToString());
                            item.SubItems.Add(reader["sSoDienThoai"].ToString());
                            item.SubItems.Add(reader["sEmail"].ToString());
                            bool gioiTinh = Convert.ToBoolean(reader["bGioiTinh"]);
                            item.SubItems.Add(gioiTinh ? "Nam" : "Nữ");
                            item.SubItems.Add(reader["sDiaChi"].ToString());
                            item.SubItems.Add(reader["sLoaiKhach"].ToString());

                            items.Add(item);
                        }

                        lvKhachHang.BeginUpdate();
                        lvKhachHang.Items.Clear();
                        lvKhachHang.Items.AddRange(items.ToArray()); // Thêm tất cả vào một lần
                        lvKhachHang.EndUpdate();
                    }
                }
            }
        }

        //private DataTable BangKhachHang()
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("Mã khách hàng");
        //    dt.Columns.Add("Tên khách hàng");
        //    dt.Columns.Add("Số điện thoại");
        //    dt.Columns.Add("Email");
        //    dt.Columns.Add("Giới tính");
        //    dt.Columns.Add("Địa chỉ");
        //    return dt;
        //}

        //public DataTable GetKhachHangFromLV()
        //{
        //    DataTable dt = BangKhachHang();
        //    foreach(ListViewItem lv in lvKhachHang.SelectedItems)
        //    {
        //        DataRow row = dt.NewRow();
        //        row["Mã khách hàng"] = lv.SubItems[0].Text;
        //        row["Tên khách hàng"] = lv.SubItems[1].Text;
        //        row["Số điện thoại"] = lv.SubItems[2].Text;
        //        row["Email"] = lv.SubItems[3].Text;
        //        row["Giới tính"] = lv.SubItems[4].Text;
        //        row["Địa chỉ"] = lv.SubItems[5].Text;

        //        dt.Rows.Add(row);
        //    }
        //    return dt;
        //}

        private void ClearFields()
        {
            txtHoTenKH.Text = "";
            txtEmailKH.Text = "";
            txtSdtKH.Text = "";
            txtDiaChiKH.Text = "";
            rabNam.Checked = false;
            rabNu.Checked = false;
            cboLoaiKhach.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadKhachHang();
        }

        private void groupBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTim_TextChanged(object sender, EventArgs e)
        {
            //btnTim_Click(sender, e);
        }

        private void txtSdtKH_TextChanged(object sender, EventArgs e)
        {
            //btnTim_Click(sender, e);
        }

        private void txtEmailKH_TextChanged(object sender, EventArgs e)
        {
            //btnTim_Click(sender,e);
        }

        private void txtDiaChiKH_TextChanged(object sender, EventArgs e)
        {
            //btnTim_Click(sender, e) ;
        }

        private void groupBox2_TextChanged(object sender, EventArgs e)
        {
            //btnTim_Click(sender,e);
        }

        private void cboLoaiKhach_TextChanged(object sender, EventArgs e)
        {
            //btnTim_Click(sender, e);
        }

        //lấy danh sách mã khách được chon

        private void btnIn_Click(object sender, EventArgs e)
        {
            List<string> selectedMaKH = new List<string>();

            foreach (ListViewItem item in lvKhachHang.SelectedItems)
            {
                selectedMaKH.Add(item.SubItems[0].Text); // Giả sử mã KH nằm ở cột 0
            }

            if (lvKhachHang.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một khách hàng!", "Thông báo");
                return;
            }
            string maKH = txtMaKH.Text.Trim();

            

            frmHienKhachHangRpt frm = new frmHienKhachHangRpt();
            if (string.IsNullOrEmpty(txtMaKH.Text))
            {
                frm.maKH = null;
            }
            else
            {
                frm.maKH = txtMaKH.Text;
            }

            frm.Show();

           
        }
    }
}
