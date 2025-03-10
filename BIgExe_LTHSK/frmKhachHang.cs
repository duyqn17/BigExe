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
using System.Xml.Linq;

namespace BIgExe_LTHSK
{
    public partial class frmKhachHang : Form
    {
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
            string query = "select * from tblKhach";
            List<KhachHang> khachHangs = modify.getKhachHangs(query);

            foreach (var kh in khachHangs)
            {
                ListViewItem item = new ListViewItem(kh.maKH.ToString());
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
                            MessageBox.Show("Đã thêm thành công khách hàng '" + txtHoTenKH.Text + "' vào danh sách", "Thông báo", MessageBoxButtons.OK);
                            ClearFields();
                        }
                        else
                        {
                            MessageBox.Show("Thêm thất bại.");
                        }
                    }
                }
            }
            catch { }
        }

        int ma = -1;
        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            ma = int.Parse(lvKhachHang.SelectedItems[0].SubItems[0].Text);
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
                        MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK);
                        ClearFields();
                        LoadKhachHang();
                    }else
                    {
                        MessageBox.Show("Sửa thất bại", "Thông báo", MessageBoxButtons.OK);
                    }    
                }
                connection.Close();
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            int ma = int.Parse(lvKhachHang.SelectedItems[0].SubItems[0].Text);
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
                            MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK);
                            ClearFields();
                            LoadKhachHang();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK);
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
            //try
            //{
            //    if (txtHoTenKH.Text.Trim() != string.Empty)
            //    {
            //        errorProvider1.Clear();
            //    }
            //}
            //catch {
            //    errorProvider1.SetError(txtHoTenKH, "Họ tên đang trống");
            //}
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
            lvKhachHang.Items.Clear();
            using (SqlConnection connection = Connection.getConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_TimKiemKH", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    string hoTen = txtHoTenKH.Text.Trim();
                    command.Parameters.AddWithValue("@hoten", string.IsNullOrEmpty(hoTen) ? "" : hoTen);

                    //command.Parameters.AddWithValue("@hoten", string.IsNullOrWhiteSpace(txtHoTenKH.Text) ? (object)DBNull.Value : txtHoTenKH.Text);
                    command.Parameters.AddWithValue("@sdt", string.IsNullOrWhiteSpace(txtSdtKH.Text) ? (object)DBNull.Value : txtSdtKH.Text);
                    command.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(txtEmailKH.Text) ? (object)DBNull.Value : txtEmailKH.Text);
                    

                    if (rabNam.Checked)
                    {
                        
                        command.Parameters.AddWithValue("@gioitinh", true);
                    }
                    else
                    {
                       
                        command.Parameters.AddWithValue("@gioitinh", false);
                    }

                    command.Parameters.AddWithValue("@diachi", string.IsNullOrWhiteSpace(txtDiaChiKH.Text) ? (object)DBNull.Value : txtDiaChiKH.Text);
                   
                    command.Parameters.AddWithValue("@loaikhach", cboLoaiKhach.SelectedItem == null ? (object)DBNull.Value : cboLoaiKhach.SelectedItem.ToString());


                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["PK_iMaKhach"].ToString()); // Cột ID
                            item.SubItems.Add(reader["sHoTen"].ToString());  // Họ tên
                            item.SubItems.Add(reader["sSoDienThoai"].ToString()); // Số điện thoại
                            item.SubItems.Add(reader["sEmail"].ToString());  // Email
                            bool gioiTinh = Convert.ToBoolean(reader["bGioiTinh"]);
                            item.SubItems.Add(gioiTinh ? "Nam" : "Nữ");
                            item.SubItems.Add(reader["sDiaChi"].ToString());  // Địa chỉ
                            item.SubItems.Add(reader["sLoaiKhach"].ToString());  // Loại khách

                            // Thêm vào ListView
                            lvKhachHang.Items.Add(item);
                        }
                    }

                }
                connection.Close();
            }
        }

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
    }
}
