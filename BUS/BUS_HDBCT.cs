using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_HDBCT
    {
        public static DataTable hienthiHDBCT()
        {
            return DAO_HDBCT.hienthiHDBCT();
        }

        public static DataTable hienthiHDBCTcuthe(string mahdb)
        {
            return DAO_HDBCT.hienthiHDBCTcuthe(mahdb);
        }

        public static DataTable hienthiHDBCTLenBC(string mahdb)
        {
            return DAO_HDBCT.hienthiHDBCTlenBC(mahdb);
        }

        public static void themHDBCT(DTO_HDBCT hdbct)
        {
            DAO_HDBCT.ThemHDBCT(hdbct);
        }

        public static void RunDelSQLHDBCT(string mahdb, string masp)
        {
            DAO_HDBCT.RunDelSQLHDBCT(mahdb, masp);
        }

        public static void suaHDBCT(DTO_HDBCT hdbct)
        {
            DAO_HDBCT.SuaHDBCT(hdbct);
        }

        public static bool ktHDBCTtrung(string mahdb, string masp)
        {
            return DAO_HDBCT.ktHDBCTtrung(mahdb, masp);
        }
    }
}
