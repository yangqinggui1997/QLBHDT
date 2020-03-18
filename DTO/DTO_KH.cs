namespace DTO
{
    public class DTO_KH
    {
        private string idkh;
        private string tenkh;
        private string ngaysinh;
        private string gioitinh;
        private string socmnd;
        private string diachi;
        private string sđt;
        private string loaikh;
        private string conno;
        private string danhgia;

        public string Idkh { get => idkh; set => idkh = value; }
        public string Tenkh { get => tenkh; set => tenkh = value; }
        public string Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Socmnd { get => socmnd; set => socmnd = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Sđt { get => sđt; set => sđt = value; }
        public string Loaikh { get => loaikh; set => loaikh = value; }
        public string Conno { get => conno; set => conno = value; }
        public string Danhgia { get => danhgia; set => danhgia = value; }

        public DTO_KH() { }

        public DTO_KH(string idkh, string tenkh, string ngaysinh, string gioitinh, string socmnd, string diachi, string sđt, string loaikh, string conno, string danhgia)
        {
            Idkh = idkh;
            Tenkh = tenkh;
            Ngaysinh = ngaysinh;
            Gioitinh = gioitinh;
            Socmnd = socmnd;
            Diachi = diachi;
            Sđt = sđt;
            Loaikh = loaikh;
            Conno = conno;
            Danhgia = danhgia;
        }
    }
}
