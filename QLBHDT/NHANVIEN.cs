using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using DTO;
using BUS;

namespace QLBHDT
{
    public partial class NHANVIEN : Form
    {
        public NHANVIEN()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);

        }

        private DataTable nv;
        private bool dgvnvclick;
        private string lblkq;
        private const int cGrip = 16, cCaption = 32;
        private string tencv;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);
                if (pos.X < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if (pos.X >= ClientSize.Width - cGrip && pos.Y >= ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }

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


        private void NHANVIEN_Load(object sender, EventArgs e)
        {
            lblkq = lblkqtknv.Text;
            dgvnvclick = false;
            btnundo.Enabled = false;
            btnredo.Enabled = false;
            LoadDataGridView();
        }

        public void LoadDataGridView()
        {
            nv = BUS_NV.hienthinv(); //lấy dữ liệu
            DGVNhanVien.DataSource = nv;
            DGVNhanVien.AllowUserToAddRows = false;
            DGVNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaNhanVien.Text = string.Empty;
            txtTenNhanVien.Text = string.Empty;
            chkGioitinh.Checked = false;
            txtDiaChi.Text = string.Empty;
            dtpNgaySinh.Value = DateTime.Now;
            mskDienthoai.Text = string.Empty;
            txtchucvu.Text = string.Empty;
            txtluongcb.Text = string.Empty;
            txthsl.Text = string.Empty;
            txtthuclinh.Text = string.Empty;
            txttaikhoan.Text = string.Empty;

            rdbbanhang.Checked = false;
            rdbketoan.Checked = false;
            rdbquantri.Checked = false;
            rdbthukho.Checked = false;

            rdbbanhang.Enabled = true;
            rdbketoan.Enabled = true;
            rdbquantri.Enabled = true;
            rdbthukho.Enabled = true;

            dgvnvclick = false;

            lblkqtknv.Text = lblkq;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string gt;
            if (txtMaNhanVien.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mskDienthoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return;
            }
            if (txtluongcb.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập lương cơ bản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtluongcb.Focus();
                return;
            }
            if (txthsl.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập hệ số lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txthsl.Focus();
                return;
            }


            if (chkGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";

            if (!BUS_NV.ktnvtrung(txtMaNhanVien.Text))
            {
                MessageBox.Show("Mã nhân viên này đã có! Hãy nhập mã khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            DTO_NV nv = new DTO_NV(txtMaNhanVien.Text, txtTenNhanVien.Text, dtpNgaySinh.Text, gt, mskDienthoai.Text, txtDiaChi.Text, tencv, BUS_HDB.ConvertToFloatType(txtluongcb.Text), txthsl.Text, BUS_HDB.ConvertToFloatType(txtthuclinh.Text), txttaikhoan.Text);
            BUS_NV.themnv(nv);
            LoadDataGridView();
            ResetValues();

            //thêm dữ liệu cho danh mục quản lý truy cập
            DANGNHAP.thaotac += "Thêm, ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (nv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xoá nhân viên sẽ xoá tất cả dữ liệu của nhân viên trên bảng người dùng, bảng truy cập, bảng hoá đơn bán chi tiết, bảng hoá đơn nhập chi tiết, bảng hoá đơn nhập, bảng hoá đơn bán. Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //xóa nhân viên hiện tại
                List<string> idnd = new List<string>();

                DataTable dt;
                DataRow dr;
                //Lấy mã người dùng của nhân viên hiện tại trên bảng người dùng.
                dt = BUS_ND.hienthiND();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < BUS_ND.hienthiND().Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        if(dr[2].ToString() == txtMaNhanVien.Text)
                        {
                            idnd.Add(dr["IdND"].ToString());
                        }
                    }
                }

                //Xóa trên bảng hóa đơn bán chi tiết và bảng hóa đơn bán
                dt = BUS_NV.layMaHDBTheoMaNV(txtMaNhanVien.Text);
                if(dt.Rows.Count > 0)
                {
                    for(int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_HDB.RunDelSQLOnHDBCT(dr[0].ToString());
                        BUS_HDB.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên bảng hóa đơn nhập chi tiết và bảng hóa đơn nhập
                dt = BUS_NV.LayMaHDNTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_HDN.RunDelSQLOnHDNCT(dr[0].ToString());
                        BUS_HDN.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên bảng công nợ chi tiết và bảng công nợ
                dt = BUS_NV.LayMaCNTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_CN.RunDelSQLOnCNCT(dr[0].ToString());
                        BUS_CN.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên bảng hàng tồn chi tiết và bảng hàng tồn
                dt = BUS_NV.LayMaHTTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_HT.RunDelSQLOnHTCT(dr[0].ToString());
                        BUS_HT.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên thống kê doanh thu
                dt = BUS_NV.LayMaDTTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_DT.RunDelSQL(dr[0].ToString());
                    }
                }

                foreach (string items in idnd)
                {
                    BUS_ND.RunDelSQLOnTC(items); //xoá trên bảng truy cập
                    BUS_ND.RunDelSQL(items);
                }

                BUS_NV.RunDelSQL(DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString());

                LoadDataGridView();
                ResetValues();
                DANGNHAP.thaotac += "Xoá, ";
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string gt;
            if (nv.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (mskDienthoai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskDienthoai.Focus();
                return;
            }
            if (chkGioitinh.Checked == true)
                gt = "Nam";
            else
                gt = "Nữ";
            //mã nhân viên đã thay đổi so với ban đầu do đổi nhóm nhân viên của nv hiện tại
            if (txtMaNhanVien.Text.Substring(0,3) != DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0,3))
            {
                //xóa nhân viên hiện tại
                List<string> idnd = new List<string>();

                DataTable dt;
                DataRow dr;
                //Lấy mã người dùng của nhân viên hiện tại trên bảng người dùng.
                dt = BUS_ND.hienthiND();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < BUS_ND.hienthiND().Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        idnd.Add(dr["IdND"].ToString());

                    }
                }

                //Xóa trên bảng hóa đơn bán chi tiết và bảng hóa đơn bán
                dt = BUS_NV.layMaHDBTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_HDB.RunDelSQLOnHDBCT(dr[0].ToString());
                        BUS_HDB.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên bảng hóa đơn nhập chi tiết và bảng hóa đơn nhập
                dt = BUS_NV.LayMaHDNTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_HDN.RunDelSQLOnHDNCT(dr[0].ToString());
                        BUS_HDN.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên bảng công nợ chi tiết và bảng công nợ
                dt = BUS_NV.LayMaCNTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_CN.RunDelSQLOnCNCT(dr[0].ToString());
                        BUS_CN.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên bảng hàng tồn chi tiết và bảng hàng tồn
                dt = BUS_NV.LayMaHTTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_HT.RunDelSQLOnHTCT(dr[0].ToString());
                        BUS_HT.RunDelSQL(dr[0].ToString());
                    }
                }

                //Xóa trên thống kê doanh thu
                dt = BUS_NV.LayMaDTTheoMaNV(txtMaNhanVien.Text);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; ++i)
                    {
                        dr = dt.Rows[i];
                        BUS_DT.RunDelSQL(dr[0].ToString());
                    }
                }

                foreach (string items in idnd)
                {
                    BUS_ND.RunDelSQLOnTC(items); //xoá trên bảng truy cập
                    BUS_ND.RunDelSQL(items);
                }

                BUS_NV.RunDelSQL(DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString());

                //thêm mới nhân viên
                DTO_NV nv = new DTO_NV(txtMaNhanVien.Text, txtTenNhanVien.Text.Trim(), dtpNgaySinh.Text, gt, mskDienthoai.Text.Trim(), txtDiaChi.Text.Trim(), tencv, BUS_HDB.ConvertToFloatType(txtluongcb.Text.Trim()), txthsl.Text.Trim(), BUS_HDB.ConvertToFloatType(txtthuclinh.Text), txttaikhoan.Text);
                BUS_NV.themnv(nv);

                LoadDataGridView();
                ResetValues();
                DANGNHAP.thaotac += "Sửa, ";
            }
            else
            {
                DTO_NV nv = new DTO_NV(txtMaNhanVien.Text, txtTenNhanVien.Text.Trim(), dtpNgaySinh.Text, gt, mskDienthoai.Text.Trim(), txtDiaChi.Text.Trim(), tencv, BUS_HDB.ConvertToFloatType(txtluongcb.Text.Trim()), txthsl.Text.Trim(), BUS_HDB.ConvertToFloatType(txtthuclinh.Text), txttaikhoan.Text);

                BUS_NV.suaNV(nv);
                LoadDataGridView();
                ResetValues();
                DANGNHAP.thaotac += "Sửa, ";
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DGVNhanVien_Click(object sender, EventArgs e)
        {
            if (DGVNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DGVNhanVien.CurrentRow.Index == DGVNhanVien.NewRowIndex)
            {
                MessageBox.Show("Hãy chọn dòng có thông tin!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvnvclick = true;
            txtMaNhanVien.Text = DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString();
            if (txtMaNhanVien.Text.Substring(0, 3) == "NQT")
            {
                rdbquantri.Checked = true;
                rdbketoan.Checked = false;
                rdbbanhang.Checked = false;
                rdbthukho.Checked = false;
            }
            if (txtMaNhanVien.Text.Substring(0, 3) == "NKT")
            {
                rdbquantri.Checked = false;
                rdbketoan.Checked = true;
                rdbbanhang.Checked = false;
                rdbthukho.Checked = false;
            }
            if (txtMaNhanVien.Text.Substring(0, 3) == "NBH")
            {
                rdbquantri.Checked = false;
                rdbketoan.Checked = false;
                rdbbanhang.Checked = true;
                rdbthukho.Checked = false;
            }
            if (txtMaNhanVien.Text.Substring(0, 3) == "NTK")
            {
                rdbquantri.Checked = false;
                rdbketoan.Checked = false;
                rdbbanhang.Checked = false;
                rdbthukho.Checked = true;
            }

            txtTenNhanVien.Text = DGVNhanVien.CurrentRow.Cells["TenNV"].Value.ToString();
            if (DGVNhanVien.CurrentRow.Cells["Gioitinh"].Value.ToString() == "Nam") chkGioitinh.Checked = true;
            else chkGioitinh.Checked = false;
            txtDiaChi.Text = DGVNhanVien.CurrentRow.Cells["DiaChi"].Value.ToString();
            mskDienthoai.Text = DGVNhanVien.CurrentRow.Cells["SĐT"].Value.ToString();
            dtpNgaySinh.Text = DGVNhanVien.CurrentRow.Cells["NgaySinh"].Value.ToString();
            txtchucvu.Text = DGVNhanVien.CurrentRow.Cells["ChucVu"].Value.ToString();
            txtluongcb.Text = BUS_HDB.FormatNumber(DGVNhanVien.CurrentRow.Cells["LuongCB"].Value.ToString());
            txthsl.Text = BUS_HDB.FormatNumber(DGVNhanVien.CurrentRow.Cells["hsl"].Value.ToString());
            txtthuclinh.Text = BUS_HDB.FormatNumber(DGVNhanVien.CurrentRow.Cells["Thuclinh"].Value.ToString());
            txttaikhoan.Text = DGVNhanVien.CurrentRow.Cells["Taikhoan"].Value.ToString();
        }

        private void mskDienthoai_Leave(object sender, EventArgs e)
        {
            if (mskDienthoai.Text.Trim() != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(mskDienthoai.Text, out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    mskDienthoai.Focus();
                    return;
                }
                else
                {
                    if (a < 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        mskDienthoai.Focus();
                        return;
                    }
                    else
                    {
                        if (mskDienthoai.Text.Length < 10 || mskDienthoai.Text.Length > 11)
                        {
                            MessageBox.Show("Số điện thoại hợp lệ phải chứa ít nhất 10 số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            mskDienthoai.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void rdbquantri_CheckedChanged(object sender, EventArgs e)
        {
            if ((rdbquantri.Checked == true && dgvnvclick == false) || (rdbquantri.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) != "NQT"))
            {
                //tạo mã ngẫu nhiên
                int gt;
                bool kt = false;
                Random rand = new Random();
                gt = rand.Next(100000000, 999999999);
                string quantri = "NQT" + gt;
                DataRow dr;
                if (BUS_NV.hienthinv().Rows.Count > 0)
                {
                    while (kt == false)
                    {
                        for (int i = 0; i < BUS_NV.hienthinv().Rows.Count; ++i)
                        {
                            dr = BUS_NV.hienthinv().Rows[i];
                            if (quantri == dr["IdNV"].ToString())
                            {
                                kt = false;
                                gt = rand.Next(100000000, 999999999);
                                quantri = "NQT" + gt;
                                break;
                            }
                            else
                            {
                                kt = true;
                            }
                        }
                    }
                    txtMaNhanVien.Text = quantri;
                }
                else
                {
                    txtMaNhanVien.Text = quantri;
                }
                tencv = "Nhân viên quản trị";
                txtchucvu.Text= "Nhân viên quản trị";
            }
            if (DGVNhanVien.Rows.Count > 0)
            {
                if (rdbquantri.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) == "NQT")
                {
                    txtMaNhanVien.Text = DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString();
                    tencv = "Nhân viên quản trị";
                    txtchucvu.Text = "Nhân viên quản trị";
                }
            }
        }

        private void rdbketoan_CheckedChanged(object sender, EventArgs e)
        {
            if ((rdbketoan.Checked == true && dgvnvclick == false) || (rdbketoan.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) != "NKT"))
            {
                //tạo mã ngẫu nhiên
                int gt;
                bool kt = false;
                Random rand = new Random();
                gt = rand.Next(100000000, 999999999);
                string ketoan = "NKT" + gt;
                DataRow dr;
                if (BUS_NV.hienthinv().Rows.Count > 0)
                {
                    while (kt == false)
                    {
                        for (int i = 0; i < BUS_NV.hienthinv().Rows.Count; ++i)
                        {
                            dr = BUS_NV.hienthinv().Rows[i];
                            if (ketoan == dr["IdNV"].ToString())
                            {
                                kt = false;
                                gt = rand.Next(100000000, 999999999);
                                ketoan = "NKT" + gt;
                                break;
                            }
                            else
                            {
                                kt = true;
                            }
                        }
                    }
                    txtMaNhanVien.Text = ketoan;
                }
                else
                {
                    txtMaNhanVien.Text = ketoan;
                }
                tencv = "Nhân viên kế toán";
                txtchucvu.Text = "Nhân viên kế toán";
            }
            if (DGVNhanVien.Rows.Count > 0)
            {
                if (rdbketoan.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) == "NKT")
                {
                    txtMaNhanVien.Text = DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString();
                    tencv = "Nhân viên kế toán";
                    txtchucvu.Text = "Nhân viên kế toán";
                }
            }
        }

        private void rdbbanhang_CheckedChanged(object sender, EventArgs e)
        {
            if ((rdbbanhang.Checked == true && dgvnvclick == false) || (rdbbanhang.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) != "NBH"))
            {
                //tạo mã ngẫu nhiên
                int gt;
                bool kt = false;
                Random rand = new Random();
                gt = rand.Next(100000000, 999999999);
                string banhang = "NBH" + gt;
                DataRow dr;
                if (BUS_NV.hienthinv().Rows.Count > 0)
                {
                    while (kt == false)
                    {
                        for (int i = 0; i < BUS_NV.hienthinv().Rows.Count; ++i)
                        {
                            dr = BUS_NV.hienthinv().Rows[i];
                            if (banhang == dr["IdNV"].ToString())
                            {
                                kt = false;
                                gt = rand.Next(100000000, 999999999);
                                banhang = "NBH" + gt;
                                break;
                            }
                            else
                            {
                                kt = true;
                            }
                        }
                    }
                    txtMaNhanVien.Text = banhang;
                }
                else
                {
                    txtMaNhanVien.Text = banhang;
                }
                tencv = "Nhân viên bán hàng";
                txtchucvu.Text = "Nhân viên bán hàng";
            }
            if (DGVNhanVien.Rows.Count > 0)
            {
                if (rdbbanhang.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) == "NBH")
            {
                    txtMaNhanVien.Text = DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString();
                    tencv = "Nhân viên bán hàng";
                    txtchucvu.Text = "Nhân viên bán hàng";
                }
            }
        }

        private void rdbthukho_CheckedChanged(object sender, EventArgs e)
        {
            if ((rdbthukho.Checked == true && dgvnvclick == false) || (rdbthukho.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) != "NTK"))
            {
                //tạo mã ngẫu nhiên
                int gt;
                bool kt = false;
                Random rand = new Random();
                gt = rand.Next(100000000, 999999999);
                string thukho = "NTK" + gt;
                DataRow dr;
                if (BUS_NV.hienthinv().Rows.Count > 0)
                {
                    while (kt == false)
                    {
                        for (int i = 0; i < BUS_NV.hienthinv().Rows.Count; ++i)
                        {
                            dr = BUS_NV.hienthinv().Rows[i];
                            if (thukho == dr["IdNV"].ToString())
                            {
                                kt = false;
                                gt = rand.Next(100000000, 999999999);
                                thukho = "NTK" + gt;
                                break;
                            }
                            else
                            {
                                kt = true;
                            }
                        }
                    }
                    txtMaNhanVien.Text = thukho;
                }
                else
                {
                    txtMaNhanVien.Text = thukho;
                }
                tencv = "Nhân viên thủ kho";
                txtchucvu.Text = "Nhân viên thủ kho";
            }
            if (DGVNhanVien.Rows.Count > 0)
            {
                if (rdbthukho.Checked == true && dgvnvclick == true && DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString().Substring(0, 3) == "NTK")
                {
                    txtMaNhanVien.Text = DGVNhanVien.CurrentRow.Cells["IdNV"].Value.ToString();
                    tencv = "Nhân viên thủ kho";
                    txtchucvu.Text = "Nhân viên thủ kho";
                }
            }
        }

        private void NHANVIEN_FormClosed(object sender, FormClosedEventArgs e)
        {
            DANGNHAP.thaotac += " | ";
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if(txtTenNhanVien.Text == string.Empty && mskDienthoai.Text == string.Empty && txtchucvu.Text == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập điều kiện tìm kiếm!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO_NV nv = new DTO_NV();
            nv.Chucvu = txtchucvu.Text;
            nv.Ten = txtTenNhanVien.Text;
            nv.Sdt = mskDienthoai.Text;
            DataTable dt = BUS_NV.timkiemnv(nv.Ten, nv.Sdt, nv.Chucvu);
            DGVNhanVien.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                lblkqtknv.Text = "Không có nhân viên nào thoả mãn điều kiện tìm kiếm!";
            }
            else
            {
                lblkqtknv.Text = "Có " + dt.Rows.Count + " nhân viên nào thoả mãn điều kiện tìm kiếm!";
            }
            DANGNHAP.thaotac += "Tìm kiếm, ";
        }

        private void btnundo_Click(object sender, EventArgs e)
        {

        }

        private void btnredo_Click(object sender, EventArgs e)
        {

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
                btnMaximize.Image= Properties.Resources.minimize__1_;
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

        private void txtluongcb_Leave(object sender, EventArgs e)
        {
            if (txtluongcb.Text.Trim() != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtluongcb.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtluongcb.Focus();
                }
                else
                {
                    if (a < 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtluongcb.Focus();
                        return;
                    }
                    else
                    {
                        if (a.ToString().Length < 7)
                        {
                            MessageBox.Show("Lương hợp lệ phải >= 1 000 000!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtluongcb.Focus();
                            return;
                        }
                        else
                        {
                            txtluongcb.Text = BUS_HDB.FormatNumber(a.ToString());
                            txtluongcb_TextChanged(sender, e);
                        }
                    }

                }
            }
        }

        private void txthsl_Leave(object sender, EventArgs e)
        {
            if (txthsl.Text.Trim() != "")
            {
                float a = 0;
                if (!float.TryParse(BUS_HDB.ConvertToFloatType(txthsl.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txthsl.Focus();
                    return;
                }
                else
                {
                    if (a < 0.1)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0.1!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txthsl.Focus();
                        return;
                    }
                    else
                    {
                        txthsl.Text = BUS_HDB.FormatNumber(a.ToString());
                        txthsl_TextChanged(sender, e);
                    }
                }
            }
        }

        private void txtluongcb_TextChanged(object sender, EventArgs e)
        {
            if (txtluongcb.Text.Trim().Length > 0 && txthsl.Text.Length > 0)
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtluongcb.Text), out a))
                {
                    txtthuclinh.Text = string.Empty;
                }
                else
                {
                    txtthuclinh.Text = Math.Round((float.Parse(BUS_HDB.ConvertToFloatType(txthsl.Text)) * a)).ToString();
                    txtthuclinh.Text = BUS_HDB.FormatNumber(txtthuclinh.Text);
                }
            }
            else
            {
                txtthuclinh.Text = string.Empty;
            }
        }

        private void txthsl_TextChanged(object sender, EventArgs e)
        {
            if (txthsl.Text.Trim().Length > 0 && txtluongcb.Text.Length > 0)
            {
                float a = 0;
                if (!float.TryParse(BUS_HDB.ConvertToFloatType(txthsl.Text), out a))
                {
                    txtthuclinh.Text = string.Empty;
                }
                else
                {
                    txtthuclinh.Text = Math.Round((Int64.Parse(BUS_HDB.ConvertToFloatType(txtluongcb.Text)) * a)).ToString();
                    txtthuclinh.Text = BUS_HDB.FormatNumber(txtthuclinh.Text);
                }
            }
            else
            {
                txtthuclinh.Text = string.Empty;
            }
        }

        private void btnhienthi_Click(object sender, EventArgs e)
        {
            DGVNhanVien.DataSource = nv;
            ResetValues();
        }
    }
}
