using System.Data;
using DTO;
using DAO;
using System.Windows.Forms;

namespace BUS
{
    public class BUS_HDB
    {
        public static DataTable hienthiHDB()
        {
            return DAO_HDB.hienthiHDB();
        }

        public static DataTable hienthiHDBcuthe(string mahdb)
        {
            return DAO_HDB.hienthiHDBcuthe(mahdb);
        }

        public static DataTable LayTenNV(string manv)
        {
            return DAO_HDB.LayTenNV(manv);
        }

        public static DataTable LayTTSP(string masp)
        {
            return DAO_HDB.LayTTSP(masp);
        }

        public static DataTable LayTTKH(string makh)
        {
            return DAO_HDB.LayTTKH(makh);
        }

        public static void themHDB(DTO_HDB hdb)
        {
            DAO_HDB.ThemHDB(hdb);
        }

        public static void suaHDB(DTO_HDB hdb)
        {
            DAO_HDB.SuaHDB(hdb);
        }

        public static void RunDelSQL(string mahdb)
        {
            DAO_HDB.RunDelSQL(mahdb);
        }

        public static void RunDelSQLOnHDBCT(string mahdb)
        {
            DAO_HDB.RunDelSQLOnHDBCT(mahdb);
        }

        public static bool ktHDBtrung(string mahdb)
        {
            return DAO_HDB.ktHDBtrung(mahdb);
        }

        public static DataTable timkiemHDB(string ngaylap, string thanglap, string namlap)
        {
            return DAO_HDB.timkiemhdb(ngaylap, thanglap, namlap);
        }

        public static void FillComboMaHD(ComboBox cb, string ma, string ten)
        {
            DAO_HDB.FillComboMaHD(cb, ma, ten);
        }

        public static void FillComboMaNV(ComboBox cb, string ma, string ten)
        {
            DAO_HDB.FillComboMaNV(cb, ma, ten);
        }

        public static void FillComboMaKH(ComboBox cb, string ma, string ten)
        {
            DAO_HDB.FillComboMaKH(cb, ma, ten);
        }

        public static void FillComboMaSP(ComboBox cb, string ma, string ten)
        {
            DAO_HDB.FillComboMaSP(cb, ma, ten);
        }

        public static void CapNhatNoKH(string makh)
        {
            DAO_HDB.CapnhatNoKH(makh);
        }

        public static void CapNhatSLSanPham(DTO_SP sp)
        {
            DAO_HDB.CapnhatSLSamPham(sp);
        }

        public static void CapNhatSLSPtrenHDB(string mahdb)
        {
            DAO_HDB.CapnhatSLSPtrenHDB(mahdb);
        }

        public static void CapNhatTTtrenHDB(string mahdb)
        {
            DAO_HDB.CapnhatTTtrenHDB(mahdb);
        }

        public static void CapNhatNotrenHDB(string mahdb, string conno)
        {
            DAO_HDB.CapnhatNotrenHDB(mahdb, conno);
        }

        public static string FormatNumber(string number)
        {
            return DAO_HDB.FormatNumber(number);
        }

        public static string ConvertToFloatType(string number)
        {
            return DAO_HDB.ConvertToFloatType(number);
        }

    }
}
