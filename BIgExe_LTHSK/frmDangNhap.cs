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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void llbDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy dk = new frmDangKy();
            dk.Show();
        }

        private void llbQuenMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmQuenMatKhau qmk = new frmQuenMatKhau();
            qmk.Show();
        }
        Modify modify = new Modify();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            
            string tenTK = txtTenDN.Text;
            string matKhau = txtMatKhau.Text;
            if (tenTK.Trim() == "")
            {
                lblThongbaodangnhap.Text = "Tên đăng nhập đang để trống";
                lblThongbaodangnhap.Show();
                //MessageBox.Show("Tên tài khoản đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if(matKhau.Trim() == "")
            {
                
                lblThongbaodangnhap.Text = "Mật khẩu đang để trống";
                lblThongbaodangnhap.Show();
                //MessageBox.Show("Mật khẩu đang trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string query = "select * from tblTaiKhoan where sTenTaiKhoan = '"+tenTK+"' and sMatKhau = '"+matKhau+"' ";
                if (modify.taiKhoans(query).Count != 0)
                {
                    //lblThongbaodangnhap.Text = "Đăng nhập thành công";
                    //lblThongbaodangnhap.Show();
                    
                    frmTrangChu trangChu = new frmTrangChu();
                    trangChu.Show();
                    this.Hide();
                }
                else
                {
                    lblThongbaodangnhap.Text = "Tên đăng nhập hoặc mật khẩu không chính xác";
                    lblThongbaodangnhap.Show();
                    //MessageBox.Show("Tên tài khoản hoặc mật khẩu không đúng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

            
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            lblThongbaodangnhap.Hide();
        }
    }
}
