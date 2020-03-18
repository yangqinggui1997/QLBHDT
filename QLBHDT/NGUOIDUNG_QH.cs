using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;

namespace QLBHDT
{
    public partial class NGUOIDUNG_QH : Form
    {
        public NGUOIDUNG_QH()
        {
            InitializeComponent();
        }

        public DataTable nd;
        public static string manhomND = "";
        public static bool xacnhan = false;

        private string lbl;

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        // Bóng đổ
        //        cp.ClassStyle |= 0x20000;
        //        // Load các control cùng lúc
        //        cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}

        private void NGUOIDUNG_QH_Load(object sender, EventArgs e)
        {
            lbl = lblkqtknd.Text;
            BUS_ND.FillComboMaNND(cbManhomnd, "IdNND", "IdNND");
            cbManhomnd.SelectedIndex = -1;

            BUS_ND.FillComboTenND(cbtenND, "TenND", "TenND");
            cbtenND.SelectedIndex = -1;

            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            btnHienThi.Enabled = false;
            btnxemquyen.Enabled = false;
            LoadDataGridViewnd();
        }

        public void LoadDataGridViewnd()
        {
            nd = BUS_ND.hienthiND();
            dgvNguoidung.DataSource = nd;
            dgvNguoidung.AllowUserToAddRows = false;
            dgvNguoidung.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        public void ResetValuesnd()
        {
            txtMand.Text = string.Empty;
            cbManhomnd.Text = string.Empty;
            txtMaNV.Text = string.Empty;
            cbtenND.Text = string.Empty;
            txtTenTK.Text = string.Empty;
            txttennhomnd.Text = string.Empty;
            txttennhomqh.Text = string.Empty;
            dtpngaytaotk.Value = DateTime.Now;
            lblkqtknd.Text = lbl;
            btnHienThi.Enabled = false;

            btnsua.Enabled = false;
            btnxoa.Enabled = false;
            chkQuantri.Checked = false;
            chkqlhd.Checked = false;
            chkqlkh.Checked = false;
            chkqlnv.Checked = false;
            chkqlsp.Checked = false;
            chktkbc.Checked = false;
            chkqlncu.Checked = false;

            chkQuantri.Enabled = false;
            chkqlhd.Enabled = false;
            chkqlkh.Enabled = false;
            chkqlnv.Enabled = false;
            chkqlsp.Enabled = false;
            chktkbc.Enabled = false;
            chkqlncu.Enabled = false;

            clbqlhd.Enabled = false;
            clbqlkh.Enabled = false;
            clbqlnv.Enabled = false;
            clbqlsp.Enabled = false;
            clbqltkbc.Enabled = false;
            clbquantri.Enabled = false;
            clbqlncu.Enabled = false;
            for (int i = 0; i < clbqlnv.Items.Count; ++i)
            {
                clbqlnv.SetItemChecked(i, false);
            }
            for (int i = 0; i < clbqlkh.Items.Count; ++i)
            {
                clbqlkh.SetItemChecked(i, false);
            }
            for (int i = 0; i < clbqlncu.Items.Count; ++i)
            {
                clbqlncu.SetItemChecked(i, false);
            }
            for (int i = 0; i < clbqlsp.Items.Count; ++i)
            {
                clbqlsp.SetItemChecked(i, false);
            }
            for (int i = 0; i < clbqlhd.Items.Count; ++i)
            {
                clbqlhd.SetItemChecked(i, false);
            }
            for (int i = 0; i < clbqltkbc.Items.Count; ++i)
            {
                clbqltkbc.SetItemChecked(i, false);
            }
            for (int i = 0; i < clbquantri.Items.Count; ++i)
            {
                clbquantri.SetItemChecked(i, false);
            }
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            DANGNHAP.thaotac += "Tìm kiếm, ";

            //search on Nguoi_Dung table
            DTO_ND nd = new DTO_ND();
            nd.Mannd = cbManhomnd.Text;
            nd.Tennd = cbtenND.Text;
            nd.Ngaytaotk = dtpngaytaotk.Text;

            DataTable ND = BUS_ND.TimkiemND(nd.Mannd, nd.Tennd, nd.Ngaytaotk);
            if (ND.Rows.Count == 0)
                lblkqtknd.Text = "Không có bản ghi nào được tìm thấy!";
            else
                lblkqtknd.Text = "Có " + ND.Rows.Count + " bản ghi được tìm thấy!";
            dgvNguoidung.DataSource = ND;
            btnHienThi.Enabled = true;
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            LoadDataGridViewnd();
            ResetValuesnd();
        }

        private void btnxoand_Click(object sender, EventArgs e)
        {
            if (nd.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMand.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xoá người dùng sẽ xoá tất cả các thông tin truy cập có liên quan với người dùng này. Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if(txtMaNV.Text.Length > 3)
                {
                    if (txtMaNV.Text.Substring(0, 3) == "NQT")
                    {
                        MessageBox.Show("Không thể xóa người dùng là quản trị viên, người dùng này được bảo vệ!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        return;
                    }
                }
                BUS_ND.RunDelSQLOnTC(dgvNguoidung.CurrentRow.Cells["IdND"].Value.ToString());

                //Tìm tên tài khoản trên bảng nhân viên để xoá tài
                DataTable dt = BUS_NV.hienthinvcuthe(dgvNguoidung.CurrentRow.Cells["IdNV"].Value.ToString());
                if(dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    string TK = "";
                    string[] tk = dr[9].ToString().Split(',');
                    foreach (string items in tk)
                    {
                        if (items.Trim().Length >= 6 && items.Trim() != dgvNguoidung.CurrentRow.Cells[4].Value.ToString())
                        {
                            TK += items.Trim() + ", ";
                        }
                    }

                    BUS_ND.CapnhatTKNV(dgvNguoidung.CurrentRow.Cells["IdNV"].Value.ToString(), TK);
                }

                BUS_ND.RunDelSQL(dgvNguoidung.CurrentRow.Cells["IdND"].Value.ToString());

                LoadDataGridViewnd();
                ResetValuesnd();
                DANGNHAP.thaotac += "Xoá, ";
            }

            BUS_ND.FillComboTenND(cbtenND, "TenND", "TenND");
            cbtenND.SelectedIndex = -1;
        }



        private void dgvNguoidung_Click(object sender, EventArgs e)
        {
            if (dgvNguoidung.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dgvNguoidung.CurrentRow.Index == dgvNguoidung.NewRowIndex)
            {
                MessageBox.Show("Hãy chọn dòng có thông tin!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ResetValuesnd();
            cbManhomnd.Text = dgvNguoidung.CurrentRow.Cells["IdNND"].Value.ToString();

            DataTable dt = BUS_NND.hienthiNNDcuthe(dgvNguoidung.CurrentRow.Cells["IdNND"].Value.ToString());
            if(dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                txttennhomnd.Text = dr[2].ToString();
                dt= BUS_NQH.hienthiNQHcuthe(dr[1].ToString());
                if(dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    txttennhomqh.Text = dr[1].ToString();
                }
            }

            txtMand.Text = dgvNguoidung.CurrentRow.Cells["IdND"].Value.ToString();
            txtMaNV.Text = dgvNguoidung.CurrentRow.Cells["IdNV"].Value.ToString();
            cbtenND.Text = dgvNguoidung.CurrentRow.Cells["TenND"].Value.ToString();
            txtTenTK.Text = dgvNguoidung.CurrentRow.Cells["TenTK"].Value.ToString();
            dtpngaytaotk.Text = dgvNguoidung.CurrentRow.Cells["NgayTaoTK"].Value.ToString();
            string[] danhmuctmp = dgvNguoidung.CurrentRow.Cells["DanhmucTC"].Value.ToString().Split('|');
            string[] quyentmp = dgvNguoidung.CurrentRow.Cells["QuyenDM"].Value.ToString().Split('|');
            for (int j = 0; j < danhmuctmp.Length; ++j)
            {
                if (danhmuctmp[j].Trim() == "Quản lý nhân viên")
                {
                    chkqlnv.Checked = true;
                    string[] nv;
                    nv = quyentmp[j].Split(';');
                    if (nv != null)
                    {
                        foreach (string items in nv)
                        {
                            if (items.Trim() == "Xem (đọc)")
                            {
                                clbqlnv.SetItemChecked(0, true);
                            }
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                clbqlnv.SetItemChecked(1, true);
                            }
                            if (items.Trim() == "Sửa (cập nhật) bản ghi")
                            {
                                clbqlnv.SetItemChecked(2, true);
                            }
                            if (items.Trim() == "Xoá (huỷ) bản ghi")
                            {
                                clbqlnv.SetItemChecked(3, true);
                            }
                        }
                    }
                }
                else if (danhmuctmp[j].Trim() == "Quản lý khách hàng")
                {
                    chkqlkh.Checked = true;
                    string[] kh;
                    kh = quyentmp[j].Split(';');
                    if (kh != null)
                    {
                        foreach (string items in kh)
                        {
                            if (items.Trim() == "Xem (đọc)")
                            {
                                clbqlkh.SetItemChecked(0, true);
                            }
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                clbqlkh.SetItemChecked(1, true);
                            }
                            if (items.Trim() == "Sửa (cập nhật) bản ghi")
                            {
                                clbqlkh.SetItemChecked(2, true);
                            }
                            if (items.Trim() == "Xoá (huỷ) bản ghi")
                            {
                                clbqlkh.SetItemChecked(3, true);
                            }
                        }
                    }
                }
                else if (danhmuctmp[j].Trim() == "Quản lý nhà cung ứng")
                {
                    chkqlncu.Checked = true;
                    string[] ncu;
                    ncu = quyentmp[j].Split(';');
                    if (ncu != null)
                    {
                        foreach (string items in ncu)
                        {
                            if (items.Trim() == "Xem (đọc)")
                            {
                                clbqlncu.SetItemChecked(0, true);
                            }
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                clbqlncu.SetItemChecked(1, true);
                            }
                            if (items.Trim() == "Sửa (cập nhật) bản ghi")
                            {
                                clbqlncu.SetItemChecked(2, true);
                            }
                            if (items.Trim() == "Xoá (huỷ) bản ghi")
                            {
                                clbqlncu.SetItemChecked(3, true);
                            }
                        }
                    }
                }
                else if (danhmuctmp[j].Trim() == "Quản lý sản phẩm")
                {
                    chkqlsp.Checked = true;
                    string[] sp;
                    sp = quyentmp[j].Split(';');
                    if (sp != null)
                    {
                        foreach (string items in sp)
                        {
                            if (items.Trim() == "Xem (đọc)")
                            {
                                clbqlsp.SetItemChecked(0, true);
                            }
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                clbqlsp.SetItemChecked(1, true);
                            }
                            if (items.Trim() == "Sửa (cập nhật) bản ghi")
                            {
                                clbqlsp.SetItemChecked(2, true);
                            }
                            if (items.Trim() == "Xoá (huỷ) bản ghi")
                            {
                                clbqlsp.SetItemChecked(3, true);
                            }
                        }
                    }
                }
                else if (danhmuctmp[j].Trim() == "Quản lý hoá đơn")
                {
                    chkqlhd.Checked = true;
                    string[] hd;
                    hd = quyentmp[j].Split(';');
                    if (hd != null)
                    {
                        foreach (string items in hd)
                        {
                            if (items.Trim() == "Xem (đọc)")
                            {
                                clbqlhd.SetItemChecked(0, true);
                            }
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                clbqlhd.SetItemChecked(1, true);
                            }
                            if (items.Trim() == "Sửa (cập nhật) bản ghi")
                            {
                                clbqlhd.SetItemChecked(2, true);
                            }
                            if (items.Trim() == "Xoá (huỷ) bản ghi")
                            {
                                clbqlhd.SetItemChecked(3, true);
                            }
                        }
                    }
                }
                else if (danhmuctmp[j].Trim() == "Thống kê, báo cáo")
                {
                    chktkbc.Checked = true;
                    string[] tk;
                    tk = quyentmp[j].Split(';');
                    if (tk != null)
                    {
                        foreach (string items in tk)
                        {
                            if (items.Trim() == "Xem (đọc)")
                            {
                                clbqltkbc.SetItemChecked(0, true);
                            }
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                clbqltkbc.SetItemChecked(1, true);
                            }
                            if (items.Trim() == "Sửa (cập nhật) bản ghi")
                            {
                                clbqltkbc.SetItemChecked(2, true);
                            }
                            if (items.Trim() == "Xoá (huỷ) bản ghi")
                            {
                                clbqltkbc.SetItemChecked(3, true);
                            }
                        }
                    }
                }
                else if (danhmuctmp[j].Trim() == "Quản trị hệ thống (quản lý người dùng)")
                {
                    chkQuantri.Checked = true;
                    string[] qt;
                    qt = quyentmp[j].Split(';');
                    if (qt != null)
                    {
                        foreach (string items in qt)
                        {
                            if (items.Trim() == "Xem (đọc)")
                            {
                                clbquantri.SetItemChecked(0, true);
                            }
                            if (items.Trim() == "Thêm (tạo) bản ghi")
                            {
                                clbquantri.SetItemChecked(1, true);
                            }
                            if (items.Trim() == "Sửa (cập nhật) bản ghi")
                            {
                                clbquantri.SetItemChecked(2, true);
                            }
                            if (items.Trim() == "Xoá (huỷ) bản ghi")
                            {
                                clbquantri.SetItemChecked(3, true);
                            }
                        }
                    }
                }
            }

            chkqlhd.Enabled = true;
            chkqlkh.Enabled = true;
            chkqlnv.Enabled = true;
            chkqlsp.Enabled = true;
            chkQuantri.Enabled = true;
            chktkbc.Enabled = true;
            chkqlncu.Enabled = true;

            btnxoa.Enabled = true;
            btnsua.Enabled = true;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            if (cbManhomnd.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Bạn phải nhập mã nhóm người dùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbManhomnd.Focus();
                return;
            }
            else
            {
                //kiểm tra nhóm người dùng có tồn tại không
                bool kt = false;
                for (int i = 0; i < cbManhomnd.Items.Count; ++i)
                {
                    if (cbManhomnd.Text.Trim() == cbManhomnd.GetItemText(cbManhomnd.Items[i]))
                    {
                        kt = true;
                        break;
                    }
                    else
                    {
                        kt = false;
                    }
                }
                if (kt == false)
                {
                    MessageBox.Show("Mã nhóm người dùng không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbManhomnd.Focus();
                    return;
                }
                else
                {
                    if (chkqlhd.Checked == false && chkqlkh.Checked == false && chkqlnv.Checked == false && chkqlsp.Checked == false && chkQuantri.Checked == false && chktkbc.Checked == false && chkqlncu.Checked == false)
                    {
                        MessageBox.Show("Bạn chưa chọn danh mục quản lý nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    string danhmuc = "", quyen = "";

                    if (chkqlnv.Checked == true)
                    {
                        danhmuc = chkqlnv.Text.Substring(0, chkqlnv.Text.Length - 1);
                        if (!clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                        {
                            MessageBox.Show("Bạn chưa xác nhận quyền hạn đối với danh mục 'Quản lý nhân viên'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //chon 1 quyen
                            if (clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc)";
                                goto Label1;
                            }
                            if (!clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Thêm (tạo) bản ghi";
                                goto Label1;
                            }
                            if (!clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Sửa (cập nhật) bản ghi";
                                goto Label1;
                            }
                            if (!clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                            //cho 2 quyen
                            if (clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc); Thêm (tạo) bản ghi";
                                goto Label1;
                            }
                            if (clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc); Sửa (cập nhật) bản ghi";
                                goto Label1;
                            }
                            if (clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc); Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                            if (!clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label1;
                            }
                            if (!clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                            if (!clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                            //chon 3 quyen
                            if (clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && !clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label1;
                            }
                            if (clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && !clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc); Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                            if (!clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                            if (clbqlnv.GetItemChecked(0) && !clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc); Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                            //chon 4 quyen
                            if (clbqlnv.GetItemChecked(0) && clbqlnv.GetItemChecked(1) && clbqlnv.GetItemChecked(2) && clbqlnv.GetItemChecked(3))
                            {
                                quyen = "Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label1;
                            }
                        }
                    }
                    Label1:
                    if (chkqlkh.Checked == true)
                    {
                        danhmuc += " | " + chkqlkh.Text.Substring(0, chkqlkh.Text.Length - 1);
                        if (!clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                        {
                            MessageBox.Show("Bạn chưa xác nhận quyền hạn đối với danh mục 'Quản lý khách hàng'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //chon 1 quyen
                            if (clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc)";
                                goto Label2;
                            }
                            if (!clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi";
                                goto Label2;
                            }
                            if (!clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi";
                                goto Label2;
                            }
                            if (!clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                            //cho 2 quyen
                            if (clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi";
                                goto Label2;
                            }
                            if (clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi";
                                goto Label2;
                            }
                            if (clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                            if (!clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label2;
                            }
                            if (!clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                            if (!clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                            //chon 3 quyen
                            if (clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && !clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label2;
                            }
                            if (clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && !clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                            if (!clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                            if (clbqlkh.GetItemChecked(0) && !clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                            //chon 4 quyen
                            if (clbqlkh.GetItemChecked(0) && clbqlkh.GetItemChecked(1) && clbqlkh.GetItemChecked(2) && clbqlkh.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label2;
                            }
                        }
                    }
                    Label2:
                    if (chkqlsp.Checked == true)
                    {
                        danhmuc += " | " + chkqlsp.Text.Substring(0, chkqlsp.Text.Length - 1);
                        if (!clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                        {
                            MessageBox.Show("Bạn chưa xác nhận quyền hạn đối với danh mục 'Quản lý sản phẩm'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //chon 1 quyen
                            if (clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc)";
                                goto Label3;
                            }
                            if (!clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi";
                                goto Label3;
                            }
                            if (!clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi";
                                goto Label3;
                            }
                            if (!clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                            //cho 2 quyen
                            if (clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi";
                                goto Label3;
                            }
                            if (clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi";
                                goto Label3;
                            }
                            if (clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                            if (!clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label3;
                            }
                            if (!clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                            if (!clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                            //chon 3 quyen
                            if (clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && !clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label3;
                            }
                            if (clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && !clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                            if (!clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                            if (clbqlsp.GetItemChecked(0) && !clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                            //chon 4 quyen
                            if (clbqlsp.GetItemChecked(0) && clbqlsp.GetItemChecked(1) && clbqlsp.GetItemChecked(2) && clbqlsp.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label3;
                            }
                        }
                    }
                    Label3:
                    if (chkqlhd.Checked == true)
                    {
                        danhmuc += " | " + chkqlhd.Text.Substring(0, chkqlhd.Text.Length - 1);
                        if (!clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                        {
                            MessageBox.Show("Bạn chưa xác nhận quyền hạn đối với danh mục 'Quản lý hoá đơn'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //chon 1 quyen
                            if (clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc)";
                                goto Label4;
                            }
                            if (!clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi";
                                goto Label4;
                            }
                            if (!clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi";
                                goto Label4;
                            }
                            if (!clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                            //cho 2 quyen
                            if (clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi";
                                goto Label4;
                            }
                            if (clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi";
                                goto Label4;
                            }
                            if (clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                            if (!clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label4;
                            }
                            if (!clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                            if (!clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                            //chon 3 quyen
                            if (clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && !clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label4;
                            }
                            if (clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && !clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                            if (!clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                            if (clbqlhd.GetItemChecked(0) && !clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                            //chon 4 quyen
                            if (clbqlhd.GetItemChecked(0) && clbqlhd.GetItemChecked(1) && clbqlhd.GetItemChecked(2) && clbqlhd.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label4;
                            }
                        }
                    }
                    Label4:
                    if (chktkbc.Checked == true)
                    {
                        danhmuc += " | " + chktkbc.Text.Substring(0, chktkbc.Text.Length - 1);
                        if (!clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                        {
                            MessageBox.Show("Bạn chưa xác nhận quyền hạn đối với danh mục 'Thống kê, báo cáo'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //chon 1 quyen
                            if (clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc)";
                                goto Label5;
                            }
                            if (!clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi";
                                goto Label5;
                            }
                            if (!clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi";
                                goto Label5;
                            }
                            if (!clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xoá (huỷ) bản ghi";
                                goto Label5;
                            }
                            //cho 2 quyen
                            if (clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi";
                                goto Label5;
                            }
                            if (clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi";
                                goto Label5;
                            }
                            if (clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Xoá (huỷ) bản ghi";
                                goto Label5;
                            }
                            if (!clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label5;
                            }
                            if (!clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label5;
                            }
                            if (!clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label5;
                            }

                            //chon 3 quyen
                            if (clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && !clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label5;
                            }
                            if (clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && !clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label5;
                            }
                            if (!clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label5;
                            }
                            if (clbqltkbc.GetItemChecked(0) && !clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label5;
                            }
                            //chon 4 quyen
                            if (clbqltkbc.GetItemChecked(0) && clbqltkbc.GetItemChecked(1) && clbqltkbc.GetItemChecked(2) && clbqltkbc.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label5;
                            }
                        }
                    }
                    Label5:
                    if (chkQuantri.Checked == true)
                    {
                        danhmuc += " | " + chkQuantri.Text.Substring(0, chkQuantri.Text.Length - 1);
                        if (!clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                        {
                            MessageBox.Show("Bạn chưa xác nhận quyền hạn đối với danh mục 'Quản trị hệ thống (quản lý người dùng)'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //chon 1 quyen
                            if (clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc)";
                                goto Label6;
                            }
                            if (!clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi";
                                goto Label6;
                            }
                            if (!clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi";
                                goto Label6;
                            }
                            if (!clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                            //cho 2 quyen
                            if (clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi";
                                goto Label6;
                            }
                            if (clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi";
                                goto Label6;
                            }
                            if (clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                            if (!clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label6;
                            }
                            if (!clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                            if (!clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                            //chon 3 quyen
                            if (clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && !clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                                goto Label6;
                            }
                            if (clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && !clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                            if (!clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                            if (clbquantri.GetItemChecked(0) && !clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                            //chon 4 quyen
                            if (clbquantri.GetItemChecked(0) && clbquantri.GetItemChecked(1) && clbquantri.GetItemChecked(2) && clbquantri.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                                goto Label6;
                            }
                        }
                    }
                    Label6:
                    if (chkqlncu.Checked == true)
                    {
                        danhmuc += " | " + chkqlncu.Text.Substring(0, chkqlncu.Text.Length - 1);
                        if (!clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                        {
                            MessageBox.Show("Bạn chưa xác nhận quyền hạn đối với danh mục 'Quản lý nhà cung ứng'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            //chon 1 quyen
                            if (clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc)";
                            }
                            if (!clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi";
                            }
                            if (!clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi";
                            }
                            if (!clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xoá (huỷ) bản ghi";
                            }
                            //cho 2 quyen
                            if (clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi";
                            }
                            if (clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi";
                            }
                            if (clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Xoá (huỷ) bản ghi";
                            }
                            if (!clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                            }
                            if (!clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                            }
                            if (!clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                            }
                            //chon 3 quyen
                            if (clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && !clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi";
                            }
                            if (clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && !clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Xoá (huỷ) bản ghi";
                            }
                            if (!clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                            }
                            if (clbqlncu.GetItemChecked(0) && !clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                            }
                            //chon 4 quyen
                            if (clbqlncu.GetItemChecked(0) && clbqlncu.GetItemChecked(1) && clbqlncu.GetItemChecked(2) && clbqlncu.GetItemChecked(3))
                            {
                                quyen += " | Xem (đọc); Thêm (tạo) bản ghi; Sửa (cập nhật) bản ghi; Xoá (huỷ) bản ghi";
                            }
                        }
                    }

                    if (dgvNguoidung.CurrentRow.Cells[1].Value.ToString() != cbManhomnd.Text.Trim())
                    {
                        if (MessageBox.Show("Nếu thay đổi nhóm người dùng của người dùng hiện tại thì tất cả các quyền của người dùng hiện tại sẽ bị huỷ. Bạn có muốn tiếp tục không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            string danhmuctmp = "", quyentmp = "";
                            DataTable dt = BUS_NND.hienthiNNDcuthe(dgvNguoidung.CurrentRow.Cells[1].Value.ToString());
                            if(dt.Rows.Count == 1)
                            {
                                DataRow dr = dt.Rows[0];
                                dt = BUS_NQH.hienthiNQHcuthe(dr[1].ToString());
                                if(dt.Rows.Count == 1)
                                {
                                    dr = dt.Rows[0];
                                    danhmuctmp = dr[3].ToString();
                                    quyentmp = dr[4].ToString();
                                }
                            }

                            DTO_ND nd = new DTO_ND(txtMand.Text, cbManhomnd.Text.Trim(), "", "", "", "", "", danhmuctmp, quyentmp);
                            BUS_ND.suaND(nd);
                            LoadDataGridViewnd();
                            ResetValuesnd();
                            DANGNHAP.thaotac += "Sửa, ";
                        }
                    }
                    else
                    {
                        DTO_ND nd = new DTO_ND(txtMand.Text, cbManhomnd.Text.Trim(), "", "", "", "", "", danhmuc, quyen);
                        BUS_ND.suaND(nd);
                        LoadDataGridViewnd();
                        ResetValuesnd();
                        DANGNHAP.thaotac += "Sửa, ";
                    }
                }
            }
        }

        private void btnxemquyen_Click(object sender, EventArgs e)
        {
            xacnhan = true;
            manhomND = cbManhomnd.Text;
            XEMQUYEN xq = new XEMQUYEN();
            xq.ShowDialog();
        }

        private void cbManhomnd_TextChanged(object sender, EventArgs e)
        {
            if (cbManhomnd.Text.Trim() == "")
            {
                txttennhomnd.Text = "";
                txttennhomqh.Text = "";
                btnxemquyen.Enabled = false;
            }
            else
            {
                // Khi chọn mã nhóm người dùng thì tên nhóm người dùng và tên nhóm quyền hạn hiện ra
                if (cbManhomnd.Items.Count > 0)
                {
                    for (int i = 0; i < cbManhomnd.Items.Count; ++i)
                    {
                        if (cbManhomnd.Text.Trim() == cbManhomnd.GetItemText(cbManhomnd.Items[i]))
                        {
                            DataTable dt = BUS_NND.hienthiNNDcuthe(cbManhomnd.Text.Trim());
                            if (dt.Rows.Count == 1)
                            {
                                DataRow dr = dt.Rows[0];
                                txttennhomnd.Text = dr[2].ToString();
                                dt = BUS_NQH.hienthiNQHcuthe(dr[1].ToString());
                                if (dt.Rows.Count == 1)
                                {
                                    dr = dt.Rows[0];
                                    txttennhomqh.Text = dr[1].ToString();
                                }
                            }

                            btnxemquyen.Enabled = true;
                            break;
                        }
                        else
                        {
                            txttennhomnd.Text = "";
                            txttennhomqh.Text = "";
                            btnxemquyen.Enabled = false;
                        }
                    }

                }
            }
        }

        private void txttennhomnd_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ToolTipIcon = ToolTipIcon.Info;
            tt.ToolTipTitle = "Tên nhóm người dùng: ";
            tt.SetToolTip(txttennhomnd, txttennhomnd.Text);
        }

        private void txttennhomqh_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.ToolTipIcon = ToolTipIcon.Info;
            tt.ToolTipTitle = "Tên nhóm quyền hạn: ";
            tt.SetToolTip(txttennhomqh, txttennhomqh.Text);
        }

        private void chkqlnv_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqlnv.Checked == true)
            {
                clbqlnv.Enabled = true;
            }
            else
            {
                clbqlnv.Enabled = false;
            }
        }

        private void chkqlkh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqlkh.Checked == true)
            {
                clbqlkh.Enabled = true;
            }
            else
            {
                clbqlkh.Enabled = false;
            }
        }

        private void chkqlsp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqlsp.Checked == true)
            {
                clbqlsp.Enabled = true;
            }
            else
            {
                clbqlsp.Enabled = false;
            }
        }

        private void chkqlhd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqlhd.Checked == true)
            {
                clbqlhd.Enabled = true;
            }
            else
            {
                clbqlhd.Enabled = false;
            }
        }

        private void chktkbc_CheckedChanged(object sender, EventArgs e)
        {
            if (chktkbc.Checked == true)
            {
                clbqltkbc.Enabled = true;
            }
            else
            {
                clbqltkbc.Enabled = false;
            }
        }

        private void chkQuantri_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQuantri.Checked == true)
            {
                clbquantri.Enabled = true;
            }
            else
            {
                clbquantri.Enabled = false;
            }
        }

        private void cbtenND_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbtenND.Text == "")
            {
                ResetValuesnd();
            }
            else
            {
                // Khi chọn mã nhóm người dùng thì tên nhóm người dùng và tên nhóm quyền hạn hiện ra
                if (cbtenND.Items.Count > 0)
                {
                    for (int i = 0; i < cbtenND.Items.Count; ++i)
                    {
                        if (cbtenND.Text.Trim() == cbtenND.GetItemText(cbtenND.Items[i]))
                        {
                            DataTable dt = BUS_ND.hienthiNDtheoten(cbtenND.Text.Trim());
                            ResetValuesnd();
                            if (dt.Rows.Count == 1)
                            {
                                DataRow dr = dt.Rows[0];
                                txtMand.Text = dr[0].ToString();
                                cbManhomnd.Text = dr[1].ToString();
                                txtMaNV.Text = dr[2].ToString();
                                txtTenTK.Text = dr[4].ToString();
                                cbtenND.Text = cbtenND.GetItemText(cbtenND.Items[i]);
                                //gọi lại sự kiện cbManhomnd_TextChanged
                                cbManhomnd_TextChanged(sender, e);
                                dtpngaytaotk.Text = dr[6].ToString();
                                string danhmuc = dr[7].ToString();
                                string quyendm = dr[8].ToString();
                                string[] danhmuctmp = danhmuc.Split('|');
                                string[] quyentmp = quyendm.Split('|');

                                for (int j = 0; j < danhmuctmp.Length; ++j)
                                {
                                    if (danhmuctmp[j].Trim() == "Quản lý nhân viên")
                                    {
                                        chkqlnv.Checked = true;
                                        string[] nv;
                                        nv = quyentmp[j].Split(';');
                                        if (nv != null)
                                        {
                                            foreach (string items in nv)
                                            {
                                                if (items.Trim() == "Xem (đọc)")
                                                {
                                                    clbqlnv.SetItemChecked(0, true);
                                                }
                                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                                {
                                                    clbqlnv.SetItemChecked(1, true);
                                                }
                                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                                {
                                                    clbqlnv.SetItemChecked(2, true);
                                                }
                                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                                {
                                                    clbqlnv.SetItemChecked(3, true);
                                                }
                                            }
                                        }
                                    }
                                    else if (danhmuctmp[j].Trim() == "Quản lý khách hàng")
                                    {
                                        chkqlkh.Checked = true;
                                        string[] kh;
                                        kh = quyentmp[j].Split(';');
                                        if (kh != null)
                                        {
                                            foreach (string items in kh)
                                            {
                                                if (items.Trim() == "Xem (đọc)")
                                                {
                                                    clbqlkh.SetItemChecked(0, true);
                                                }
                                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                                {
                                                    clbqlkh.SetItemChecked(1, true);
                                                }
                                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                                {
                                                    clbqlkh.SetItemChecked(2, true);
                                                }
                                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                                {
                                                    clbqlkh.SetItemChecked(3, true);
                                                }
                                            }
                                        }
                                    }
                                    else if (danhmuctmp[j].Trim() == "Quản lý nhà cung ứng")
                                    {
                                        chkqlncu.Checked = true;
                                        string[] ncu;
                                        ncu = quyentmp[j].Split(';');
                                        if (ncu != null)
                                        {
                                            foreach (string items in ncu)
                                            {
                                                if (items.Trim() == "Xem (đọc)")
                                                {
                                                    clbqlncu.SetItemChecked(0, true);
                                                }
                                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                                {
                                                    clbqlncu.SetItemChecked(1, true);
                                                }
                                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                                {
                                                    clbqlncu.SetItemChecked(2, true);
                                                }
                                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                                {
                                                    clbqlncu.SetItemChecked(3, true);
                                                }
                                            }
                                        }
                                    }
                                    else if (danhmuctmp[j].Trim() == "Quản lý sản phẩm")
                                    {
                                        chkqlsp.Checked = true;
                                        string[] sp;
                                        sp = quyentmp[j].Split(';');
                                        if (sp != null)
                                        {
                                            foreach (string items in sp)
                                            {
                                                if (items.Trim() == "Xem (đọc)")
                                                {
                                                    clbqlsp.SetItemChecked(0, true);
                                                }
                                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                                {
                                                    clbqlsp.SetItemChecked(1, true);
                                                }
                                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                                {
                                                    clbqlsp.SetItemChecked(2, true);
                                                }
                                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                                {
                                                    clbqlsp.SetItemChecked(3, true);
                                                }
                                            }
                                        }
                                    }
                                    else if (danhmuctmp[j].Trim() == "Quản lý hoá đơn")
                                    {
                                        chkqlhd.Checked = true;
                                        string[] hd;
                                        hd = quyentmp[j].Split(';');
                                        if (hd != null)
                                        {
                                            foreach (string items in hd)
                                            {
                                                if (items.Trim() == "Xem (đọc)")
                                                {
                                                    clbqlhd.SetItemChecked(0, true);
                                                }
                                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                                {
                                                    clbqlhd.SetItemChecked(1, true);
                                                }
                                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                                {
                                                    clbqlhd.SetItemChecked(2, true);
                                                }
                                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                                {
                                                    clbqlhd.SetItemChecked(3, true);
                                                }
                                            }
                                        }
                                    }
                                    else if (danhmuctmp[j].Trim() == "Thống kê, báo cáo")
                                    {
                                        chktkbc.Checked = true;
                                        string[] tk;
                                        tk = quyentmp[j].Split(';');
                                        if (tk != null)
                                        {
                                            foreach (string items in tk)
                                            {
                                                if (items.Trim() == "Xem (đọc)")
                                                {
                                                    clbqltkbc.SetItemChecked(0, true);
                                                }
                                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                                {
                                                    clbqltkbc.SetItemChecked(1, true);
                                                }
                                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                                {
                                                    clbqltkbc.SetItemChecked(2, true);
                                                }
                                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                                {
                                                    clbqltkbc.SetItemChecked(3, true);
                                                }
                                            }
                                        }
                                    }
                                    else if (danhmuctmp[j].Trim() == "Quản trị hệ thống (quản lý người dùng)")
                                    {
                                        chkQuantri.Checked = true;
                                        string[] qt;
                                        qt = quyentmp[j].Split(';');
                                        if (qt != null)
                                        {
                                            foreach (string items in qt)
                                            {
                                                if (items.Trim() == "Xem (đọc)")
                                                {
                                                    clbquantri.SetItemChecked(0, true);
                                                }
                                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                                {
                                                    clbquantri.SetItemChecked(1, true);
                                                }
                                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                                {
                                                    clbquantri.SetItemChecked(2, true);
                                                }
                                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                                {
                                                    clbquantri.SetItemChecked(3, true);
                                                }
                                            }
                                        }
                                    }

                                }
                                chkqlhd.Enabled = true;
                                chkqlncu.Enabled = true;
                                chkqlkh.Enabled = true;
                                chkqlnv.Enabled = true;
                                chkqlsp.Enabled = true;
                                chkQuantri.Enabled = true;
                                chktkbc.Enabled = true;

                                break;
                            }
                        }
                        else
                        {
                            txtMand.Text = "";
                            cbManhomnd.Text = "";
                            cbManhomnd_TextChanged(sender, e);
                            txtMaNV.Text = "";
                            txtTenTK.Text = "";
                            dtpngaytaotk.Value = DateTime.Now;

                            chkqlhd.Enabled = true;
                            chkqlkh.Enabled = true;
                            chkqlnv.Enabled = true;
                            chkqlsp.Enabled = true;
                            chkQuantri.Enabled = true;
                            chktkbc.Enabled = true;
                            chkqlncu.Enabled = true;

                            chkqlhd.Checked = false;
                            chkqlkh.Checked = false;
                            chkqlnv.Checked = false;
                            chkqlsp.Checked = false;
                            chkQuantri.Checked = false;
                            chktkbc.Checked = false;
                            chkqlncu.Checked = false;
                        }
                    }

                }
            }
        }

        private void chkqlncu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkqlncu.Checked == true)
            {
                clbqlncu.Enabled = true;
            }
            else
            {
                clbqlncu.Enabled = false;
            }
        }

        private void NGUOIDUNG_QH_FormClosed(object sender, FormClosedEventArgs e)
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
