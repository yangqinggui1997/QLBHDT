using System.Data;
using DTO;
using DAO;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_NND
    {
        public static DataTable hienthiNND()
        {
            return DAO_NND.hienthinnd();
        }

        public static DataTable hienthiNNDcuthe(string mannd)
        {
            return DAO_NND.hienthinndcuthe(mannd);
        }

        public static DataTable hienthiNNDtheoten(string tennnd)
        {
            return DAO_NND.hienthinndtheoten(tennnd);
        }

        public static void themNND(DTO_NND nnd)
        {
            DAO_NND.Themnnd(nnd);
        }

        public static void suaNND(DTO_NND nnd)
        {
            DAO_NND.SuaNND(nnd);
        }

        public static void RunDelSQL(string mannd)
        {
            DAO_NND.RunDelSQL(mannd);
        }

        public static bool ktnndtrung(string mannd)
        {
            return DAO_NND.checkmannd(mannd);
        }

        public static DataTable timkiemNND(string manqh, string tennnd)
        {
            return DAO_NND.timkiemNND(manqh, tennnd);
        }

        public static void FillComboMaNQH(ComboBox cb, string ma, string ten)
        {
            DAO_NND.FillComboMaNQH(cb, ma, ten);
        }

    }
}
