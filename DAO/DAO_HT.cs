using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_HT
    {
        public static DataTable hienthiHT()
        {
            return DataProvider.GetDatatoTable("sp_hienthiHT", DataProvider.con);
        }

        public static DataTable hienthiHTcuthe(string maht)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHTcuthe", DataProvider.con, maht, "@IdHT");
        }

        public static void ThemHT(DTO_HT ht)
        {
            SqlCommand cmd = new SqlCommand("sp_themHT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaytk", SqlDbType.DateTime, 50);

            //Gan gia tri
            cmd.Parameters["@maht"].Value = ht.Idht;
            cmd.Parameters["@manv"].Value = ht.Idnv;
            cmd.Parameters["@ngaytk"].Value = ht.Ngaytk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string maht)
        {
            DataProvider.RunDelSQL("sp_xoaHT", DataProvider.con, maht, "@maht");
        }

        public static void RunDelSQLOnHTCT(string maht)
        {
            DataProvider.RunDelSQL("sp_XoaHTtrenHTCT", DataProvider.con, maht, "@IdHT");
        }

        public static bool checkmaHT(string maht)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraHTtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);
            cmd.Parameters["@maht"].Value = maht;
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

        public static void CapnhatNgayTK(string madt, string ngaytk)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatNgayTKHT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaytk", SqlDbType.DateTime, 50);

            //Gan gia tri
            cmd.Parameters[0].Value = madt;
            cmd.Parameters[1].Value = ngaytk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static DataTable KiemTraTKHTDaTonTai(string thangtk, string namtk)
        {
            SqlCommand cmd = new SqlCommand("sp_KTTKHTTT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@thangtk", SqlDbType.VarChar, 2);
            cmd.Parameters.Add("@namtk", SqlDbType.VarChar, 4);

            cmd.Parameters["@thangtk"].Value = thangtk;
            cmd.Parameters["@namtk"].Value = namtk;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            da.Fill(dtb);
            return dtb;
        }

        public static DataTable timkiemHT(string manv, string maht)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemHT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@maht", SqlDbType.VarChar, 50);

            cmd.Parameters["@manv"].Value = manv;
            cmd.Parameters["@maht"].Value = maht;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static void FillComboMaTKHT(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiHT", cb, ma, ten);
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
                if (datatmp.ElementAt(i).Substring(0, 3) == "NTK")
                {
                    cb.Items.Add(datatmp.ElementAt(i));
                }
            }
        }
    }
}
