using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_SP
    {
        public static DataTable hienthisp()
        {
            return DataProvider.GetDatatoTable("sp_hienthiSP", DataProvider.con);
        }

        public static DataTable hienthincucuthe(string mancu)
        {
            return DataProvider.GetDataSpecific("sp_hienthinhacungungcuthe", DataProvider.con, mancu, "@IdNCU");
        }

        public static DataTable hienthiSPcuthe(string masp)
        {
            return DataProvider.GetDataSpecific("sp_hienthiSPcuthe", DataProvider.con, masp, "@IdSP");
        }

        public static void ThemSP(DTO_SP sp)
        {
            SqlCommand cmd = new SqlCommand("sp_ThemSP", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ngaysx", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngayhh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaynhap", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@nhasx", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@slnhap", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongianhap", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongiabanle", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongiabansi", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@donvi", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@giamgia", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@anhsp", SqlDbType.NVarChar, 1000);

            //Gan gia tri
            cmd.Parameters[0].Value = sp.Masp;
            cmd.Parameters[1].Value = sp.Mancu;
            cmd.Parameters[2].Value = sp.Tensp;
            cmd.Parameters[3].Value = sp.Ngaysx;
            cmd.Parameters[4].Value = sp.Ngayhh;
            cmd.Parameters[5].Value = sp.Ngaynhap;
            cmd.Parameters[6].Value = sp.Nhasx;
            cmd.Parameters[7].Value = sp.Slnhap;
            cmd.Parameters[8].Value = sp.Dongianhap;
            cmd.Parameters[9].Value = sp.Dongiabanle;
            cmd.Parameters[10].Value = sp.Dongiabansi;
            cmd.Parameters[11].Value = sp.Donvi;
            cmd.Parameters[12].Value = sp.Giamgia;
            cmd.Parameters[13].Value = sp.Anhsp;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void SuaSP(DTO_SP sp)
        {
            SqlCommand cmd = new SqlCommand("sp_suaSP", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@ngaysx", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngayhh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaynhap", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@nhasx", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@slnhap", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongianhap", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongiabanle", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongiabansi", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@donvi", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@giamgia", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@anhsp", SqlDbType.NVarChar, 1000);

            //Gan gia tri
            cmd.Parameters[0].Value = sp.Masp;
            cmd.Parameters[1].Value = sp.Mancu;
            cmd.Parameters[2].Value = sp.Tensp;
            cmd.Parameters[3].Value = sp.Ngaysx;
            cmd.Parameters[4].Value = sp.Ngayhh;
            cmd.Parameters[5].Value = sp.Ngaynhap;
            cmd.Parameters[6].Value = sp.Nhasx;
            cmd.Parameters[7].Value = sp.Slnhap;
            cmd.Parameters[8].Value = sp.Dongianhap;
            cmd.Parameters[9].Value = sp.Dongiabanle;
            cmd.Parameters[10].Value = sp.Dongiabansi;
            cmd.Parameters[11].Value = sp.Donvi;
            cmd.Parameters[12].Value = sp.Giamgia;
            cmd.Parameters[13].Value = sp.Anhsp;


            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string masp)
        {
            DataProvider.RunDelSQL("sp_xoaSP", DataProvider.con, masp, "@masp");
        }

        public static void RunDelSQLOnHTCT(string masp)
        {
            DataProvider.RunDelSQL("sp_xoaSPtrenHTCT", DataProvider.con, masp, "@IdSP");
        }

        public static void RunDelSQLOnHDNCT(string masp)
        {
            DataProvider.RunDelSQL("sp_xoaSPtrenHDNCT", DataProvider.con, masp, "@IdSP");
        }

        public static void RunDelSQLOnHDBCT(string masp)
        {
            DataProvider.RunDelSQL("sp_xoaSPtrenHDBCT", DataProvider.con, masp, "@IdSP");
        }

        public static bool checkmasp(string masp)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraSPtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters["@masp"].Value = masp;
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

        public static DataTable timkiemSP(string mancu, string tensp)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemSP", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tensp", SqlDbType.NVarChar, 200);

            cmd.Parameters["@mancu"].Value = mancu;
            cmd.Parameters["@tensp"].Value = tensp;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static void FillCombo(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiNCU",cb, ma, ten);
        }
    }
}
