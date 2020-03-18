using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_CNCT
    {
        public static DataTable hienthiCNCT()
        {
            return DAO_CNCT.hienthiCNCT();
        }

        public static DataTable hienthiCNCTcuthe(string macn)
        {
            return DAO_CNCT.hienthiCNCTcuthe(macn);
        }

        public static void themCNCT(DTO_CNCT cnct)
        {
            DAO_CNCT.ThemCNCT(cnct);
        }

        public static bool ktCNCTtrung(string macn, string mancu)
        {
            return DAO_CNCT.ktCNCTtrung(macn, mancu);
        }

        public static void CapnhatTKCNCT(string macn, string mancu)
        {
            DAO_CNCT.CapnhatTKCNCT(macn, mancu);
        }

    }
}
