using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_HDNCT
    {
        public static DataTable hienthiHDNCT()
        {
            return DAO_HDNCT.hienthiHDNCT();
        }

        public static DataTable hienthiHDNCTcuthe(string mahdn)
        {
            return DAO_HDNCT.hienthiHDNCTcuthe(mahdn);
        }

        public static DataTable hienthiHDNCTLenBC(string mahdn)
        {
            return DAO_HDNCT.hienthiHDNCTlenBC(mahdn);
        }

        public static void themHDNCT(DTO_HDNCT hdnct)
        {
            DAO_HDNCT.ThemHDNCT(hdnct);
        }

        public static void RunDelSQLHDNCT(string mahdn, string masp)
        {
            DAO_HDNCT.RunDelSQLHDNCT(mahdn, masp);
        }

        public static void suaHDNCT(DTO_HDNCT hdnct)
        {
            DAO_HDNCT.SuaHDNCT(hdnct);
        }

        public static bool ktHDNCTtrung(string mahdn, string masp)
        {
            return DAO_HDNCT.ktHDNCTtrung(mahdn, masp);
        }
    }
}
