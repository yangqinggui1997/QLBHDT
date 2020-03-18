using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_HTCT
    {
        public static DataTable hienthiHTCT()
        {
            return DataProvider.GetDatatoTable("sp_hienthiHTCT", DataProvider.con);
        }

        public static DataTable hienthiHTCTlenbc(string maht)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHTCTLenBC", DataProvider.con,maht,"@maht");
        }  

        public static DataTable hienthiHTCTcuthe(string maht)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHTCTcuthe", DataProvider.con, maht, "@IdHT");
        }

        public static void ThemHTCT(DTO_HTCT htct)
        {
            SqlCommand cmd = new SqlCommand("sp_themHTCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ngaysx", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngayhh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaynhap", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@maht"].Value = htct.Idht;
            cmd.Parameters["@masp"].Value = htct.Idsp;
            cmd.Parameters["@tensp"].Value = htct.Tensp;
            cmd.Parameters["@ngaysx"].Value = htct.Ngaysx;
            cmd.Parameters["@ngayhh"].Value = htct.Ngayhh;
            cmd.Parameters["@ngaynhap"].Value = htct.Ngaynhap;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string maht, string masp)
        {
            SqlCommand cmd = new SqlCommand("sp_xoaHTCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);

            cmd.Parameters["@maht"].Value = maht;
            cmd.Parameters["@masp"].Value = masp;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.connect();
            }
            cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            DataProvider.disconnect();
            cmd.Dispose();//Giải phóng bộ nhớ
            cmd = null;

        }

        public static void CapnhatTKHTCT(string maht, string masp, string sl)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatTKHTCT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@SL", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters[0].Value = maht;
            cmd.Parameters[1].Value = masp;
            cmd.Parameters[2].Value = sl;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static bool ktHTCTtrung(string maht, string masp)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraHTCTtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);

            cmd.Parameters["@maht"].Value = maht;
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
