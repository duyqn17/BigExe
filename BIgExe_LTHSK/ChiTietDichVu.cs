using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class ChiTietDichVu
    {
        public string maCTDV {  get; set; }
        public string maDK { get; set; }
        public string maDV {  get; set; }
        public int soLuong { get; set; }
        public DateTime ngaySD { get; set; }
        public string ghiChu { get; set; }

        public ChiTietDichVu()
        {
        }

        public ChiTietDichVu(string maCTDV, string maDK, string maDV, int soLuong, DateTime ngaySD, string ghiChu)
        {
            this.maCTDV = maCTDV;
            this.maDK = maDK;
            this.maDV = maDV;
            this.soLuong = soLuong;
            this.ngaySD = ngaySD;
            this.ghiChu = ghiChu;
        }
    }
}
