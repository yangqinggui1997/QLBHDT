namespace DTO
{
    public class DTO_ND
    {
        private string mand;
        private string mannd;
        private string manv;
        private string tennd;
        private string tentk;
        private string password;
        private string ngaytaotk;
        private string danhmuctc;
        private string quyendm;

        public string Mand { get => mand; set => mand = value; }
        public string Mannd { get => mannd; set => mannd = value; }
        public string Manv { get => manv; set => manv = value; }
        public string Tennd { get => tennd; set => tennd = value; }
        public string Tentk { get => tentk; set => tentk = value; }
        public string Password { get => password; set => password = value; }
        public string Ngaytaotk { get => ngaytaotk; set => ngaytaotk = value; }
        public string Danhmuctc { get => danhmuctc; set => danhmuctc = value; }
        public string Quyendm { get => quyendm; set => quyendm = value; }

        public DTO_ND() { }

        public DTO_ND(string mand,string mannd,string manv, string tennd, string tentk,string password,string ngaytaotk,string danhmuctc, string quyendm)
        {
            Mand = mand;
            Mannd = mannd;
            Manv = manv;
            Tennd = tennd;
            Tentk = tentk;
            Password = password;
            Ngaytaotk = ngaytaotk;
            Danhmuctc = danhmuctc;
            Quyendm = quyendm;
        }
    }
}
