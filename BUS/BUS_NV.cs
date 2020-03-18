using System.Data;
using DTO;
using DAO;

namespace BUS
{
    public class BUS_NV
    {
        public static DataTable hienthinv()
        {
            return DAO_NV.hienthinv();
        }

        public static DataTable hienthinvcuthe(string manv)
        {
            return DAO_NV.hienthinvcuthe(manv);
        }

        public static DataTable layMaHDBTheoMaNV(string manv)
        {
            return DAO_NV.LayMaHDBTheoMaNV(manv);
        }

        public static DataTable LayMaHDNTheoMaNV(string manv)
        {
            return DAO_NV.LayMaHDNTheoMaNV(manv);
        }

        public static DataTable LayMaCNTheoMaNV(string manv)
        {
            return DAO_NV.LayMaCNTheoMaNV(manv);
        }

        public static DataTable LayMaHTTheoMaNV(string manv)
        {
            return DAO_NV.LayMahtTheoMaNV(manv);
        }

        public static DataTable LayMaDTTheoMaNV(string manv)
        {
            return DAO_NV.LayMaDTTheoMaNV(manv);
        }

        public static void themnv(DTO_NV nv)
        {
            DAO_NV.Themnv(nv);
        }

        public static void suaNV(DTO_NV nv)
        {
            DAO_NV.Suanv(nv);
        }

        public static void RunDelSQL(string manv)
        {
            DAO_NV.RunDelSQL(manv);
        }

        public static void RunDelSQLNVOnTKDT(string manv)
        {
            DAO_NV.RunDelSQLNVOnTKDT(manv);
        }

        public static bool ktnvtrung(string manv)
        {
            return DAO_NV.ktnvtrung(manv);
        }

        public static DataTable timkiemnv(string ten, string sdt, string cv)
        {
            return DAO_NV.timkiemnv(ten, sdt, cv);
        }
    }
}
