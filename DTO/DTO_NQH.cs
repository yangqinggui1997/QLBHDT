namespace DTO
{
    public class DTO_NQH
    {
        private string manqh;
        private string tennqh;
        private string mota;
        private string danhmuctc;
        private string quyendm;

        public string Manqh { get => manqh; set => manqh = value; }
        public string Tennqh { get => tennqh; set => tennqh = value; }
        public string Mota { get => mota; set => mota = value; }
        public string Danhmuctc { get => danhmuctc; set => danhmuctc = value; }
        public string Quyendm { get => quyendm; set => quyendm = value; }

        public DTO_NQH() { }

        public DTO_NQH(string manqh, string tennqh, string mota, string danhmuctc, string quyendm)
        {
            Manqh = manqh;
            Tennqh = tennqh;
            Mota = mota;
            Danhmuctc = danhmuctc;
            Quyendm = quyendm;
        }
    }
}
