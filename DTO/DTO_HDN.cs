namespace DTO
{
    public class DTO_HDN
    {
        private string mahdn;
        private string manv;
        private string mancu;
        private string ngaylaphd;
        private string sl;
        private string tongtien;
        private string dathanhtoan;
        private string conno;
        private string ngayhhtt;

        public string Mahdn { get => mahdn; set => mahdn = value; }
        public string Manv { get => manv; set => manv = value; }
        public string Mancu { get => mancu; set => mancu = value; }
        public string Ngaylaphd { get => ngaylaphd; set => ngaylaphd = value; }
        public string Tongtien { get => tongtien; set => tongtien = value; }
        public string Dathanhtoan { get => dathanhtoan; set => dathanhtoan = value; }
        public string Conno { get => conno; set => conno = value; }
        public string Ngayhhtt { get => ngayhhtt; set => ngayhhtt = value; }
        public string Sl { get => sl; set => sl = value; }

        public DTO_HDN() { }

        public DTO_HDN(string mahdn, string manv, string mancu, string ngaylaphd, string sl, string tongtien, string dathanhtoan, string conno, string ngayhhtt)
        {
            Mahdn = mahdn;
            Manv = manv;
            Mancu = mancu;
            Ngaylaphd = ngaylaphd;
            Sl = sl;
            Tongtien = tongtien;
            Dathanhtoan = dathanhtoan;
            Conno = conno;
            Ngayhhtt = ngayhhtt;
        }
    }
}
