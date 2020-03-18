using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_NCU
    {
        public static DataTable hienthincu()
        {
            return DataProvider.GetDatatoTable("sp_hienthiNCU", DataProvider.con);
        }

        public static DataTable hienthincucuthe(string mancu)
        {
            return DataProvider.GetDataSpecific("sp_hienthiNCUcuthe", DataProvider.con, mancu,"@IdNCU");
        }

        public static void Themncu(DTO_NCU ncu)
        {
            SqlCommand cmd = new SqlCommand("sp_ThemNCU", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tenncu", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@quymoncu", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@conno", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mancu"].Value = ncu.Mancu;
            cmd.Parameters["@tenncu"].Value = ncu.Tenncu;
            cmd.Parameters["@diachi"].Value = ncu.Diachi;
            cmd.Parameters["@sdt"].Value = ncu.Sdt;
            cmd.Parameters["@quymoncu"].Value = ncu.Quymoncu;
            cmd.Parameters["@conno"].Value = ncu.Connoncu;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void SuaNCU(DTO_NCU ncu)
        {
            SqlCommand cmd = new SqlCommand("sp_suaNCU", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tenncu", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@quymoncu", SqlDbType.NVarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mancu"].Value = ncu.Mancu;
            cmd.Parameters["@tenncu"].Value = ncu.Tenncu;
            cmd.Parameters["@diachi"].Value = ncu.Diachi;
            cmd.Parameters["@sdt"].Value = ncu.Sdt;
            cmd.Parameters["@quymoncu"].Value = ncu.Quymoncu;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string mancu)
        {
            DataProvider.RunDelSQL("sp_xoaNCU", DataProvider.con, mancu, "@IdNCU");
        }

        public static void RunDelSQLOnCN(string mancu)
        {
            DataProvider.RunDelSQL("sp_xoaNCUtrenCN", DataProvider.con, mancu, "@@IdNCU");
        }

        public static void RunDelSQLOnHDN(string mancu)
        {
            DataProvider.RunDelSQL("sp_xoaNCUtrenHDN", DataProvider.con, mancu, "@mancu");
        }

        public static void RunDelSQLOnSP(string mancu)
        {
            DataProvider.RunDelSQL("sp_xoaNCUtrenSP", DataProvider.con, mancu, "@idncu");
        }

        public static bool checkmancu(string mancu)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraNCUtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters["@mancu"].Value = mancu;
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

        public static DataTable timkiemNCU(string ten, string sdt, string quymoncu)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemNCU", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@quymoncu", SqlDbType.NVarChar, 50);

            cmd.Parameters["@ten"].Value = ten;
            cmd.Parameters["@sdt"].Value = sdt;
            cmd.Parameters["@quymoncu"].Value = quymoncu;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

    }
}
