namespace DTO
{
    public class DTO_SP
    {
        private string masp;
        private string mancu;
        private string tensp;
        private string ngaysx;
        private string ngayhh;
        private string ngaynhap;
        private string nhasx;
        private string slnhap;
        private string dongianhap;
        private string dongiabanle;
        private string dongiabansi;
        private string donvi;
        private string giamgia;
        private string anhsp;

        public string Masp { get => masp; set => masp = value; }
        public string Mancu { get => mancu; set => mancu = value; }
        public string Tensp { get => tensp; set => tensp = value; }
        public string Ngaysx { get => ngaysx; set => ngaysx = value; }
        public string Ngayhh { get => ngayhh; set => ngayhh = value; }
        public string Ngaynhap { get => ngaynhap; set => ngaynhap = value; }
        public string Nhasx { get => nhasx; set => nhasx = value; }
        public string Slnhap { get => slnhap; set => slnhap = value; }
        public string Dongianhap { get => dongianhap; set => dongianhap = value; }
        public string Dongiabanle { get => dongiabanle; set => dongiabanle = value; }
        public string Dongiabansi { get => dongiabansi; set => dongiabansi = value; }
        public string Donvi { get => donvi; set => donvi = value; }
        public string Giamgia { get => giamgia; set => giamgia = value; }
        public string Anhsp { get => anhsp; set => anhsp = value; }

        public DTO_SP() { }

        public DTO_SP(string masp, string mancu, string tensp, string ngaysx, string ngayhh, string ngaynhap, string nhasx, string slnhap, string dongianhap, string dongiabanle, string dongiabansi, string donvi, string giamgia, string anhsp)
        {
            Masp = masp;
            Mancu = mancu;
            Tensp = tensp;
            Ngaysx = ngaysx;
            Ngayhh = ngayhh;
            Ngaynhap = ngaynhap;
            Nhasx = nhasx;
            Slnhap = slnhap;
            Dongianhap = dongianhap;
            Dongiabanle = dongiabanle;
            Dongiabansi = dongiabansi;
            Donvi = donvi;
            Giamgia = giamgia;
            Anhsp = anhsp;
        }
    }
}
