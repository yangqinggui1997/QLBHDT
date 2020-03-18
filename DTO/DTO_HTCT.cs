namespace DTO
{
    public class DTO_HTCT
    {
        private string idht;
        private string idsp;
        private string tensp;
        private string ngaysx;
        private string ngayhh;
        private string ngaynhap;
        private string slcon;

        public string Idht { get => idht; set => idht = value; }
        public string Idsp { get => idsp; set => idsp = value; }
        public string Tensp { get => tensp; set => tensp = value; }
        public string Ngaysx { get => ngaysx; set => ngaysx = value; }
        public string Ngayhh { get => ngayhh; set => ngayhh = value; }
        public string Ngaynhap { get => ngaynhap; set => ngaynhap = value; }
        public string Slcon { get => slcon; set => slcon = value; }

        public DTO_HTCT() { }

        public DTO_HTCT(string maht, string masp, string tensp, string ngaysx, string ngayhh, string ngaynhap, string slcon)
        {
            Idht = maht;
            Idsp = masp;
            Tensp = tensp;
            Ngaysx = ngaysx;
            Ngayhh = ngayhh;
            Ngaynhap = ngaynhap;
            Slcon = slcon;
        }
    }
}
