using System;
using System.Data;
using System.Windows.Forms;
using BUS;
using DTO;

namespace QLBHDT
{
    public partial class QLTRUYCAP : Form
    {
        public QLTRUYCAP()
        {
            InitializeComponent();
        }

        private DataTable tc;
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

        private void QLTRUYCAP_Load(object sender, EventArgs e)
        {
            BUS_TC.FillComboTenTK(cbTenTK, "TenTK", "TenTK");
            cbTenTK.SelectedIndex = -1;
            LoadDataGridView();
            lbl = lblkqtktc.Text;
        }

        private void LoadDataGridView()
        {
            tc = BUS_TC.hienthiTC();
            dgvtruycap.DataSource = tc;
            dgvtruycap.AllowUserToAddRows = false;
            dgvtruycap.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            btnxoa.Enabled = false;
            btnhienthi.Enabled = false;
            txtmatc.Text = string.Empty;
            txtmanguoidung.Text = string.Empty;
            cbTenTK.Text = string.Empty;
            dtplandoimkcuoi.Value = DateTime.Now;
            dtplanDNcuoi.Value = DateTime.Now;
            dgvdanhmuc_thaotac.Rows.Clear();
            lblkqtktc.Text = lbl;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (tc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmatc.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BUS_TC.RunDelSQL(txtmatc.Text.Trim());
                LoadDataGridView();
                ResetValues();
                DANGNHAP.thaotac += "Xoá, ";

                BUS_TC.FillComboTenTK(cbTenTK, "TenTK", "TenTK");
                cbTenTK.SelectedIndex = -1;
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            DANGNHAP.thaotac += "Tìm kiếm, ";
            DTO_TC tc = new DTO_TC();
            tc.Tentk = cbTenTK.Text.Trim();
            tc.Landmkcuoi = dtplanDNcuoi.Text;
            DataTable dt = BUS_TC.TimkiemTC(tc.Tentk, tc.Landmkcuoi);
            if (dt.Rows.Count == 0)
                lblkqtktc.Text = "Không có bản ghi nào được tìm thấy!";
            else
                lblkqtktc.Text = "Có " + dt.Rows.Count + " bản ghi được tìm thấy!";
            dgvtruycap.DataSource = dt;
            btnhienthi.Enabled = true;
        }

        private void dgvtruycap_Click(object sender, EventArgs e)
        {
            if (dgvtruycap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgvtruycap.CurrentRow.Index == dgvtruycap.NewRowIndex)
            {
                MessageBox.Show("Hãy chọn dòng có thông tin!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtmatc.Text = dgvtruycap.CurrentRow.Cells["IdTC"].Value.ToString();
            txtmanguoidung.Text = dgvtruycap.CurrentRow.Cells["IdND"].Value.ToString();
            cbTenTK.Text = dgvtruycap.CurrentRow.Cells["TenTK"].Value.ToString();
            dtplandoimkcuoi.Text = dgvtruycap.CurrentRow.Cells["LanDMKCuoi"].Value.ToString();
            dtplanDNcuoi.Text=dgvtruycap.CurrentRow.Cells["LanDNCuoi"].Value.ToString();
            string[] danmuc = dgvtruycap.CurrentRow.Cells["DanhmucTC"].Value.ToString().Split('|');
            string[] thaotac = dgvtruycap.CurrentRow.Cells["Thaotac"].Value.ToString().Split('|');
            if (danmuc.Length > 0 && thaotac.Length > 0)
            {
                int sldong = 0;
                for (int j = 0; j < danmuc.Length; ++j)
                {
                    if (danmuc[j].Trim().Length > 0)
                    {
                        sldong ++;
                    }
                }
                //Thêm vào bảng số dòng tương ứng với số danh mục mà người dùng đã truy cập
                if(dgvdanhmuc_thaotac.Rows.Count != sldong && sldong > 0)
                {
                    dgvdanhmuc_thaotac.Rows.Clear();
                    dgvdanhmuc_thaotac.Rows.Add(sldong);
                }
                else
                {
                    dgvdanhmuc_thaotac.Rows.Clear();
                }
                int k = 0;
                for (int i = 0; i < dgvdanhmuc_thaotac.Rows.Count; ++i)
                {
                    if (danmuc[i].Trim().Length > 0)
                    {
                        string[] dmct = danmuc[i].Split(';');
                        if (dmct.Length != 0)
                        {
                            dgvdanhmuc_thaotac.Rows[k].Cells["ThoidiemTC"].Value = dmct[0].Trim();
                            dgvdanhmuc_thaotac.Rows[k].Cells["DanhmucTCCT"].Value = dmct[1].Trim();
                            dgvdanhmuc_thaotac.Rows[k].Cells["ThaotacCT"].Value = thaotac[i].Trim();
                            k++;
                        }
                    }
                }
            }
            btnxoa.Enabled = true;
        }

        private void btnxoatatca_Click(object sender, EventArgs e)
        {
            if (tc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xoá tất cả bản ghi không?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                BUS_TC.RunDelAllData();
                LoadDataGridView();
                ResetValues();
                BUS_TC.FillComboTenTK(cbTenTK, "TenTK", "TenTK");
                cbTenTK.SelectedIndex = -1;

                DANGNHAP.thaotac += "Xoá tất cả, ";
            }
            else
            {
                return;
            }
        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
            ResetValues();
        }

        private void QLTRUYCAP_FormClosed(object sender, FormClosedEventArgs e)
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
    }
}
