using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_HDBCT
    {
        public static DataTable hienthiHDBCT()
        {
            return DataProvider.GetDatatoTable("sp_hienthiHDBCT", DataProvider.con);
        }

        public static DataTable hienthiHDBCTcuthe(string mahdb)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHDBCThientai", DataProvider.con, mahdb, "@mahdb");
        }

        public static DataTable hienthiHDBCTlenBC(string mahdb)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHDBCTLenBC", DataProvider.con, mahdb, "@mahdb");
        }

        public static void ThemHDBCT(DTO_HDBCT hdbct)
        {
            SqlCommand cmd = new SqlCommand("sp_ThemHDBCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongiaban", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@giamgia", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@thanhtien", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdb"].Value = hdbct.Mahdb;
            cmd.Parameters["@masp"].Value = hdbct.Masp;
            cmd.Parameters["@sl"].Value = hdbct.Sl;
            cmd.Parameters["@dongiaban"].Value = hdbct.Dongiaban;
            cmd.Parameters["@giamgia"].Value = hdbct.Giamgia;
            cmd.Parameters["@thanhtien"].Value = hdbct.Thanhtien;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQLHDBCT(string mahdb, string masp)
        {
            SqlCommand cmd = new SqlCommand("sp_xoaHDBCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@IdHDB", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@IdSP", SqlDbType.VarChar, 50);

            cmd.Parameters["@IdHDB"].Value = mahdb;
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

        public static void SuaHDBCT(DTO_HDBCT hdbct)
        {
            SqlCommand cmd = new SqlCommand("sp_suaHDBCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongiaban", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@giamgia", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@thanhtien", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdb"].Value = hdbct.Mahdb;
            cmd.Parameters["@masp"].Value = hdbct.Masp;
            cmd.Parameters["@sl"].Value = hdbct.Sl;
            cmd.Parameters["@dongiaban"].Value = hdbct.Dongiaban;
            cmd.Parameters["@giamgia"].Value = hdbct.Giamgia;
            cmd.Parameters["@thanhtien"].Value = hdbct.Thanhtien;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static bool ktHDBCTtrung(string mahdb, string masp)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraHDBCTtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);

            cmd.Parameters["@mahdb"].Value = mahdb;
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
