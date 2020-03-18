using DTO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_HDN
    {
        public static DataTable hienthiHDN()
        {
            return DataProvider.GetDatatoTable("sp_hienthiHDN", DataProvider.con);
        }

        public static DataTable hienthiHDNcuthe(string mahdn)
        {
            return DataProvider.GetDataSpecific("sp_hienthiHDNcuthe", DataProvider.con, mahdn, "@IdHDN");
        }

        public static void ThemHDN(DTO_HDN hdn)
        {
            SqlCommand cmd = new SqlCommand("sp_ThemHDN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaylaphd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@tongtien", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dathanhtoan", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@conno", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ngayhhtt", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mahdn"].Value = hdn.Mahdn;
            cmd.Parameters["@manv"].Value = hdn.Manv;
            cmd.Parameters["@mancu"].Value = hdn.Mancu;
            cmd.Parameters["@ngaylaphd"].Value = hdn.Ngaylaphd;
            cmd.Parameters["@sl"].Value = hdn.Sl;
            cmd.Parameters["@tongtien"].Value = hdn.Tongtien;
            cmd.Parameters["@dathanhtoan"].Value = hdn.Dathanhtoan;
            cmd.Parameters["@conno"].Value = hdn.Conno;
            cmd.Parameters["@ngayhhtt"].Value = hdn.Ngayhhtt;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void SuaHDN(DTO_HDN hdn)
        {
            SqlCommand cmd = new SqlCommand("sp_suaHDN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@ngaylaphd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@tongtien", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dathanhtoan", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@conno", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ngayhhtt", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mahdn"].Value = hdn.Mahdn;
            cmd.Parameters["@manv"].Value = hdn.Manv;
            cmd.Parameters["@mancu"].Value = hdn.Mancu;
            cmd.Parameters["@ngaylaphd"].Value = hdn.Ngaylaphd;
            cmd.Parameters["@sl"].Value = hdn.Sl;
            cmd.Parameters["@tongtien"].Value = hdn.Tongtien;
            cmd.Parameters["@dathanhtoan"].Value = hdn.Dathanhtoan;
            cmd.Parameters["@conno"].Value = hdn.Conno;
            cmd.Parameters["@ngayhhtt"].Value = hdn.Ngayhhtt;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void RunDelSQL(string mahdn)
        {
            DataProvider.RunDelSQL("sp_XoaHDN", DataProvider.con, mahdn, "@IdHDN");
        }

        public static void RunDelSQLOnHDNCT(string mahdn)
        {
            DataProvider.RunDelSQL("sp_XoaHDNtrenHDNCT", DataProvider.con, mahdn, "@IdHDN");
        }

        public static DataTable LayTTNCU(string mancu)
        {
            return DataProvider.GetDataSpecific("sp_LayTTNCU", DataProvider.con, mancu, "@IdNCU");
        }

        public static DataTable LayTTSPTheoTenSP(string tensp)
        {
            string sql = "select IdSP, NhaSX from SANPHAM GROUP BY IdSP,NhaSX,TenSP HAVING TenSP= N'" + tensp + "'";
            SqlCommand cmd = new SqlCommand(sql, DataProvider.con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            da.Fill(dtb);
            return dtb;
        }

        public static bool ktHDNtrung(string mahdn)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraHDNtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);
            cmd.Parameters["@mahdn"].Value = mahdn;
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

        public static DataTable timkiemhdn(string ngaylap, string thanglap, string namlap)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemHDN", DataProvider.con);
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
            DataProvider.FillCombo("sp_hienthiHDN", cb, ma, ten);
        }

        public static void FillComboMaNCU(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiNCU", cb, ma, ten);
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

        public static void CapnhatTTtrenSP(string masp, string nhasx, string sl, string dongianhap)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatTTtrenSP", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@masp", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@nhasx", SqlDbType.NVarChar, 200);
            cmd.Parameters.Add("@sl", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@dongianhap", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@masp"].Value = masp;
            cmd.Parameters["@nhasx"].Value = nhasx;
            cmd.Parameters["@sl"].Value = sl;
            cmd.Parameters["@dongianhap"].Value = dongianhap;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatnoNCU(string mancu)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatnoNCU", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mancu", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mancu"].Value = mancu;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatSLSPtrenHDN(string mahdn)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatSLSPtrenHDN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mahdn"].Value = mahdn;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatNotrenHDN(string mahdn, string conno)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatNotrenHDN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@conno", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mahdn"].Value = mahdn;
            cmd.Parameters["@conno"].Value = conno;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void CapnhatTTtrenHDN(string mahdb)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatTTtrenHDN", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mahdn", SqlDbType.VarChar, 50);

            //Gan gia tri
            cmd.Parameters["@mahdn"].Value = mahdb;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }
    }
}
