using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_NQH
    {
        public static DataTable hienthinqh()
        {
            return DataProvider.GetDatatoTable("sp_hienthiNQH", DataProvider.con);
        }

        public static DataTable hienthinqhcuthe(string manqh)
        {
            return DataProvider.GetDataSpecific("sp_hienthiNQHcuthe", DataProvider.con, manqh, "@IdNQH");
        }

        public static void Themnqh(DTO_NQH nqh)
        {
            SqlCommand cmd = new SqlCommand("sp_themNQH", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manqh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tennqh", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@mota", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@danhmuctc", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@quyendm", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@manqh"].Value = nqh.Manqh;
            cmd.Parameters["@tennqh"].Value = nqh.Tennqh;
            cmd.Parameters["@mota"].Value = nqh.Mota;
            cmd.Parameters["@danhmuctc"].Value = nqh.Danhmuctc;
            cmd.Parameters["@quyendm"].Value = nqh.Quyendm;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void SuaNQH(DTO_NQH nqh)
        {
            SqlCommand cmd = new SqlCommand("sp_suaNQH", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manqh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tennqh", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@mota", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@danhmuctc", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@quyendm", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@manqh"].Value = nqh.Manqh;
            cmd.Parameters["@tennqh"].Value = nqh.Tennqh;
            cmd.Parameters["@mota"].Value = nqh.Mota;
            cmd.Parameters["@danhmuctc"].Value = nqh.Danhmuctc;
            cmd.Parameters["@quyendm"].Value = nqh.Quyendm;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string manqh)
        {
            DataProvider.RunDelSQL("sp_xoaNQH", DataProvider.con, manqh, "@manqh");
        }

        public static void RunDelSQLOnNND(string manqh)
        {
            DataProvider.RunDelSQL("sp_xoaNQHtrenNND", DataProvider.con, manqh, "@manqh");
        }

        public static bool checkmanqh(string manqh)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraNQHtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manqh", SqlDbType.VarChar, 50);
            cmd.Parameters["@manqh"].Value = manqh;
            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            if (cmd.ExecuteScalar() == null)
            {
                DataProvider.disconnect();
                return true;
            }
            else
            {
                DataProvider.disconnect();
                return false;
            }
        }

        public static DataTable timkiemNQH(string tennqh)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemNQH", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@tennqh", SqlDbType.NVarChar, 500);

            cmd.Parameters["@tennqh"].Value = tennqh;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }
    }
}
