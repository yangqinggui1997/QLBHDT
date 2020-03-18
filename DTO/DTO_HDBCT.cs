namespace DTO
{
    public class DTO_HDBCT
    {
        private string mahdb;
        private string masp;
        private string sl;
        private string dongiaban;
        private string giamgia;
        private string thanhtien;

        public string Mahdb { get => mahdb; set => mahdb = value; }
        public string Masp { get => masp; set => masp = value; }
        public string Sl { get => sl; set => sl = value; }
        public string Dongiaban { get => dongiaban; set => dongiaban = value; }
        public string Giamgia { get => giamgia; set => giamgia = value; }
        public string Thanhtien { get => thanhtien; set => thanhtien = value; }

        public DTO_HDBCT()
        {

        }

        public DTO_HDBCT(string mahdb, string masp, string sl, string dongiaban, string giamgia, string thanhtien)
        {
            Mahdb = mahdb;
            Masp = masp;
            Sl = sl;
            Dongiaban = dongiaban;
            Giamgia = giamgia;
            Thanhtien = thanhtien;
        }
    }
}
