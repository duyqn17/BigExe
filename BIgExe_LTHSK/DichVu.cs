using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class DichVu
    {
        public int maDV {  get; set; }
        public string tenDV { get; set; }
        public float donGia { get; set; }

        public DichVu()
        {
        }

        public DichVu(int maDV, string tenDV, float donGia)
        {
            this.maDV = maDV;
            this.tenDV = tenDV;
            this.donGia = donGia;
        }
    }
}
