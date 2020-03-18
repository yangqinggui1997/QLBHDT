namespace DTO
{
    public class DTO_CN
    {
        private string macn;
        private string manv;
        private string ngaytk;

        public string Macn { get => macn; set => macn = value; }
        public string Ngaytk { get => ngaytk; set => ngaytk = value; }
        public string Manv { get => manv; set => manv = value; }

        public DTO_CN() { }

        public DTO_CN(string macn, string manv, string ngaytk)
        {
            Macn = macn;
            Manv = manv;
            Ngaytk = ngaytk;
        }
    }
}
