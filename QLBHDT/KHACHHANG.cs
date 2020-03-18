using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Collections.Generic;

namespace QLBHDT
{
    public partial class KHACHHANG : Form
    {
        public KHACHHANG()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private DataTable kh;
        private string lbl;

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

        private void KHACHHANG_Load(object sender, EventArgs e)
        {
            lbl = lblkqtkkh.Text;
            btnundo.Enabled = false;
            btnredo.Enabled = false;
            LoadDataGridView();

            string[] mang = { "Công ty", "Đại lý", "Khác" };
            cbLoaikh.Items.AddRange(mang);
            cbLoaikh.SelectedIndex = -1;

            //QLTC
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            if (MAIN.tkkh == false)
            {
                string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
                string[] quyenhan = DANGNHAP.Quyen.Split('|');

                for (int j = 0; j < danhmuctmp.Length; ++j)
                {
                    if (danhmuctmp[j].Trim() == "Quản lý khách hàng")
                    {
                        string[] kh;
                        kh = quyenhan[j].Split(';');
                        if (kh != null)
                        {
                            foreach (string items in kh)
                            {
                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                {
                                    btnThem.Enabled = true;
                                }
                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                {
                                    btnSua.Enabled = true;
                                }
                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                {
                                    btnXoa.Enabled = true;
                                }
                            }
                        }
                        break;
                    }

                }
            }
            else
            {
                MAIN.tkkh = false;
            }
        }

        private void LoadDataGridView()
        {
            kh = BUS_KH.hienthikh(); //Lấy dữ liệu từ bảng
            DGVKhachHang.DataSource = kh;
            DGVKhachHang.AllowUserToAddRows = false;
            DGVKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaKhachHang.Text = "Mã khách hàng sẽ tự động thêm!";
            txtTenKhachHang.Text = string.Empty;
            chkGioitinh.Checked = false;
            txtdiachi.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now;
            txtSĐT.Text = string.Empty;
            txtsocmnd.Text = string.Empty;
            txtdanhgia.Text = string.Empty;
            cbLoaikh.Text = string.Empty;
            txtconno.Text = string.Empty;
            lblkqtkkh.Text = lbl;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string gt;
            if (txtTenKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhachHang.Focus();
                return;
            }
            if (txtdiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdiachi.Focus();
                return;
            }
            if (txtSĐT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSĐT.Focus();
                return;
            }
            if (cbLoaikh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLoaikh.Focus();
                return;
            }
            if (txtsocmnd.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số chứng minh nhân dân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsocmnd.Focus();
                return;
            }

            //tạo mã ngẫu nhiên
            int value;
            bool kt = false;
            Random rand = new Random();
            value = rand.Next(100000000, 999999999);
            string makh = "KH" + value;
            DataRow dr;
            if (BUS_KH.hienthikh().Rows.Count > 0)
            {
                while (kt == false)
                {
                    for (int i = 0; i < BUS_KH.hienthikh().Rows.Count; ++i)
                    {
                        dr = BUS_KH.hienthikh().Rows[i];
                        if (makh == dr["IdKH"].ToString())
                        {
                            kt = false;
                            value = rand.Next(100000000, 999999999);
                            makh = "KH" + value;
                            break;
                        }
                        else
                        {
                            kt = true;
                        }
                    }
                }
            }

            //Kiểm tra đã tồn tại mã khách chưa
            if (!BUS_KH.ktkhtrung(makh))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhachHang.Focus();
                return;
            }

            if (chkGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            //Chèn thêm
            DTO_KH kh = new DTO_KH(makh, txtTenKhachHang.Text, dtpNgaySinh.Text, gt, txtsocmnd.Text, txtdiachi.Text, txtSĐT.Text, cbLoaikh.Text, txtconno.Text, txtdanhgia.Text);
            BUS_KH.themkh(kh);

            LoadDataGridView();
            ResetValues();

            //thêm dữ liệu cho danh mục quản lý truy cập
            DANGNHAP.thaotac += "Thêm, ";

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string gt;
            if (kh.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtMaKhachHang.Text == "Mã khách hàng sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhachHang.Focus();
                return;
            }
            if (txtdiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdiachi.Focus();
                return;
            }
            if (txtSĐT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSĐT.Focus();
                return;
            }
            if (cbLoaikh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập loại khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbLoaikh.Focus();
                return;
            }
            if (txtsocmnd.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số chứng minh nhân dân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtsocmnd.Focus();
                return;
            }

            if (chkGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            DTO_KH KH = new DTO_KH(txtMaKhachHang.Text, txtTenKhachHang.Text, dtpNgaySinh.Text, gt, txtsocmnd.Text, txtdiachi.Text, txtSĐT.Text, cbLoaikh.Text, txtconno.Text, txtdanhgia.Text);

            BUS_KH.suaKH(KH);

            LoadDataGridView();
            ResetValues();

            DANGNHAP.thaotac += "Sửa, ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (kh.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhachHang.Text == "Mã khách hàng sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xoá khách hàng sẽ xoá tất cả dữ liệu của khách hàng và tất cả các thông tin về hoá đơn có liên quan với khách hàng này. Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                List<string> idkh = new List<string>();

                DataTable dt;
                DataRow dr;

                //Lấy mã hóa đơn bán của khách hàng hiện tại để xóa trên bảng hóa đơn bán
                dt = BUS_HDB.hienthiHDB();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        if(dr[2].ToString() == txtMaKhachHang.Text)
                        {
                            idkh.Add(dr["IdHDB"].ToString());
                        }

                    }
                }

                //Xóa trên bảng hóa đơn bán và hóa đơn bán chi tiết
                foreach(string item in idkh)
                {
                    BUS_HDB.RunDelSQLOnHDBCT(item);
                    BUS_HDB.RunDelSQL(item);
                }

                BUS_KH.RunDelSQL(txtMaKhachHang.Text);
                LoadDataGridView();
                ResetValues();

            }
            DANGNHAP.thaotac += "Xoá, ";
        }


        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtSĐT_Leave(object sender, EventArgs e)
        {
            if (txtSĐT.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(txtSĐT.Text, out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSĐT.Focus();
                }
                else
                {
                    if (a < 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSĐT.Focus();
                    }
                    else
                    {
                        if (txtSĐT.Text.Length < 10 || txtSĐT.Text.Length > 11)
                        {
                            MessageBox.Show("Số điện thoại hợp lệ phải chứa ít nhất 10 số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSĐT.Focus();
                            return;
                        }
                    }

                }
            }
        }

        private void txtsocmnd_Leave(object sender, EventArgs e)
        {
            if (txtsocmnd.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(txtsocmnd.Text, out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtsocmnd.Focus();
                }
                else
                {
                    if (a < 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtsocmnd.Focus();
                    }
                    else
                    {
                        if (txtsocmnd.Text.Length != 9)
                        {
                            MessageBox.Show("Số chứng minh nhân dân hợp lệ phải chứa 9 chữ số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtsocmnd.Focus();
                            return;
                        }
                    }
                }

            }
        }

        private void txtconno_Leave(object sender, EventArgs e)
        {
            if (txtconno.Text != "")
            {
                double a = 0;
                if (!double.TryParse(txtconno.Text, out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtconno.Focus();
                }
                else
                {
                    if (a < 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtconno.Focus();
                    }
                }

            }
        }

        private void cbLoaikh_Leave(object sender, EventArgs e)
        {
            if (cbLoaikh.Text != "")
            {
                bool flag = true;
                for(int i=0; i < cbLoaikh.Items.Count; ++i)
                {
                    if (cbLoaikh.Text == cbLoaikh.Items[i].ToString())
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (flag == false)
                {
                    MessageBox.Show("Loại khách hàng chỉ bao gồm: 'Công ty', 'Đại lý' và 'Khác'! Chú ý viết hoa ký tự đầu.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbLoaikh.Focus();

                }
            }
        }

        private void KHACHHANG_FormClosed(object sender, FormClosedEventArgs e)
        {
            DANGNHAP.thaotac += " | ";

        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (txtTenKhachHang.Text == string.Empty && txtSĐT.Text == string.Empty && cbLoaikh.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập điều kiện tìm kiếm!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO_KH kh = new DTO_KH();
            kh.Tenkh = txtTenKhachHang.Text;
            kh.Sđt = txtSĐT.Text;
            kh.Loaikh = cbLoaikh.Text;
            DataTable dt = BUS_KH.timkiemkh(kh.Tenkh, kh.Sđt, kh.Loaikh);
            DGVKhachHang.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                lblkqtkkh.Text = "Không có khách hàng nào thoả mãn điều kiện tìm kiếm!";
            }
            else
            {
                lblkqtkkh.Text = "Có " + dt.Rows.Count + " khách hàng nào thoả mãn điều kiện tìm kiếm!";
            }
            DANGNHAP.thaotac += "Tìm kiếm, ";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                btnMaximize.Image = Properties.Resources.minimize__1_;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                btnMaximize.Image = Properties.Resources.maximize_size_option;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btnredo_Click(object sender, EventArgs e)
        {

        }

        private void btnundo_Click(object sender, EventArgs e)
        {

        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            DGVKhachHang.DataSource = kh;
            ResetValues();
        }

        private void DGVKhachHang_Click(object sender, EventArgs e)
        {
            if (DGVKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DGVKhachHang.CurrentRow.Index == DGVKhachHang.NewRowIndex)
            {
                MessageBox.Show("Hãy chọn dòng có thông tin!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtMaKhachHang.Text = DGVKhachHang.CurrentRow.Cells["IdKH"].Value.ToString();
            txtTenKhachHang.Text = DGVKhachHang.CurrentRow.Cells["TenKH"].Value.ToString();
            if (DGVKhachHang.CurrentRow.Cells["Gioitinh"].Value.ToString() == "Nam") chkGioitinh.Checked = true;
            else chkGioitinh.Checked = false;
            txtdiachi.Text = DGVKhachHang.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtSĐT.Text = DGVKhachHang.CurrentRow.Cells["SĐT"].Value.ToString();
            dtpNgaySinh.Text = DGVKhachHang.CurrentRow.Cells["NgaySinh"].Value.ToString();
            cbLoaikh.Text = DGVKhachHang.CurrentRow.Cells["LoaiKH"].Value.ToString();
            txtconno.Text = DGVKhachHang.CurrentRow.Cells["Conno"].Value.ToString();
            txtdanhgia.Text = DGVKhachHang.CurrentRow.Cells["Danhgia"].Value.ToString();
            txtsocmnd.Text = DGVKhachHang.CurrentRow.Cells["SoCMND"].Value.ToString();

        }

        private void txtconno_TextChanged(object sender, EventArgs e)
        {
            string conno = BUS_HDB.ConvertToFloatType(txtconno.Text);
            txtconno.Text = BUS_HDB.FormatNumber(conno);
        }
    }
}
