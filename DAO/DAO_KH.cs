using DTO;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class DAO_KH
    {
        public static DataTable hienthikh()
        {
            return DataProvider.GetDatatoTable("sp_hienthikhachhang", DataProvider.con);
        }

        public static DataTable hienthikhcuthe(string makh)
        {
            return DataProvider.GetDataSpecific("sp_hienthiKHcuthe", DataProvider.con, makh, "@IdKH");
        }

        public static void ThemKH(DTO_KH kh)
        {
            SqlCommand cmd = new SqlCommand("sp_themkhachhang", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@makh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tenkh", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ngaysinh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@socmnd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@loaikh", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@conno", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@danhgia", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@makh"].Value = kh.Idkh;
            cmd.Parameters["@tenkh"].Value = kh.Tenkh;
            cmd.Parameters["@ngaysinh"].Value = kh.Ngaysinh;
            cmd.Parameters["@gioitinh"].Value = kh.Gioitinh;
            cmd.Parameters["@socmnd"].Value = kh.Socmnd;
            cmd.Parameters["@diachi"].Value = kh.Diachi;
            cmd.Parameters["@sdt"].Value = kh.Sđt;
            cmd.Parameters["@loaikh"].Value = kh.Loaikh;
            cmd.Parameters["@conno"].Value = kh.Conno;
            cmd.Parameters["@danhgia"].Value = kh.Danhgia;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void SuaKH(DTO_KH kh)
        {
            SqlCommand cmd = new SqlCommand("sp_suaKH", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@makh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tenkh", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ngaysinh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@gioitinh", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@socmnd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@diachi", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@loaikh", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@conno", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@danhgia", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@makh"].Value = kh.Idkh;
            cmd.Parameters["@tenkh"].Value = kh.Tenkh;
            cmd.Parameters["@ngaysinh"].Value = kh.Ngaysinh;
            cmd.Parameters["@gioitinh"].Value = kh.Gioitinh;
            cmd.Parameters["@socmnd"].Value = kh.Socmnd;
            cmd.Parameters["@diachi"].Value = kh.Diachi;
            cmd.Parameters["@sdt"].Value = kh.Sđt;
            cmd.Parameters["@loaikh"].Value = kh.Loaikh;
            cmd.Parameters["@conno"].Value = kh.Conno;
            cmd.Parameters["@danhgia"].Value = kh.Danhgia;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string makh)
        {
            DataProvider.RunDelSQL("sp_XoaKH", DataProvider.con, makh, "@IdKH");
        }

        public static void RunDelSQLOnHDB(string makh)
        {
            DataProvider.RunDelSQL("sp_xoaKHtrenHDB", DataProvider.con, makh, "@IdKH");
        }

        public static bool ktkhtrung(string makh)
        {
            SqlCommand cmd = new SqlCommand("sp_Kiemtrakhtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@makh", SqlDbType.VarChar, 50);
            cmd.Parameters["@makh"].Value = makh;
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

        public static DataTable timkiemkh(string ten, string sdt, string loaikh)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemkh", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ten", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@sdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@loaikh", SqlDbType.NVarChar, 50);

            cmd.Parameters["@ten"].Value = ten;
            cmd.Parameters["@sdt"].Value = sdt;
            cmd.Parameters["@loaikh"].Value = loaikh;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

    }
}
