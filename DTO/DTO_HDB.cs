using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DTO_HDB
    {
        private string mahbd;
        private string manv;
        private string makh;
        private string ngaylaphd;
        private string sl;
        private string tongtien;
        private string hinhthuctt;
        private string dathanhtoan;
        private string conno;

        public string Mahbd { get => mahbd; set => mahbd = value; }
        public string Manv { get => manv; set => manv = value; }
        public string Makh { get => makh; set => makh = value; }
        public string Ngaylaphd { get => ngaylaphd; set => ngaylaphd = value; }
        public string Tongtien { get => tongtien; set => tongtien = value; }
        public string Hinhthuctt { get => hinhthuctt; set => hinhthuctt = value; }
        public string Dathanhtoan { get => dathanhtoan; set => dathanhtoan = value; }
        public string Conno { get => conno; set => conno = value; }
        public string Sl { get => sl; set => sl = value; }

        public DTO_HDB() { }

        public DTO_HDB(string idhdb, string manv, string makh, string ngaylaphd, string sl, string tongtien, string hinhthuctt, string dathanhtoan, string conno)
        {
            Mahbd = idhdb;
            Manv = manv;
            Makh = makh;
            Ngaylaphd = ngaylaphd;
            Sl = sl;
            Tongtien = tongtien;
            Hinhthuctt = hinhthuctt;
            Dathanhtoan = dathanhtoan;
            Conno = conno;
        }
    }
}
