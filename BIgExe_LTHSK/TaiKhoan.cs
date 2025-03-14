﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIgExe_LTHSK
{
    public class TaiKhoan
    {
        public string maTK {  get; set; }
        public string maNV { get; set; }
        public string tenTK { get; set; }
        public string matKhau { get; set; }
        public string email { get; set; }

        public TaiKhoan()
        {
        }

        public TaiKhoan(string tenTK, string matKhau)
        {
            this.tenTK = tenTK;
            this.matKhau = matKhau;
        }

        public TaiKhoan(string maTK, string tenTK, string matKhau, string email)
        {
            this.maTK = maTK;
            this.tenTK = tenTK;
            this.matKhau = matKhau;
            this.email = email;
        }
    }
}
