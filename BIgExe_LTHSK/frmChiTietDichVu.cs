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
    public partial class frmChiTietDichVu : Form
    {
        public frmChiTietDichVu()
        {
            InitializeComponent();
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            LoadCTDV();
        }

        private void LoadCTDV()
        {
            lvChiTietDV.Items.Clear();
            Modify modify = new Modify();
            List<ChiTietDichVu> chiTietDichVus = modify.getChiTietDichVus();

            foreach (ChiTietDichVu ctdv in chiTietDichVus)
            {
                ListViewItem lvi = new ListViewItem(ctdv.maCTDV);
                lvi.SubItems.Add(ctdv.maDK);
                lvi.SubItems.Add(ctdv.maDV);
                lvi.SubItems.Add(ctdv.soLuong.ToString());
                lvi.SubItems.Add(ctdv.ngaySD.ToString());
                lvi.SubItems.Add(ctdv.ghiChu);

                lvChiTietDV.Items.Add(lvi);
            }
        }
    }
}
