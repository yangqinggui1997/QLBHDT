using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_NV
    {

        public static DataTable hienthinv()
        {
            return DataProvider.GetDatatoTable("sp_hienthinhanvien", DataProvider.con);
        }

        public static DataTable hienthinvcuthe(string manv)
        {
            return DataProvider.GetDataSpecific("sp_hienthinhanviencuthe", DataProvider.con, manv, "@IdNV");
        }

        public static void Themnv(DTO_NV nv)
        {
            SqlCommand cmd = new SqlCommand("sp_themnhanvien", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ngaysinh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@chucvu", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@luongcb", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@hsl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@thuclinh", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@taikhoan", SqlDbType.VarChar, 200);

            //Gan gia tri
            cmd.Parameters["@manv"].Value = nv.Manv;
            cmd.Parameters["@ten"].Value = nv.Ten;
            cmd.Parameters["@ngaysinh"].Value = nv.Ngaysinh;
            cmd.Parameters["@gioitinh"].Value = nv.Gioitinh;
            cmd.Parameters["@diachi"].Value = nv.Diachi;
            cmd.Parameters["@sdt"].Value = nv.Sdt;
            cmd.Parameters["@chucvu"].Value = nv.Chucvu;
            cmd.Parameters["@luongcb"].Value = nv.Luongcb;
            cmd.Parameters["@hsl"].Value = nv.Hsl;
            cmd.Parameters["@thuclinh"].Value = nv.Thuclinh;
            cmd.Parameters["@taikhoan"].Value = nv.Taikhoan;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void Suanv(DTO_NV nv)
        {
            SqlCommand cmd = new SqlCommand("sp_suaNV", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ngaysinh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@chucvu", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@luongcb", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@hsl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@thuclinh", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@taikhoan", SqlDbType.VarChar, 200);

            //Gan gia tri
            cmd.Parameters["@manv"].Value = nv.Manv;
            cmd.Parameters["@ten"].Value = nv.Ten;
            cmd.Parameters["@ngaysinh"].Value = nv.Ngaysinh;
            cmd.Parameters["@gioitinh"].Value = nv.Gioitinh;
            cmd.Parameters["@diachi"].Value = nv.Diachi;
            cmd.Parameters["@sdt"].Value = nv.Sdt;
            cmd.Parameters["@chucvu"].Value = nv.Chucvu;
            cmd.Parameters["@luongcb"].Value = nv.Luongcb;
            cmd.Parameters["@hsl"].Value = nv.Hsl;
            cmd.Parameters["@thuclinh"].Value = nv.Thuclinh;
            cmd.Parameters["@taikhoan"].Value = nv.Taikhoan;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string manv)
        {
            DataProvider.RunDelSQL("sp_XoaNV", DataProvider.con, manv, "@IdNV");
        }

        public static void RunDelSQLNVOnTKDT(string manv)
        {
            DataProvider.RunDelSQL("sp_xoaNVtrenTKDT", DataProvider.con, manv, "@manv");
        }

        public static bool ktnvtrung(string manv)
        {
            SqlCommand cmd = new SqlCommand("sp_Kiemtranvtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters["@manv"].Value = manv;
            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();

            }
            if (cmd.ExecuteScalar() == null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static DataTable timkiemnv(string ten, string sdt, string cv)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemnv", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@chucvu", SqlDbType.NVarChar, 100);

            cmd.Parameters["@ten"].Value = ten;
            cmd.Parameters["@sdt"].Value = sdt;
            cmd.Parameters["@chucvu"].Value = cv;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static DataTable LayMaHDBTheoMaNV(string manv)
        {
            return DataProvider.GetDataSpecific("sp_LayMahdbTheoMaNV", DataProvider.con, manv, "@manv");
        }

        public static DataTable LayMaHDNTheoMaNV(string manv)
        {
            return DataProvider.GetDataSpecific("sp_LayMahdnTheoMaNV", DataProvider.con, manv, "@manv");
        }

        public static DataTable LayMaCNTheoMaNV(string manv)
        {
            return DataProvider.GetDataSpecific("sp_LayMacnTheoMaNV", DataProvider.con, manv, "@manv");
        }

        public static DataTable LayMahtTheoMaNV(string manv)
        {
            return DataProvider.GetDataSpecific("sp_LayMahtTheoMaNV", DataProvider.con, manv, "@manv");
        }

        public static DataTable LayMaDTTheoMaNV(string manv)
        {
            return DataProvider.GetDataSpecific("sp_LayMadtTheoMaNV", DataProvider.con, manv, "@manv");
        }

    }
}
