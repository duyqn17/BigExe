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
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            LoadHoaDon();
        }
        private void LoadHoaDon()
        {
            lvHoaDon.Items.Clear();
            Modify modify = new Modify();
            string query = "select * from tblHoaDon";
            List<HoaDon> hoaDons = modify.getHoaDons(query);

            foreach (var hd in hoaDons)
            {
                ListViewItem item = new ListViewItem(hd.maHD.ToString());
                item.SubItems.Add(hd.maDK.ToString());
                item.SubItems.Add(hd.ngayLap.ToString());
                item.SubItems.Add(hd.tongTien.ToString());
      
                lvHoaDon.Items.Add(item);
            }
        }
    }
}
