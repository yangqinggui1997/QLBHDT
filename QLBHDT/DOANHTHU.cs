using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;

namespace QLBHDT
{
    public partial class DOANHTHU : Form
    {
        public DOANHTHU()
        {
            InitializeComponent();
        }

        private DataTable dt;
        private string lbl;
        public static string madt;
        public static bool dtprint;

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

        private void DOANHTHU_Load(object sender, EventArgs e)
        {
            lbl = lblkqtkdt.Text;
            btnprint.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;

            BUS_DT.FillComboMaNV(cbmanv, "IdNV", "IdNV");
            cbmanv.SelectedIndex = -1;

            BUS_DT.FillComboMaTKDT(cbmatkdt, "IdDT", "IdDT");
            cbmatkdt.SelectedIndex = -1;

            cbmatkdt.Text = "Mã sẽ tự động thêm!";

            LoadDataGridView();

            string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
            string[] quyenhan = DANGNHAP.Quyen.Split('|');

            for (int j = 0; j < danhmuctmp.Length; ++j)
            {
                if (danhmuctmp[j].Trim() == "Thống kê, báo cáo")
                {
                    string[] ht;
                    ht = quyenhan[j].Split(';');
                    if (ht != null)
                    {
                        foreach (string items in ht)
                        {
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                btnThem.Enabled = true;
                                btnprint.Enabled = true;
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

        private void LoadDataGridView()
        {
            dt = BUS_DT.hienthiDT(); //Lấy dữ liệu từ bảng
            dgvdoanhthu.DataSource = dt;
            dgvdoanhthu.AllowUserToAddRows = false;
            dgvdoanhthu.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            cbmatkdt.Text = "Mã sẽ tự động thêm!";
            cbmanv.Text = string.Empty;
            txtDoanhSoban.Text = string.Empty;
            txtdoanhthu.Text = string.Empty;
            txtloinhuan.Text = string.Empty;
            lblkqtkdt.Text = lbl;
            dtpngaytk.Text = DateTime.Now.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbmanv.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbmanv.Focus();
                return;
            }

            //Kiểm tra xem có thống kê nào trong tháng được tạo chưa để cập nhật mới
            DataTable dt = BUS_DT.KiemtraTKDTTonTai(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
            if (dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                BUS_DT.CapnhatTKDT(r[0].ToString(), DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.ToString());

                BUS_DT.FillComboMaTKDT(cbmatkdt, "IdDT", "IdDT");
                cbmatkdt.SelectedIndex = -1;
                ResetValues();
                LoadDataGridView();

                DANGNHAP.thaotac += "Thêm, ";

                MessageBox.Show("Đã thống kê xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            else
            {
                //Kiểm tra trong tháng có hóa đơn nào không để tạo thống kê
                dt = BUS_DT.KiemtraHDBTThang(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
                if (dt.Rows.Count > 0)
                {
                    //tạo mã ngẫu nhiên
                    int value;
                    bool kt = false;
                    Random rand = new Random();
                    value = rand.Next(100000000, 999999999);
                    string madt = "DT" + value;
                    DataRow dr;
                    if (BUS_DT.hienthiDT().Rows.Count > 0)
                    {
                        while (kt == false)
                        {
                            for (int i = 0; i < BUS_DT.hienthiDT().Rows.Count; ++i)
                            {
                                dr = BUS_DT.hienthiDT().Rows[i];
                                if (madt == dr["IdDT"].ToString())
                                {
                                    kt = false;
                                    value = rand.Next(100000000, 999999999);
                                    madt = "DT" + value;
                                    break;
                                }
                                else
                                {
                                    kt = true;
                                }
                            }
                        }
                    }

                    //Kiểm tra đã tồn tại mã thống kê doanh thu chưa
                    if (!BUS_DT.ktDTtrung(madt))
                    {
                        MessageBox.Show("Mã doanh thu đã tồn tại! Hãy nhấn Thêm lần nữa để lấy mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnThem.Focus();
                        return;
                    }

                    //Chèn thêm
                    DTO_DT DT = new DTO_DT(madt, cbmanv.Text.Trim(), "0", "0", "0", DateTime.Now.ToString());
                    BUS_DT.themDT(DT, DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());

                    BUS_DT.FillComboMaTKDT(cbmatkdt, "IdDT", "IdDT");
                    cbmatkdt.SelectedIndex = -1;
                    ResetValues();
                    LoadDataGridView();

                    DANGNHAP.thaotac += "Thêm, ";

                    MessageBox.Show("Đã thống kê xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;

                }
                else
                {
                    MessageBox.Show("Trong tháng chưa có hóa đơn bán nào để thống kê!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DOANHTHU_FormClosed(object sender, FormClosedEventArgs e)
        {
            DANGNHAP.thaotac += " | ";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
            btnhienthi.Enabled = false;
        }

        private void dgvdoanhthu_Click(object sender, EventArgs e)
        {
            if (dgvdoanhthu.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgvdoanhthu.CurrentRow.Index != dgvdoanhthu.NewRowIndex)
            {
                cbmatkdt.Text = dgvdoanhthu.CurrentRow.Cells["IdDT"].Value.ToString();
                cbmanv.Text = dgvdoanhthu.CurrentRow.Cells["IdNV"].Value.ToString();
                txtDoanhSoban.Text = dgvdoanhthu.CurrentRow.Cells["Doanhsoban"].Value.ToString();
                txtdoanhthu.Text = dgvdoanhthu.CurrentRow.Cells["Doanhthubh"].Value.ToString();
                txtloinhuan.Text = dgvdoanhthu.CurrentRow.Cells["Loinhuan"].Value.ToString();
                dtpngaytk.Text = dgvdoanhthu.CurrentRow.Cells["NgayTK"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Hãy chọn bản ghi có thông tin!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cbmatkdt_Leave(object sender, EventArgs e)
        {
            if (cbmatkdt.Text.Trim() == string.Empty)
            {
                cbmatkdt.Text = "Mã sẽ tự động thêm!";
            }
            else
            {
                cbmatkdt_TextChanged(sender, e);
            }
        }

        private void cbmanv_Leave(object sender, EventArgs e)
        {
            if (cbmanv.Text.Trim().Length != 0)
            {
                DataTable dt = BUS_NV.hienthinv();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        bool flag = false;
                        DataRow dr;
                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            dr = dt.Rows[i];
                            if (cbmanv.Text.Trim() == dr[0].ToString())
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
                            MessageBox.Show("Mã nhân viên không tồn tại. Hãy kiểm tra lại và chọn mã khác!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cbmanv.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void cbmatkdt_TextChanged(object sender, EventArgs e)
        {
            if (cbmatkdt.Text.Trim() != string.Empty)
            {
                bool flag = true;
                for (int i = 0; i < cbmatkdt.Items.Count; ++i)
                {
                    if (cbmatkdt.Text.Trim() == cbmatkdt.Items[i].ToString())
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    DataTable dt = BUS_DT.hienthiDTcuthe(cbmatkdt.Text.Trim());

                    if (dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        cbmanv.Text = dr[1].ToString();
                        txtDoanhSoban.Text = dr[2].ToString();
                        txtdoanhthu.Text = dr[3].ToString();
                        txtloinhuan.Text = dr[4].ToString();
                        dtpngaytk.Text = dr[5].ToString();
                        dgvdoanhthu.DataSource = dt;
                    }
                    else
                    {
                        cbmanv.Text = string.Empty;
                        txtdoanhthu.Text = string.Empty;
                        txtDoanhSoban.Text = string.Empty;
                        txtloinhuan.Text = string.Empty;
                        dtpngaytk.Text = DateTime.Now.ToString();
                        dgvdoanhthu.DataSource = dt;
                    }
                }
                else
                {
                    cbmanv.Text = string.Empty;
                    txtdoanhthu.Text = string.Empty;
                    txtDoanhSoban.Text = string.Empty;
                    txtloinhuan.Text = string.Empty;
                    dtpngaytk.Text = DateTime.Now.ToString();
                    dgvdoanhthu.DataSource = BUS_DT.hienthiDTcuthe(cbmatkdt.Text.Trim());
                }
            }
            else
            {
                cbmanv.Text = string.Empty;
                txtdoanhthu.Text = string.Empty;
                txtDoanhSoban.Text = string.Empty;
                txtloinhuan.Text = string.Empty;
                dtpngaytk.Text = DateTime.Now.ToString();
                dgvdoanhthu.DataSource = BUS_DT.hienthiDTcuthe(cbmatkdt.Text.Trim());
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dt = BUS_DT.hienthiDTcuthe(cbmatkdt.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                BUS_DT.RunDelSQL(cbmatkdt.Text.Trim());

                //Cập nhật lại dữ liệu trên combobox mã dt.
                BUS_DT.FillComboMaTKDT(cbmatkdt, "IdDT", "IdDT");
                cbmatkdt.SelectedIndex = -1;

                ResetValues();
                LoadDataGridView();
                DANGNHAP.thaotac += "Xoá, ";
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (cbmanv.Text.Trim() == string.Empty && cbmatkdt.Text.Trim() == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn phải nhập điều kiện tìm kiếm!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnhienthi.Enabled = true;

            DTO_DT DT = new DTO_DT();
            if (cbmatkdt.Text.Trim() == "Mã sẽ tự động thêm!")
            {
                DT.Madt = "";
            }
            else
            {
                DT.Madt = cbmatkdt.Text.Trim();
            }
            DT.Manv = cbmanv.Text.Trim();
            DataTable dt = BUS_DT.timkiemDT(DT.Manv, DT.Madt);

            if (dt.Rows.Count == 0)
            {
                lblkqtkdt.Text = "Không có báo cáo nào thoả mãn điều kiện tìm kiếm!";
                dgvdoanhthu.DataSource = dt;
            }
            else
            {
                lblkqtkdt.Text = "Có " + dt.Rows.Count + " báo cáo nào thoả mãn điều kiện tìm kiếm!";
                dgvdoanhthu.DataSource = dt;
            }
            DANGNHAP.thaotac += "Tìm kiếm, ";
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            DataTable dt = BUS_DT.hienthiDTcuthe(cbmatkdt.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dtprint = true;
            madt = cbmatkdt.Text.Trim();
            PRINTPREVIEW pp = new PRINTPREVIEW();
            pp.ShowDialog();

            DANGNHAP.thaotac += "In báo cáo, ";
        }

        private void txtDoanhSoban_TextChanged(object sender, EventArgs e)
        {
            string doanhsoban = BUS_HDB.ConvertToFloatType(txtDoanhSoban.Text);
            txtDoanhSoban.Text = BUS_HDB.FormatNumber(doanhsoban);
        }

        private void txtdoanhthu_TextChanged(object sender, EventArgs e)
        {
            string doanhthu = BUS_HDB.ConvertToFloatType(txtdoanhthu.Text);
            txtdoanhthu.Text = BUS_HDB.FormatNumber(doanhthu);
        }

        private void txtloinhuan_TextChanged(object sender, EventArgs e)
        {
            string loinhuan = BUS_HDB.ConvertToFloatType(txtloinhuan.Text);
            txtloinhuan.Text = BUS_HDB.FormatNumber(loinhuan);
        }
    }
}
