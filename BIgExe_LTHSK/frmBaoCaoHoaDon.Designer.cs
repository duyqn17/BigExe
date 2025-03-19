namespace BIgExe_LTHSK
{
    partial class frmBaoCaoHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.btnLoadHD = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(800, 450);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(52, 59);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(100, 22);
            this.txtMaHD.TabIndex = 1;
            // 
            // btnLoadHD
            // 
            this.btnLoadHD.Location = new System.Drawing.Point(43, 104);
            this.btnLoadHD.Name = "btnLoadHD";
            this.btnLoadHD.Size = new System.Drawing.Size(123, 29);
            this.btnLoadHD.TabIndex = 2;
            this.btnLoadHD.Text = "LoadHoaDon";
            this.btnLoadHD.UseVisualStyleBackColor = true;
            this.btnLoadHD.Click += new System.EventHandler(this.btnLoadHD_Click);
            // 
            // frmBaoCaoHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLoadHD);
            this.Controls.Add(this.txtMaHD);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "frmBaoCaoHoaDon";
            this.Text = "frmBaoCaoThongKe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Button btnLoadHD;
    }
}