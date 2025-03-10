using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class HoaDon
    {
        public int maHD {  get; set; }
        public int maDK { get; set; }
        public DateTime ngayLap { get; set; }
        public float tongTien { get; set; }

        public HoaDon()
        {
        }

        public HoaDon(int maHD, int maDK, DateTime ngayLap, float tongTien)
        {
            this.maHD = maHD;
            this.maDK = maDK;
            this.ngayLap = ngayLap;
            this.tongTien = tongTien;
        }

    }
}
