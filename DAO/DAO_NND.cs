using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_NND
    {
        public static DataTable hienthinnd()
        {
            return DataProvider.GetDatatoTable("sp_hienthiNND", DataProvider.con);
        }

        public static DataTable hienthinndcuthe(string mannd)
        {
            return DataProvider.GetDataSpecific("sp_hienthiNNDcuthe", DataProvider.con, mannd, "@IdNND");
        }

        public static DataTable hienthinndtheoten(string tennnd)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_hienthiNNDtheoten", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tennnd", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@tennnd"].Value = tennnd;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;
        }

        public static void Themnnd(DTO_NND nnd)
        {
            SqlCommand cmd = new SqlCommand("sp_themNND", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mannd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manqh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tennnd", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@mota", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@mannd"].Value = nnd.Mannd;
            cmd.Parameters["@manqh"].Value = nnd.Manqh;
            cmd.Parameters["@tennnd"].Value = nnd.Tennnd;
            cmd.Parameters["@mota"].Value = nnd.Mota;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void SuaNND(DTO_NND nnd)
        {
            SqlCommand cmd = new SqlCommand("sp_suaNND", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mannd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manqh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tennnd", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@mota", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@mannd"].Value = nnd.Mannd;
            cmd.Parameters["@manqh"].Value = nnd.Manqh;
            cmd.Parameters["@tennnd"].Value = nnd.Tennnd;
            cmd.Parameters["@mota"].Value = nnd.Mota;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string mannd)
        {
            DataProvider.RunDelSQL("sp_xoaNND", DataProvider.con, mannd, "@mannd");
        }

        public static bool checkmannd(string mannd)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraNNDtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mannd", SqlDbType.VarChar, 50);
            cmd.Parameters["@mannd"].Value = mannd;
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

        public static DataTable timkiemNND(string manqh, string tennnd)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemNND", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@manqh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tennnd", SqlDbType.NVarChar, 500);

            cmd.Parameters["@manqh"].Value = manqh;
            cmd.Parameters["@tennnd"].Value = tennnd;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static void FillComboMaNQH(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiNQH", cb, ma, ten);
        }

    }
}
