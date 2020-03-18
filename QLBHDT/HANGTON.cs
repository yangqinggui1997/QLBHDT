using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using DTO;
using BUS;

namespace QLBHDT
{
    public partial class HANGTON : Form
    {
        public HANGTON()
        {
            InitializeComponent();
        }

        private DataTable htct;
        private string lbl;
        public static string maht;
        public static bool htprint;

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

        private void HANGTON_Load(object sender, EventArgs e)
        {
            lbl = lblsoluongtk.Text;
            btnprint.Enabled = false;
            btnXoa.Enabled = false;
            btnThem.Enabled = false;

            BUS_HT.FillComboMaNV(cbmanv, "IdNV", "IdNV");
            cbmanv.SelectedIndex = -1;

            BUS_HT.FillComboMaTKHT(cbmatkht, "IdHT", "IdHT");
            cbmatkht.SelectedIndex = -1;

            cbmatkht.Text = "Mã sẽ tự động thêm!";

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
                                btnprint.Enabled = true;
                                btnThem.Enabled = true;
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
            htct = BUS_HTCT.hienthiHTCT(); //Lấy dữ liệu từ bảng
            dgvhangton.DataSource = htct;
            dgvhangton.AllowUserToAddRows = false;
            dgvhangton.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            cbmatkht.Text = "Mã sẽ tự động thêm!";
            cbmanv.Text = string.Empty;
            txtmasp.Text = string.Empty;
            txtslton.Text = string.Empty;
            lblsoluongtk.Text = lbl;
            dtpngaytk.Value = DateTime.Now;
            dtpNgaySX.Value = DateTime.Now;
            dtpngaynhap.Value = DateTime.Now;
            dtpngayhh.Value = DateTime.Now;
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
            DataTable dt = BUS_HT.KiemtraTKHTTonTai(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];

                //Cập nhật thống kê hàng tồn chi tiết
                dt = BUS_SP.hienthisp();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        DataRow r = dt.Rows[i];
                        BUS_HTCT.CapnhatTKHTCT(row[0].ToString(), r[0].ToString(), r[7].ToString());
                    }
                }
                //Cập nhật ngày thống kê
                BUS_HT.CapnhatNgayTK(row[0].ToString(), DateTime.Now.ToString());

                BUS_HT.FillComboMaTKHT(cbmatkht, "IdHT", "IdHT");
                cbmatkht.SelectedIndex = -1;
                ResetValues();
                LoadDataGridView();

                DANGNHAP.thaotac += "Thêm, ";

                MessageBox.Show("Đã thống kê xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            else
            {
                //tạo mã ngẫu nhiên
                int value;
                bool kt = false;
                Random rand = new Random();
                value = rand.Next(100000000, 999999999);
                string maht = "HT" + value;
                DataRow dr;
                if (BUS_HT.hienthiHT().Rows.Count > 0)
                {
                    while (kt == false)
                    {
                        for (int i = 0; i < BUS_HT.hienthiHT().Rows.Count; ++i)
                        {
                            dr = BUS_HT.hienthiHT().Rows[i];
                            if (maht == dr["IdHT"].ToString())
                            {
                                kt = false;
                                value = rand.Next(100000000, 999999999);
                                maht = "HT" + value;
                                break;
                            }
                            else
                            {
                                kt = true;
                            }
                        }
                    }
                }

                //Kiểm tra đã tồn tại mã thống kê hàng tồn
                if (!BUS_HT.ktHTtrung(maht))
                {
                    MessageBox.Show("Mã hàng tồn đã tồn tại! Hãy nhấn Thêm lần nữa để lấy mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnThem.Focus();
                    return;
                }

                //Chèn thêm
                //Thêm thống kê hàng tồn
                DTO_HT HT = new DTO_HT(maht, cbmanv.Text.Trim(), DateTime.Now.ToString());
                BUS_HT.themHT(HT);

                //Thêm thống kê hàng tồn chi tiết
                dt = BUS_SP.hienthisp();
                DTO_HTCT HTCT;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        DataRow r = dt.Rows[i];
                        HTCT = new DTO_HTCT(maht, r[0].ToString(), r[2].ToString(), r[3].ToString(), r[4].ToString(), r[5].ToString(),"0");
                        BUS_HTCT.themHTCT(HTCT);
                    }
                }

                //giữ thông tin chung của thống kê vừa thêm trên các control
                dt = BUS_HT.hienthiHTcuthe(maht);
                if (dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    cbmatkht.Text = r[0].ToString();
                    cbmanv.Text = r[1].ToString();
                    dtpngaytk.Text = r[2].ToString();
                }

                BUS_HT.FillComboMaTKHT(cbmatkht, "IdHT", "IdHT");
                cbmatkht.SelectedIndex = -1;
                ResetValues();
                LoadDataGridView();

                DANGNHAP.thaotac += "Thêm, ";

                MessageBox.Show("Đã thống kê xong!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HANGTON_FormClosed(object sender, FormClosedEventArgs e)
        {
            DANGNHAP.thaotac += " | ";
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            DataTable dt = BUS_HT.hienthiHTcuthe(cbmatkht.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            htprint = true;
            maht = cbmatkht.Text.Trim();
            PRINTPREVIEW pp = new PRINTPREVIEW();
            pp.ShowDialog();

            DANGNHAP.thaotac += "In báo cáo, ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dt = BUS_HT.hienthiHTcuthe(cbmatkht.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Xóa trên bảng hàng tồn chi tiết
                BUS_HT.RunDelSQLOnHTCT(cbmatkht.Text.Trim());
                //Xóa trên bảng thống kê hàng tồn
                BUS_HT.RunDelSQL(cbmatkht.Text.Trim());

                //Cập nhật lại dữ liệu trên combobox mã dt.
                BUS_HT.FillComboMaTKHT(cbmatkht, "IdHT", "IdHT");
                cbmatkht.SelectedIndex = -1;

                ResetValues();
                LoadDataGridView();
                DANGNHAP.thaotac += "Xoá, ";
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (cbmanv.Text.Trim() == string.Empty && cbmatkht.Text.Trim() == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn phải nhập điều kiện tìm kiếm!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnhienthi.Enabled = true;

            DTO_HT HT = new DTO_HT();
            if (cbmatkht.Text.Trim() == "Mã sẽ tự động thêm!")
            {
                HT.Idht = "";
            }
            else
            {
                HT.Idht = cbmatkht.Text.Trim();
            }
            HT.Idnv = cbmanv.Text.Trim();
            DataTable dt = BUS_HT.timkiemHT(HT.Idnv, HT.Idht);

            if (dt.Rows.Count == 0)
            {
                lblsoluongtk.Text = "Không có báo cáo nào thoả mãn điều kiện tìm kiếm!";
                dgvhangton.DataSource = BUS_HTCT.hienthiHTCTcuthe("NULL");
            }
            else
            {
                lblsoluongtk.Text = "Có " + dt.Rows.Count + " báo cáo nào thoả mãn điều kiện tìm kiếm!";

                //Thêm soucre khi kết quả tìm kiếm trả về nhiều bảng thống kê hàng tồn.

                List<DTO_HTCT> row = new List<DTO_HTCT>();
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    DataRow dr = dt.Rows[i];
                    DataTable DT = BUS_HTCT.hienthiHTCTcuthe(dr[0].ToString());
                    if(DT.Rows.Count > 0)
                    {
                        for (int j = 0; j < DT.Rows.Count; ++j)
                        {
                            dr = DT.Rows[i];
                            DTO_HTCT htct = new DTO_HTCT(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString());
                            row.Add(htct);
                        }
                    }

                }

                dgvhangton.DataSource = row;
            }
            DANGNHAP.thaotac += "Tìm kiếm, ";
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

        private void cbmatkht_Leave(object sender, EventArgs e)
        {
            if (cbmatkht.Text.Trim() == string.Empty)
            {
                cbmatkht.Text = "Mã sẽ tự động thêm!";
            }
            else
            {
                cbmatkht_TextChanged(sender, e);
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

        private void cbmatkht_TextChanged(object sender, EventArgs e)
        {
            if (cbmatkht.Text.Trim() != string.Empty)
            {
                DataTable dt = BUS_HTCT.hienthiHTCTcuthe(cbmatkht.Text.Trim());

                if (dt.Rows.Count > 0)
                {
                    dgvhangton.DataSource = dt;
                    dt = BUS_HT.hienthiHTcuthe(cbmatkht.Text.Trim());
                    DataRow dr = dt.Rows[0];
                    cbmanv.Text = dr[1].ToString();
                    dtpngaytk.Text = dr[2].ToString();

                }
                else
                {
                    cbmanv.Text = string.Empty;
                    dtpngaytk.Value = DateTime.Now;
                    txtmasp.Text = string.Empty;
                    txtslton.Text = string.Empty;
                    dtpNgaySX.Value = DateTime.Now;
                    dtpngayhh.Value = DateTime.Now;
                    dtpngaynhap.Value = DateTime.Now;
                    dgvhangton.DataSource = dt;
                }
            }
            else
            {
                cbmanv.Text = string.Empty;
                dtpngaytk.Value = DateTime.Now;
                txtmasp.Text = string.Empty;
                txtslton.Text = string.Empty;
                dtpNgaySX.Value = DateTime.Now;
                dtpngayhh.Value = DateTime.Now;
                dtpngaynhap.Value = DateTime.Now;
                dgvhangton.DataSource = BUS_HTCT.hienthiHTCTcuthe(cbmatkht.Text);
            }
        }

        private void dgvhangton_Click(object sender, EventArgs e)
        {
            if (dgvhangton.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgvhangton.CurrentRow.Index != dgvhangton.NewRowIndex)
            {
                cbmatkht.Text = dgvhangton.CurrentRow.Cells["IdHT"].Value.ToString();
                txtmasp.Text = dgvhangton.CurrentRow.Cells["IdSP"].Value.ToString();
                dtpNgaySX.Text = dgvhangton.CurrentRow.Cells["NgaySX"].Value.ToString();
                dtpngayhh.Text = dgvhangton.CurrentRow.Cells["NgayHH"].Value.ToString();
                dtpngaynhap.Text = dgvhangton.CurrentRow.Cells["NgayNhap"].Value.ToString();
                txtslton.Text = dgvhangton.CurrentRow.Cells["SLCon"].Value.ToString();

                DataTable dt = BUS_HT.hienthiHTcuthe(cbmatkht.Text);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    cbmanv.Text = dr[1].ToString();

                    dtpngaytk.Text = dr[2].ToString();
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn bản ghi có thông tin!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtslton_TextChanged(object sender, EventArgs e)
        {
            string cn = BUS_HDB.ConvertToFloatType(txtslton.Text);
            txtslton.Text = BUS_HDB.FormatNumber(cn);

        }
    }
}
