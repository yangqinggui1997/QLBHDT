using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_HDNCT
    {
        public static DataTable hienthiHDNCT()
        {
            return DataProvider.GetDatatoTable("sp_hienthiHDNCT", DataProvider.con);
        }

        public static DataTable hienthiHDNCTcuthe(string mahdn)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHDNCTcuthe", DataProvider.con, mahdn, "@IdHDN");
        }

        public static DataTable hienthiHDNCTlenBC(string mahdn)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHDNCTLenBC", DataProvider.con, mahdn, "@mahdn");
        }

        public static void ThemHDNCT(DTO_HDNCT hdnct)
        {
            SqlCommand cmd = new SqlCommand("sp_ThemHDNCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongianhap", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@giamgia", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@thanhtien", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdn"].Value = hdnct.Mahdn;
            cmd.Parameters["@masp"].Value = hdnct.Masp;
            cmd.Parameters["@sl"].Value = hdnct.Sl;
            cmd.Parameters["@dongianhap"].Value = hdnct.Dongianhap;
            cmd.Parameters["@giamgia"].Value = hdnct.Giamgia;
            cmd.Parameters["@thanhtien"].Value = hdnct.Thanhtien;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQLHDNCT(string mahdn, string masp)
        {
            SqlCommand cmd = new SqlCommand("sp_xoaHDNCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@IdHDN", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@IdSP", SqlDbType.VarChar, 50);

            cmd.Parameters["@IdHDN"].Value = mahdn;
            cmd.Parameters["@IdSP"].Value = masp;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.connect();
            }
            cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            DataProvider.disconnect();
            cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;

        }

        public static void SuaHDNCT(DTO_HDNCT hdnct)
        {
            SqlCommand cmd = new SqlCommand("sp_suaHDNCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongianhap", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@giamgia", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@thanhtien", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdn"].Value = hdnct.Mahdn;
            cmd.Parameters["@masp"].Value = hdnct.Masp;
            cmd.Parameters["@sl"].Value = hdnct.Sl;
            cmd.Parameters["@dongianhap"].Value = hdnct.Dongianhap;
            cmd.Parameters["@giamgia"].Value = hdnct.Giamgia;
            cmd.Parameters["@thanhtien"].Value = hdnct.Thanhtien;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static bool ktHDNCTtrung(string mahdn, string masp)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraHDNCTtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);

            cmd.Parameters["@mahdn"].Value = mahdn;
            cmd.Parameters["@masp"].Value = masp;

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
