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
                item.SubItems.Add(kh.gioiTinh);
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
            using(SqlConnection conn = Connection.getConnection())
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
                    cmd.Parameters.AddWithValue("@loaikhach", txtLoaiKH.Text);
                    
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Đã thêm thành công khách hàng '" + txtHoTenKH + "' vào danh sách");
                    }
                }
            }
        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {

        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {

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
            if (string.IsNullOrEmpty(txtLoaiKH.Text))
            {
                errorProvider1.SetError(txtLoaiKH, "Vui lòng nhập loại khách hàng");
            }
            else
            {
                errorProvider1.SetError(txtLoaiKH , null);
            }
        }
    }
}
