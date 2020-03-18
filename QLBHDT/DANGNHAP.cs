using System;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using DTO;
using BUS;

namespace QLBHDT
{
    public partial class DANGNHAP : Form
    {
        public DANGNHAP()
        {
            InitializeComponent();
        }

        public static string Danhmuc = "", Quyen = "", user = "", pass = "", IdNV = "";
        public static string matc = "", mand = "", danhmuctc = "", thaotac = "";
        public static bool ghinhomk = false;


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // Bóng đổ
                cp.ClassStyle |= 0x20000;
                // Load các control cùng lúc
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void DANGNHAP_FormClosed(object sender, FormClosedEventArgs e)
        {
            BUS_TC.Disconnect();
            BUS_TC.GetCon().Dispose();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DANGNHAP_Load(object sender, EventArgs e)
        {
            if (ghinhomk == true)
            {
                btndangnhap.Enabled = true;
            }
            else
            {
                btndangnhap.Enabled = false;
            }
            danhmuctc = "";
            thaotac = "";
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                BUS_TC.Connect();
            }
            catch
            {
                MessageBox.Show("Kết nối cơ sỡ dữ liệu thất bại. Hãy thử lại lần nữa!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (BUS_TC.GetCon().State == ConnectionState.Open)
            {
                Danhmuc = "";
                Quyen = "";
                IdNV = "";
                DataTable dt = BUS_ND.hienthiND();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr;
                    bool kt = false;
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        string tentk = "", mk = "";
                        dr = dt.Rows[i];
                        tentk = dr["TenTK"].ToString();
                        mk = DecryptDataByTripleDES(dr["PassWord"].ToString(), "123");
                        if (tentk == txtUsers.Text.Trim() && mk == txtPass.Text)
                        {
                            kt = true;
                            Danhmuc = dr["DanhmucTC"].ToString();
                            Quyen = dr["QuyenDM"].ToString();
                            IdNV = dr["IdNV"].ToString();

                            //lấy mã người dùng, lần đăng nhập cuối
                            mand = dr["IdND"].ToString();

                            break;
                        }
                        else
                        {
                            kt = false;
                        }

                    }
                    if (kt == true)
                    {
                        if (ckbgn.CheckState == CheckState.Checked)
                        {
                            ghinhomk = true;
                        }
                        else
                        {
                            ghinhomk = false;
                        }
                        user = txtUsers.Text;
                        pass = txtPass.Text;

                        // tạo dữ liệu trên bảng truy cập
                        //tạo mã ngẫu nhiên
                        int gt;
                        bool k = false;
                        Random rand = new Random();
                        gt = rand.Next(100000000, 999999999);
                        string ma = "TC" + gt;
                        DataTable table = BUS_TC.hienthiTC();
                        DataRow drt;
                        if (table.Rows.Count > 0)
                        {
                            while (k == false)
                            {
                                for (int j = 0; j < table.Rows.Count; ++j)
                                {
                                    drt = table.Rows[j];
                                    if (ma == drt["IdTC"].ToString())
                                    {
                                        k = false;
                                        gt = rand.Next(100000000, 999999999);
                                        ma = "TC" + gt;
                                        break;
                                    }
                                    else
                                    {
                                        k = true;
                                    }
                                }
                            }
                            matc = ma;
                        }
                        else
                        {
                            matc = ma;
                        }

                        //chèn dữ liệu vào bảng truy cập
                        DTO_TC tc = new DTO_TC(matc,mand,user,"",DateTime.Now.ToShortDateString(),"","");
                        BUS_TC.themTC(tc);

                        this.Hide();
                        MAIN m = new MAIN();
                        m.mnuTaiKhoan.Text = txtUsers.Text;
                        m.Show();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mặt khẩu không đúng hoặc không tồn tại! Bạn chưa đăng ký tài khoản?","Error!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        txtPass.Text = "";
                        txtPass.Focus();
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mặt khẩu không đúng hoặc không tồn tại! Bạn chưa đăng ký tài khoản?", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Text = "";
                    txtPass.Focus();
                    return;
                }

            }
        }

        //giải mã bằng 3DES
        public static string DecryptDataByTripleDES(string Message, string password)
        {
            byte[] Results;
            var UTF8 = new UTF8Encoding();
            var HashProvider = new MD5CryptoServiceProvider();
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = HashProvider.ComputeHash(UTF8.GetBytes(password));
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            var DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                Results = TDESAlgorithm.CreateDecryptor().TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

        private void txtUsers_TextChanged(object sender, EventArgs e)
        {
            if (txtUsers.Text == "" || txtPass.Text == "")
            {
                btndangnhap.Enabled = false;
            }
            else if (txtUsers.Text != "" && txtPass.Text != "")
            {
                btndangnhap.Enabled = true;
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtUsers.Text == "" || txtPass.Text == "")
            {
                btndangnhap.Enabled = false;
            }
            else if (txtUsers.Text != "" && txtPass.Text != "")
            {
                btndangnhap.Enabled = true;
            }
        }

        private void btndangky_Click(object sender, EventArgs e)
        {
            DANGKY dk = new DANGKY();
            dk.ShowDialog();
        }
    }
}
