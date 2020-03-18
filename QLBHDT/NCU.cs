using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Collections.Generic;

namespace QLBHDT
{
    public partial class NCU : Form
    {
        public delegate void GetdatainCB(ComboBox cb);
        public event GetdatainCB Getdataincb;

        public NCU()
        {
            InitializeComponent();
        }

        private DataTable ncu;
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

        private void NCU_Load(object sender, EventArgs e)
        {
            lbl = lblkqtkncu.Text;
            btnundo.Enabled = false;
            btnredo.Enabled = false;
            LoadDataGridView();

            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            if (MAIN.tkncu == false)
            {
                string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
                string[] quyenhan = DANGNHAP.Quyen.Split('|');

                for (int j = 0; j < danhmuctmp.Length; ++j)
                {
                    if (danhmuctmp[j].Trim() == "Quản lý nhà cung ứng")
                    {
                        string[] ncu;
                        ncu = quyenhan[j].Split(';');
                        if (ncu != null)
                        {
                            foreach (string items in ncu)
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
                MAIN.tkncu = false;
            }
        }

        private void LoadDataGridView()
        {
            ncu = BUS_NCU.hienthiNCU(); //Lấy dữ liệu từ bảng
            DGVNCU.DataSource = ncu;
            DGVNCU.AllowUserToAddRows = false;
            DGVNCU.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaNCU.Text = "Mã nhà cung ứng sẽ tự động thêm!";
            txtTenNCU.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtSĐT.Text = string.Empty;
            txtSĐT.Text = string.Empty;
            cbQuymoNCU.Text = string.Empty;
            txtconnoncu.Text = string.Empty;
            lblkqtkncu.Text = lbl;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenNCU.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung ứng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCU.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtSĐT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSĐT.Focus();
                return;
            }
            if (cbQuymoNCU.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập quy mô NCƯ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbQuymoNCU.Focus();
                return;
            }

            //tạo mã ngẫu nhiên
            int value;
            bool kt = false;
            Random rand = new Random();
            value = rand.Next(100000000, 999999999);
            string mancu = "NCU" + value;
            DataRow dr;
            if (BUS_NCU.hienthiNCU().Rows.Count > 0)
            {
                while (kt == false)
                {
                    for (int i = 0; i < BUS_NCU.hienthiNCU().Rows.Count; ++i)
                    {
                        dr = BUS_NCU.hienthiNCU().Rows[i];
                        if (mancu == dr["IdNCU"].ToString())
                        {
                            kt = false;
                            value = rand.Next(100000000, 999999999);
                            mancu = "NCU" + value;
                            break;
                        }
                        else
                        {
                            kt = true;
                        }
                    }
                }
            }

            //Kiểm tra đã tồn tại mã nhà cung ứng chưa
            if (!BUS_NCU.ktncutrung(mancu))
            {
                MessageBox.Show("Mã nhà cung ứng này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNCU.Focus();
                return;
            }

            //Chèn thêm
            DTO_NCU ncu = new DTO_NCU(mancu, txtTenNCU.Text, txtDiaChi.Text, txtSĐT.Text, cbQuymoNCU.Text,txtconnoncu.Text);
            BUS_NCU.themNCU(ncu);
      
            LoadDataGridView();
            ResetValues();

            //Cập nhật lại mã nhà cung ứng trên combobx mã nhà cung ứng trên form sản phẩm.
            CapnhatCB_NCU();

            DANGNHAP.thaotac += "Thêm, ";
        }

        private void CapnhatCB_NCU()
        {
            ComboBox cb = new ComboBox();
            DataGridViewRow r;
            for (int i = 0; i < DGVNCU.RowCount; ++i)
            {
                r = DGVNCU.Rows[i];
                cb.Items.Add(r.Cells[0].Value);
            }

            if (Getdataincb != null)
            {
                Getdataincb(cb);
            }
            else
            {
            }

        }


        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void NCU_FormClosed(object sender, FormClosedEventArgs e)
        {
            DANGNHAP.thaotac += " | ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (ncu.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNCU.Text == "Mã nhà cung ứng sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xoá nhà cung ứng sẽ xoá tất cả dữ liệu của nhà cung ứng và tất cả các thông tin về nhà cung ứng trên bảng sản phẩm, bảng hoá đơn nhập, bảng hoá đơn bán và bảng thống kê công nợ. Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                //Lấy mã nhà cung ứng trên bảng thống kê công nợ và xoá thông tin của NCƯ trên bảng thống kê công nợ.
                List<string> id = new List<string>();
                DataTable dt;
                DataRow dr;
                dt = BUS_CNCT.hienthiCNCT();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        if(dr[1].ToString() == txtMaNCU.Text)
                            id.Add(dr["IdCN"].ToString());

                    }
                }

                //Xóa trên bảng công nợ chi tiết
                foreach(string item in id)
                {
                    BUS_CN.RunDelSQLOnCNCT(item);
                }

                id.Clear();

                //xóa nhà cung ứng trên bảng hóa đơn nhập
                //Lấy mã hóa đơn nhập trên bảng hóa đơn nhập
                dt = BUS_HDN.hienthiHDN();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        if (dr[2].ToString() == txtMaNCU.Text)
                        {
                            id.Add(dr["IdHDN"].ToString());
                        }
                    }
                }

                //Xóa trên bảng hóa đơn nhập chi tiết
                foreach (string item in id)
                {
                    BUS_HDN.RunDelSQLOnHDNCT(item);
                    BUS_HDN.RunDelSQL(item);
                }

                id.Clear();

                //Lấy mã sản phẩm trên bảng sản phẩm để xóa sản phẩm
                dt = BUS_SP.hienthisp();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        if (dr[1].ToString() == txtMaNCU.Text)
                        {
                            id.Add(dr["IdSP"].ToString());
                        }
                    }
                }

                //Xóa trên bảng sản phẩm
                foreach (string item in id)
                {
                    BUS_SP.RunDelSQLOnHTCT(item);
                    BUS_SP.RunDelSQLOnHDBCT(item);
                    BUS_SP.RunDelSQLOnHDNCT(item);
                    BUS_SP.RunDelSQL(item);
                }

                BUS_NCU.RunDelSQL(txtMaNCU.Text);

                LoadDataGridView();
                ResetValues();

                //Cập nhật lại mã nhà cung ứng trên combobx mã nhà cung ứng trên form sản phẩm.
                CapnhatCB_NCU();
                DANGNHAP.thaotac += "Xoá, ";
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (ncu.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtMaNCU.Text == "Mã nhà cung ứng sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtTenNCU.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhà cung ứng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenNCU.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiaChi.Focus();
                return;
            }
            if (txtSĐT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSĐT.Focus();
                return;
            }
            if (cbQuymoNCU.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập quy mô NCƯ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbQuymoNCU.Focus();
                return;
            }

            DTO_NCU NCU = new DTO_NCU(txtMaNCU.Text, txtTenNCU.Text, txtDiaChi.Text, txtSĐT.Text, cbQuymoNCU.Text, txtconnoncu.Text);

            BUS_NCU.suaNCU(NCU);
            LoadDataGridView();
            ResetValues();

            DANGNHAP.thaotac += "Sửa, ";
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (txtTenNCU.Text == string.Empty && txtSĐT.Text == string.Empty && cbQuymoNCU.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập điều kiện tìm kiếm!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO_NCU ncu = new DTO_NCU();
            ncu.Tenncu = txtTenNCU.Text;
            ncu.Sdt = txtSĐT.Text;
            ncu.Quymoncu = cbQuymoNCU.Text;
            DataTable dt = BUS_NCU.timkiemNCU(ncu.Tenncu, ncu.Sdt, ncu.Quymoncu);
            DGVNCU.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                lblkqtkncu.Text = "Không có nhà cung ứng nào thoả mãn điều kiện tìm kiếm!";
            }
            else
            {
                lblkqtkncu.Text = "Có " + dt.Rows.Count + " nhà cung ứng nào thoả mãn điều kiện tìm kiếm!";
            }
            DANGNHAP.thaotac += "Tìm kiếm, ";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
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

        private void DGVNCU_Click(object sender, EventArgs e)
        {
            if (DGVNCU.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DGVNCU.CurrentRow.Index == DGVNCU.NewRowIndex)
            {
                MessageBox.Show("Hãy chọn dòng có thông tin!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtMaNCU.Text = DGVNCU.CurrentRow.Cells["IdNCU"].Value.ToString();
            txtTenNCU.Text = DGVNCU.CurrentRow.Cells["TenNCU"].Value.ToString();
            txtDiaChi.Text = DGVNCU.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtSĐT.Text = DGVNCU.CurrentRow.Cells["SĐT"].Value.ToString();
            cbQuymoNCU.Text = DGVNCU.CurrentRow.Cells["QuyMoNCU"].Value.ToString();
            txtconnoncu.Text = DGVNCU.CurrentRow.Cells["ConnoNCU"].Value.ToString();

        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            DGVNCU.DataSource = ncu;
            ResetValues();
        }

        private void btnundo_Click(object sender, EventArgs e)
        {

        }

        private void btnredo_Click(object sender, EventArgs e)
        {

        }

        private void cbQuymoNCU_Leave(object sender, EventArgs e)
        {
            if (cbQuymoNCU.Text != "")
            {
                bool flag = true;
                for (int i = 0; i < cbQuymoNCU.Items.Count; ++i)
                {
                    if (cbQuymoNCU.Text == cbQuymoNCU.Items[i].ToString())
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
                    MessageBox.Show("Quy mô nhà cung ứng chỉ bao gồm: 'Lớn(Tập đoàn, Công Ty)', 'Vừa(Đại Lý)' và 'Nhỏ'! Chú ý viết hoa ký tự đầu.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);     
                }
            }
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
                            MessageBox.Show("Số điện thoại hợp lệ phải chứa ít nhất 10 số và nhiều nhất là 11 số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSĐT.Focus();
                            return;
                        }
                    }

                }
            }
        }

        private void txtconnoncu_TextChanged(object sender, EventArgs e)
        {
            string conno = BUS_HDB.ConvertToFloatType(txtconnoncu.Text);
            txtconnoncu.Text = BUS_HDB.FormatNumber(conno);
        }
    }
}
