using System.Data;
using System.Windows.Forms;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_DT
    {
        public static DataTable hienthiDT()
        {
            return DAO_DT.hienthiDT();
        }

        public static DataTable hienthiDTcuthe(string madt)
        {
            return DAO_DT.hienthiDTcuthe(madt);
        }

        public static void themDT(DTO_DT dt, string thangtk, string namtk)
        {
            DAO_DT.ThemDT(dt, thangtk, namtk);
        }

        public static void RunDelSQL(string madt)
        {
            DAO_DT.RunDelSQL(madt);
        }

        public static void CapnhatTKDT(string madt, string thangtk, string namtk, string ngaytk)
        {
            DAO_DT.CapnhatTK(madt, thangtk, namtk, ngaytk);
        }

        public static bool ktDTtrung(string madt)
        {
            return DAO_DT.checkmaDT(madt);
        }

        public static DataTable KiemtraTKDTTonTai(string thangtk, string namtk)
        {
            return DAO_DT.KiemTraTKDTDaTonTai(thangtk, namtk);
        }

        public static DataTable KiemtraHDBTThang(string thang, string nam)
        {
            return DAO_DT.KiemTraTKHDBTThnang(thang, nam);
        }

        public static DataTable timkiemDT(string manv, string madt)
        {
            return DAO_DT.timkiemDT(manv, madt);
        }

        public static void FillComboMaTKDT(ComboBox cb, string ma, string ten)
        {
            DAO_DT.FillComboMaTKDT(cb, ma, ten);
        }

        public static void FillComboMaNV(ComboBox cb, string ma, string ten)
        {
            DAO_DT.FillComboMaNV(cb, ma, ten);
        }
    }
}
