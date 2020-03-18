namespace DTO
{
    public class DTO_CNCT
    {
        private string macn;
        private string manncu;
        private string tenncu;
        private string sdt;
        private string diachi;
        private string conno;

        public string Macn { get => macn; set => macn = value; }
        public string Manncu { get => manncu; set => manncu = value; }
        public string Tenncu { get => tenncu; set => tenncu = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Conno { get => conno; set => conno = value; }

        public DTO_CNCT() { }

        public DTO_CNCT(string macn, string manncu, string tenncu, string sdt, string diachi, string conno)
        {
            Macn = macn;
            Manncu = manncu;
            Tenncu = tenncu;
            Sdt = sdt;
            Diachi = diachi;
            Conno = conno;
        }
    }
}
