using System.Data;
using DTO;
using DAO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BUS
{
    public class BUS_TC
    {
        public static DataTable hienthiTC()
        {
            return DAO_TC.hienthiTC();
        }

        public static void themTC(DTO_TC tc)
        {
            DAO_TC.ThemTC(tc);
        }

        public static void RunDelSQL(string matc)
        {
            DAO_TC.RunDelSQL(matc);
        }

        public static void RunDelAllData()
        {
            DAO_TC.RunDelAllData();
        }

        public static bool ktTCtrung(string matc)
        {
            return DAO_TC.checkmatc(matc);
        }

        public static void FillComboTenTK(ComboBox cb, string ma, string ten)
        {
            DAO_TC.FillComboTenTK(cb, ma, ten);
        }

        public static void CapnhatTTTruyCap(string matc, string danhmuctc, string thaotactc)
        {
            DAO_TC.capnhatthaotactc(matc, danhmuctc, thaotactc);
        }

        public static DataTable TimkiemTC(string tentk, string landncuoi)
        {
            return DAO_TC.timkiemTC(tentk, landncuoi);
        }

        public static void Connect()
        {
            DAO_TC.Connect();
        }

        public static void Disconnect()
        {
            DAO_TC.Disconnect();
        }

        public static SqlConnection GetCon()
        {
            return DAO_TC.GetCon();
        }
    }
}
