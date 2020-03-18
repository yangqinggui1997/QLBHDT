namespace DTO
{
    public class DTO_NV
    {
        private string manv;
        private string ten;
        private string ngaysinh;
        private string gioitinh;
        private string sdt;
        private string diachi;
        private string chucvu;
        private string luongcb;
        private string hsl;
        private string thuclinh;
        private string taikhoan;

        public string Manv { get => manv; set => manv = value; }
        public string Ten { get => ten; set => ten = value; }
        public string Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Chucvu { get => chucvu; set => chucvu = value; }
        public string Taikhoan { get => taikhoan; set => taikhoan = value; }
        public string Luongcb { get => luongcb; set => luongcb = value; }
        public string Hsl { get => hsl; set => hsl = value; }
        public string Thuclinh { get => thuclinh; set => thuclinh = value; }

        public DTO_NV() { }

        public DTO_NV(string manv, string ten, string ngaysinh, string gioitinh, string sdt, string diachi, string chucvu, string luongcb, string hsl, string thuclinh, string taikhoan)
        {
            Manv = manv;
            Ten = ten;
            Ngaysinh = ngaysinh;
            Gioitinh = gioitinh;
            Sdt = sdt;
            Diachi = diachi;
            Chucvu = chucvu;
            Luongcb = luongcb;
            Hsl = hsl;
            Thuclinh = thuclinh;
            Taikhoan = taikhoan;
        }
    }
}
