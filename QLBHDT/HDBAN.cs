using System;
using System.Data;
using System.Windows.Forms;
using DTO;
using BUS;
using System.Collections.Generic;
using System.Linq;

namespace QLBHDT
{
    public partial class HDBAN : Form
    {
        public HDBAN()
        {
            InitializeComponent();
        }

        public DataTable hdbct, hdbctht;
        private string lbl;
        public static bool hdbprint;
        public static string mahdb;

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

        private void HDBAN_Load(object sender, EventArgs e)
        {
            lbl = lblkqtkhdb.Text;
            btnundo.Enabled = false;
            btnredo.Enabled = false;

            //Đỗ dữ liệu từ các bảng lên các combobox tương ứng
            BUS_HDB.FillComboMaHD(cbmaHD, "IdHDB", "IdHDB");
            cbmaHD.SelectedIndex = -1;

            BUS_HDB.FillComboMaNV(cbMaNhanVien, "IdNV", "IdNV");
            cbMaNhanVien.SelectedIndex = -1;

            BUS_HDB.FillComboMaKH(cbMaKhachHang, "IdKH", "IdKH");
            cbMaKhachHang.SelectedIndex = -1;

            BUS_HDB.FillComboMaSP(cbMaSanPham, "IdSP", "IdSP");
            cbMaSanPham.SelectedIndex = -1;

            grbTTmathang.Enabled = false;
            btnhienthi.Enabled = false;

            //QLTC
            btnThemHD.Enabled = false;
            btnSuaHD.Enabled = false;
            btnXoaHD.Enabled = false;
            btnprint.Enabled = false;
            if (MAIN.tkhdb == false)
            {
                string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
                string[] quyenhan = DANGNHAP.Quyen.Split('|');

                for (int j = 0; j < danhmuctmp.Length; ++j)
                {
                    if (danhmuctmp[j].Trim() == "Quản lý hoá đơn")
                    {
                        string[] hdb;
                        hdb = quyenhan[j].Split(';');
                        if (hdb != null)
                        {
                            foreach (string items in hdb)
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
                MAIN.tkhdb = false;
            }
        }

        private void ResetValuesHDB()
        {
            txtMaHoaDon.Text = "Mã sẽ tự động thêm!";
            dtpNgayLapHD.Value = DateTime.Now;
            txtTenNhanVien.Text = string.Empty;
            cbMaNhanVien.Text = string.Empty;
            cbMaKhachHang.Text = string.Empty;
            txtTenKhachHang.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtDienThoai.Text = string.Empty;
            cbHinhthuctt.Text = string.Empty;
            txtdathanhtoan.Text = string.Empty;
            txtconno.Text = string.Empty;
            txtTongTien.Text = string.Empty;
            txttongsoluong.Text = string.Empty;

            lblkqtkhdb.Text = lbl;
        }

        private void ResetValuesHDBCT()
        {
            cbMaSanPham.Text = string.Empty;
            txtTenSanPham.Text = string.Empty;
            txtSoLuong.Text = string.Empty;
            txtDonGia.Text = string.Empty;
            txtGiamGia.Text = string.Empty;
            txtThanhTien.Text = string.Empty;
            btnThemSP.Enabled = true;
            btnSuaSP.Enabled = false;
            btnXoaSP.Enabled = false;
            btnBoqua.Enabled = true;
        }

        private void LoadDataGridView()
        {
            hdbct = BUS_HDBCT.hienthiHDBCT();
            DGVHoaDonBan.DataSource = hdbct;
            DGVHoaDonBan.AllowUserToAddRows = false;
            DGVHoaDonBan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void LoadHDBCTHT()
        {
            hdbctht = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
            DGVHoaDonBan.DataSource = hdbctht;
            DGVHoaDonBan.AllowUserToAddRows = false;
            DGVHoaDonBan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaNhanVien.Focus();
                return;
            }
            if (cbMaKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaKhachHang.Focus();
                return;
            }

            //tạo mã ngẫu nhiên
            int value;
            bool kt = false;
            Random rand = new Random();
            value = rand.Next(100000000, 999999999);
            string mahdb = "HDB" + value;
            DataRow dr;
            if (BUS_HDB.hienthiHDB().Rows.Count > 0)
            {
                while (kt == false)
                {
                    for (int i = 0; i < BUS_HDB.hienthiHDB().Rows.Count; ++i)
                    {
                        dr = BUS_HDB.hienthiHDB().Rows[i];
                        if (mahdb == dr["IdHDB"].ToString())
                        {
                            kt = false;
                            value = rand.Next(100000000, 999999999);
                            mahdb = "HDB" + value;
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
            if (!BUS_HDB.ktHDBtrung(mahdb))
            {
                MessageBox.Show("Mã hoá đơn bán này đã tồn tại, bạn hãy nhấn nút 'Thêm' lần nữa để chọn mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHoaDon.Focus();
                return;
            }

            //Thêm trên bảng hoá đơn bán
            DTO_HDB hdb = new DTO_HDB(mahdb, cbMaNhanVien.Text, cbMaKhachHang.Text, DateTime.Now.ToString(), "0", "0", "", "0", "0");
            BUS_HDB.themHDB(hdb);

            //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
            BUS_HDB.FillComboMaHD(cbmaHD, "IdHDB", "IdHDB");
            cbmaHD.SelectedIndex = -1;

            btnThemHD.Enabled = false;
            btnLamMoi.Enabled = true;
            btnhienthi.Enabled = false;
            btnTimKiem.Enabled = false;
            cbmaHD.Enabled = false;
            grbTTmathang.Enabled = true;
            cbMaSanPham.Focus();

            ResetValuesHDB();
            ResetValuesHDBCT();
            //Hiển thị mã hiện tại lên control phục vụ việc thêm sản phẩm
            txtMaHoaDon.Text = mahdb;
            DataTable dt = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text);
            //Cập nhật lại datasource
            LoadHDBCTHT();
            if (dt.Rows.Count == 1)
            {
                dr = dt.Rows[0];
                cbMaNhanVien.Text = dr[1].ToString();
                cbMaKhachHang.Text = dr[2].ToString();
                dtpNgayLapHD.Text = dr[3].ToString();
                txttongsoluong.Text = dr[4].ToString();
                txtTongTien.Text = dr[5].ToString();
                cbHinhthuctt.Text = dr[6].ToString();
                txtdathanhtoan.Text = dr[7].ToString();
                txtconno.Text = dr[8].ToString();

                dt = BUS_HDB.LayTenNV(cbMaNhanVien.Text);
                if (dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    txtTenNhanVien.Text = dr[0].ToString();
                }

                dt = BUS_HDB.LayTTKH(cbMaKhachHang.Text);
                if (dt.Rows.Count == 1)
                {
                    dr = dt.Rows[0];
                    txtTenKhachHang.Text = dr[0].ToString();
                    txtDiaChi.Text = dr[1].ToString();
                    txtDienThoai.Text = dr[2].ToString();
                }
            }

            DANGNHAP.thaotac += "Thêm, ";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaHoaDon.Text == "Mã sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xoá hoá đơn bán sẽ xoá tất cả các thông tin về chi tiết của hoá đơn hiện tại. Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                BUS_HDB.RunDelSQLOnHDBCT(txtMaHoaDon.Text);
                BUS_HDB.RunDelSQL(txtMaHoaDon.Text);

                //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                BUS_HDB.FillComboMaHD(cbmaHD, "IdHDB", "IdHDB");
                cbmaHD.SelectedIndex = -1;

                ResetValuesHDB();
                ResetValuesHDBCT();
                if (btnThemHD.Enabled == false)
                {
                    cbmaHD.Enabled = true;
                }
                btnThemSP.Enabled = false;
                btnBoqua.Enabled = false;
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
            if (cbMaKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaKhachHang.Focus();
                return;
            }
            if (cbHinhthuctt.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập hình thức thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbHinhthuctt.Focus();
                return;
            }

            DTO_HDB hdb = new DTO_HDB(txtMaHoaDon.Text, cbMaNhanVien.Text, cbMaKhachHang.Text, DateTime.Now.ToString(), BUS_HDB.ConvertToFloatType(txttongsoluong.Text), BUS_HDB.ConvertToFloatType(txtTongTien.Text), cbHinhthuctt.Text, BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text), BUS_HDB.ConvertToFloatType(txtconno.Text));
            BUS_HDB.suaHDB(hdb);

            //Cập nhật lại nợ của khách hàng
            DataTable dt = BUS_KH.hienthikhcuthe(cbMaKhachHang.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    BUS_HDB.CapNhatNoKH(cbMaKhachHang.Text);
                }
            }

            DANGNHAP.thaotac += "Sửa, ";
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            if (btnLamMoi.Enabled == false)
            {
                ResetValuesHDBCT();
                btnThemSP.Enabled = false;
            }
            else
            {
                ResetValuesHDBCT();
            }
        }

        private void btnundo_Click(object sender, EventArgs e)
        {

        }

        private void btnredo_Click(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text).Rows.Count == 1)
            {
                DataRow dr = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text).Rows[0];
                if (cbMaNhanVien.Text.Trim() != dr[1].ToString() || cbMaKhachHang.Text.Trim() != dr[2].ToString() || txtdathanhtoan.Text.Trim() != BUS_HDB.FormatNumber(dr[7].ToString()) || cbHinhthuctt.Text.Trim() != dr[6].ToString())
                {
                    if (MessageBox.Show("Đã phát hiện thay đổi so với dữ liệu mà bạn đã lưu trước đó. Nấu Bạn muốn in với thay đổi hiện tại hãy nhấn nút 'Sửa HD' để cập nhật lại thay đổi, ngược lại sẽ in với dữ liệu trong lần cập nhật gần nhất", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btnSuaHD.Focus();
                        return;
                    }
                    else
                    {
                        if (dr[5].ToString() == "0")
                        {
                            if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn đã xóa hết chi tiết hóa đơn. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                btnSuaHD.Focus();
                                return;
                            }
                            else
                            {
                                //Xoá những chi tiết hoá đơn nếu có
                                BUS_HDB.RunDelSQLOnHDBCT(txtMaHoaDon.Text);
                                //Xoá hoá đơn bán hiện tại
                                BUS_HDB.RunDelSQL(txtMaHoaDon.Text);
                            }

                        }
                        else
                        {
                            //Cập nhật lại số lượng sản phẩm
                            DataTable dt = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; ++i)
                                    {
                                        DataRow r = dt.Rows[i];
                                        DataTable table = BUS_SP.hienthiSPcuthe(r[1].ToString());
                                        if (table != null)
                                        {
                                            if (table.Rows.Count == 1)
                                            {
                                                DataRow row = table.Rows[0];
                                                string sl = row[7].ToString();
                                                DTO_SP sp = new DTO_SP();
                                                sp.Masp = r[1].ToString();
                                                sp.Slnhap = (Int64.Parse(sl) - Int64.Parse(r[2].ToString())).ToString();

                                                BUS_HDB.CapNhatSLSanPham(sp);
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
                    if (dr[5].ToString() == "0")
                    {
                        if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn chưa thêm sản phẩm nào. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnSuaHD.Focus();
                            return;
                        }
                        else
                        {
                            //Xoá những chi tiết hoá đơn nếu có
                            BUS_HDB.RunDelSQLOnHDBCT(txtMaHoaDon.Text);
                            //Xoá hoá đơn bán hiện tại
                            BUS_HDB.RunDelSQL(txtMaHoaDon.Text);
                        }

                    }
                    else
                    {
                        //Cập nhật lại số lượng sản phẩm
                        DataTable dt = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; ++i)
                                {
                                    DataRow r = dt.Rows[i];
                                    DataTable table = BUS_SP.hienthiSPcuthe(r[1].ToString());
                                    if (table != null)
                                    {
                                        if (table.Rows.Count == 1)
                                        {
                                            DataRow row = table.Rows[0];
                                            string sl = row[7].ToString();
                                            DTO_SP sp = new DTO_SP();
                                            sp.Masp = r[1].ToString();
                                            sp.Slnhap = (Int64.Parse(sl) - Int64.Parse(r[2].ToString())).ToString();

                                            BUS_HDB.CapNhatSLSanPham(sp);
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
            hdbct = BUS_HDB.timkiemHDB(dtpNgayLapHD.Value.Day.ToString(), dtpNgayLapHD.Value.Month.ToString(), dtpNgayLapHD.Value.Year.ToString());

            if (hdbct.Rows.Count == 0)
            {
                lblkqtkhdb.Text = "Không có hóa đơn nào thoả mãn điều kiện tìm kiếm!";
                DGVHoaDonBan.DataSource = BUS_HDBCT.hienthiHDBCTcuthe("NULL");

            }
            else
            {
                DGVHoaDonBan.DataSource = hdbct;
                List<string> list = new List<string>();

                for (int i = 0; i < DGVHoaDonBan.Rows.Count; ++i)
                {
                    if (DGVHoaDonBan.Rows[i].IsNewRow == false)
                    {
                        list.Add(DGVHoaDonBan.Rows[i].Cells["IdHDB"].Value.ToString());
                    }
                }
                while (KiemtraTrung(list) == true)
                {
                    LocDuLieuTrung(list);
                }
                lblkqtkhdb.Text = "Có " + list.Count + " hóa đơn thoả mãn điều kiện tìm kiếm!";
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
            ResetValuesHDB();
            ResetValuesHDBCT();
            btnBoqua.Enabled = false;
            btnThemSP.Enabled = false;
            btnhienthi.Enabled = false;
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            if (BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text).Rows.Count == 1)
            {
                if(cbHinhthuctt.Text.Trim() ==string.Empty)
                {
                    MessageBox.Show("Bạn phải nhập hình thức thanh toán trước khi in!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbHinhthuctt.Focus();
                    return;
                }
                if(txtdathanhtoan.Text.Trim()==string.Empty || txtdathanhtoan.Text.Trim() == "0")
                {
                    MessageBox.Show("Bạn phải nhập số tiền mà khách hàng đã thanh toán trước khi in!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtdathanhtoan.Focus();
                    return;
                }
                DataRow dr = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text).Rows[0];
                if (dr[7].ToString() == "0" || cbHinhthuctt.Text.Trim() == "")
                {
                    MessageBox.Show("Số tiền đã thanh toán của khách hàng và hình thức thanh toán vẫn chưa được cập nhật. Hãy click 'Sửa HĐ' để cập nhật và thử lại!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnSuaHD.Focus();
                    return;
                }

                if (cbMaNhanVien.Text.Trim() != dr[1].ToString() || cbMaKhachHang.Text.Trim() != dr[2].ToString() || cbHinhthuctt.Text.Trim() != dr[6].ToString() || txtdathanhtoan.Text.Trim() != BUS_HDB.FormatNumber(dr[7].ToString()))
                {
                    if (MessageBox.Show("Đã phát hiện thay đổi so với dữ liệu mà bạn đã lưu trước đó. Nấu Bạn muốn in với thay đổi hiện tại hãy nhấn nút 'Sửa HD' để cập nhật lại thay đổi, ngược lại sẽ in với dữ liệu trong lần cập nhật gần nhất!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btnSuaHD.Focus();
                        return;
                    }
                    else
                    {
                        //print
                        hdbprint = true;
                        mahdb = txtMaHoaDon.Text;
                        PRINTPREVIEW pp = new PRINTPREVIEW();
                        pp.ShowDialog();
                        DANGNHAP.thaotac += "In hóa đơn, ";
                    }
                }
                else
                {
                    //print
                    hdbprint = true;
                    mahdb = txtMaHoaDon.Text;
                    PRINTPREVIEW pp = new PRINTPREVIEW();
                    pp.ShowDialog();
                    DANGNHAP.thaotac += "In hóa đơn, ";
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn hóa đơn để in!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbmaHD.Focus();
                return;
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

        private void cbMaKhachHang_Leave(object sender, EventArgs e)
        {
            if (cbMaKhachHang.Text.Trim().Length != 0)
            {
                DataTable dt = BUS_KH.hienthikh();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        bool flag = false;
                        DataRow dr;
                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            dr = dt.Rows[i];
                            if (cbMaKhachHang.Text.Trim() == dr[0].ToString())
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
                            MessageBox.Show("Mã nhà khách hàng không tồn tại. Hãy kiểm tra lại và chọn mã khác!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cbMaKhachHang.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void cbHinhthuctt_Leave(object sender, EventArgs e)
        {
            if (cbHinhthuctt.Text != "")
            {
                bool flag = true;
                for (int i = 0; i < cbHinhthuctt.Items.Count; ++i)
                {
                    if (cbHinhthuctt.Text == cbHinhthuctt.Items[i].ToString())
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
                    MessageBox.Show("Hình thức thanh toán chỉ bao gồm: 'Trả một lần', 'Trả góp (trả nhiều lần)'! Chú ý viết hoa ký tự đầu.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbHinhthuctt.Focus();
                }
            }
        }

        private void cbMaSanPham_Leave(object sender, EventArgs e)
        {
            if (cbMaSanPham.Text.Trim().Length != 0)
            {
                DataTable dt = BUS_SP.hienthisp();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        bool flag = false;
                        DataRow dr;
                        for (int i = 0; i < dt.Rows.Count; ++i)
                        {
                            dr = dt.Rows[i];
                            if (cbMaSanPham.Text.Trim() == dr[0].ToString())
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
                            MessageBox.Show("Mã sản phẩm không tồn tại. Hãy kiểm tra lại và chọn mã khác!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            cbMaSanPham.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void txtSoLuong_Leave(object sender, EventArgs e)
        {
            if (txtSoLuong.Text.Trim() != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtSoLuong.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoLuong.Focus();
                }
                else
                {
                    if (a <= 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải > 0!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSoLuong.Focus();
                    }
                    else
                    {
                        if (cbMaSanPham.Text.Trim() != string.Empty)
                        {
                            if (BUS_SP.hienthiSPcuthe(cbMaSanPham.Text.Trim()).Rows.Count == 1)
                            {
                                DataRow dr = BUS_SP.hienthiSPcuthe(cbMaSanPham.Text.Trim()).Rows[0];
                                if (a > Int64.Parse(dr["SLNhap"].ToString()))
                                {
                                    MessageBox.Show("Số lượng sản phẩm hiện tại chỉ còn " + dr["SLNhap"].ToString() + " !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtSoLuong.Focus();
                                    return;
                                }
                                else
                                {
                                    txtSoLuong.Text = BUS_HDB.FormatNumber(a.ToString());
                                }
                            }
                            else
                            {
                                txtSoLuong.Text = BUS_HDB.FormatNumber(a.ToString());
                            }

                        }
                        else
                        {
                            txtSoLuong.Text = BUS_HDB.FormatNumber(a.ToString());
                        }

                    }
                }
            }
        }

        private void DGVHoaDonBan_Click(object sender, EventArgs e)
        {
            if (DGVHoaDonBan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DGVHoaDonBan.CurrentRow.Index != DGVHoaDonBan.NewRowIndex)
            {
                btnThemSP.Enabled = false;
                if (btnLamMoi.Enabled == true)
                {
                    btnSuaSP.Enabled = true;
                    btnXoaSP.Enabled = true;
                }
                txtMaHoaDon.Text = DGVHoaDonBan.CurrentRow.Cells["IdHDB"].Value.ToString();
                cbMaSanPham.Text = DGVHoaDonBan.CurrentRow.Cells["IdSP"].Value.ToString();
                txtSoLuong.Text = DGVHoaDonBan.CurrentRow.Cells["SL"].Value.ToString();
                txtDonGia.Text = DGVHoaDonBan.CurrentRow.Cells["Dongiaban"].Value.ToString();
                txtGiamGia.Text = DGVHoaDonBan.CurrentRow.Cells["Giamgia"].Value.ToString();
                txtThanhTien.Text = DGVHoaDonBan.CurrentRow.Cells["Thanhtien"].Value.ToString();

                DataTable dt = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    cbMaNhanVien.Text = dr[1].ToString();
                    cbMaKhachHang.Text = dr[2].ToString();
                    dtpNgayLapHD.Text = dr[3].ToString();
                    txttongsoluong.Text = dr[4].ToString();
                    txtTongTien.Text = dr[5].ToString();
                    cbHinhthuctt.Text = dr[6].ToString();
                    txtdathanhtoan.Text = dr[7].ToString();
                    txtconno.Text = dr[8].ToString();

                    dt = BUS_HDB.LayTenNV(cbMaNhanVien.Text.Trim());
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        txtTenNhanVien.Text = dr[0].ToString();
                    }

                    dt = BUS_HDB.LayTTSP(cbMaSanPham.Text.Trim());
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        txtTenSanPham.Text = dr[0].ToString();
                    }

                    dt = BUS_HDB.LayTTKH(cbMaKhachHang.Text.Trim());
                    if (dt.Rows.Count == 1)
                    {
                        dr = dt.Rows[0];
                        txtTenKhachHang.Text = dr[0].ToString();
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
                            if (a > Int64.Parse(tt))
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

        private void cbMaKhachHang_TextChanged(object sender, EventArgs e)
        {
            if (cbMaKhachHang.Text.Trim().Length > 0)
            {
                DataTable dt = BUS_HDB.LayTTKH(cbMaKhachHang.Text);
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txtTenKhachHang.Text = dr[0].ToString();
                    txtDiaChi.Text = dr[1].ToString();
                    txtDienThoai.Text = dr[2].ToString();
                }
                else
                {
                    txtTenKhachHang.Text = string.Empty;
                    txtDiaChi.Text = string.Empty;
                    txtDienThoai.Text = string.Empty;
                }
            }
            else
            {
                txtTenKhachHang.Text = string.Empty;
                txtDiaChi.Text = string.Empty;
                txtDienThoai.Text = string.Empty;
            }
        }

        private void cbMaSanPham_TextChanged(object sender, EventArgs e)
        {
            if (cbMaSanPham.Text.Trim().Length > 0)
            {
                DataTable dt = BUS_HDB.LayTTSP (cbMaSanPham.Text);

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txtTenSanPham.Text = dr[0].ToString();
                    txtGiamGia.Text = dr[3].ToString();
                    txtSoLuong.Text = string.Empty;
                }
                else
                {
                    txtTenSanPham.Text = string.Empty;
                    txtGiamGia.Text = string.Empty;
                }
            }
            else
            {
                txtTenSanPham.Text = string.Empty;
                txtGiamGia.Text = string.Empty;
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text.Trim() != "")
            {
                Int64 a = 0;
                if (Int64.TryParse(BUS_HDB.ConvertToFloatType(txtSoLuong.Text), out a))
                {
                    if (a <= 0)
                    {
                        txtDonGia.Text = string.Empty;
                        txtThanhTien.Text = string.Empty;
                    }
                    else
                    {
                        if (a < 10 && a > 0)
                        {
                            DataTable dt = BUS_HDB.LayTTSP(cbMaSanPham.Text);

                            if (dt.Rows.Count == 1)
                            {
                                DataRow dr = dt.Rows[0];
                                txtDonGia.Text = dr[1].ToString();
                                txtThanhTien.Text = ((Int64.Parse(BUS_HDB.ConvertToFloatType(txtDonGia.Text)) - ((Int64.Parse(BUS_HDB.ConvertToFloatType(txtDonGia.Text)) * Int32.Parse(txtGiamGia.Text)) / 100)) * a).ToString();
                            }
                            else
                            {
                                txtDonGia.Text = string.Empty;
                                txtThanhTien.Text = string.Empty;
                            }
                        }
                        else if (a >= 10)
                        {
                            DataTable dt = BUS_HDB.LayTTSP(cbMaSanPham.Text);

                            if (dt.Rows.Count == 1)
                            {
                                DataRow dr = dt.Rows[0];
                                txtDonGia.Text = dr[2].ToString();
                                txtThanhTien.Text = ((Int64.Parse(BUS_HDB.ConvertToFloatType(txtDonGia.Text)) - ((Int64.Parse(BUS_HDB.ConvertToFloatType(txtDonGia.Text)) * Int32.Parse(txtGiamGia.Text)) / 100)) * a).ToString();
                            }
                            else
                            {
                                txtDonGia.Text = string.Empty;
                                txtThanhTien.Text = string.Empty;
                            }
                        }
                    }
                }
                else
                {
                    txtDonGia.Text = string.Empty;
                    txtThanhTien.Text = string.Empty;
                }
            }
            else
            {
                txtDonGia.Text = string.Empty;
                txtThanhTien.Text = string.Empty;
            }
        }

        private void txtdathanhtoan_TextChanged(object sender, EventArgs e)
        {
            if (txtdathanhtoan.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text.Trim()), out a))
                {
                    txtconno.Text = txtTongTien.Text;
                }
                else
                {
                    if (a <= 0)
                    {
                        txtconno.Text = txtTongTien.Text;
                    }

                    if (txtTongTien.Text != string.Empty && txtTongTien.Text != "0")
                    {
                        if (a > Int64.Parse(BUS_HDB.ConvertToFloatType(txtTongTien.Text)))
                            txtconno.Text = "0";
                        else
                            txtconno.Text = (Int64.Parse(BUS_HDB.ConvertToFloatType(txtTongTien.Text)) - Int64.Parse(BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text))).ToString();

                    }
                    else
                    {
                        txtconno.Text = txtTongTien.Text;
                    }
                }
            }
            else
            {
                txtconno.Text = txtTongTien.Text;
            }
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (cbMaSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaSanPham.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return;
            }

            //Kiểm tra sản phẩm đã được thêm chưa
            if (!BUS_HDBCT.ktHDBCTtrung(txtMaHoaDon.Text, cbMaSanPham.Text.Trim()))
            {
                MessageBox.Show("Sản phẩm này đã được thêm, bạn hãy chọn sản phẩm khác. Nếu muốn chỉnh sửa lại số lượng sản phẩm, bạn hãy chọn sản phẩm ở bảng bên dưới, chỉnh sửa số lượng và nhấn 'Sửa SP'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaSanPham.Focus();
                return;
            }

            if (cbHinhthuctt.Enabled == false)
            {
                cbHinhthuctt.Enabled = true;
            }
            //Thêm trên bảng hoá đơn bán chi tiết
            DTO_HDBCT hdbct = new DTO_HDBCT(txtMaHoaDon.Text, cbMaSanPham.Text, BUS_HDB.ConvertToFloatType(txtSoLuong.Text), BUS_HDB.ConvertToFloatType(txtDonGia.Text), 
              BUS_HDB.ConvertToFloatType(txtGiamGia.Text), BUS_HDB.ConvertToFloatType(txtThanhTien.Text));

            BUS_HDBCT.themHDBCT(hdbct);

            //Cap nhat tong so luong va tong tien
            BUS_HDB.CapNhatSLSPtrenHDB(txtMaHoaDon.Text);
            BUS_HDB.CapNhatTTtrenHDB(txtMaHoaDon.Text);
            //hien thi dữ liệu vừa cập nhật lên textbox
            DataTable dt = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txttongsoluong.Text = dr[4].ToString();
                    txtTongTien.Text = dr[5].ToString();

                    //Cập nhật nợ trên hóa đơn nhập
                    BUS_HDB.CapNhatNotrenHDB(txtMaHoaDon.Text, BUS_HDB.ConvertToFloatType(txtconno.Text));
                }
            }
            //Cập nhật lại nợ khách hàng
            dt = BUS_KH.hienthikhcuthe(cbMaKhachHang.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    BUS_HDB.CapNhatNoKH(cbMaKhachHang.Text);
                }
            }

            LoadHDBCTHT();
            ResetValuesHDBCT();
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (hdbctht.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbMaSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                BUS_HDBCT.RunDelSQLHDBCT(DGVHoaDonBan.CurrentRow.Cells[0].Value.ToString(), DGVHoaDonBan.CurrentRow.Cells[1].Value.ToString());

                //Cap nhat tong so luong va tong tien
                BUS_HDB.CapNhatSLSPtrenHDB(txtMaHoaDon.Text);
                BUS_HDB.CapNhatTTtrenHDB(txtMaHoaDon.Text);
                //hien thi dữ liệu vừa cập nhật lên textbox
                DataTable dt = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        txttongsoluong.Text = dr[4].ToString();
                        txtTongTien.Text = dr[5].ToString();

                        //Cập nhật nợ trên hóa đơn nhập
                        BUS_HDB.CapNhatNotrenHDB(txtMaHoaDon.Text, BUS_HDB.ConvertToFloatType(txtconno.Text));
                    }
                }
                //Cập nhật lại nợ khách hàng
                dt = BUS_KH.hienthikhcuthe(cbMaKhachHang.Text);
                if (dt != null)
                {
                    if (dt.Rows.Count == 1)
                    {
                        BUS_HDB.CapNhatNoKH(cbMaKhachHang.Text);
                    }
                }

                LoadHDBCTHT();
                ResetValuesHDBCT();
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if(BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text).Rows.Count == 1)
            {
                DataRow dr = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text).Rows[0];
                if (cbMaNhanVien.Text.Trim() != dr[1].ToString() || cbMaKhachHang.Text.Trim() != dr[2].ToString() || txtdathanhtoan.Text.Trim() != BUS_HDB.FormatNumber(dr[7].ToString()) || cbHinhthuctt.Text.Trim() != dr[6].ToString())
                {
                    if (MessageBox.Show("Đã phát hiện thay đổi so với dữ liệu mà bạn đã lưu trước đó.Nấu Bạn muốn in với thay đổi hiện tại hãy nhấn nút 'Sửa HD' để cập nhật lại thay đổi, ngược lại sẽ in với dữ liệu trong lần cập nhật gần nhất", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btnSuaHD.Focus();
                        return;
                    }
                    else
                    {
                        if (dr[5].ToString() == "0")
                        {
                            if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn đã xóa hết chi tiết hóa đơn. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                btnSuaHD.Focus();
                                return;
                            }
                            else
                            {
                                //Xoá những chi tiết hoá đơn nếu có
                                BUS_HDB.RunDelSQLOnHDBCT(txtMaHoaDon.Text);
                                //Xoá hoá đơn bán hiện tại
                                BUS_HDB.RunDelSQL(txtMaHoaDon.Text);

                                btnLamMoi.Enabled = false;
                                btnThemHD.Enabled = true;
                                btnTimKiem.Enabled = true;
                                btnhienthi.Enabled = false;
                                cbmaHD.Enabled = true;
                                grbTTmathang.Enabled = false;
                                ResetValuesHDB();
                                ResetValuesHDBCT();
                                LoadDataGridView();

                                //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                                BUS_HDB.FillComboMaHD(cbmaHD, "IdHDB", "IdHDB");
                                cbmaHD.SelectedIndex = -1;

                            }
                        }
                        else
                        {
                            //Cập nhật lại số lượng sản phẩm
                            DataTable dt = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; ++i)
                                    {
                                        DataRow r = dt.Rows[i];
                                        DataTable table = BUS_SP.hienthiSPcuthe(r[1].ToString());
                                        if (table != null)
                                        {
                                            if (table.Rows.Count == 1)
                                            {
                                                DataRow row = table.Rows[0];
                                                string sl = row[7].ToString();
                                                DTO_SP sp = new DTO_SP();
                                                sp.Masp = r[1].ToString();
                                                sp.Slnhap = (Int64.Parse(sl) - Int64.Parse(r[2].ToString())).ToString();

                                                BUS_HDB.CapNhatSLSanPham(sp);
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
                            grbTTmathang.Enabled = false;
                            ResetValuesHDB();
                            ResetValuesHDBCT();
                            LoadDataGridView();

                            //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                            BUS_HDB.FillComboMaHD(cbmaHD, "IdHDB", "IdHDB");
                            cbmaHD.SelectedIndex = -1;

                        }
                    }
                }
                else
                {
                    if (dr[5].ToString() == "0")
                    {
                        if (MessageBox.Show("Hóa đơn hiện tại rỗng vì bạn chưa thêm sản phẩm nào. Vì vậy, hóa đơn sẽ bị xóa!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            btnSuaHD.Focus();
                            return;
                        }
                        else
                        {
                            //Xoá những chi tiết hoá đơn nếu có
                            BUS_HDB.RunDelSQLOnHDBCT(txtMaHoaDon.Text);
                            //Xoá hoá đơn bán hiện tại
                            BUS_HDB.RunDelSQL(txtMaHoaDon.Text);

                            btnLamMoi.Enabled = false;
                            btnThemHD.Enabled = true;
                            btnTimKiem.Enabled = true;
                            btnhienthi.Enabled = false;
                            cbmaHD.Enabled = true;
                            grbTTmathang.Enabled = false;
                            ResetValuesHDB();
                            ResetValuesHDBCT();
                            LoadDataGridView();

                            //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                            BUS_HDB.FillComboMaHD(cbmaHD, "IdHDB", "IdHDB");
                            cbmaHD.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        //Cập nhật lại số lượng sản phẩm
                        DataTable dt = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; ++i)
                                {
                                    DataRow r = dt.Rows[i];
                                    DataTable table = BUS_SP.hienthiSPcuthe(r[1].ToString());
                                    if (table != null)
                                    {
                                        if (table.Rows.Count == 1)
                                        {
                                            DataRow row = table.Rows[0];
                                            string sl = row[7].ToString();
                                            DTO_SP sp = new DTO_SP();
                                            sp.Masp = r[1].ToString();
                                            sp.Slnhap = (Int64.Parse(sl) - Int64.Parse(r[2].ToString())).ToString();

                                            BUS_HDB.CapNhatSLSanPham(sp);
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
                        grbTTmathang.Enabled = false;
                        ResetValuesHDB();
                        ResetValuesHDBCT();
                        LoadDataGridView();

                        //Cập nhật lại dữ liệu trên combobox mã hoá đơn.
                        BUS_HDB.FillComboMaHD(cbmaHD, "IdHDB", "IdHDB");
                        cbmaHD.SelectedIndex = -1;
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
                grbTTmathang.Enabled = false;
                ResetValuesHDB();
                ResetValuesHDBCT();
                LoadDataGridView();
            }
        }

        private void cbmaHD_TextChanged(object sender, EventArgs e)
        {
            if(cbmaHD.Text.Trim() != string.Empty)          
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
                    DataTable dt = BUS_HDB.hienthiHDBcuthe(cbmaHD.Text);
                    if (dt.Rows.Count == 1)
                    {
                        DataRow dr = dt.Rows[0];
                        cbMaNhanVien.Text = dr[1].ToString();
                        cbMaKhachHang.Text = dr[2].ToString();
                        dtpNgayLapHD.Text = dr[3].ToString();
                        txttongsoluong.Text = dr[4].ToString();
                        txtTongTien.Text = dr[5].ToString();
                        cbHinhthuctt.Text = dr[6].ToString();
                        txtdathanhtoan.Text = BUS_HDB.FormatNumber(dr[7].ToString());
                        txtconno.Text = dr[8].ToString();

                        dt = BUS_HDB.LayTenNV(cbMaNhanVien.Text);
                        if (dt.Rows.Count == 1)
                        {
                            dr = dt.Rows[0];
                            txtTenNhanVien.Text = dr[0].ToString();
                        }

                        dt = BUS_HDB.LayTTKH(cbMaKhachHang.Text);
                        if (dt.Rows.Count == 1)
                        {
                            dr = dt.Rows[0];
                            txtTenKhachHang.Text = dr[0].ToString();
                            txtDiaChi.Text = dr[1].ToString();
                            txtDienThoai.Text = dr[2].ToString();
                        }
                        DGVHoaDonBan.DataSource = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);

                    }
                    else
                    {
                        ResetValuesHDB();
                        ResetValuesHDBCT();
                        DGVHoaDonBan.DataSource = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
                    }
                }
                else
                {
                    ResetValuesHDB();
                    ResetValuesHDBCT();
                    DGVHoaDonBan.DataSource = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
                }

            }
            else
            {
                ResetValuesHDB();
                ResetValuesHDBCT();
                DGVHoaDonBan.DataSource = BUS_HDBCT.hienthiHDBCTcuthe(txtMaHoaDon.Text);
            }
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            if (txtTongTien.Text.Trim() != string.Empty)
            {

                if (txtdathanhtoan.Text.Trim() != string.Empty && txtdathanhtoan.Text.Trim() != "0")
                {
                    string tt = BUS_HDB.ConvertToFloatType(txtTongTien.Text);
                    txtconno.Text = (Int64.Parse(tt) - Int64.Parse(BUS_HDB.ConvertToFloatType(txtdathanhtoan.Text.Trim()))).ToString();
                }
                else
                {
                    txtconno.Text = txtTongTien.Text;
                }

                txtTongTien.Text = BUS_HDB.FormatNumber(txtTongTien.Text);
            }
            else
            {
                txtdathanhtoan.Text = string.Empty;
                txtconno.Text = txtTongTien.Text;
            }
        }

        private void txtconno_TextChanged(object sender, EventArgs e)
        {
            string cn = BUS_HDB.ConvertToFloatType(txtconno.Text);
            txtconno.Text = BUS_HDB.FormatNumber(cn);
        }

        private void cbHinhthuctt_TextChanged(object sender, EventArgs e)
        {
            if(cbHinhthuctt.Text.Trim() != string.Empty)
            {
                if(cbHinhthuctt.Text.Trim() == "Trả một lần")
                {
                    txtdathanhtoan.Text = txtTongTien.Text;
                    txtdathanhtoan.ReadOnly = true;
                }
                else if (cbHinhthuctt.Text.Trim() == "Trả góp (trả nhiều lần)")
                {
                    if (txtTongTien.Text != "0" && txtTongTien.Text != string.Empty)
                    {
                        txtdathanhtoan.ReadOnly = false;
                        txtdathanhtoan.Text = string.Empty;
                    }
                    else
                    {
                        txtdathanhtoan.ReadOnly = true;
                        txtdathanhtoan.Text = string.Empty;
                    }
                }
                else
                {
                    txtdathanhtoan.ReadOnly = true;
                    txtdathanhtoan.Text = string.Empty;
                }
            }
            else
            {
                txtdathanhtoan.ReadOnly = true;
                txtdathanhtoan.Text = string.Empty;
            }
        }

        private void txtThanhTien_TextChanged(object sender, EventArgs e)
        {
            string thanhtien = BUS_HDB.ConvertToFloatType(txtThanhTien.Text);
            txtThanhTien.Text = BUS_HDB.FormatNumber(thanhtien);
        }

        private void txtMaHoaDon_TextChanged(object sender, EventArgs e)
        {
            if(txtMaHoaDon.Text == "Mã sẽ tự động thêm!")
            {
                btnprint.Enabled = false;
            }
            else
            {
                btnprint.Enabled = true;
            }
        }

        private void cbHinhthuctt_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbMaKhachHang_TextChanged(sender, e);
        }

        private void cbmaHD_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbmaHD_TextChanged(sender, e);
        }

        private void txttongsoluong_TextChanged(object sender, EventArgs e)
        {
            string tongsoluong = BUS_HDB.ConvertToFloatType(txttongsoluong.Text);
            txttongsoluong.Text = BUS_HDB.FormatNumber(tongsoluong);
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            string dongia = BUS_HDB.ConvertToFloatType(txtDonGia.Text);
            txtDonGia.Text = BUS_HDB.FormatNumber(dongia);

        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            if (hdbctht.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cbMaSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cbMaSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaSanPham.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return;
            }

            //Kiểm tra sản phẩm đã được thêm chưa
            if (cbMaSanPham.Text.Trim() != DGVHoaDonBan.CurrentRow.Cells["IdSP"].Value.ToString())
            {
                //Kiểm tra sản phẩm đã được thêm chưa
                if (!BUS_HDBCT.ktHDBCTtrung(txtMaHoaDon.Text, cbMaSanPham.Text.Trim()))
                {
                    MessageBox.Show("Sản phẩm này đã được thêm, bạn hãy chọn sản phẩm khác. Nếu muốn chỉnh sửa lại số lượng sản phẩm, bạn hãy chọn sản phẩm ở bảng bên dưới, chỉnh sửa số lượng và nhấn 'Sửa SP'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbMaSanPham.Focus();
                    return;
                }
            }

            DTO_HDBCT hdbct = new DTO_HDBCT(txtMaHoaDon.Text, cbMaSanPham.Text.Trim(), BUS_HDB.ConvertToFloatType(txtSoLuong.Text), BUS_HDB.ConvertToFloatType(txtDonGia.Text), BUS_HDB.ConvertToFloatType(txtGiamGia.Text), BUS_HDB.ConvertToFloatType(txtThanhTien.Text));

            BUS_HDBCT.suaHDBCT(hdbct);

            //Cap nhat tong so luong va tong tien
            BUS_HDB.CapNhatSLSPtrenHDB(txtMaHoaDon.Text);
            BUS_HDB.CapNhatTTtrenHDB(txtMaHoaDon.Text);
            //hien thi dữ liệu vừa cập nhật lên textbox
            DataTable dt = BUS_HDB.hienthiHDBcuthe(txtMaHoaDon.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    txttongsoluong.Text = dr[4].ToString();
                    txtTongTien.Text = dr[5].ToString();

                    //Cập nhật nợ trên hóa đơn nhập
                    BUS_HDB.CapNhatNotrenHDB(txtMaHoaDon.Text, BUS_HDB.ConvertToFloatType(txtconno.Text));
                }
            }
            //Cập nhật lại nợ khách hàng
            dt = BUS_KH.hienthikhcuthe(cbMaKhachHang.Text);
            if (dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    BUS_HDB.CapNhatNoKH(cbMaKhachHang.Text);
                }
            }

            LoadHDBCTHT();
            ResetValuesHDBCT();
        }
    }
}
