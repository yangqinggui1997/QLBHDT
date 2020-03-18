using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_CNCT
    {
        public static DataTable hienthiCNCT()
        {
            return DataProvider.GetDatatoTable("sp_hienthiCNCT", DataProvider.con);
        }

        public static DataTable hienthiCNCTcuthe(string macn)
        {
            return DataProvider.GetDataSpecific("sp_hienthiCNCTcuthe", DataProvider.con, macn, "@IdCN");
        }

        public static void ThemCNCT(DTO_CNCT cnct)
        {
            SqlCommand cmd = new SqlCommand("sp_themTKCNCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@matkcn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@matkcn"].Value = cnct.Macn;
            cmd.Parameters["@mancu"].Value = cnct.Manncu;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatTKCNCT(string macn, string mancu)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatTKCNCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@macn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters[0].Value = macn;
            cmd.Parameters[1].Value = mancu;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static bool ktCNCTtrung(string macn, string mancu)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraCNCTtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@macn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);

            cmd.Parameters["@macn"].Value = macn;
            cmd.Parameters["@mancu"].Value = mancu;

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
    }
}
