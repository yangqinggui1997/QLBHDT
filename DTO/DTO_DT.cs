namespace DTO
{
    public class DTO_DT
    {
        private string madt;
        private string manv;
        private string doanhsoban;
        private string doanhthubh;
        private string loinhuan;
        private string ngaytk;

        public string Madt { get => madt; set => madt = value; }
        public string Doanhsoban { get => doanhsoban; set => doanhsoban = value; }
        public string Ngaytk { get => ngaytk; set => ngaytk = value; }
        public string Doanhthubh { get => doanhthubh; set => doanhthubh = value; }
        public string Loinhuan { get => loinhuan; set => loinhuan = value; }
        public string Manv { get => manv; set => manv = value; }

        public DTO_DT()
        {

        }

        public DTO_DT(string madt, string manv, string doanhsoban, string doanhthubh, string loinhuan, string ngaytk)
        {
            Madt = madt;
            Manv = manv;
            Doanhsoban = doanhsoban;
            Doanhthubh = doanhthubh;
            Loinhuan = loinhuan;
            Ngaytk = ngaytk;
        }
    }
}
