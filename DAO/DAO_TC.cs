using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_TC
    {
        public static DataTable hienthiTC()
        {
            return DataProvider.GetDatatoTable("sp_hienthiTC", DataProvider.con);
        }

        public static void ThemTC(DTO_TC tc)
        {
            SqlCommand cmd = new SqlCommand("sp_themTC", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@matc", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mand", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tentk", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@landmkcuoi", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@landncuoi", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@danhmuctc", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@thaotac", SqlDbType.NVarChar, 4000);

            //Gan gia tri
            cmd.Parameters[0].Value = tc.Idtc;
            cmd.Parameters[1].Value = tc.Idnd;
            cmd.Parameters[2].Value = tc.Tentk;
            cmd.Parameters[3].Value = tc.Landmkcuoi;
            cmd.Parameters[4].Value = tc.Landncuoi;
            cmd.Parameters[5].Value = tc.Danhmuctc;
            cmd.Parameters[6].Value = tc.Thaotac;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string matc)
        {
            DataProvider.RunDelSQL("sp_XoaTruyCap", DataProvider.con, matc, "@IdTC");
        }

        public static void RunDelAllData()
        {
            DataProvider.RunDelAllData("sp_xoatatcaTC", DataProvider.con);
        }

        public static bool checkmatc(string matc)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraTCtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@matc", SqlDbType.VarChar, 50);
            cmd.Parameters["@matc"].Value = matc;
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

        public static DataTable timkiemTC(string tentk, string landncuoi)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemTC", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@tentk", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@landncuoi", SqlDbType.VarChar, 50);

            cmd.Parameters[0].Value = tentk;
            cmd.Parameters[1].Value = landncuoi;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }
        public static void FillComboTenTK(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiTC", cb, ma, ten);
        }

        public static void capnhatthaotactc(string matc, string danhmuctc, string thaotac)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatThaotacTC", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@matc", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@danhmuctc", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@thaotac", SqlDbType.NVarChar, 4000);
            //Gan gia tri
            cmd.Parameters["@matc"].Value = matc;
            cmd.Parameters["@danhmuctc"].Value = danhmuctc;
            cmd.Parameters["@thaotac"].Value = thaotac;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();

        }

        public static void Connect()
        {
            DataProvider.connect();
        }

        public static void Disconnect()
        {
            DataProvider.disconnect();
        }

        public static SqlConnection GetCon()
        {
            return DataProvider.con;
        }
    }
}
