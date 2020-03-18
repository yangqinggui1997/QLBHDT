using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_DT
    {
        public static DataTable hienthiDT()
        {
            return DataProvider.GetDatatoTable("sp_hienthiDT", DataProvider.con);
        }

        public static DataTable hienthiDTcuthe(string madt)
        {
            return DataProvider.GetDataSpecific("sp_hienthiDTcuthe", DataProvider.con, madt, "@IdDT");
        }

        public static void ThemDT(DTO_DT dt, string thangtk, string namtk)
        {
            SqlCommand cmd = new SqlCommand("sp_themTKDT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@matkdt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaytk", SqlDbType.DateTime, 50);
            cmd.Parameters.Add("@thangtk", SqlDbType.VarChar, 2);
            cmd.Parameters.Add("@namtk", SqlDbType.VarChar, 4);

            //Gan gia tri
            cmd.Parameters["@matkdt"].Value = dt.Madt;
            cmd.Parameters["@manv"].Value = dt.Manv;
            cmd.Parameters["@ngaytk"].Value = dt.Ngaytk;
            cmd.Parameters["@thangtk"].Value = thangtk;
            cmd.Parameters["@namtk"].Value = namtk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatTK(string madt, string thangtk, string namtk, string ngaytk)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatTKDT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@madt", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@thangtk", SqlDbType.VarChar, 2);
            cmd.Parameters.Add("@namtk", SqlDbType.VarChar, 4);
            cmd.Parameters.Add("@ngaytk", SqlDbType.DateTime, 50);

            //Gan gia tri
            cmd.Parameters[0].Value = madt;
            cmd.Parameters[1].Value = thangtk;
            cmd.Parameters[2].Value = namtk;
            cmd.Parameters[3].Value = ngaytk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string madt)
        {
            DataProvider.RunDelSQL("sp_xoaDT", DataProvider.con, madt, "@madt");
        }

        public static bool checkmaDT(string madt)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraDTtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@madt", SqlDbType.VarChar, 50);
            cmd.Parameters["@madt"].Value = madt;
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

        public static DataTable KiemTraTKDTDaTonTai(string thangtk, string namtk)
        {
            SqlCommand cmd = new SqlCommand("sp_KTTKDTDT", DataProvider.con);
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

        public static DataTable KiemTraTKHDBTThnang(string thang, string nam)
        {
            SqlCommand cmd = new SqlCommand("sp_KTHDBTThang", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@thang", SqlDbType.VarChar, 2);
            cmd.Parameters.Add("@nam", SqlDbType.VarChar, 4);

            cmd.Parameters["@thang"].Value = thang;
            cmd.Parameters["@nam"].Value = nam;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            da.Fill(dtb);
            return dtb;
        }

        public static DataTable timkiemDT(string manv, string madt)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemDT", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@madt", SqlDbType.VarChar, 50);

            cmd.Parameters["@manv"].Value = manv;
            cmd.Parameters["@madt"].Value = madt;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static void FillComboMaTKDT(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiDT", cb, ma, ten);
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
                if (datatmp.ElementAt(i).Substring(0, 3) == "NKT")
                {
                    cb.Items.Add(datatmp.ElementAt(i));
                }
            }
        }
    }
}
