using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class DatPhong
    {
        public string maDP {  get; set; }
        public string maKH {  get; set; }
        public string maPhong { get; set; }
        public string maNV { get; set; }
        public DateTime ngayNhan { get; set; }
        public DateTime ngayTra {  get; set; }
        public string trangThai { get; set; }

        public DatPhong()
        {
        }

        public DatPhong(string maDP, string maKH, string maPhong, string maNV, DateTime ngayNhan, DateTime ngayTra, string trangThai)
        {
            this.maDP = maDP;
            this.maKH = maKH;
            this.maPhong = maPhong;
            this.maNV = maNV;
            this.ngayNhan = ngayNhan;
            this.ngayTra = ngayTra;
            this.trangThai = trangThai;
        }
    }
}
