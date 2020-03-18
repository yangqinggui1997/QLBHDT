using System;
using System.Windows.Forms;
using System.Data;
using BUS;
using DTO;
using System.Collections.Generic;

namespace QLBHDT
{
    public partial class CONGNO : Form
    {
        public CONGNO()
        {
            InitializeComponent();
        }
        private DataTable cnct;
        private string lbl;
        public static string macn;
        public static bool cnprint;

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

            BUS_CN.FillComboMaNV(cbmanv, "IdNV", "IdNV");
            cbmanv.SelectedIndex = -1;

            BUS_CN.FillComboMaTKCN(cbmatkcn, "IdCN", "IdCN");
            cbmatkcn.SelectedIndex = -1;

            cbmatkcn.Text = "Mã sẽ tự động thêm!";

            LoadDataGridView();

            string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
            string[] quyenhan = DANGNHAP.Quyen.Split('|');

            for (int j = 0; j < danhmuctmp.Length; ++j)
            {
                if (danhmuctmp[j].Trim() == "Thống kê, báo cáo")
                {
                    string[] cn;
                    cn = quyenhan[j].Split(';');
                    if (cn != null)
                    {
                        foreach (string items in cn)
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
            cnct = BUS_CNCT.hienthiCNCT(); //Lấy dữ liệu từ bảng
            dgvcongno.DataSource = cnct;
            dgvcongno.AllowUserToAddRows = false;
            dgvcongno.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            cbmatkcn.Text = "Mã sẽ tự động thêm!";
            cbmanv.Text = string.Empty;
            txtmancu.Text = string.Empty;
            txtconno.Text = string.Empty;
            lblsoluongtk.Text = lbl;
            dtpngaytk.Value = DateTime.Now;
            txtdiachi.Text = string.Empty;
            txtsdt.Text = string.Empty;
            txttenncu.Text = string.Empty;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbmanv.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbmanv.Focus();
                return;
            }
            string manv = cbmanv.Text;
            //Kiểm tra có bảng thống kê nào rỗng hay không nếu là rỗng thì xóa đi và cập nhật lại combobox tương ứng
            DataTable table = BUS_CN.hienthiCN();
            if(table.Rows.Count > 0)
            {
                for(int i = 0; i < table.Rows.Count; ++i)
                {
                    DataRow r = table.Rows[0];
                    if(BUS_CNCT.hienthiCNCTcuthe(r[0].ToString()).Rows.Count == 0)
                    {
                        BUS_CN.RunDelSQL(r[0].ToString());
                    }
                }
                BUS_CN.FillComboMaTKCN(cbmatkcn, "IdCN", "IdCN");
                cbmatkcn.SelectedIndex = -1;
            }

            //Kiểm tra xem có thống kê nào trong tháng được tạo chưa để cập nhật mới

            DataTable dt = BUS_CN.KiemtraTKCNTonTai(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString());
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];

                //Cập nhật thống kê hàng tồn chi tiết
                dt = BUS_NCU.hienthiNCU();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        DataRow r = dt.Rows[i];
                        //Cập nhật lại nợ nhà cung ứng, nếu nợ = 0 thì xóa nhà cung ứng ra khỏi danh sách nợ
                        BUS_CNCT.CapnhatTKCNCT(row[0].ToString(), r[0].ToString());
                    }

                    //Nếu nhà cung ứng bị xóa khỏi danh sách nhưng sau đó có thêm nợ thì thêm nhà cung ứng vào danh sách nợ
                    DataTable DT = BUS_CNCT.hienthiCNCTcuthe(row[0].ToString());

                    //Lập danh sách các nhà cung ứng không có trong danh sách nợ
                    List<string> idncu = new List<string>();
                    for(int i = 0; i < dt.Rows.Count; ++i)
                    {
                        bool flag = false;
                        DataRow r = dt.Rows[i];
                        for(int j = 0; j < DT.Rows.Count; ++j)
                        {
                            DataRow dr = DT.Rows[j];
                            if(r[0].ToString() == dr[1].ToString())
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (flag == false)
                        {
                            idncu.Add(r[0].ToString());
                        }
                    }
                    foreach(string item in idncu)
                    {
                        DataRow r = BUS_NCU.hienthiNCUcuthe(item).Rows[0];
                        //Thêm thống kê công nợ chi tiết
                        DTO_CNCT CNCT = new DTO_CNCT(row[0].ToString(), r[0].ToString(), r[1].ToString(), r[3].ToString(), r[2].ToString(), r[5].ToString());
                        BUS_CNCT.themCNCT(CNCT);
                    }
                }
                //Cập nhật ngày thống kê
                BUS_CN.CapnhatNgayTK(row[0].ToString(), DateTime.Now.ToString());

                BUS_CN.FillComboMaTKCN(cbmatkcn, "IdCN", "IdCN");
                cbmatkcn.SelectedIndex = -1;

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
                string macn = "CN" + value;
                DataRow dr;
                if (BUS_CN.hienthiCN().Rows.Count > 0)
                {
                    while (kt == false)
                    {
                        for (int i = 0; i < BUS_CN.hienthiCN().Rows.Count; ++i)
                        {
                            dr = BUS_CN.hienthiCN().Rows[i];
                            if (macn == dr["IdCN"].ToString())
                            {
                                kt = false;
                                value = rand.Next(100000000, 999999999);
                                macn = "CN" + value;
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
                if (!BUS_CN.ktCNtrung(macn))
                {
                    MessageBox.Show("Mã công nợ đã tồn tại! Hãy nhấn Thêm lần nữa để lấy mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnThem.Focus();
                    return;
                }

                //Chèn thêm
                //Thêm thống kê công nợ
                DTO_CN CN = new DTO_CN(macn, manv, DateTime.Now.ToString());
                BUS_CN.themCN(CN);

                //Thêm thống kê công nợ chi tiết
                dt = BUS_NCU.hienthiNCU();
                DTO_CNCT CNCT;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        DataRow r = dt.Rows[i];
                        CNCT = new DTO_CNCT(macn, r[0].ToString(), r[1].ToString(), r[3].ToString(), r[2].ToString(), r[5].ToString());
                        BUS_CNCT.themCNCT(CNCT);
                    }
                }

                //giữ thông tin chung của thống kê vừa thêm trên các control
                dt = BUS_CN.hienthiCNcuthe(macn);
                if (dt.Rows.Count == 1)
                {
                    DataRow r = dt.Rows[0];
                    cbmatkcn.Text = r[0].ToString();
                    cbmanv.Text = r[1].ToString();
                    dtpngaytk.Text = r[2].ToString();
                }

                BUS_CN.FillComboMaTKCN(cbmatkcn, "IdCN", "IdCN");
                cbmatkcn.SelectedIndex = -1;

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
            DataTable dt = BUS_CN.hienthiCNcuthe(cbmatkcn.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cnprint = true;
            macn = cbmatkcn.Text.Trim();
            PRINTPREVIEW pp = new PRINTPREVIEW();
            pp.ShowDialog();

            DANGNHAP.thaotac += "In báo cáo, ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dt = BUS_CN.hienthiCNcuthe(cbmatkcn.Text.Trim());
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Xóa trên bảng hàng tồn chi tiết
                BUS_CN.RunDelSQLOnCNCT(cbmatkcn.Text.Trim());
                //Xóa trên bảng thống kê hàng tồn
                BUS_CN.RunDelSQL(cbmatkcn.Text.Trim());

                //Cập nhật lại dữ liệu trên combobox mã dt.
                BUS_CN.FillComboMaTKCN(cbmatkcn, "IdCN", "IdCN");
                cbmatkcn.SelectedIndex = -1;

                ResetValues();
                LoadDataGridView();

                DANGNHAP.thaotac += "Xoá, ";
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (cbmanv.Text.Trim() == string.Empty && cbmatkcn.Text.Trim() == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn phải nhập điều kiện tìm kiếm!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnhienthi.Enabled = true;

            DTO_CN CN = new DTO_CN();
            if (cbmatkcn.Text.Trim() == "Mã sẽ tự động thêm!")
            {
                CN.Macn = "";
            }
            else
            {
                CN.Macn = cbmatkcn.Text.Trim();
            }
            CN.Manv = cbmanv.Text.Trim();
            DataTable dt = BUS_CN.timkiemCN(CN.Manv, CN.Macn);

            if (dt.Rows.Count == 0)
            {
                lblsoluongtk.Text = "Không có báo cáo nào thoả mãn điều kiện tìm kiếm!";
                dgvcongno.DataSource = BUS_CNCT.hienthiCNCTcuthe("NULL");
            }
            else
            {
                lblsoluongtk.Text = "Có " + dt.Rows.Count + " báo cáo nào thoả mãn điều kiện tìm kiếm!";

                //Thêm soucre khi kết quả tìm kiếm trả về nhiều bảng thống kê hàng tồn.

                List<DTO_CNCT> row = new List<DTO_CNCT>();
                for (int i = 0; i < dt.Rows.Count; ++i)
                {
                    DataRow dr = dt.Rows[i];
                    DataTable DT = BUS_CNCT.hienthiCNCTcuthe(dr[0].ToString());
                    if (DT.Rows.Count > 0)
                    {
                        for (int j = 0; j < DT.Rows.Count; ++j)
                        {
                            dr = DT.Rows[i];
                            DTO_CNCT cnct = new DTO_CNCT(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
                            row.Add(cnct);
                        }
                    }

                }

                dgvcongno.DataSource = row;
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
            if (cbmatkcn.Text.Trim() == string.Empty)
            {
                cbmatkcn.Text = "Mã sẽ tự động thêm!";
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
            DataTable dt = BUS_CNCT.hienthiCNCTcuthe(cbmatkcn.Text.Trim());
            if (cbmatkcn.Text.Trim() != string.Empty)
            {
                if (dt.Rows.Count > 0)
                {
                    dgvcongno.DataSource = dt;
                    dt = BUS_CN.hienthiCNcuthe(cbmatkcn.Text.Trim());
                    DataRow dr = dt.Rows[0];
                    cbmanv.Text = dr[1].ToString();
                    dtpngaytk.Text = dr[2].ToString();
                }
                else
                {
                    dgvcongno.DataSource = dt;
                    cbmanv.Text = string.Empty;
                    dtpngaytk.Value = DateTime.Now;
                    txtmancu.Text = string.Empty;
                    txtconno.Text = string.Empty;
                    txtmancu.Text = string.Empty;
                    txttenncu.Text = string.Empty;
                    txtsdt.Text = string.Empty;
                    txtdiachi.Text = string.Empty;
                }
            }
            else
            {
                dgvcongno.DataSource = dt;
                cbmanv.Text = string.Empty;
                dtpngaytk.Value = DateTime.Now;
                txtmancu.Text = string.Empty;
                txtconno.Text = string.Empty;
                txtmancu.Text = string.Empty;
                txttenncu.Text = string.Empty;
                txtsdt.Text = string.Empty;
                txtdiachi.Text = string.Empty;
            }
        }

        private void dgvcongno_Click(object sender, EventArgs e)
        {
            if (dgvcongno.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgvcongno.CurrentRow.Index != dgvcongno.NewRowIndex)
            {
                cbmatkcn.Text = dgvcongno.CurrentRow.Cells["IdCN"].Value.ToString();
                txtmancu.Text = dgvcongno.CurrentRow.Cells[1].Value.ToString();
                txttenncu.Text = dgvcongno.CurrentRow.Cells[2].Value.ToString();
                txtdiachi.Text = dgvcongno.CurrentRow.Cells[4].Value.ToString();
                txtsdt.Text = dgvcongno.CurrentRow.Cells[3].Value.ToString();
                txtconno.Text = dgvcongno.CurrentRow.Cells[5].Value.ToString();

                DataTable dt = BUS_CN.hienthiCNcuthe(cbmatkcn.Text);
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

        private void txtconno_TextChanged(object sender, EventArgs e)
        {
            string conno = BUS_HDB.ConvertToFloatType(txtconno.Text);
            txtconno.Text = BUS_HDB.FormatNumber(conno);

        }
    }
}
