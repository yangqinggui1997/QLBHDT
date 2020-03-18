using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_KH
    {
        public static DataTable hienthikh()
        {
            return DAO_KH.hienthikh();
        }

        public static DataTable hienthikhcuthe(string makh)
        {
            return DAO_KH.hienthikhcuthe(makh);
        }

        public static void themkh(DTO_KH kh)
        {
            DAO_KH.ThemKH(kh);
        }

        public static void suaKH(DTO_KH kh)
        {
            DAO_KH.SuaKH(kh);
        }

        public static void RunDelSQL(string makh)
        {
            DAO_KH.RunDelSQL(makh);
        }

        public static void RunDelSQLOnHDB(string makh)
        {
            DAO_KH.RunDelSQLOnHDB(makh);
        }

        public static bool ktkhtrung(string makh)
        {
            return DAO_KH.ktkhtrung(makh);
        }

        public static DataTable timkiemkh(string ten, string sdt, string loaikh)
        {
            return DAO_KH.timkiemkh(ten, sdt, loaikh);
        }
    }
}
