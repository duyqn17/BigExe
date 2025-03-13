using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class KhachHang
    {
        public string maKH {  get; set; }
        public string hoTen {  get; set; }
        public string soDienThoai { get; set; }
        public string email { get; set; }
        public bool gioiTinh { get; set; }
        public string diaChi { get; set; }
        public string loaiKhach {  get; set; }

        public KhachHang()
        {
        }

        public KhachHang(string maKH, string hoTen, string soDienThoai, string email, bool gioiTinh, string diaChi, string loaiKhach)
        {
            this.maKH = maKH;
            this.hoTen = hoTen;
            this.soDienThoai = soDienThoai;
            this.email = email;
            this.gioiTinh = gioiTinh;
            this.diaChi = diaChi;
            this.loaiKhach = loaiKhach;
        }


    }
}
