using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Collections.Generic;
using System.Linq;

namespace QLBHDT
{
    public partial class HDNHAP : Form
    {
        public HDNHAP()
        {
            InitializeComponent();
        }

        public DataTable hdnct, hdnctht;
        private string lbl;
        public static bool hdnprint;
        public static string mahdn;
        private List<string> idspcu = new List<string>(), idsapmoi = new List<string>();

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
        private void btnClose_Click(object sender, EventArgs e)
        {
            btnDong_Click(sender, e);
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

        private void HDNHAP_Load(object sender, EventArgs e)
        {
            lbl = lblkqtkhdn.Text;
            btnundo.Enabled = false;
            btnredo.Enabled = false;

            //Đỗ dữ liệu từ các bảng lên các combobox tương ứng
            BUS_HDN.FillComboMaHD(cbmaHD, "IdHDN", "IdHDN");
            cbmaHD.SelectedIndex = -1;

            BUS_HDN.FillComboMaNV(cbMaNhanVien, "IdNV", "IdNV");
            cbMaNhanVien.SelectedIndex = -1;

            BUS_HDN.FillComboMaNCU(cbmanhacungung, "IdNCU", "IdNCU");
            cbmanhacungung.SelectedIndex = -1;

            BUS_HDB.FillComboMaSP(cbtensp, "TenSP", "TenSP");
            cbtensp.SelectedIndex = -1;

            btnThemSP.Enabled = false;
            btnhienthi.Enabled = false;

            //QLTC
            btnThemHD.Enabled = false;
            btnSuaHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnprint.Enabled = false;
            if (MAIN.tkhdn == false)
            {
                string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
                string[] quyenhan = DANGNHAP.Quyen.Split('|');

                for (int j = 0; j < danhmuctmp.Length; ++j)
                {
                    if (danhmuctmp[j].Trim() == "Quản lý hoá đơn")
                    {
                        string[] hdn;
                        hdn = quyenhan[j].Split(';');
                        if (hdn != null)
                        {
                            foreach (string items in hdn)
                            {
                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                {
                                    btnThemHD.Enabled = true;
                                }
                                if (items.Trim() == "Sửa (cập nhật) bản ghi")
                                {
                                    btnSuaHD.Enabled = true;
                                }
                                if (items.Trim() == "Xoá (huỷ) bản ghi")
                                {
                                    btnXoaHD.Enabled = true;
                                }
                            }
                        }
                        break;
                    }

                }
            }
            else
            {
                MAIN.tkhdn = false;
            }
        }

        private void ResetValuesHDN()
        {
            txtMaHoaDon.Text = "Mã sẽ tự động thêm!";
            dtpNgayLapHD.Value = DateTime.Now;
            dtpNgayHHTT.Value = DateTime.Now;
            txtTenNhanVien.Text = string.Empty;
            cbMaNhanVien.Text = string.Empty;
            cbmanhacungung.Text = string.Empty;
            txtTenNCU.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            txtdathanhtoan.Text = string.Empty;
            txtConno.Text = string.Empty;
            txtTongTien.Text = string.Empty;
            txttongsoluong.Text = string.Empty;

            lblkqtkhdn.Text = lbl;
        }

        private void ResetValuesHDNCT()
        {
            cbtensp.Text = string.Empty;
            txtmasp.Text = "Mã sẽ tự động thêm!";
            txtSoLuongnhap.Text = string.Empty;
            txthangsx.Text = string.Empty;
            txtdongia.Text = string.Empty;
            txtgiamgia.Text = string.Empty;
            txtthanhtien.Text = string.Empty;
            txtdongia.ReadOnly = true;
            txtgiamgia.ReadOnly = true;
            if (btnLamMoi.Enabled == true)
            {
                btnThemSP.Enabled = true;
            }
            btnSuaSP.Enabled = false;
            btnXoaSP.Enabled = false;
            btnBoqua.Enabled = true;
        }

        private void LoadDataGridView()
        {
            hdnct = BUS_HDNCT.hienthiHDNCT();
            DGVHoaDonNhap.DataSource = hdnct;
            DGVHoaDonNhap.AllowUserToAddRows = false;
            DGVHoaDonNhap.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void LoadHDNCTHT()
        {
            hdnctht = BUS_HDNCT.hienthiHDNCTcuthe(txtMaHoaDon.Text);
            DGVHoaDonNhap.DataSource = hdnctht;
            DGVHoaDonNhap.AllowUserToAddRows = false;
            DGVHoaDonNhap.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaNhanVien.Focus();
                return;
            }
            if (cbmanhacungung.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung ứng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbmanhacungung.Focus();
                return;
            }

            //tạo mã ngẫu nhiên
            int value;
            bool kt = false;
            Random rand = new Random();
            value = rand.Next(100000000, 999999999);
            string mahdn = "HDN" + value;
            DataRow dr;
            if (BUS_HDN.hienthiHDN().Rows.Count > 0)
            {
                while (kt == false)
                {
                    for (int i = 0; i < BUS_HDN.hienthiHDN().Rows.Count; ++i)
                    {
                        dr = BUS_HDN.hienthiHDN().Rows[i];
                        if (mahdn == dr["IdHDN"].ToString())
                        {
                            kt = false;
                            value = rand.Next(100000000, 999999999);
                            mahdn = "HDN" + value;
                            break;
                        }
                        else
                        {
                            kt = true;
                        }
                    }
                }
            }

            //Kiểm tra mã hoá đơn đã tồn tại chưa
            if (!BUS_HDN.ktHDNtrung(mahdn))
            {
                MessageBox.Show("Mã hoá đơn bán này đã tồn tại, bạn hãy nhấn nút 'Thêm' lần nữa để chọn mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHoaDon.Focus();
                return;
            }

            //Thêm trên bảng hoá đơn nhập
            DTO_HDN hdn = new DTO_HDN(mahdn, cbMaNhanVien.Text, cbmanhacungung.Text, DateTime.Now.ToString(), "0", "0", "0", "0", dtpNgayHHTT.Text);
            BUS_HDN.themHDN(hdn);

            //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
            BUS_HDN.FillComboMaHD(cbmaHD, "IdHDN", "IdHDN");
            cbmaHD.SelectedIndex = -1;

            btnThemHD.Enabled = false;
            btnLamMoi.Enabled = true;
            btnhienthi.Enabled = false;
            btnTimKiem.Enabled = false;
            cbmaHD.Enabled = false;
            btnThemSP.Enabled = true;
            cbtensp.Focus();

            ResetValuesHDN();
            ResetValuesHDNCT();
            //Hiển thị mã hiện tại lên control phục vụ việc thêm sản phẩm
            txtMaHoaDon.Text = mahdn;
            DataTable dt = BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text);
            //Cập nhật lại datasource
            LoadHDNCTHT();
            if (dt.Rows.Count == 1)
            {
                dr = dt.Rows[0];
                cbMaNhanVien.Text = dr[1].ToString();
                cbmanhacungung.Text = dr[2].ToString();
                dtpNgayLapHD.Text = dr[3].ToString();
                txttongsoluong.Text = dr[4].ToString();
                txtTongTien.Text = dr[5].ToString();
                txtdathanhtoan.Text = dr[6].ToString();
                txtConno.Text = dr[7].ToString();
                dtpNgayHHTT.Text = dr[8].ToString();

                dt = BUS_HDB.LayTenNV(cbMaNhanVien.Text);
                if (dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    txtTenNhanVien.Text = dr[0].ToString();
                }

                dt = BUS_HDN.LayTTNCU(cbmanhacungung.Text);
                if (dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    txtTenNCU.Text = dr[0].ToString();
                    txtDiaChi.Text = dr[1].ToString();
                    txtDienThoai.Text = dr[2].ToString();
                }
            }

            //Khởi tạo danh sách các mã sản phẩm đã tồn tại trong bảng sản phẩm để khi thêm sản phẩm sẽ tiến hành cộng dồn số lượng
            idspcu = new List<string>();
            idsapmoi = new List<string>();

            DANGNHAP.thaotac += "Thêm, ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text.Trim() == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xoá hoá đơn nhập sẽ xoá tất cả các thông tin về chi tiết của hoá đơn hiện tại. Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                BUS_HDN.RunDelSQLOnHDNCT(txtMaHoaDon.Text);
                BUS_HDN.RunDelSQL(txtMaHoaDon.Text);

                //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                BUS_HDN.FillComboMaHD(cbmaHD, "IdHDN", "IdHDN");
                cbmaHD.SelectedIndex = -1;

                ResetValuesHDN();
                ResetValuesHDNCT();
                if (btnThemHD.Enabled == false)
                {
                    cbmaHD.Enabled = true;
                }
                btnThemSP.Enabled = false;
                btnThemHD.Enabled = true;
                btnLamMoi.Enabled = false;
                LoadDataGridView();
                DANGNHAP.thaotac += "Xoá, ";
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaNhanVien.Focus();
                return;
            }
            if (cbmanhacungung.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung ứng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbmanhacungung.Focus();
                return;
            }

            DTO_HDN hdn = new DTO_HDN(txtMaHoaDon.Text, cbMaNhanVien.Text, cbmanhacungung.Text, dtpNgayLapHD.Text, BUS_HDB.ConvertToFloatType(txttongsoluong.Text), BUS_HDB.ConvertToFloatType(txtTongTien.Text), BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text), BUS_HDB.ConvertToFloatType(txtConno.Text), dtpNgayHHTT.Text);

            BUS_HDN.suaHDN(hdn);

            //Cập nhật lại nợ nhà cung ứng

            DataTable dt = BUS_NCU.hienthiNCUcuthe(cbmanhacungung.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    BUS_HDN.CapNhatnoNCU(cbmanhacungung.Text);
                }
            }

            DANGNHAP.thaotac += "Sửa, ";
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValuesHDNCT();
        }

        private void btnundo_Click(object sender, EventArgs e)
        {

        }

        private void btnredo_Click(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text).Rows.Count == 1)
            {
                DataRow dr = BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text).Rows[0];
                DataTable dt = BUS_HDNCT.hienthiHDNCTcuthe(txtMaHoaDon.Text);
                if (cbMaNhanVien.Text.Trim() != dr[1].ToString() || cbmanhacungung.Text.Trim() != dr[2].ToString() || txtdathanhtoan.Text.Trim() != BUS_HDB.FormatNumber(dr[6].ToString()))
                {
                    if (MessageBox.Show("Đã phát hiện thay đổi so với dữ liệu mà bạn đã lưu trước đó. Nấu Bạn muốn in với thay đổi hiện tại hãy nhấn nút 'Sửa HD' để cập nhật lại thay đổi, ngược lại sẽ in với dữ liệu trong lần cập nhật gần nhất", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btnSuaHD.Focus();
                        return;
                    }
                    else
                    {
                        if (dt.Rows.Count == 0)
                        {
                            if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn đã xóa hết chi tiết hóa đơn. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                btnSuaHD.Focus();
                                return;
                            }
                            else
                            {
                                //Xoá hoá đơn bán hiện tại
                                BUS_HDN.RunDelSQL(txtMaHoaDon.Text);
                            }

                        }
                        else
                        {
                            //Cập nhật lại số lượng cho những sản phẩm cũ
                            if (idspcu.Count > 0)
                            {
                                for (int i = 0; i < idspcu.Count; ++i)
                                {
                                    for (int j = 0; j < dt.Rows.Count; ++j)
                                    {
                                        dr = dt.Rows[j];
                                        if (idspcu[i] == dr[1].ToString())
                                        {
                                            DataTable table = BUS_SP.hienthiSPcuthe(idspcu[i]);
                                            if (table != null)
                                            {
                                                if (table.Rows.Count == 1)
                                                {
                                                    DataRow row = table.Rows[0];
                                                    string sl = row[7].ToString();
                                                    DTO_SP sp = new DTO_SP();
                                                    sp.Masp = idspcu[i];
                                                    sp.Slnhap = (Int64.Parse(sl) + Int64.Parse(dr[2].ToString())).ToString();

                                                    BUS_HDB.CapNhatSLSanPham(sp);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count == 0)
                    {
                        if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn chưa thêm sản phẩm nào. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnSuaHD.Focus();
                            return;
                        }
                        else
                        {
                            //Xoá hoá đơn bán hiện tại
                            BUS_HDN.RunDelSQL(txtMaHoaDon.Text);
                        }

                    }
                    else
                    {
                        //Cập nhật lại số lượng cho những sản phẩm cũ
                        if (idspcu.Count > 0)
                        {
                            for (int i = 0; i < idspcu.Count; ++i)
                            {
                                for (int j = 0; j < dt.Rows.Count; ++j)
                                {
                                    dr = dt.Rows[j];
                                    if (idspcu[i] == dr[1].ToString())
                                    {
                                        DataTable table = BUS_SP.hienthiSPcuthe(idspcu[i]);
                                        if (table != null)
                                        {
                                            if (table.Rows.Count == 1)
                                            {
                                                DataRow row = table.Rows[0];
                                                string sl = row[7].ToString();
                                                DTO_SP sp = new DTO_SP();
                                                sp.Masp = idspcu[i];
                                                sp.Slnhap = (Int64.Parse(sl) + Int64.Parse(dr[2].ToString())).ToString();

                                                BUS_HDB.CapNhatSLSanPham(sp);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
            DANGNHAP.thaotac += " | ";
            Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            btnhienthi.Enabled = true;
            DataTable dt = BUS_HDN.timkiemHDN(dtpNgayLapHD.Value.Day.ToString(),dtpNgayLapHD.Value.Month.ToString(),dtpNgayLapHD.Value.Year.ToString());

            if (dt.Rows.Count == 0)
            {
                lblkqtkhdn.Text = "Không có hóa đơn nào thoả mãn điều kiện tìm kiếm!";
                DGVHoaDonNhap.DataSource = BUS_HDNCT.hienthiHDNCTcuthe("NULL");
            }
            else
            {
                DGVHoaDonNhap.DataSource = dt;
                List<string> list = new List<string>();

                for (int i = 0; i < DGVHoaDonNhap.Rows.Count; ++i)
                {
                    if (DGVHoaDonNhap.Rows[i].IsNewRow == false)
                    {
                        list.Add(DGVHoaDonNhap.Rows[i].Cells["IdHDN"].Value.ToString());
                    }
                }
                while (KiemtraTrung(list) == true)
                {
                    LocDuLieuTrung(list);
                }
                lblkqtkhdn.Text = "Có " + list.Count + " hóa đơn thoả mãn điều kiện tìm kiếm!";
            }
            DANGNHAP.thaotac += "Tìm kiếm, ";
        }

        public static bool KiemtraTrung(List<string> list)
        {
            bool flag = false;
            for (int i = 0; i < list.Count; ++i)
            {
                for (int j = i + 1; j < list.Count; ++j)
                {
                    if (list.ElementAt(i) == list.ElementAt(j))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }

        public static void LocDuLieuTrung(List<string> list)
        {
            int flag;
            for (int j = 0; j < list.Count; ++j)
            {
                for (int i = j + 1; i < list.Count; ++i)
                {
                    flag = 0;
                    if (list.ElementAt(i) == list.ElementAt(j))
                    {
                        flag = 1;
                    }
                    if (flag == 1)
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }

        private void bthienthi_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
            ResetValuesHDN();
            ResetValuesHDNCT();
            btnBoqua.Enabled = false;
            btnThemSP.Enabled = false;
            btnhienthi.Enabled = false;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text).Rows.Count == 1)
            {
                DataRow dr = BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text).Rows[0];

                if (cbMaNhanVien.Text.Trim() != dr[1].ToString() || cbmanhacungung.Text.Trim() != dr[2].ToString() || txtdathanhtoan.Text.Trim() != BUS_HDB.FormatNumber(dr[6].ToString()))
                {
                    if (MessageBox.Show("Đã phát hiện thay đổi về số tiền đã thanh toán so với dữ liệu mà bạn đã lưu trước đó. Nấu Bạn muốn thay đổi số tiền đã thanh toán, hãy nhấn nút 'Sửa HD' để cập nhật lại thay đổi, ngược lại hệ thống sẽ in với dữ liệu trong lần cập nhật gần nhất!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btnSuaHD.Focus();
                        return;
                    }
                    else
                    {
                        //Không muốn cập nhật thay đổi, in với giá trị đã lưu trước đó.
                        //print
                        hdnprint = true;
                        mahdn = txtMaHoaDon.Text;
                        PRINTPREVIEW pp = new PRINTPREVIEW();
                        pp.ShowDialog();
                    }
                }
                else
                {
                    //Các thông tin đã trùng khớp và hợp lệ.

                    //print
                    hdnprint = true;
                    mahdn = txtMaHoaDon.Text;
                    PRINTPREVIEW pp = new PRINTPREVIEW();
                    pp.ShowDialog();
                }
            }

        }

        private void cbMaNhanVien_Leave(object sender, EventArgs e)
        {
            if (cbMaNhanVien.Text.Trim().Length != 0)
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
                            if (cbMaNhanVien.Text.Trim() == dr[0].ToString())
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
                            cbMaNhanVien.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void cbMaNCU_Leave(object sender, EventArgs e)
        {
            if (cbmanhacungung.Text.Trim().Length != 0)
            {
                DataTable dt = BUS_NCU.hienthiNCU();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        bool flag = false;
                        DataRow dr;
                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            dr = dt.Rows[i];
                            if (cbmanhacungung.Text.Trim() == dr[0].ToString())
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
                            MessageBox.Show("Mã nhà cung ứng không tồn tại. Hãy kiểm tra lại và chọn mã khác!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cbmanhacungung.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void txtSoLuong_Leave(object sender, EventArgs e)
        {
            if (txtSoLuongnhap.Text.Trim() != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoLuongnhap.Focus();
                }
                else
                {
                    if (a <= 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải > 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSoLuongnhap.Focus();
                    }
                    else
                    {
                        txtSoLuongnhap.Text = BUS_HDB.FormatNumber(a.ToString());
                    }
                }
            }
        }

        private void DGVHoaDonNhap_Click(object sender, EventArgs e)
        {
            if (DGVHoaDonNhap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DGVHoaDonNhap.CurrentRow.Index != DGVHoaDonNhap.NewRowIndex)
            {
                btnThemSP.Enabled = false;
                btnSuaSP.Enabled = true;
                btnXoaSP.Enabled = true;
                txtdongia.ReadOnly = false;
                txtgiamgia.ReadOnly = false;

                txtMaHoaDon.Text = DGVHoaDonNhap.CurrentRow.Cells["IdHDN"].Value.ToString();
                txtmasp.Text = DGVHoaDonNhap.CurrentRow.Cells["IdSP"].Value.ToString();
                txtSoLuongnhap.Text = DGVHoaDonNhap.CurrentRow.Cells["SL"].Value.ToString();
                txtdongia.Text = DGVHoaDonNhap.CurrentRow.Cells["Dongianhap"].Value.ToString();
                txtgiamgia.Text = DGVHoaDonNhap.CurrentRow.Cells["Giamgia"].Value.ToString();
                txtthanhtien.Text = DGVHoaDonNhap.CurrentRow.Cells["TT"].Value.ToString();

                DataTable dt = BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    dr = dt.Rows[0];
                    cbMaNhanVien.Text = dr[1].ToString();
                    cbmanhacungung.Text = dr[2].ToString();
                    dtpNgayLapHD.Text = dr[3].ToString();
                    txttongsoluong.Text = dr[4].ToString();
                    txtTongTien.Text = dr[5].ToString();
                    txtdathanhtoan.Text = dr[6].ToString();
                    txtConno.Text = dr[7].ToString();
                    dtpNgayHHTT.Text = dr[8].ToString();

                    dt = BUS_HDB.LayTenNV(cbMaNhanVien.Text.Trim());
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        txtTenNhanVien.Text = dr[0].ToString();
                    }

                    dt = BUS_HDN.LayTTSPTheoTenSP(cbtensp.Text);
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        txtmasp.Text = dr[0].ToString();
                        txthangsx.Text = dr[1].ToString();
                    }

                    dt = BUS_HDB.LayTTSP(txtmasp.Text);
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        cbtensp.Text = dr[0].ToString();
                    }

                    dt = BUS_HDN.LayTTNCU(cbmanhacungung.Text.Trim());
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        txtTenNCU.Text = dr[0].ToString();
                        txtDiaChi.Text = dr[1].ToString();
                        txtDienThoai.Text = dr[2].ToString();
                    }
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn bản ghi có thông tin!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtdathanhtoan_Leave(object sender, EventArgs e)
        {
            if (txtdathanhtoan.Text.Trim() != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtdathanhtoan.Focus();
                }
                else
                {
                    if (a <= 0 && txtdathanhtoan.ReadOnly == false)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải > 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtdathanhtoan.Focus();
                    }
                    else
                    {
                        if (txtTongTien.Text != string.Empty && txtTongTien.Text != "0")
                        {
                            string tt = BUS_HDB.ConvertToFloatType(txtTongTien.Text);
                            if (a >= Int64.Parse(tt))
                            {
                                MessageBox.Show("Giá trị hợp lệ phải nhỏ hơn hoặc bằng tổng giá trị của hoá đơn!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtdathanhtoan.Focus();
                            }
                            else
                            {
                                txtdathanhtoan.Text = BUS_HDB.FormatNumber(a.ToString());
                            }

                        }
                        else
                        {
                            txtdathanhtoan.Text = BUS_HDB.FormatNumber(a.ToString());
                        }
                    }
                }
            }
        }

        private void cbMaNhanVien_TextChanged(object sender, EventArgs e)
        {
            if (cbMaNhanVien.Text.Trim().Length > 0)
            {
                DataTable dt = BUS_HDB.LayTenNV(cbMaNhanVien.Text);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txtTenNhanVien.Text = dr[0].ToString();
                }
                else
                {
                    txtTenNhanVien.Text = string.Empty;
                }
            }
            else
            {
                txtTenNhanVien.Text = string.Empty;
            }
        }

        private void cbMaNCU_TextChanged(object sender, EventArgs e)
        {
            if (cbmanhacungung.Text.Trim().Length > 0)
            {
                DataTable dt = BUS_HDN.LayTTNCU(cbmanhacungung.Text);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txtTenNCU.Text = dr[0].ToString();
                    txtDiaChi.Text = dr[1].ToString();
                    txtDienThoai.Text = dr[2].ToString();
                }
                else
                {
                    txtTenNCU.Text = string.Empty;
                    txtDiaChi.Text = string.Empty;
                    txtDienThoai.Text = string.Empty;
                }
            }
            else
            {
                txtTenNCU.Text = string.Empty;
                txtDiaChi.Text = string.Empty;
                txtDienThoai.Text = string.Empty;
            }
        }

        private void cbMaSanPham_TextChanged(object sender, EventArgs e)
        {
            if (cbtensp.Text.Trim().Length > 0)
            {
                DataTable dt = BUS_HDN.LayTTSPTheoTenSP(cbtensp.Text);

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txtmasp.Text = dr[0].ToString();
                    txthangsx.Text = dr[1].ToString();
                    dt = BUS_HDB.LayTTSP(txtmasp.Text);
                    if(dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        txtdongia.Text = dr[4].ToString();
                    }
                }
                else
                {
                    txtmasp.Text = "Mã sẽ tự động thêm!";
                    txtgiamgia.Text = string.Empty;
                    txtSoLuongnhap.Text = string.Empty;
                    txtdongia.Text = string.Empty;
                    txtgiamgia.Text = string.Empty;
                    txtthanhtien.Text = string.Empty;
                    txthangsx.Text = string.Empty;
                }
            }
            else
            {
                txtmasp.Text = "Mã sẽ tự động thêm!";
                txthangsx.Text = string.Empty;
                txtSoLuongnhap.Text = string.Empty;
                txtdongia.Text = string.Empty;
                txtgiamgia.Text = string.Empty;
                txtthanhtien.Text = string.Empty;
                txthangsx.Text = string.Empty;
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuongnhap.Text.Trim() != "")
            {
                Int64 a = 0;
                if (Int64.TryParse(BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), out a))
                {
                    if (a <= 0)
                    {
                        txtthanhtien.Text = string.Empty;
                    }
                    else
                    {
                        Int64 dongia = 0, giamgia = 0;
                        if (Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdongia.Text), out dongia) && Int64.TryParse(BUS_HDB.ConvertToFloatType(txtgiamgia.Text), out giamgia))
                        {
                            txtthanhtien.Text = ((dongia - ((dongia * giamgia) / 100)) * a).ToString();

                        }
                        else
                        {
                            txtthanhtien.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    txtthanhtien.Text = string.Empty;
                }
            }
            else
            {
                txtthanhtien.Text = string.Empty;
            }
        }

        private void txtdathanhtoan_TextChanged(object sender, EventArgs e)
        {
            if (txtdathanhtoan.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text.Trim()), out a))
                {
                    txtConno.Text = txtTongTien.Text;
                }
                else
                {
                    if (a <= 0)
                    {
                        txtConno.Text = txtTongTien.Text;
                    }

                    if (txtTongTien.Text != string.Empty && txtTongTien.Text != "0")
                    {
                        if (a > Int64.Parse(BUS_HDB.ConvertToFloatType(txtTongTien.Text)))
                            txtConno.Text = "0";
                        else
                            txtConno.Text = (Int64.Parse(BUS_HDB.ConvertToFloatType(txtTongTien.Text)) - Int64.Parse(BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text))).ToString();

                    }
                    else
                    {
                        txtConno.Text = txtTongTien.Text;
                    }
                }
            }
            else
            {
                txtConno.Text = txtTongTien.Text;
            }
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (cbtensp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbtensp.Focus();
                return;
            }
            if (txthangsx.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập hãng sản xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txthangsx.Focus();
                return;
            }
            if (txtSoLuongnhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongnhap.Focus();
                return;
            }
            //Kiểm tra sản phẩm đã được thêm chưa
            DataRow dr;
            DataTable dt = BUS_HDN.LayTTSPTheoTenSP(cbtensp.Text.Trim());
            if (dt.Rows.Count == 1)
            {
                //Kiểm tra sản phẩm đã tồn tại trong bảng sản phẩm chưa
                dr = dt.Rows[0];
                txtmasp.Text = dr[0].ToString();
                if (!BUS_HDNCT.ktHDNCTtrung(txtMaHoaDon.Text, txtmasp.Text))
                {
                    MessageBox.Show("Sản phẩm này đã được thêm, bạn hãy chọn sản phẩm khác. Nếu muốn chỉnh sửa lại số lượng sản phẩm, bạn hãy chọn sản phẩm ở bảng bên dưới, chỉnh sửa số lượng và nhấn 'Sửa SP'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbtensp.Focus();
                    return;
                }

                //Thêm trên bảng hoá đơn bán chi tiết
                DTO_HDNCT hdnct = new DTO_HDNCT(txtMaHoaDon.Text, txtmasp.Text, BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), BUS_HDB.ConvertToFloatType(txtdongia.Text), txtgiamgia.Text, BUS_HDB.ConvertToFloatType(txtthanhtien.Text));

                BUS_HDNCT.themHDNCT(hdnct);

                //Thêm phần tử cho list mã sản phẩm để cập nhật số lượng.
                idspcu.Add(txtmasp.Text);
            }
            else
            {
                //tạo mã ngẫu nhiên
                int value;
                bool kt = false;
                Random rand = new Random();
                value = rand.Next(100000000, 999999999);
                string masp = "SP" + value;
                if (BUS_SP.hienthisp().Rows.Count > 0)
                {
                    while (kt == false)
                    {
                        for (int i = 0; i < BUS_SP.hienthisp().Rows.Count; ++i)
                        {
                            dr = BUS_SP.hienthisp().Rows[i];
                            if (masp == dr["IdSP"].ToString())
                            {
                                kt = false;
                                value = rand.Next(100000000, 999999999);
                                masp = "SP" + value;
                                break;
                            }
                            else
                            {
                                kt = true;
                            }
                        }
                    }
                }

                if (!BUS_SP.ktSPtrung(masp))
                {
                    MessageBox.Show("Mã hàng này đã tồn tại, bạn phải chọn mã hàng khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (!BUS_HDNCT.ktHDNCTtrung(txtMaHoaDon.Text, masp))
                {
                    MessageBox.Show("Sản phẩm này đã được thêm, bạn hãy chọn sản phẩm khác. Nếu muốn chỉnh sửa lại số lượng sản phẩm, bạn hãy chọn sản phẩm ở bảng bên dưới, chỉnh sửa số lượng và nhấn 'Sửa SP'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbtensp.Focus();
                    return;
                }

                //Thêm sản phẩm vào bảng sản phẩm trước

                DTO_SP sp = new DTO_SP(masp, cbmanhacungung.Text, cbtensp.Text, DateTime.Now.ToShortDateString
                    (), DateTime.Now.ToShortDateString
                    (), DateTime.Now.ToShortDateString
                    (), txthangsx.Text, BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), "0", "0", "0", "Chưa nhập đơn vị", "0", "Chưa chọn ảnh minh họa");
                BUS_SP.themSP(sp);

                //Thêm trên bảng hoá đơn bán chi tiết
                DTO_HDNCT hdnct = new DTO_HDNCT(txtMaHoaDon.Text, masp, BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), BUS_HDB.ConvertToFloatType(txtdongia.Text), txtgiamgia.Text, BUS_HDB.ConvertToFloatType(txtthanhtien.Text));

                BUS_HDNCT.themHDNCT(hdnct);

                idsapmoi.Add(masp);
            }

            //Cập nhật lại sản phẩm vừa thêm lên combobox tên sản phẩm.
            BUS_HDB.FillComboMaSP(cbtensp, "TenSP", "TenSP");
            cbtensp.SelectedIndex = -1;

            LoadHDNCTHT();
            ResetValuesHDNCT();

            DANGNHAP.thaotac += "Thêm sản phẩm, ";
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (DGVHoaDonNhap.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmasp.Text == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào. Hãy click chọn thông tin bên dưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                //Xóa sản phẩm nếu là sản phẩm mới

                //Xóa sản phẩm trên các bảng dữ liệu có liên quan
                bool flag = false;
                if(idsapmoi.Count > 0)
                {
                    for(int i = 0; i < idsapmoi.Count; ++i)
                    {
                        if (idsapmoi[i] == txtmasp.Text)
                        {
                            flag = true;
                            break;
                        }
                    }

                }

                if (flag)
                {
                    BUS_SP.RunDelSQLOnHTCT(txtmasp.Text);
                    BUS_SP.RunDelSQLOnHDBCT(txtmasp.Text);
                    BUS_HDNCT.RunDelSQLHDNCT(DGVHoaDonNhap.CurrentRow.Cells[0].Value.ToString(), DGVHoaDonNhap.CurrentRow.Cells[1].Value.ToString());
                    BUS_SP.RunDelSQL(txtmasp.Text);
                }
                else
                {
                    BUS_HDNCT.RunDelSQLHDNCT(DGVHoaDonNhap.CurrentRow.Cells[0].Value.ToString(), DGVHoaDonNhap.CurrentRow.Cells[1].Value.ToString());
                }

                //Cập nhật lại  tổng số lượng sản phẩm và tổng tiền trên hóa đơn bán
                BUS_HDN.CapNhatSLSPtrenHDN(txtMaHoaDon.Text);
                BUS_HDN.CapNhatTTtrenHDN(txtMaHoaDon.Text);

                //hien thi dữ liệu vừa cập nhật lên textbox
                DataTable dt = BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        txttongsoluong.Text = BUS_HDB.FormatNumber(dr[4].ToString());
                        txtTongTien.Text = BUS_HDB.FormatNumber(dr[5].ToString());

                        //Cập nhật nợ trên hóa đơn nhập
                        BUS_HDN.CapNhatNotrenHDN(txtMaHoaDon.Text, BUS_HDB.ConvertToFloatType(txtConno.Text));

                    }
                }
                //Cập nhật lại nợ nhà cung ứng
                dt = BUS_NCU.hienthiNCUcuthe(cbmanhacungung.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        BUS_HDN.CapNhatnoNCU(cbmanhacungung.Text);
                    }
                }

                //Xóa mã sản phẩm ra khỏi list
                idspcu.Remove(txtmasp.Text);
                idsapmoi.Remove(txtmasp.Text);

                LoadHDNCTHT();
                ResetValuesHDNCT();
                DANGNHAP.thaotac += "Xóa sản phẩm, ";
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text).Rows.Count == 1)
            {
                DataRow dr = BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text).Rows[0];
                DataTable dt = BUS_HDNCT.hienthiHDNCTcuthe(txtMaHoaDon.Text);
                if (cbMaNhanVien.Text.Trim() != dr[1].ToString() || cbmanhacungung.Text.Trim() != dr[2].ToString() || txtdathanhtoan.Text.Trim() != BUS_HDB.FormatNumber(dr[6].ToString()))
                {
                    if (MessageBox.Show("Đã phát hiện thay đổi so với dữ liệu mà bạn đã lưu trước đó. Nấu Bạn muốn in với thay đổi hiện tại hãy nhấn nút 'Sửa HD' để cập nhật lại thay đổi, ngược lại sẽ in với dữ liệu trong lần cập nhật gần nhất", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btnSuaHD.Focus();
                        return;
                    }
                    else
                    {
                        if (dt.Rows.Count == 0)
                        {
                            if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn đã xóa hết chi tiết hóa đơn. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                btnSuaHD.Focus();
                                return;
                            }
                            else
                            {
                                //Xoá hoá đơn nhập hiện tại
                                BUS_HDN.RunDelSQL(txtMaHoaDon.Text);

                                btnLamMoi.Enabled = false;
                                btnThemHD.Enabled = true;
                                btnTimKiem.Enabled = true;
                                btnhienthi.Enabled = false;
                                cbmaHD.Enabled = true;
                                btnThemSP.Enabled = false;
                                ResetValuesHDN();
                                ResetValuesHDNCT();
                                LoadDataGridView();

                                //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                                BUS_HDN.FillComboMaHD(cbmaHD, "IdHDN", "IdHDN");
                                cbmaHD.SelectedIndex = -1;
                            }

                        }
                        else
                        {
                            //Cập nhật lại số lượng cho những sản phẩm cũ
                            if(idspcu.Count > 0)
                            {
                                for (int i = 0; i < idspcu.Count; ++i)
                                {
                                    for(int j = 0; j < dt.Rows.Count; ++j)
                                    {
                                        dr = dt.Rows[j];
                                        if (idspcu[i] == dr[1].ToString())
                                        {
                                            DataTable table = BUS_SP.hienthiSPcuthe(idspcu[i]);
                                            if (table != null)
                                            {
                                                if (table.Rows.Count == 1)
                                                {
                                                    DataRow row = table.Rows[0];
                                                    string sl = row[7].ToString();
                                                    DTO_SP sp = new DTO_SP();
                                                    sp.Masp = idspcu[i];
                                                    sp.Slnhap = (Int64.Parse(sl) + Int64.Parse(dr[2].ToString())).ToString();

                                                    BUS_HDB.CapNhatSLSanPham(sp);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            btnLamMoi.Enabled = false;
                            btnThemHD.Enabled = true;
                            btnTimKiem.Enabled = true;
                            btnhienthi.Enabled = false;
                            cbmaHD.Enabled = true;
                            btnThemSP.Enabled = false;
                            ResetValuesHDN();
                            ResetValuesHDNCT();
                            LoadDataGridView();                        
                        }
                    }
                }
                else
                {
                    if (dt.Rows.Count == 0)
                    {
                        if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn chưa thêm sản phẩm nào. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnSuaHD.Focus();
                            return;
                        }
                        else
                        {
                            //Xoá hoá đơn bán hiện tại
                            BUS_HDN.RunDelSQL(txtMaHoaDon.Text);
                            btnLamMoi.Enabled = false;
                            btnThemHD.Enabled = true;
                            btnTimKiem.Enabled = true;
                            btnhienthi.Enabled = false;
                            cbmaHD.Enabled = true;
                            btnThemSP.Enabled = false;
                            ResetValuesHDN();
                            ResetValuesHDNCT();
                            LoadDataGridView();

                            //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                            BUS_HDN.FillComboMaHD(cbmaHD, "IdHDN", "IdHDN");
                            cbmaHD.SelectedIndex = -1;
                        }

                    }
                    else
                    {
                        //Cập nhật lại số lượng cho những sản phẩm cũ
                        if (idspcu.Count > 0)
                        {
                            for (int i = 0; i < idspcu.Count; ++i)
                            {
                                for (int j = 0; j < dt.Rows.Count; ++j)
                                {
                                    dr = dt.Rows[j];
                                    if (idspcu[i] == dr[1].ToString())
                                    {
                                        DataTable table = BUS_SP.hienthiSPcuthe(idspcu[i]);
                                        if (table != null)
                                        {
                                            if (table.Rows.Count == 1)
                                            {
                                                DataRow row = table.Rows[0];
                                                string sl = row[7].ToString();
                                                DTO_SP sp = new DTO_SP();
                                                sp.Masp = idspcu[i];
                                                sp.Slnhap = (Int64.Parse(sl) + Int64.Parse(dr[2].ToString())).ToString();

                                                BUS_HDB.CapNhatSLSanPham(sp);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                        btnLamMoi.Enabled = false;
                        btnThemHD.Enabled = true;
                        btnTimKiem.Enabled = true;
                        btnhienthi.Enabled = false;
                        cbmaHD.Enabled = true;
                        btnThemSP.Enabled = false;
                        ResetValuesHDN();
                        ResetValuesHDNCT();
                        LoadDataGridView();
                    }
                }
            }
            else
            {
                btnLamMoi.Enabled = false;
                btnThemHD.Enabled = true;
                btnTimKiem.Enabled = true;
                btnhienthi.Enabled = false;
                cbmaHD.Enabled = true;
                btnThemSP.Enabled = false;
                ResetValuesHDN();
                ResetValuesHDNCT();
                LoadDataGridView();
            }
        }


        private void cbmaHD_TextChanged(object sender, EventArgs e)
        {
            if (cbmaHD.Text.Trim() != string.Empty)
            {
                bool flag = true;
                for (int i = 0; i < cbmaHD.Items.Count; ++i)
                {
                    if (cbmaHD.Text.Trim() == cbmaHD.Items[i].ToString())
                    {
                        flag = true;
                        break;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (flag == true)
                {
                    txtMaHoaDon.Text = cbmaHD.Text;
                    DataTable dt = BUS_HDN.hienthiHDNcuthe(cbmaHD.Text.Trim());
                    if (dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        cbMaNhanVien.Text = dr[1].ToString();
                        cbmanhacungung.Text = dr[2].ToString();
                        dtpNgayLapHD.Text = dr[3].ToString();
                        txttongsoluong.Text = dr[4].ToString();
                        txtTongTien.Text = dr[5].ToString();
                        txtdathanhtoan.Text = dr[6].ToString();
                        txtConno.Text = dr[7].ToString();
                        dtpNgayHHTT.Text = dr[8].ToString();

                        dt = BUS_HDB.LayTenNV(cbMaNhanVien.Text);
                        if (dt.Rows.Count == 1)
                        {
                            dr = dt.Rows[0];
                            txtTenNhanVien.Text = dr[0].ToString();
                        }

                        dt = BUS_HDN.LayTTNCU(cbmanhacungung.Text);
                        if (dt.Rows.Count == 1)
                        {
                            dr = dt.Rows[0];
                            txtTenNCU.Text = dr[0].ToString();
                            txtDiaChi.Text = dr[1].ToString();
                            txtDienThoai.Text = dr[2].ToString();
                        }
                        DGVHoaDonNhap.DataSource = BUS_HDNCT.hienthiHDNCTcuthe(txtMaHoaDon.Text);
                    }
                    else
                    {
                        ResetValuesHDN();
                        ResetValuesHDNCT();
                        DGVHoaDonNhap.DataSource = BUS_HDNCT.hienthiHDNCTcuthe(txtMaHoaDon.Text);
                    }
                }
                else
                {
                    ResetValuesHDN();
                    ResetValuesHDNCT();
                    DGVHoaDonNhap.DataSource = BUS_HDNCT.hienthiHDNCTcuthe(txtMaHoaDon.Text);
                }

            }
            else
            {
                ResetValuesHDN();
                ResetValuesHDNCT();
                DGVHoaDonNhap.DataSource = BUS_HDNCT.hienthiHDNCTcuthe(txtMaHoaDon.Text);
            }
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            if (txtTongTien.Text.Trim() != string.Empty)
            {

                if (txtdathanhtoan.Text.Trim() != string.Empty && txtdathanhtoan.Text.Trim() != "0")
                {
                    string tt = BUS_HDB.ConvertToFloatType(txtTongTien.Text);
                    txtConno.Text = (Int64.Parse(tt) - Int64.Parse(BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text.Trim()))).ToString();
                }
                else
                {
                    txtConno.Text = txtTongTien.Text;
                }

                txtTongTien.Text = BUS_HDB.FormatNumber(txtTongTien.Text);
            }
            else
            {
                txtdathanhtoan.Text = string.Empty;
                txtConno.Text = txtTongTien.Text;
            }
        }

        private void txtconno_TextChanged(object sender, EventArgs e)
        {
            string cn = BUS_HDB.ConvertToFloatType(txtConno.Text);
            txtConno.Text = BUS_HDB.FormatNumber(cn);
        }

        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            string thanhtien = BUS_HDB.ConvertToFloatType(txtthanhtien.Text);
            txtthanhtien.Text = BUS_HDB.FormatNumber(thanhtien);
        }

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text == "Mã sẽ tự động thêm!")
            {
                btnprint.Enabled = false;
            }
            else
            {
                btnprint.Enabled = true;
            }
        }

        private void cbmaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbmaHD_TextChanged(sender, e);
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtdongia.Text.Trim() != "")
            {
                Int64 a = 0;
                if (Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdongia.Text), out a))
                {
                    if (a <= 0)
                    {
                        txtthanhtien.Text = string.Empty;
                    }
                    else
                    {
                        Int64 sl = 0, giamgia = 0;
                        if (Int64.TryParse(BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), out sl) && Int64.TryParse(BUS_HDB.ConvertToFloatType(txtgiamgia.Text), out giamgia))
                        {
                            txtthanhtien.Text = ((a - ((a * giamgia) / 100)) * sl).ToString();

                        }
                        else
                        {
                            txtthanhtien.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    txtthanhtien.Text = string.Empty;
                }
            }
            else
            {
                txtthanhtien.Text = string.Empty;
            }
        }

        private void txttongsoluong_TextChanged(object sender, EventArgs e)
        {
            string tongsoluong = BUS_HDB.ConvertToFloatType(txttongsoluong.Text);
            txttongsoluong.Text = BUS_HDB.FormatNumber(tongsoluong);
        }

        private void txtdongia_Leave(object sender, EventArgs e)
        {
            if (txtdongia.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdongia.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtdongia.Focus();
                }
                else
                {
                    if (a <= 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải > 0 !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtdongia.Focus();
                    }
                    else
                    {
                        txtdongia.Text = BUS_HDB.FormatNumber(a.ToString());
                    }
                }
            }
        }

        private void txtgiamgia_Leave(object sender, EventArgs e)
        {
            if (txtgiamgia.Text != "")
            {
                int a = 0;
                if (!int.TryParse(txtgiamgia.Text, out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtgiamgia.Focus();
                }
                else
                {
                    if (a < 0 || a > 100)
                    {
                        MessageBox.Show("Giá trị hợp lệ là 0 -> 100!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtgiamgia.Focus();
                    }
                    else
                    {
                        txtgiamgia.Text = BUS_HDB.FormatNumber(txtgiamgia.Text);
                    }
                }
            }
        }

        private void txtgiamgia_TextChanged(object sender, EventArgs e)
        {
            if (txtgiamgia.Text.Trim() != "")
            {
                int a = 0;
                if (int.TryParse(BUS_HDB.ConvertToFloatType(txtgiamgia.Text), out a))
                {
                    if (a < 0)
                    {
                        txtthanhtien.Text = string.Empty;
                    }
                    else
                    {
                        if(a <= 100)
                        {
                            Int64 dongia = 0, sl = 0;
                            if (Int64.TryParse(BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), out sl) && Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdongia.Text), out dongia))
                            {
                                txtthanhtien.Text = ((dongia - ((dongia * a) / 100)) * sl).ToString();

                            }
                            else
                            {
                                txtthanhtien.Text = string.Empty;
                            }
                        }
                        else
                        {
                            txtthanhtien.Text = string.Empty;
                        }
                    }
                }
                else
                {
                    txtthanhtien.Text = string.Empty;
                }
            }
            else
            {
                txtthanhtien.Text = string.Empty;
            }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            if (DGVHoaDonNhap.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtmasp.Text == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào. Hãy clik chọn thông tin bên dưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbtensp.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbtensp.Focus();
                return;
            }
            if (txtSoLuongnhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuongnhap.Focus();
                return;
            }
            if (txthangsx.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập hãng sản xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txthangsx.Focus();
                return;
            }

            //Kiểm tra sản phẩm đã được thêm chưa
            if (txtmasp.Text != DGVHoaDonNhap.CurrentRow.Cells["IdSP"].Value.ToString())
            {
                //Kiểm tra sản phẩm đã được thêm chưa
                if (!BUS_HDNCT.ktHDNCTtrung(txtMaHoaDon.Text, DGVHoaDonNhap.CurrentRow.Cells["IdSP"].Value.ToString()))
                {
                    MessageBox.Show("Sản phẩm này đã được thêm, bạn hãy chọn sản phẩm khác. Nếu muốn chỉnh sửa lại số lượng sản phẩm, bạn hãy chọn sản phẩm ở bảng bên dưới, chỉnh sửa số lượng và nhấn 'Sửa SP'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbtensp.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("Sản phẩm này chưa được thêm, bạn hãy thêm sản phẩm để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbtensp.Focus();
                    return;
                }
            }

            DTO_HDNCT hdnct = new DTO_HDNCT(txtMaHoaDon.Text, txtmasp.Text, BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), BUS_HDB.ConvertToFloatType(txtdongia.Text), txtgiamgia.Text, BUS_HDB.ConvertToFloatType(txtthanhtien.Text));

            BUS_HDNCT.suaHDNCT(hdnct);

            //Cap nhat no, tong so luong va tong tien
            BUS_HDN.CapNhatSLSPtrenHDN(txtMaHoaDon.Text);
            BUS_HDN.CapNhatTTtrenHDN(txtMaHoaDon.Text);

            //hien thi dữ liệu vừa cập nhật lên textbox
            DataTable dt = BUS_HDN.hienthiHDNcuthe(txtMaHoaDon.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txttongsoluong.Text = dr[4].ToString();
                    txtTongTien.Text = dr[5].ToString();
                    //Cập nhật nợ trên hóa đơn nhập
                    BUS_HDN.CapNhatNotrenHDN(txtMaHoaDon.Text, BUS_HDB.ConvertToFloatType(txtConno.Text));
                }
            }

            //Cập nhật lại nợ nhà cung ứng
            dt = BUS_NCU.hienthiNCUcuthe(cbmanhacungung.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    BUS_HDN.CapNhatnoNCU(cbmanhacungung.Text);
                }
            }


            //Cập nhật lại thông tin trên bảng sản phẩm đối với những sản phẩm mới chưa từng được thêm trước kia
            if(idsapmoi.Count > 0)
            {
                for(int i = 0; i < idsapmoi.Count; ++i)
                {
                    if(idsapmoi[i] == txtmasp.Text)
                    {
                        BUS_HDN.CapNhatTTtrenSP(txtmasp.Text, txthangsx.Text, BUS_HDB.ConvertToFloatType(txtSoLuongnhap.Text), BUS_HDB.ConvertToFloatType(txtdongia.Text));
                        break;
                    }
                }
            }
            else
            {
                //Không cập nhật số lượng vì hóa đơn đã được làm mới
                dt = BUS_SP.hienthiSPcuthe(txtmasp.Text);
                DataRow dr = dt.Rows[0];
                BUS_HDN.CapNhatTTtrenSP(txtmasp.Text, txthangsx.Text, dr[7].ToString(), BUS_HDB.ConvertToFloatType(txtdongia.Text));
            }
            LoadHDNCTHT();
            ResetValuesHDNCT();
            DANGNHAP.thaotac += "Sửa thông tin sản phẩm, ";
        }
    }
}
