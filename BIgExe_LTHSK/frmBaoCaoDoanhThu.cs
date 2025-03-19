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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;

namespace BIgExe_LTHSK
{
    public partial class frmBaoCaoDoanhThu : Form
    {
        private ReportDocument reportDocument;
        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void LoadReportData()
        {
            try
            {
                using (SqlConnection conn = Connection.getConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetReportData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = dtpNgayBD.Value.Date });
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = dtpNgayKT.Value.Date });

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("Không có dữ liệu trong khoảng thời gian này!");
                        }

                        dgvDoanhThu.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            dtpNgayBD.Value = DateTime.Today.AddDays(-7); // Mặc định 7 ngày trước
            dtpNgayKT.Value = DateTime.Today;
            LoadReportData();
            LoadCrystalReport();
        }

        private void LoadCrystalReport()
        {
            try
            {
                // Giải phóng tài nguyên báo cáo cũ nếu có
                if (reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                }

                // Tạo đối tượng báo cáo mới
                reportDocument = new ReportDocument();
                reportDocument.Load(@"D:\BTL_LTHSK\ReportDoanhThu.rpt");

                // Tạo dataset và điền dữ liệu
                DataSet ds = new DataSet();
                using (SqlConnection conn = Connection.getConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetReportData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Sử dụng tham số với kiểu dữ liệu chính xác
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = dtpNgayBD.Value.Date });
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = dtpNgayKT.Value.Date });

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds, "ReportData");
                    }
                }

                if (ds.Tables["ReportData"].Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để hiển thị trong báo cáo!");
                    return;
                }

                // Gán dữ liệu vào Crystal Report
                reportDocument.SetDataSource(ds.Tables["ReportData"]);

                // Gán tham số vào báo cáo - lưu ý bỏ ký tự @ ở tên tham số
                try
                {
                    // Cách 1: Theo vị trí tham số
                    reportDocument.SetParameterValue(0, dtpNgayBD.Value.Date);
                    reportDocument.SetParameterValue(1, dtpNgayKT.Value.Date);
                }
                catch (Exception)
                {
                    try
                    {
                        // Cách 2: Theo tên tham số (không có @)
                        reportDocument.SetParameterValue("StartDate", dtpNgayBD.Value.Date);
                        reportDocument.SetParameterValue("EndDate", dtpNgayKT.Value.Date);
                    }
                    catch (Exception)
                    {
                        // Cách 3: Thử lại với tên đầy đủ có @
                        reportDocument.SetParameterValue("@StartDate", dtpNgayBD.Value.Date);
                        reportDocument.SetParameterValue("@EndDate", dtpNgayKT.Value.Date);
                    }
                }

                // Gán vào CrystalReportViewer
                crystalReportViewer1.ReportSource = reportDocument;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace,
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
            //ReportDocument rptDoc = new ReportDocument();
            //rptDoc.Load(@"D:\BTL_LTHSK\ReportDoanhThu.rpt"); // Đường dẫn file RPT của bạn
            //crystalReportViewer1.ReportSource = rptDoc;
            //crystalReportViewer1.Refresh();ư

            ReportDocument rptDoc = new ReportDocument();
            rptDoc.Load(@"D:\BTL_LTHSK\ReportDoanhThu.rpt"); // Thay tên file

            // Cấu hình thông tin đăng nhập database
            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.ServerName = @"ADMIN\SQLEXPRESS"; // Tên SQL Server của bạn
            connInfo.DatabaseName = "QuanLyThuePhongKhachSan1"; // Tên database
            connInfo.UserID = "sa"; // User đăng nhập SQL
            connInfo.Password = "123456"; // Mật khẩu

            // Áp dụng thông tin kết nối
            TableLogOnInfo tableLogonInfo = new TableLogOnInfo();
            tableLogonInfo.ConnectionInfo = connInfo;

            foreach (Table table in rptDoc.Database.Tables)
            {
                table.ApplyLogOnInfo(tableLogonInfo);
                table.LogOnInfo.ConnectionInfo.IntegratedSecurity = false;
            }

            crystalReportViewer1.ReportSource = rptDoc;
            crystalReportViewer1.Refresh();
        }
    }
}
