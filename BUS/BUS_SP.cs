using System.Data;
using DTO;
using DAO;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_SP
    {
        public static DataTable hienthisp()
        {
            return DAO_SP.hienthisp();
        }

        public static DataTable hienthincucuthe(string mancu)
        {
            return DAO_SP.hienthincucuthe(mancu);
        }

        public static DataTable hienthiSPcuthe(string masp)
        {
            return DAO_SP.hienthiSPcuthe(masp);
        }

        public static void themSP(DTO_SP sp)
        {
            DAO_SP.ThemSP(sp);
        }

        public static void suaSP(DTO_SP sp)
        {
            DAO_SP.SuaSP(sp);
        }

        public static void RunDelSQL(string masp)
        {
            DAO_SP.RunDelSQL(masp);
        }

        public static void RunDelSQLOnHTCT(string masp)
        {
            DAO_SP.RunDelSQLOnHTCT(masp);
        }

        public static void RunDelSQLOnHDNCT(string masp)
        {
            DAO_SP.RunDelSQLOnHDNCT(masp);
        }

        public static void RunDelSQLOnHDBCT(string masp)
        {
            DAO_SP.RunDelSQLOnHDBCT(masp);
        }

        public static bool ktSPtrung(string masp)
        {
            return DAO_SP.checkmasp(masp);
        }

        public static DataTable timkiemSP(string mancu, string tensp)
        {
            return DAO_SP.timkiemSP(mancu, tensp);
        }

        public static void FillCombo(ComboBox cb, string ma, string ten)
        {
            DAO_SP.FillCombo(cb, ma, ten);
        }
    }
}
