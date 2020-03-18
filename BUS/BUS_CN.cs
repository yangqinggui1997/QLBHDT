using System.Data;
using DTO;
using DAO;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_CN
    {
        public static DataTable hienthiCN()
        {
            return DAO_CN.hienthiCN();
        }

        public static DataTable hienthiCNcuthe(string macn)
        {
            return DAO_CN.hienthiCNcuthe(macn);
        }

        public static void themCN(DTO_CN cn)
        {
            DAO_CN.ThemCN(cn);
        }

        public static void CapnhatNgayTK(string macn, string ngaytk)
        {
            DAO_CN.CapnhatNgayTK(macn, ngaytk);
        }

        public static void RunDelSQL(string macn)
        {
            DAO_CN.RunDelSQL(macn);
        }

        public static void RunDelSQLOnCNCT(string macn)
        {
            DAO_CN.RunDelSQLOnCNCT(macn);
        }

        public static DataTable KiemtraTKCNTonTai(string thangtk, string namtk)
        {
            return DAO_CN.KiemTraTKCNDaTonTai(thangtk, namtk);
        }

        public static bool ktCNtrung(string macn)
        {
            return DAO_CN.checkmaCN(macn);
        }

        public static DataTable timkiemCN(string manv, string macn)
        {
            return DAO_CN.timkiemCN(manv, macn);
        }

        public static void FillComboMaTKCN(ComboBox cb, string ma, string ten)
        {
            DAO_CN.FillComboMaTKCN(cb, ma, ten);
        }

        public static void FillComboMaNV(ComboBox cb, string ma, string ten)
        {
            DAO_CN.FillComboMaNV(cb, ma, ten);
        }
    }
}
