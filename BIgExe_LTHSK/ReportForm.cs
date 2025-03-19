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

namespace BIgExe_LTHSK
{
    public partial class ReportForm : Form
    {

        string connectionString = "Server=ADMIN\\SQLEXPRESS;Database=QuanlyThuePhongKhachSan1;Integrated Security=True";
        private ReportDocument reportDocument;
        public ReportForm()
        {
            InitializeComponent();
            LoadReportData();
        }
        private void LoadReportData()
        {
            crystalReportViewer1.BringToFront();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GetReportData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = dateNgayBD.Value.Date });
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = dateNgayKT.Value.Date });

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        if (dt.Rows.Count == 0)
                        {

                        }

                        dataGridViewReport.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadReportData();
            LoadCrystalReport();
        }

        private void LoadCrystalReport()
        {
            try
            {
                if(reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                }
                reportDocument = new ReportDocument();
                reportDocument.Load(@"D:\c#\BIgExe_LTHSK\BIgExe_LTHSK\ReportDoanhThu.rpt");
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetReportData", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Sử dụng tham số với kiểu dữ liệu chính xác
                        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = dateNgayBD.Value.Date });
                        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = dateNgayKT.Value.Date });

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds, "ReportData");
                    }
                }
                if (ds.Tables["ReportData"].Rows.Count == 0)
                {


                }

                // Gán dữ liệu vào Crystal Report
                reportDocument.SetDataSource(ds.Tables["ReportData"]);

                // Gán tham số vào báo cáo - lưu ý bỏ ký tự @ ở tên tham số
                try
                {
                    // Cách 1: Theo vị trí tham số
                    reportDocument.SetParameterValue(0, dateNgayBD.Value.Date);
                    reportDocument.SetParameterValue(1, dateNgayKT.Value.Date);
                }
                catch (Exception)
                {
                    try
                    {
                        // Cách 2: Theo tên tham số (không có @)
                        reportDocument.SetParameterValue("StartDate", dateNgayBD.Value.Date);
                        reportDocument.SetParameterValue("EndDate", dateNgayKT.Value.Date);
                    }
                    catch (Exception)
                    {
                        // Cách 3: Thử lại với tên đầy đủ có @
                        reportDocument.SetParameterValue("@StartDate", dateNgayBD.Value.Date);
                        reportDocument.SetParameterValue("@EndDate", dateNgayKT.Value.Date);
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

    
            
            
        

        private void ReportForm_Load(object sender, EventArgs e)
        {
            dateNgayBD.Value = DateTime.Today.AddDays(-7);
            dateNgayKT.Value = DateTime.Today;
            LoadReportData();
            LoadCrystalReport();

        }
    }
}
