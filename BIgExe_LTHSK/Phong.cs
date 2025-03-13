using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class Phong
    {
        public string maPhong {  get; set; }
        public string loaiPhong { get; set; }
        public float giaPhong { get; set; }
        public string tinhTrang { get; set; }

        public Phong()
        {
        }

        public Phong(string maPhong, string loaiPhong, float giaPhong, string tinhTrang)
        {
            this.maPhong = maPhong;
            this.loaiPhong = loaiPhong;
            this.giaPhong = giaPhong;
            this.tinhTrang = tinhTrang;
        }
    }
}
