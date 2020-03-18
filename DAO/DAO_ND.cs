using DTO;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    public class DAO_ND
    {
        public static DataTable hienthind()
        {
            return DataProvider.GetDatatoTable("sp_hienthiND", DataProvider.con);
        }

        public static  DataTable hienthindcuthe(string manv)
        {
            return DataProvider.GetDataSpecific("sp_hienthinguoidungcuthe", DataProvider.con, manv, "@IdNV");
        }

        public static DataTable hienthindtheoten(string tennd)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_hienthiNDtheoten", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tennd", SqlDbType.NVarChar, 100);

            //Gan gia tri
            cmd.Parameters["@tennd"].Value = tennd;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;
        }

        public static void Themnd(DTO_ND nd)
        {
            SqlCommand cmd = new SqlCommand("sp_themND", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mand", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mannd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tennd", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@tentk", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@ngaytaotk", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@danhmuctc", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@quyendm", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters[0].Value = nd.Mand;
            cmd.Parameters[1].Value = nd.Mannd;
            cmd.Parameters[2].Value = nd.Manv;
            cmd.Parameters[3].Value = nd.Tennd;
            cmd.Parameters[4].Value = nd.Tentk;
            cmd.Parameters[5].Value = nd.Password;
            cmd.Parameters[6].Value = nd.Ngaytaotk;
            cmd.Parameters[7].Value = nd.Danhmuctc;
            cmd.Parameters[8].Value = nd.Quyendm;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void Suand(DTO_ND nd)
        {
            SqlCommand cmd = new SqlCommand("sp_suaND", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mand", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mannd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@danhmuctc", SqlDbType.NVarChar, 500);
            cmd.Parameters.Add("@quyendm", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters[0].Value = nd.Mand;
            cmd.Parameters[1].Value = nd.Mannd;
            cmd.Parameters[2].Value = nd.Danhmuctc;
            cmd.Parameters[3].Value = nd.Quyendm;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }


        public static void RunDelSQL(string mand)
        {
            DataProvider.RunDelSQL("sp_XoaND", DataProvider.con, mand, "@IdND");
        }

        public static void RunDelSQLOnTC(string mand)
        {
            DataProvider.RunDelSQL("sp_XoaNDtrenTruyCap", DataProvider.con, mand, "@IdND");
        }

        public static bool checkmand(string mand)
        {
            SqlCommand cmd = new SqlCommand("sp_KiemtraNDtrung", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mand", SqlDbType.VarChar, 50);
            cmd.Parameters["@mand"].Value = mand;
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

        public static DataTable timkiemND(string mannd, string tennd, string ngaytaotk)
        {
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand("sp_timkiemND", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@mannd", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@tennd", SqlDbType.NVarChar, 100);
            cmd.Parameters.Add("@ngaytaotk", SqlDbType.VarChar, 100);

            cmd.Parameters[0].Value = mannd;
            cmd.Parameters[1].Value = tennd;
            cmd.Parameters[2].Value = ngaytaotk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            table.Load(cmd.ExecuteReader());
            return table;

        }

        public static void capnhattaikhoan(string manv, string tk)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatTKNV", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@taikhoan", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@manv"].Value = manv;
            cmd.Parameters["@taikhoan"].Value = tk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();

        }

        public static void capnhatMK(string manv, string mk)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatMK", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@manv", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@mk", SqlDbType.NVarChar, 500);

            //Gan gia tri
            cmd.Parameters["@manv"].Value = manv;
            cmd.Parameters["@mk"].Value = mk;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void capnhatLanDMKcuoi(string mand, string landmkcuoi)
        {
            SqlCommand cmd = new SqlCommand("sp_CapnhatLanDMKcuoi", DataProvider.con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@mand", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@landmkcuoi", SqlDbType.VarChar, 100);

            //Gan gia tri
            cmd.Parameters["@mand"].Value = mand;
            cmd.Parameters["@landmkcuoi"].Value = landmkcuoi;

            if (DataProvider.con.State == ConnectionState.Closed)
            {
                DataProvider.con.Open();
            }
            cmd.ExecuteNonQuery();
            DataProvider.disconnect();
        }

        public static void FillComboMaNND(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiNND", cb, ma, ten);
        }

        public static void FillComboTenND(ComboBox cb, string ma, string ten)
        {
            DataProvider.FillCombo("sp_hienthiND", cb, ma, ten);
        }

        public static int CheckTenTK(TextBox txt)
        {
            int check = 0;
            if (txt.Text.Length < 6)
            {
                check = 1;
            }
            else
            {
                bool tmp = false, tmp1 = false, tmp2 = false;
                char[] wl = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                char[] wu = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                for (int i = 0; i < txt.TextLength; ++i)
                {
                    for (int j = 0; j < wl.Length; ++j)
                    {
                        if (txt.Text.ToCharArray()[i] == wl[j])
                        {
                            tmp = true;
                            goto label;
                        }
                        else
                        {
                            tmp = false;
                        }
                    }
                    for (int k = 0; k < wu.Length; ++k)
                    {
                        if (txt.Text.ToCharArray()[i] == wu[k])
                        {
                            tmp1 = true;
                            goto label;
                        }
                        else
                        {
                            tmp1 = false;
                        }
                    }
                    for (int l = 0; l < numbers.Length; ++l)
                    {
                        if (txt.Text.ToCharArray()[i] == numbers[l])
                        {
                            tmp2 = true;
                            break;
                        }
                        else
                        {
                            tmp2 = false;
                        }
                    }
                    label:
                    if (tmp == false && tmp1 == false && tmp2 == false)
                    {
                        check = 2;
                    }
                }
            }
            return check;
        }

        public static int CheckMK(TextBox txt)
        {
            int check = 0;
            if (txt.Text.Length < 6)
            {
                check = 1;
            }
            else
            {
                char[] wl = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                char[] wu = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                bool tmp = false, tmp1 = false, tmp2 = false, tmp3 = false, tmp4 = false, tmp5 = false, tmp6 = false, tmp7 = false;
                for (int i = 0; i < txt.TextLength; ++i)
                {
                    for (int j = 0; j < wl.Length; ++j)
                    {
                        if (txt.Text.ToCharArray()[i] == wl[j])
                        {
                            tmp = true;
                            tmp4 = true;
                            break;
                        }
                        else
                        {
                            tmp = false;
                        }
                    }
                    if (tmp == false)
                    {
                        tmp3 = true;
                        for (int k = 0; k < wu.Length; ++k)
                        {
                            if (txt.Text.ToCharArray()[i] == wu[k])
                            {
                                tmp1 = true;
                                tmp5 = true;
                                break;
                            }
                            else
                            {
                                tmp1 = false;
                            }
                        }
                        if (tmp1 == false)
                        {
                            for (int k = 0; k < numbers.Length; ++k)
                            {
                                if (txt.Text.ToCharArray()[i] == numbers[k])
                                {
                                    tmp2 = true;
                                    tmp6 = true;
                                    break;
                                }
                                else
                                {
                                    tmp2 = false;
                                }
                            }
                            if (tmp2 == false)
                            {
                                tmp7 = true;
                            }
                        }
                    }

                }
                if ((tmp == true && tmp3 == false) || (tmp1 == true && tmp4 == false && tmp6 == false && tmp7 == false) || (tmp2 == true && tmp4 == false && tmp5 == false && tmp7 == false))
                {
                    check = 2;
                }
                if ((tmp4 == true && tmp5 == true) || (tmp4 == true && tmp6 == true) || (tmp5 == true && tmp6 == true))
                {
                    check = 3;
                }
                if ((tmp7 == true && tmp4 == true) || (tmp7 == true && tmp5 == true) || (tmp7 == true && tmp6 == true) || (tmp7 == true && tmp4 == true && tmp5 == true) || (tmp7 == true && tmp4 == true && tmp6 == true) || (tmp7 == true && tmp5 == true && tmp6 == true) || (tmp7 == true && tmp4 == true && tmp5 == true && tmp6 == true) || (tmp4 == true && tmp5 == true && tmp6 == true))
                {
                    check = 4;
                }
            }
            return check;
        }
    }
}
