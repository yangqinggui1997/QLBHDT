using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_HTCT
    {
        public static DataTable hienthiHTCT()
        {
            return DAO_HTCT.hienthiHTCT();
        }

        public static DataTable hienthiHTCTLenBC(string maht)
        {
            return DAO_HTCT.hienthiHTCTlenbc(maht);
        }

        public static DataTable hienthiHTCTcuthe(string maht)
        {
            return DAO_HTCT.hienthiHTCTcuthe(maht);
        }

        public static void themHTCT(DTO_HTCT htct)
        {
            DAO_HTCT.ThemHTCT(htct);
        }

        public static void RunDelSQL(string maht, string masp)
        {
            DAO_HTCT.RunDelSQL(maht, masp);
        }

        public static bool ktHTCTtrung(string maht, string masp)
        {
            return DAO_HTCT.ktHTCTtrung(maht, masp);
        }

        public static void CapnhatTKHTCT(string maht, string masp, string sl)
        {
            DAO_HTCT.CapnhatTKHTCT(maht, masp, sl);
        }

    }
}
