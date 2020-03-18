using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_NCU
    {
        public static DataTable hienthiNCU()
        {
            return DAO_NCU.hienthincu();
        }

        public static DataTable hienthiNCUcuthe(string mancu)
        {
            return DAO_NCU.hienthincucuthe(mancu);
        }

        public static void themNCU(DTO_NCU ncu)
        {
            DAO_NCU.Themncu(ncu);
        }

        public static void suaNCU(DTO_NCU ncu)
        {
            DAO_NCU.SuaNCU(ncu);
        }

        public static void RunDelSQL(string mancu)
        {
            DAO_NCU.RunDelSQL(mancu);
        }

        public static void RunDelSQLOnCN(string mancu)
        {
            DAO_NCU.RunDelSQLOnCN(mancu);
        }

        public static void RunDelSQLOnHDN(string mancu)
        {
            DAO_NCU.RunDelSQLOnHDN(mancu);
        }


        public static void RunDelSQLOnSP(string mancu)
        {
            DAO_NCU.RunDelSQLOnSP(mancu);
        }

        public static bool ktncutrung(string mancu)
        {
            return DAO_NCU.checkmancu(mancu);
        }

        public static DataTable timkiemNCU(string ten, string sdt, string quymoncu)
        {
            return DAO_NCU.timkiemNCU(ten, sdt, quymoncu);
        }
    }
}
