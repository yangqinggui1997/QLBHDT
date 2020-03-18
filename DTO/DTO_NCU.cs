namespace DTO
{
    public class DTO_NCU
    {
        private string mancu;
        private string tenncu;
        private string diachi;
        private string sdt;
        private string quymoncu;
        private string connoncu;

        public string Mancu { get => mancu; set => mancu = value; }
        public string Tenncu { get => tenncu; set => tenncu = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Sdt { get => sdt; set => sdt = value; }
        public string Quymoncu { get => quymoncu; set => quymoncu = value; }
        public string Connoncu { get => connoncu; set => connoncu = value; }

        public DTO_NCU() { }

        public DTO_NCU(string mancu, string tenncu, string diachi, string sdt, string quymoncu, string connnoncu)
        {
            Mancu = mancu;
            Tenncu = tenncu;
            Diachi = diachi;
            Sdt = sdt;
            Quymoncu = quymoncu;
            Connoncu = connnoncu;
        }
    }
}
