using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    class DataProvider
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ Application.StartupPath + @"\QLBHDT.mdf" +";Integrated Security=True;Connect Timeout=30");

        public static void connect()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public static void disconnect()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public static DataTable GetDatatoTable(string sql, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            da.Fill(dtb);
            return dtb;

        }

        public static DataTable GetDataSpecific(string sql, SqlConnection con, string ma, string thamso)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(thamso, SqlDbType.VarChar, 50);
            cmd.Parameters[thamso].Value = ma;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dtb = new DataTable();
            da.Fill(dtb);
            return dtb;

        }

        public static void RunDelSQL(string sp, SqlConnection con, string ma, string thamso)
        {
            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(thamso, SqlDbType.VarChar, 50);
            cmd.Parameters[thamso].Value = ma;

            connect();//mở kết nối
            cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            cmd.Dispose();//Giải phóng bộ nhớ
            
            disconnect();//đóng kết nối
            cmd = null;
        }

        public static void RunDelAllData(string sp, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;

            connect();//mở kết nối
            cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            cmd.Dispose();//Giải phóng bộ nhớ

            disconnect();//đóng kết nối
            cmd = null;

        }

        public static void UpdateSingleColumn(string sp, SqlConnection con, string thamso1, string thamso2, string giatri1, string giatri2)
        {
            SqlCommand cmd = new SqlCommand(sp, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(thamso1, SqlDbType.VarChar, 50);
            cmd.Parameters.Add(thamso2, SqlDbType.VarChar, 50);
            cmd.Parameters[thamso1].Value = giatri1;
            cmd.Parameters[thamso2].Value = giatri2;

            connect();//mở kết nối
            cmd.ExecuteNonQuery(); //Thực hiện câu lệnh SQL
            cmd.Dispose();//Giải phóng bộ nhớ

            disconnect();//đóng kết nối
            cmd = null;
        }

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, con);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma; //Trường giá trị
            cbo.DisplayMember = ten; //Trường hiển thị
            List<string> datatmp = new List<string>();
            for (int i = 0; i < cbo.Items.Count; ++i)
            {
                datatmp.Add(cbo.GetItemText(cbo.Items[i]));
            }
            cbo.DataSource = null;
            while (KiemtraTrung(datatmp) == true)
            {
                LocDuLieuTrung(datatmp);
            }
            for (int i = 0; i < datatmp.Count; ++i)
            {
                cbo.Items.Add(datatmp.ElementAt(i));
            }
        }

        public static void LocDuLieuTrung(List<string> list)
        {
            int flag;
            for (int j = 0; j < list.Count; ++j)
            {
                for (int i = j + 1; i < list.Count; ++i)
                {
                    flag = 0;
                    if (list.ElementAt(i) == list.ElementAt(j))
                    {
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }

        public static bool KiemtraTrung(List<string> list)
        {
            bool flag = false;
            for (int i = 0; i < list.Count; ++i)
            {
                for (int j = i + 1; j < list.Count; ++j)
                {
                    if (list.ElementAt(i) == list.ElementAt(j))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }

        public static string FormatNumber(string number)
        {
            string kq = "";
            int addspace = 0;
            if (number.Trim().Length > 3)
            {
                List<char> c = new List<char>();
                for(int i = number.Trim().Length -1; i >=0 ; --i)
                {
                    if(number.Trim().ToCharArray()[i] != ' ')
                    {
                        addspace++;
                        if (addspace == 3)
                        {
                            addspace = 0;
                            if (i != 0)
                            {
                                kq = " " + number.Trim().ToCharArray()[i] + kq;
                            }
                            else
                            {
                                kq = number.Trim().ToCharArray()[i] + kq;
                            }
                        }
                        else
                        {
                            kq = number.Trim().ToCharArray()[i] + kq;
                        }
                    }
                }
                return kq;
            }
            else
            {
                return number;
            }
        }

        public static string ConvertToFloatType(string number)
        {
            string kq = "";
            if (number.Trim().Length > 3)
            {
                string[] a = number.Trim().Split(' ');
                for(int i=0; i < a.Length; ++i)
                {
                    kq += a[i].Trim();
                }
                return kq;
            }
            else
            {
                return number;
            }
        }
    }
}
