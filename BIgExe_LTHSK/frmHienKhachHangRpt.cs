using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace BIgExe_LTHSK
{
    public partial class frmHienKhachHangRpt : Form
    {
        public string maKH {  get; set; }
        public List<string> selectedMaKH { get; set; }
        public frmHienKhachHangRpt()
        {
            InitializeComponent();
            
        }

        public void crystalReportViewer1_Load(object sender, EventArgs e)
        {
           

            try
            {
                // Tạo đối tượng ReportDocument
                ReportDocument report = new ReportDocument();
                string reportPath = @"D:\c#\BIgExe_LTHSK\BIgExe_LTHSK\KhachHangRpt.rpt";
                report.Load(reportPath);

                // Kết nối database
                ConnectionInfo connInfo = new ConnectionInfo();
                connInfo.ServerName = "admin\\SQLEXPRESS";
                connInfo.DatabaseName = "QuanLyThuePhongKhachSan1";
                connInfo.UserID = "sa";
                connInfo.Password = "123456";

                // Gán thông tin đăng nhập cho tất cả bảng trong report
                foreach (Table table in report.Database.Tables)
                {
                    TableLogOnInfo logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = connInfo;
                    table.ApplyLogOnInfo(logonInfo);
                }
                if (string.IsNullOrEmpty(maKH.ToString()))
                {
                    report.SetParameterValue("prmMaKH", 0);
                }
                else
                {
                    report.SetParameterValue("prmMaKH", maKH.ToString());
                }

                //Gán tham số vào báo cáo(lọc theo tên khách hàng)

                // Hiển thị báo cáo
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message);
            }
        }


        


 
        private void btnIn_Click(object sender, EventArgs e)
        {
            
        }
    }
}
