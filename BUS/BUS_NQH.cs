using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_NQH
    {
        public static DataTable hienthiNQH()
        {
            return DAO_NQH.hienthinqh();
        }

        public static DataTable hienthiNQHcuthe(string manqh)
        {
            return DAO_NQH.hienthinqhcuthe(manqh);
        }

        public static void themNQH(DTO_NQH nqh)
        {
            DAO_NQH.Themnqh(nqh);
        }

        public static void suaNQH(DTO_NQH nqh)
        {
            DAO_NQH.SuaNQH(nqh);
        }

        public static void RunDelSQL(string manqh)
        {
            DAO_NQH.RunDelSQL(manqh);
        }

        public static void RunDelSQLOnNND(string manqh)
        {
            DAO_NQH.RunDelSQLOnNND(manqh);
        }

        public static bool ktnqhtrung(string manqh)
        {
            return DAO_NQH.checkmanqh(manqh);
        }

        public static DataTable timkiemNQH(string tennqh)
        {
            return DAO_NQH.timkiemNQH(tennqh);
        }
    }
}
