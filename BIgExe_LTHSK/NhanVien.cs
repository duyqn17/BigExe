using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class NhanVien
    {
        public string maNV {  get; set; }
        public string tenNV { get; set; }
        public string chucVu { get; set; }
        public string sdt {  get; set; }
        public string email { get; set; }
        public float luong { get; set; }

        public NhanVien()
        {
        }

        public NhanVien(string maNV, string tenNV, string chucVu, string sdt, string email, float luong)
        {
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.chucVu = chucVu;
            this.sdt = sdt;
            this.email = email;
            this.luong = luong;
        }
    }
}
