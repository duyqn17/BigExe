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
    public partial class frmBaoCaoHoaDon : Form
    {
        public frmBaoCaoHoaDon()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //ReportDocument rptDoc = new ReportDocument();
            //rptDoc.Load(@"D:\BTL_LTHSK\Hoa-Don.rpt"); // Đường dẫn file RPT của bạn
            //crystalReportViewer1.ReportSource = rptDoc;
            //crystalReportViewer1.Refresh();

        }

        private void btnLoadHD_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHD.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                ReportDocument rpt = new ReportDocument();
                //rpt.SetDatabaseLogon("sa", "123456", "ADMIN/SQLEXPRESS", "QuanLyThuePhongKhachSan1");
                rpt.Load(@"D:\c#\BIgExe_LTHSK\BIgExe_LTHSK\BaoCaoHoaDon.rpt"); // Đường dẫn file Crystal Report

                rpt.SetParameterValue("pMaHoaDon", txtMaHD.Text.Trim());

                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}
