namespace DTO
{
    public class DTO_HDNCT
    {
        private string mahdn;
        private string masp;
        private string sl;
        private string dongianhap;
        private string giamgia;
        private string thanhtien;

        public string Mahdn { get => mahdn; set => mahdn = value; }
        public string Masp { get => masp; set => masp = value; }
        public string Sl { get => sl; set => sl = value; }
        public string Dongianhap { get => dongianhap; set => dongianhap = value; }
        public string Giamgia { get => giamgia; set => giamgia = value; }
        public string Thanhtien { get => thanhtien; set => thanhtien = value; }

        public DTO_HDNCT() { }

        public DTO_HDNCT(string mahdn, string masp, string sl, string dongianhap, string giamgia, string thanhtien)
        {
            Mahdn = mahdn;
            Masp = masp;
            Sl = sl;
            Dongianhap = dongianhap;
            Giamgia = giamgia;
            Thanhtien = thanhtien;
        }
    }
}
