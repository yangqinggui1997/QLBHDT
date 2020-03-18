namespace DTO
{
    public class DTO_NND
    {
        private string mannd;
        private string manqh;
        private string tennnd;
        private string mota;

        public string Manqh { get => manqh; set => manqh = value; }
        public string Tennnd { get => tennnd; set => tennnd = value; }
        public string Mota { get => mota; set => mota = value; }
        public string Mannd { get => mannd; set => mannd = value; }

        public DTO_NND() { }

        public DTO_NND(string mannd, string manqh, string tennnd, string mota)
        {
            Mannd = mannd;
            Manqh = manqh;
            Tennnd = tennnd;
            Mota = mota;
        }
    }
}
