using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using DTO;
using BUS;

namespace QLBHDT
{
    public partial class DANGKY : Form
    {
        public DANGKY()
        {
            InitializeComponent();
        }

        public static string manhomnd = "";
        public static bool xn = false;

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

        private void btnDangky_Click(object sender, EventArgs e)
        {
            if (txtmanhomnd.Text == string.Empty)
            {
                MessageBox.Show("Nhóm người dùng hiện tại chưa được thêm. Hãy liên hệ với quản trị viên để được giải quyết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtManv.Text == "" || txtTentaikhoan.Text == "" || txtMatkhau.Text == "" || txtnhaplaimatkhau.Text == "")
            {
                if (txtManv.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtManv.Focus();
                    return;
                }
                else
                {
                    DataTable dt = BUS_NV.hienthinvcuthe(txtManv.Text.Trim());
                    if(dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Mã nhân viên không tồn tại. Có thể nó chưa được thêm vào bảng 'Nhân viên'?", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtManv.Focus();
                        return;
                    }
                }
                if (txtTentaikhoan.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập tên tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTentaikhoan.Focus();
                    return;
                }
                if (txtMatkhau.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatkhau.Focus();
                    return;
                }
                if (txtnhaplaimatkhau.Text.Length == 0)
                {
                    MessageBox.Show("Bạn chưa xác nhận lại mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtnhaplaimatkhau.Focus();
                    return;
                }
            }
            else
            {
                if (txtMatkhau.Text.Length < 6)
                {
                    MessageBox.Show("Độ dài mật khẩu không hợp lệ! Độ dài mật khẩu phải chứa ít nhất 6 ký tự.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatkhau.Focus();
                    return;
                }
                if (txtMatkhau.Text != txtnhaplaimatkhau.Text)
                {
                    MessageBox.Show("Xác nhận mật khẩu không đúng. Hãy xác nhận lại mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtnhaplaimatkhau.Focus();
                    return;
                }
                string tennd = "", TK = "";
                string danhmuc = "", quyen = "";

                DataTable dt = BUS_NQH.hienthiNQHcuthe(txtmanhomqh.Text);
                if(dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    danhmuc = dr[3].ToString();
                    quyen = dr[4].ToString();
                }


                DataTable dtnv = BUS_NV.hienthinvcuthe(txtManv.Text.Trim());
                if(dtnv.Rows.Count == 1)
                {
                    DataRow dr = dtnv.Rows[0];
                    tennd = dr[1].ToString();
                    TK = dr[10].ToString();
                }

                TK += txtTentaikhoan.Text.Trim() + ", ";
                // mã hoá mật khẩu bằng thuật toá 3DES với key là 123
                string mk = EncryptDataByTripleDES(txtMatkhau.Text, "123");

                DTO_ND nd = new DTO_ND(txtmanguoidung.Text, txtmanhomnd.Text, txtManv.Text.Trim(), tennd, txtTentaikhoan.Text.Trim(), mk, DateTime.Now.ToShortDateString(), danhmuc, quyen);

                try
                {
                    BUS_ND.themND(nd);
                    BUS_ND.CapnhatTKNV(txtManv.Text.Trim(), TK);
                    MessageBox.Show("Đăng ký thành công!", "Congratulation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Đăng ký thất bại!", "Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Close();
                }
            }
        }

        //mã hoá bằng 3DES
        public static string EncryptDataByTripleDES(string Message, string password)
        {
            byte[] Results;
            var UTF8 = new UTF8Encoding();
            var HashProvider = new MD5CryptoServiceProvider();
            var TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = HashProvider.ComputeHash(UTF8.GetBytes(password));
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            var DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                Results = TDESAlgorithm.CreateEncryptor().TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtManv_Leave(object sender, EventArgs e)
        {
            txtManv_TextChanged(sender, e);
        }

        private void txtMatkhau_Leave(object sender, EventArgs e)
        {
            if (txtMatkhau.Text != "")
            {
                if (BUS_ND.CheckMK(txtMatkhau) == 1)
                {
                    erp.SetError(txtMatkhau, "Mật khẩu phải chứa ít nhất 6 ký tự!");
                    txtnhaplaimatkhau.Text = "";
                }
                else if (BUS_ND.CheckMK(txtMatkhau) == 2)
                {
                    erp.Clear();
                    lblmatkhau.Text = "Yếu!";
                    lblmatkhau.ForeColor = Color.Gray;
                }
                else if (BUS_ND.CheckMK(txtMatkhau) == 3)
                {
                    erp.Clear();
                    lblmatkhau.Text = "Trung bình!";
                    lblmatkhau.ForeColor = Color.YellowGreen;
                }
                else if (BUS_ND.CheckMK(txtMatkhau) == 4)
                {
                    erp.Clear();
                    lblmatkhau.Text = "Mạnh!";
                    lblmatkhau.ForeColor = Color.Green;
                }
            }
            else
            {
                erp.SetError(txtMatkhau, "Mật khẩu không được để trống!");
                lblmatkhau.Text = "";
            }
        }

        private void txtnhaplaimatkhau_Leave(object sender, EventArgs e)
        {
            if (txtnhaplaimatkhau.Text != "")
            {
                if (txtMatkhau.Text != txtnhaplaimatkhau.Text)
                {
                    MessageBox.Show("Mật khẩu vừa nhập không đúng!", "Incorrectly!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatkhau.Text = "";
                    txtnhaplaimatkhau.Text = "";
                    lblmatkhau.Text = "";
                    txtMatkhau.Focus();
                    return;
                }
            }
        }

        private void txtTentaikhoan_Leave(object sender, EventArgs e)
        {
            txtTentaikhoan_TextChanged(sender, e);
        }

        private void txtManv_TextChanged(object sender, EventArgs e)
        {
            if (txtManv.Text.Length != 0)
            {
                DataTable dt = BUS_NV.hienthinvcuthe(txtManv.Text.Trim());
                if(dt.Rows.Count == 1)
                {
                    erp.Clear();
                    txtTentaikhoan.Enabled = true;
                    txtMatkhau.Enabled = true;
                    txtnhaplaimatkhau.Enabled = true;
                    btnDangky.Enabled = true;
                    if (txtManv.Text.Substring(0, 3) == "NQT")
                    {
                        //DTO_NND nnd = new DTO_NND();
                        //nnd.Tennnd = "Nhóm người dùng là nhân viên quản trị";
                        DataTable dtnnd = BUS_NND.hienthiNNDtheoten("Nhóm người dùng là nhân viên quản trị");
                        if(dtnnd.Rows.Count == 1)
                        {
                            DataRow dr = dtnnd.Rows[0];
                            txtmanhomnd.Text = dr[0].ToString();
                            txttennhomnd.Text = "Nhóm người dùng là nhân viên quản trị";
                            DataTable dtnqh = BUS_NQH.hienthiNQHcuthe(dr[1].ToString());
                            if(dtnqh.Rows.Count == 1)
                            {
                                dr = dtnqh.Rows[0];
                                txtmanhomqh.Text = dr[0].ToString();
                                txttennhomqh.Text = dr[1].ToString();
                            }
                        }
                    }

                    if (txtManv.Text.Substring(0, 3) == "NKT")
                    {
                        DataTable dtnnd = BUS_NND.hienthiNNDtheoten("Nhóm người dùng là nhân viên kế toán");
                        if (dtnnd.Rows.Count == 1)
                        {
                            DataRow dr = dtnnd.Rows[0];
                            txtmanhomnd.Text = dr[0].ToString();
                            txttennhomnd.Text = "Nhóm người dùng là nhân viên kế toán";
                            DataTable dtnqh = BUS_NQH.hienthiNQHcuthe(dr[1].ToString());
                            if (dtnqh.Rows.Count == 1)
                            {
                                dr = dtnqh.Rows[0];
                                txtmanhomqh.Text = dr[0].ToString();
                                txttennhomqh.Text = dr[1].ToString();
                            }
                        }
                    }

                    if (txtManv.Text.Substring(0, 3) == "NBH")
                    {
                        DataTable dtnnd = BUS_NND.hienthiNNDtheoten("Nhóm người dùng là nhân viên bán hàng");
                        if (dtnnd.Rows.Count == 1)
                        {
                            DataRow dr = dtnnd.Rows[0];
                            txtmanhomnd.Text = dr[0].ToString();
                            txttennhomnd.Text = "Nhóm người dùng là nhân viên bán hàng";
                            DataTable dtnqh = BUS_NQH.hienthiNQHcuthe(dr[1].ToString());
                            if (dtnqh.Rows.Count == 1)
                            {
                                dr = dtnqh.Rows[0];
                                txtmanhomqh.Text = dr[0].ToString();
                                txttennhomqh.Text = dr[1].ToString();
                            }
                        }
                    }

                    if (txtManv.Text.Substring(0, 3) == "NTK")
                    {
                        DataTable dtnnd = BUS_NND.hienthiNNDtheoten("Nhóm người dùng là nhân viên thủ kho");
                        if (dtnnd.Rows.Count == 1)
                        {
                            DataRow dr = dtnnd.Rows[0];
                            txtmanhomnd.Text = dr[0].ToString();
                            txttennhomnd.Text = "Nhóm người dùng là nhân viên thủ kho";
                            DataTable dtnqh = BUS_NQH.hienthiNQHcuthe(dr[1].ToString());
                            if (dtnqh.Rows.Count == 1)
                            {
                                dr = dtnqh.Rows[0];
                                txtmanhomqh.Text = dr[0].ToString();
                                txttennhomqh.Text = dr[1].ToString();
                            }
                        }

                    }
                }
                else
                {
                    erp.SetError(txtManv, "Mã nhân viên này không tồn tại. Có thể nhân viên chưa được thêm vào bảng 'Nhân viên'!");
                    erp.SetIconAlignment(txtManv, ErrorIconAlignment.MiddleRight);
                    txtTentaikhoan.Enabled = false;
                    txtMatkhau.Enabled = false;
                    txtnhaplaimatkhau.Enabled = false;
                    txtnhaplaimatkhau.Text = "";
                    txtTentaikhoan.Text = "";
                    txtMatkhau.Text = "";
                    txtmanhomnd.Text = "";
                    txttennhomnd.Text = "";
                    txttennhomqh.Text = "";
                    txtmanhomqh.Text = "";
                    lblmatkhau.Text = "";
                    btnDangky.Enabled = false;
                }
            }
            else
            {
                erp.SetError(txtManv, "Mã nhân viên không được để trống!");
                erp.SetIconAlignment(txtManv, ErrorIconAlignment.MiddleRight);
                txtTentaikhoan.Enabled = false;
                txtMatkhau.Enabled = false;
                txtnhaplaimatkhau.Enabled = false;
                txtnhaplaimatkhau.Text = "";
                txtTentaikhoan.Text = "";
                txtMatkhau.Text = "";
                txtmanhomnd.Text = "";
                txttennhomnd.Text = "";
                txttennhomqh.Text = "";
                txtmanhomqh.Text = "";
                lblmatkhau.Text = "";
                btnDangky.Enabled = false;
            }
        }

        private void btnxemquyen_Click(object sender, EventArgs e)
        {
            manhomnd = txtmanhomnd.Text;
            xn = true;
            XEMQUYEN xq = new XEMQUYEN();
            xq.ShowDialog();
        }

        private void DANGKY_Load(object sender, EventArgs e)
        {
            txtTentaikhoan.Enabled = false;
            txtMatkhau.Enabled = false;
            txtnhaplaimatkhau.Enabled = false;
            btnDangky.Enabled = false;
            //tạo mã ngẫu nhiên
            int gt;
            bool kt = false;
            Random rand = new Random();
            gt = rand.Next(100000000, 999999999);
            string ma = "ND" + gt;
            DataTable dt = BUS_ND.hienthiND();
            DataRow drt;
            if (dt.Rows.Count > 0)
            {
                while (kt == false)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        drt = dt.Rows[i];
                        if (ma == drt["IdND"].ToString())
                        {
                            kt = false;
                            gt = rand.Next(100000000, 999999999);
                            ma = "ND" + gt;
                            break;
                        }
                        else
                        {
                            kt = true;
                        }
                    }
                }
                txtmanguoidung.Text = ma;
            }
            else
            {
                txtmanguoidung.Text = ma;
            }
        }

        private void txtTentaikhoan_TextChanged(object sender, EventArgs e)
        {
            if (txtTentaikhoan.Text != "")
            {
                if (BUS_ND.CheckTenTK(txtTentaikhoan) == 1)
                {
                    erp.SetError(txtTentaikhoan, "Tài khoản phải chứa ít nhất 6 ký tự!");
                    erp.SetIconAlignment(txtTentaikhoan, ErrorIconAlignment.MiddleRight);
                    txtTentaikhoan.Focus();
                    btnDangky.Enabled = false;
                }
                else if (BUS_ND.CheckTenTK(txtTentaikhoan) == 2)
                {
                    erp.SetError(txtTentaikhoan, "Tài khoản không được chứa dấu cách hoặc ký tự đặc biệt!");
                    erp.SetIconAlignment(txtTentaikhoan, ErrorIconAlignment.MiddleRight);
                    txtTentaikhoan.Focus();
                    btnDangky.Enabled = false;
                }

                DataTable dt = BUS_ND.hienthiND();
                bool kt = false;
                DataRow dr;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        if (txtTentaikhoan.Text.Trim() == dr["TenTK"].ToString())
                        {
                            kt = true;
                            break;
                        }
                        else
                        {
                            kt = false;
                        }
                    }
                }
                if (kt == true)
                {
                    erp.SetError(txtTentaikhoan, "Tài khoản này đã tồn tại. Hãy chọn tên khác!");
                    erp.SetIconAlignment(txtTentaikhoan, ErrorIconAlignment.MiddleRight);
                    txtTentaikhoan.Focus();
                    btnDangky.Enabled = false;
                }
                if ((BUS_ND.CheckTenTK(txtTentaikhoan) != 1 && BUS_ND.CheckTenTK(txtTentaikhoan) != 2) && kt == false)
                {
                    erp.Clear();
                    btnDangky.Enabled = true;
                }
            }
            else
            {
                erp.SetError(txtTentaikhoan, "Tài khoản không được để trống!");
                erp.SetIconAlignment(txtTentaikhoan, ErrorIconAlignment.MiddleRight);
            }
        }

        private void txtMatkhau_TextChanged(object sender, EventArgs e)
        {
            txtMatkhau_Leave(sender, e);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
