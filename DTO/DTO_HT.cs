namespace DTO
{
    public class DTO_HT
    {
        private string idht;
        private string idnv;
        private string ngaytk;

        public string Idht { get => idht; set => idht = value; }
        public string Idnv { get => idnv; set => idnv = value; }
        public string Ngaytk { get => ngaytk; set => ngaytk = value; }

        public DTO_HT() { }

        public DTO_HT(string maht, string manv, string ngaytk)
        {
            Idht = maht;
            Idnv = manv;
            Ngaytk = ngaytk;
        }
    }
}
