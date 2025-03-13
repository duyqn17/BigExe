using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIgExe_LTHSK
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            
            InitializeComponent();
            this.IsMdiContainer = true;
        }


        private Dictionary<string, Form> openedForms = new Dictionary<string, Form>();
        private Form activeForm = null; // Biến lưu trữ form đang mở



        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btnPhong_Click(object sender, EventArgs e)
        {
            frmPhong frm = new frmPhong();
            OpenChildFormRoom(frm);
        }

        private void OpenChildFormRoom(Form childForm)
        {
            string formName = childForm.GetType().Name;

            // Kiểm tra nếu form đã tồn tại trong Dictionary
            if (openedForms.ContainsKey(formName))
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Hiển thị lại form cũ
                activeForm = openedForms[formName];
                activeForm.Show();
                activeForm.BringToFront();
            }
            else
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Cấu hình Form con
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Thêm vào Panel chứa
                panel3.Controls.Add(childForm);

                // Lưu vào Dictionary để tái sử dụng
                openedForms[formName] = childForm;

                activeForm = childForm;
                childForm.Show();
                childForm.BringToFront();
            }
        }

        

        private void OpenChildFormCustomer(Form childForm)
        {


            string formName = childForm.GetType().Name;

            // Kiểm tra nếu form đã tồn tại trong Dictionary
            if (openedForms.ContainsKey(formName))
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Hiển thị lại form cũ
                activeForm = openedForms[formName];
                activeForm.Show();
                activeForm.BringToFront();
            }
            else
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Cấu hình Form con
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Thêm vào Panel chứa
                panel3.Controls.Add(childForm);

                // Lưu vào Dictionary để tái sử dụng
                openedForms[formName] = childForm;

                activeForm = childForm;
                childForm.Show();
                childForm.BringToFront();
            }
        }

        private void OpenChildFormEmployee(Form childForm)
        {
            
            string formName = childForm.GetType().Name;

            // Kiểm tra nếu form đã tồn tại trong Dictionary
            if (openedForms.ContainsKey(formName))
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Hiển thị lại form cũ
                activeForm = openedForms[formName];
                activeForm.Show();
                activeForm.BringToFront();
            }
            else
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Cấu hình Form con
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Thêm vào Panel chứa
                panel3.Controls.Add(childForm);

                // Lưu vào Dictionary để tái sử dụng
                openedForms[formName] = childForm;

                activeForm = childForm;
                childForm.Show();
                childForm.BringToFront();
            }
        }

        private void OpenChildFormService(Form childForm)
        {
            string formName = childForm.GetType().Name;

            // Kiểm tra nếu form đã tồn tại trong Dictionary
            if (openedForms.ContainsKey(formName))
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Hiển thị lại form cũ
                activeForm = openedForms[formName];
                activeForm.Show();
                activeForm.BringToFront();
            }
            else
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Cấu hình Form con
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Thêm vào Panel chứa
                panel3.Controls.Add(childForm);

                // Lưu vào Dictionary để tái sử dụng
                openedForms[formName] = childForm;

                activeForm = childForm;
                childForm.Show();
                childForm.BringToFront();
            }
        }
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            
            OpenChildFormCustomer(frm);
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien frmNhanVien = new frmNhanVien();
            OpenChildFormEmployee(frmNhanVien);
        }

        private void btnPhong_Click_1(object sender, EventArgs e)
        {
            frmPhong frmPhong = new frmPhong();
            OpenChildFormRoom(frmPhong);
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            frmDichVu frmDichVu = new frmDichVu();
            OpenChildFormService(frmDichVu);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls[0].Dispose();
            }
        }
        private void OpenChildFormBill(Form childForm)
        {
            string formName = childForm.GetType().Name;

            // Kiểm tra nếu form đã tồn tại trong Dictionary
            if (openedForms.ContainsKey(formName))
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Hiển thị lại form cũ
                activeForm = openedForms[formName];
                activeForm.Show();
                activeForm.BringToFront();
            }
            else
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Cấu hình Form con
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Thêm vào Panel chứa
                panel3.Controls.Add(childForm);

                // Lưu vào Dictionary để tái sử dụng
                openedForms[formName] = childForm;

                activeForm = childForm;
                childForm.Show();
                childForm.BringToFront();
            }
        }

        private void btnThanhToanHD_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
            OpenChildFormBill(frm);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                this.Close();
                frmDangNhap frm = new frmDangNhap();
                frm.Show();
            }

        }

        private void frmTrangChu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            //frmKhachHang frm = new frmKhachHang();
            //OpenChildFormCustomer(frm);
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            frmChiTietDichVu frm = new frmChiTietDichVu();
            OpenChildFormCTDV(frm);
        }

        private void OpenChildFormCTDV(Form childForm)
        {
            string formName = childForm.GetType().Name;

            // Kiểm tra nếu form đã tồn tại trong Dictionary
            if (openedForms.ContainsKey(formName))
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Hiển thị lại form cũ
                activeForm = openedForms[formName];
                activeForm.Show();
                activeForm.BringToFront();
            }
            else
            {
                // Ẩn form hiện tại (nếu có)
                if (activeForm != null)
                {
                    activeForm.Hide();
                }

                // Cấu hình Form con
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Thêm vào Panel chứa
                panel3.Controls.Add(childForm);

                // Lưu vào Dictionary để tái sử dụng
                openedForms[formName] = childForm;

                activeForm = childForm;
                childForm.Show();
                childForm.BringToFront();
            }
        }
    }
}
