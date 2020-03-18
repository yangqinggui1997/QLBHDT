namespace DTO
{
    public class DTO_TC
    {
        private string idtc;
        private string idnd;
        private string tentk;
        private string landmkcuoi;
        private string landncuoi;
        private string danhmuctc;
        private string thaotac;

        public string Idtc { get => idtc; set => idtc = value; }
        public string Idnd { get => idnd; set => idnd = value; }
        public string Tentk { get => tentk; set => tentk = value; }
        public string Landmkcuoi { get => landmkcuoi; set => landmkcuoi = value; }
        public string Landncuoi { get => landncuoi; set => landncuoi = value; }
        public string Danhmuctc { get => danhmuctc; set => danhmuctc = value; }
        public string Thaotac { get => thaotac; set => thaotac = value; }

        public DTO_TC() { }

        public DTO_TC(string idtc,string idnd,string tentk,string landmkcuoi,string landncuoi, string danhmuctc,string thaotac)
        {
            Idtc = idtc;
            Idnd = idnd;
            Tentk = tentk;
            Landmkcuoi = landmkcuoi;
            Landncuoi = landncuoi;
            Danhmuctc = danhmuctc;
            Thaotac = thaotac;
        }
    }
}
