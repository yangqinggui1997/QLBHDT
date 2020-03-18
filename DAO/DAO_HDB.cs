using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_HDB
    {
        public static DataTable hienthiHDB()
        {
            return DataProvider.GetDatatoTable("sp_hienthiHDB", DataProvider.con);
        }

        public static DataTable hienthiHDBcuthe(string mahdb)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHDBcuthe", DataProvider.con, mahdb, "@IdHDB");
        }

        public static void ThemHDB(DTO_HDB hdb)
        {
            SqlCommand cmd = new SqlCommand("sp_ThemHDB", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@makh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaylaphd", SqlDbType.DateTime, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@tongtien", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@hinhthuctt", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@dathanhtoan", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@conno", SqlDbType.NVarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdb"].Value = hdb.Mahbd;
            cmd.Parameters["@manv"].Value = hdb.Manv;
            cmd.Parameters["@makh"].Value = hdb.Makh;
            cmd.Parameters["@ngaylaphd"].Value = hdb.Ngaylaphd;
            cmd.Parameters["@sl"].Value = hdb.Sl;
            cmd.Parameters["@tongtien"].Value = hdb.Tongtien;
            cmd.Parameters["@hinhthuctt"].Value = hdb.Hinhthuctt;
            cmd.Parameters["@dathanhtoan"].Value = hdb.Dathanhtoan;
            cmd.Parameters["@conno"].Value = hdb.Conno;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void SuaHDB(DTO_HDB hdb)
        {
            SqlCommand cmd = new SqlCommand("sp_suaHDB", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@makh", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaylaphd", SqlDbType.DateTime, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@tongtien", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@hinhthuctt", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@dathanhtoan", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@conno", SqlDbType.NVarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdb"].Value = hdb.Mahbd;
            cmd.Parameters["@manv"].Value = hdb.Manv;
            cmd.Parameters["@makh"].Value = hdb.Makh;
            cmd.Parameters["@ngaylaphd"].Value = hdb.Ngaylaphd;
            cmd.Parameters["@sl"].Value = hdb.Sl;
            cmd.Parameters["@tongtien"].Value = hdb.Tongtien;
            cmd.Parameters["@hinhthuctt"].Value = hdb.Hinhthuctt;
            cmd.Parameters["@dathanhtoan"].Value = hdb.Dathanhtoan;
            cmd.Parameters["@conno"].Value = hdb.Conno;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string mahdb)
        {
            DataProvider.RunDelSQL("sp_XoaHDB", DataProvider.con, mahdb, "@IdHDB");
        }

        public static void RunDelSQLOnHDBCT(string mahdb)
        {
            DataProvider.RunDelSQL("sp_XoaHDBtrenHDBCT", DataProvider.con, mahdb, "@IdHDB");
        }

        public static DataTable LayTenNV(string manv)
        {
            return DataProvider.GetDataSpecific("sp_LayTenNV", DataProvider.con, manv, "@IdNV");
        }

        public static DataTable LayTTSP(string masp)
        {
            return DataProvider.GetDataSpecific("sp_LayTTSP", DataProvider.con, masp, "@IdSP");
        }

        public static DataTable LayTTKH(string makh)
        {
            return DataProvider.GetDataSpecific("sp_LayTTKH", DataProvider.con, makh, "@IdKH");
        }

        public static bool ktHDBtrung(string mahdb)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraHDBtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);
            cmd.Parameters["@mahdb"].Value = mahdb;
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

        public static DataTable timkiemhdb(string ngaylap, string thanglap, string namlap)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemHDB", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ngaylap", SqlDbType.VarChar, 2);
            cmd.Parameters.Add("@thanglap", SqlDbType.VarChar, 2);
            cmd.Parameters.Add("@namlap", SqlDbType.VarChar, 4);

            cmd.Parameters["@ngaylap"].Value = ngaylap;
            cmd.Parameters["@thanglap"].Value = thanglap;
            cmd.Parameters["@namlap"].Value = namlap;
            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static void FillComboMaHD(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiHDB", cb, ma, ten);
        }

        public static void FillComboMaNV(ComboBox cb, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter("sp_hienthinhanvien", DataProvider.con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cb.DataSource = table;
            cb.ValueMember = ma; //Trường giá trị
            cb.DisplayMember = ten; //Trường hiển thị
            List<string> datatmp = new List<string>();
            for (int i = 0; i < cb.Items.Count; ++i)
            {
                datatmp.Add(cb.GetItemText(cb.Items[i]));
            }
            cb.DataSource = null;
            while (DataProvider.KiemtraTrung(datatmp) == true)
            {
                DataProvider.LocDuLieuTrung(datatmp);
            }
            for (int i = 0; i < datatmp.Count; ++i)
            {
                if (datatmp.ElementAt(i).Substring(0, 3) == "NBH")
                {
                    cb.Items.Add(datatmp.ElementAt(i));
                }
            }
        }

        public static void FillComboMaKH(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthikhachhang", cb, ma, ten);
        }

        public static void FillComboMaSP(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiSP", cb, ma, ten);
        }

        public static void CapnhatNoKH(string makh)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatnoKH", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@makh", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@makh"].Value = makh;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatNotrenHDB(string mahdb, string conno)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatNotrenHDB", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@conno", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdb"].Value = mahdb;
            cmd.Parameters["@conno"].Value = conno;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }
        public static void CapnhatSLSamPham(DTO_SP sp)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatSLSanPham", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@SL", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@masp"].Value = sp.Masp;
            cmd.Parameters["@SL"].Value = sp.Slnhap;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatSLSPtrenHDB(string mahdb)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatSLSPtrenHDB", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mahdb"].Value = mahdb;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatTTtrenHDB(string mahdb)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatTTtrenHDB", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdb", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mahdb"].Value = mahdb;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static string FormatNumber(string number)
        {
            return DataProvider.FormatNumber(number);
        }

        public static string ConvertToFloatType(string number)
        {
            return DataProvider.ConvertToFloatType(number);
        }

    }
}
