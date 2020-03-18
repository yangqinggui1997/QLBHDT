using System.Data;
using System.Windows.Forms;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_HT
    {
        public static DataTable hienthiHT()
        {
            return DAO_HT.hienthiHT();
        }

        public static DataTable hienthiHTcuthe(string maht)
        {
            return DAO_HT.hienthiHTcuthe(maht);
        }

        public static void themHT(DTO_HT ht)
        {
            DAO_HT.ThemHT(ht);
        }

        public static void CapnhatNgayTK(string maht, string ngaytk)
        {
            DAO_HT.CapnhatNgayTK(maht, ngaytk);
        }

        public static void RunDelSQL(string maht)
        {
            DAO_HT.RunDelSQL(maht);
        }

        public static void RunDelSQLOnHTCT(string maht)
        {
            DAO_HT.RunDelSQLOnHTCT(maht);
        }

        public static DataTable KiemtraTKHTTonTai(string thangtk, string namtk)
        {
            return DAO_HT.KiemTraTKHTDaTonTai(thangtk, namtk);
        }

        public static bool ktHTtrung(string maht)
        {
            return DAO_HT.checkmaHT(maht);
        }

        public static DataTable timkiemHT(string manv, string maht)
        {
            return DAO_HT.timkiemHT(manv, maht);
        }

        public static void FillComboMaTKHT(ComboBox cb, string ma, string ten)
        {
            DAO_HT.FillComboMaTKHT(cb, ma, ten);
        }

        public static void FillComboMaNV(ComboBox cb, string ma, string ten)
        {
            DAO_HT.FillComboMaNV(cb, ma, ten);
        }
    }
}
