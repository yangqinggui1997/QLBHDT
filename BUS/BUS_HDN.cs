using System.Data;
using DTO;
using DAO;
using System.Windows.Forms;

namespace BUS
{
     public class BUS_HDN
    {
        public static DataTable hienthiHDN()
        {
            return DAO_HDN.hienthiHDN();
        }

        public static DataTable hienthiHDNcuthe(string mahdn)
        {
            return DAO_HDN.hienthiHDNcuthe(mahdn);
        }

        public static DataTable LayTTSPTheoTenSP(string masp)
        {
            return DAO_HDN.LayTTSPTheoTenSP(masp);
        }

        public static DataTable LayTTNCU(string mancu)
        {
            return DAO_HDN.LayTTNCU(mancu);
        }

        public static void themHDN(DTO_HDN hdn)
        {
            DAO_HDN.ThemHDN(hdn);
        }

        public static void suaHDN(DTO_HDN hdn)
        {
            DAO_HDN.SuaHDN(hdn);
        }

        public static void RunDelSQL(string mahdn)
        {
            DAO_HDN.RunDelSQL(mahdn);
        }

        public static void RunDelSQLOnHDNCT(string mahdn)
        {
            DAO_HDN.RunDelSQLOnHDNCT(mahdn);
        }

        public static bool ktHDNtrung(string mahdn)
        {
            return DAO_HDN.ktHDNtrung(mahdn);
        }

        public static DataTable timkiemHDN(string ngaylap, string thanglap, string namlap)
        {
            return DAO_HDN.timkiemhdn(ngaylap, thanglap, namlap);
        }

        public static void FillComboMaHD(ComboBox cb, string ma, string ten)
        {
            DAO_HDN.FillComboMaHD(cb, ma, ten);
        }

        public static void FillComboMaNCU(ComboBox cb, string ma, string ten)
        {
            DAO_HDN.FillComboMaNCU(cb, ma, ten);
        }

        public static void FillComboMaNV(ComboBox cb, string ma, string ten)
        {
            DAO_HDN.FillComboMaNV(cb, ma, ten);
        }
        public static void CapNhatnoNCU(string mancu)
        {
            DAO_HDN.CapnhatnoNCU(mancu);
        }

        public static void CapNhatTTtrenSP(string masp, string nhasx, string sl, string dongianhap)
        {
            DAO_HDN.CapnhatTTtrenSP(masp, nhasx, sl, dongianhap);
        }

        public static void CapNhatSLSPtrenHDN(string mahdn)
        {
            DAO_HDN.CapnhatSLSPtrenHDN(mahdn);
        }

        public static void CapNhatNotrenHDN(string mahdn, string conno)
        {
            DAO_HDN.CapnhatNotrenHDN(mahdn, conno);
        }

        public static void CapNhatTTtrenHDN(string mahdn)
        {
            DAO_HDN.CapnhatTTtrenHDN(mahdn);
        }
     }
}
