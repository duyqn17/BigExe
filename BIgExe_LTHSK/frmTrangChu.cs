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
        }

        

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
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls[0].Dispose();
            }
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None;

            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void OpenChildFormCustomer(Form childForm)
        {
            // Đóng Form con hiện tại (nếu có)
            if (panel3.Controls.Count > 0)
                panel3.Controls[0].Dispose();

            // Cấu hình Form con
            childForm.TopLevel = false; // Không phải form độc lập
            childForm.FormBorderStyle = FormBorderStyle.None; // Ẩn viền
            childForm.Dock = DockStyle.Fill; // Phủ toàn bộ Panel

            // Thêm vào Panel chứa
            panel3.Controls.Add(childForm);
            panel3.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void OpenChildFormEmployee(Form childForm)
        {
            if(panel3.Controls.Count > 0)
            {
                panel3.Controls[0].Dispose();
            }
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel3.Controls.Add(childForm);
            panel3.BringToFront();
            childForm.Show();
        }

        private void OpenChildFormService(Form childForm)
        {
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls[0].Dispose();
            }
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel3.Controls.Add(childForm);
            panel3.BringToFront();
            childForm.Show();
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
            OpenChildFormRoom(frmDichVu);
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
            if (panel3.Controls.Count > 0)
            {
                panel3.Controls[0].Dispose();
            }
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panel3.Controls.Add(childForm);
            panel3.BringToFront();
            childForm.Show();
        }

        private void btnThanhToanHD_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon();
            OpenChildFormBill (frm);
        }
    }
}
