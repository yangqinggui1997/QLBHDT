using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_CN
    {
        public static DataTable hienthiCN()
        {
            return DataProvider.GetDatatoTable("sp_hienthiCN", DataProvider.con);
        }

        public static DataTable hienthiCNcuthe(string macn)
        {
            return DataProvider.GetDataSpecific("sp_hienthiCNcuthe", DataProvider.con, macn, "@IdCN");
        }

        public static void ThemCN(DTO_CN cn)
        {
            SqlCommand cmd = new SqlCommand("sp_ThemCN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@macn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaytk", SqlDbType.DateTime, 50);

            //Gan gia tri
            cmd.Parameters["@macn"].Value = cn.Macn;
            cmd.Parameters["@manv"].Value = cn.Manv;
            cmd.Parameters["@ngaytk"].Value = cn.Ngaytk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string macn)
        {
            DataProvider.RunDelSQL("sp_xoaCN", DataProvider.con, macn, "@IDCN");
        }

        public static void RunDelSQLOnCNCT(string macn)
        {
            DataProvider.RunDelSQL("sp_XoaCNtrenCNCT", DataProvider.con, macn, "@IdCN");
        }

        public static bool checkmaCN(string macn)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraCNtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@macn", SqlDbType.VarChar, 50);
            cmd.Parameters["@macn"].Value = macn;
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

        public static void CapnhatNgayTK(string macn, string ngaytk)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatNgayTKCN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@macn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaytk", SqlDbType.DateTime, 50);

            //Gan gia tri
            cmd.Parameters[0].Value = macn;
            cmd.Parameters[1].Value = ngaytk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static DataTable KiemTraTKCNDaTonTai(string thangtk, string namtk)
        {
            SqlCommand cmd = new SqlCommand("sp_KTTKCNTT", DataProvider.con);
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

        public static DataTable timkiemCN(string manv, string macn)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemCN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@macn", SqlDbType.VarChar, 50);

            cmd.Parameters["@manv"].Value = manv;
            cmd.Parameters["@macn"].Value = macn;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static void FillComboMaTKCN(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiCN", cb, ma, ten);
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
