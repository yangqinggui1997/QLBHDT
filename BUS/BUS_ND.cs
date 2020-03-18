using System.Data;
using DTO;
using DAO;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_ND
    {
        public static DataTable hienthiND()
        {
            return DAO_ND.hienthind();
        }

        public static DataTable hienthindcuthe(string manv)
        {
            return DAO_ND.hienthindcuthe(manv);
        }

        public static DataTable hienthiNDtheoten(string tennd)
        {
            return DAO_ND.hienthindtheoten(tennd);
        }

        public static void themND(DTO_ND nd)
        {
            DAO_ND.Themnd(nd);
        }

        public static void suaND(DTO_ND nd)
        {
            DAO_ND.Suand(nd);
        }

        public static void RunDelSQL(string mand)
        {
            DAO_ND.RunDelSQL(mand);
        }

        public static void RunDelSQLOnTC(string mand)
        {
            DAO_ND.RunDelSQLOnTC(mand);
        }

        public static void CapnhatTKNV(string manv, string tk)
        {
            DAO_ND.capnhattaikhoan(manv, tk);
        }

        public static void CapnhatMK(string manv, string mk)
        {
            DAO_ND.capnhatMK(manv, mk);
        }

        public static void CapnhatLanDMKcuoi(string mand, string landmkcuoi)
        {
            DAO_ND.capnhatLanDMKcuoi(mand, landmkcuoi);
        }

        public static DataTable TimkiemND(string mannd, string tennd, string ngaytaotk)
        {
            return DAO_ND.timkiemND(mannd, tennd, ngaytaotk);
        }

        public static void FillComboMaNND(ComboBox cb, string ma, string ten)
        {
            DAO_ND.FillComboMaNND(cb, ma, ten);
        }

        public static void FillComboTenND(ComboBox cb, string ma, string ten)
        {
            DAO_ND.FillComboTenND(cb, ma, ten);
        }

        public static int CheckTenTK(TextBox tentk)
        {
            return DAO_ND.CheckTenTK(tentk);
        }

        public static int CheckMK(TextBox MK)
        {
            return DAO_ND.CheckMK(MK);
        }
    }
}
