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
    public partial class frmDichVu : Form
    {
        public frmDichVu()
        {
            InitializeComponent();
        }

        private void frmDichVu_Load(object sender, EventArgs e)
        {
            HienThiDangKy();
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from tblDichVu", conn))
                {
                    lvDichVu.Items.Clear();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem lvi = new ListViewItem(reader.GetInt32(0) + "");
                            lvi.SubItems.Add(reader.GetString(1));
                            lvi.SubItems.Add(reader.GetDouble(2) + "");
                            lvDichVu.Items.Add(lvi);
                        }
                    }
                }
            }
        }

        private void HienThiDangKy()
        {
            using (SqlConnection conn = Connection.getConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from tblChiTietDichVu", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ma = reader.GetInt32(0);
                            cboDatPhong.Items.Add(ma);
                        }
                        
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void cboDatPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboDatPhong.SelectedIndex == -1)
            {
                return;
            }
            int ma = int.Parse(cboDatPhong.SelectedItem.ToString());
            
        }

        private void cboDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void lvDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lvDichVu.SelectedItems.Count == 0)
            {
                return ;
            }
            ListViewItem lvi = lvDichVu.SelectedItems[0];
            txtMaDV.Text = lvi.SubItems[0].Text;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
