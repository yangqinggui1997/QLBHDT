using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using DTO;
using BUS;

namespace QLBHDT
{
    public partial class SANPHAM : Form
    {
        public SANPHAM()
        {
            InitializeComponent();
        }

        public DataTable sp;
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

        private void SANPHAM_Load(object sender, EventArgs e)
        {
            lbl = lblkqtksp.Text;
            btnundo.Enabled = false;
            btnredo.Enabled = false;
            LoadDataGridView();

            BUS_SP.FillCombo(cbMaNCU, "IdNCU", "IdNCU");
            cbMaNCU.SelectedIndex = -1;

            //QLTC
            //btnSua.Enabled = false;
            //btnXoa.Enabled = false;
            //btnOpenAnhSP.Enabled = false;
            if (MAIN.tksp == false)
            {
                string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
                string[] quyenhan = DANGNHAP.Quyen.Split('|');

                for (int j = 0; j < danhmuctmp.Length; ++j)
                {
                    if (danhmuctmp[j].Trim() == "Quản lý sản phẩm")
                    {
                        string[] sp;
                        sp = quyenhan[j].Split(';');
                        if (sp != null)
                        {
                            foreach (string items in sp)
                            {
                                if (items.Trim() == "Thêm (tạo) bản ghi")
                                {
                                    btnOpenAnhSP.Enabled = true;
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
                MAIN.tksp = false;
            }
        }

        private void ResetValues()
        {
            txtMaSanPham.Text = "Mã sản phẩm sẽ tự động thêm!";
            txtTenSanPham.Text = string.Empty;
            cbMaNCU.Text = string.Empty;
            dtpNgaySX.Value = DateTime.Now;
            txtnhasx.Text = string.Empty;
            txtdonvi.Text = "Vd: Chiếc, Cái, Hộp, ...";
            txtdonvi.ForeColor = Color.Gray;
            txtgiamgia.Text = string.Empty;
            dtpngaynhap.Value = DateTime.Now;
            dtpngayhh.Value = DateTime.Now;
            txtSoLuongNhap.Text = string.Empty;
            txtSoLuongNhap.Text = string.Empty;
            txtDonGiaNhap.Text = string.Empty;
            txtDonGiaBanLe.Text = string.Empty;
            txtdongiabansi.Text = string.Empty;
            richtxtAnhSanPham.Text = string.Empty;
            picAnh.Image = null;
            lblkqtksp.Text = lbl;
        }

        private void LoadDataGridView()
        {
            sp = BUS_SP.hienthisp();
            DGVSanPham.DataSource = sp;
            DGVSanPham.AllowUserToAddRows = false;
            DGVSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DataGridView_Click(object sender, EventArgs e)
        {
            if (DGVSanPham.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (DGVSanPham.CurrentRow.Index == DGVSanPham.NewRowIndex)
            {
                MessageBox.Show("Hãy chọn dòng có thông tin!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtMaSanPham.Text = DGVSanPham.CurrentRow.Cells["IdSP"].Value.ToString();
            txtTenSanPham.Text = DGVSanPham.CurrentRow.Cells["TenSP"].Value.ToString();
            cbMaNCU.Text = DGVSanPham.CurrentRow.Cells["IdNCU"].Value.ToString();
            dtpNgaySX.Text = DGVSanPham.CurrentRow.Cells["NgaySX"].Value.ToString();
            dtpngayhh.Text = DGVSanPham.CurrentRow.Cells["NgayHH"].Value.ToString();
            dtpngaynhap.Text = DGVSanPham.CurrentRow.Cells["NgayNhap"].Value.ToString();
            txtDonGiaBanLe.Text = BUS_HDB.FormatNumber(DGVSanPham.CurrentRow.Cells["DonGiaBanLe"].Value.ToString());

            txtnhasx.Text = DGVSanPham.CurrentRow.Cells["NhaSX"].Value.ToString();

            txtSoLuongNhap.Text = BUS_HDB.FormatNumber(DGVSanPham.CurrentRow.Cells["SLNhap"].Value.ToString());

            txtDonGiaNhap.Text = BUS_HDB.FormatNumber(DGVSanPham.CurrentRow.Cells["DonGiaNhap"].Value.ToString());

            txtdongiabansi.Text = BUS_HDB.FormatNumber(DGVSanPham.CurrentRow.Cells["DonGiaBanSi"].Value.ToString());

            txtdonvi.Text = DGVSanPham.CurrentRow.Cells["Donvi"].Value.ToString();
            txtdonvi.ForeColor = Color.Black;
            txtgiamgia.Text = DGVSanPham.CurrentRow.Cells["GiamGia"].Value.ToString();      
            richtxtAnhSanPham.Text = DGVSanPham.CurrentRow.Cells["AnhSP"].Value.ToString();

            if(!File.Exists(richtxtAnhSanPham.Text) && !Directory.Exists(richtxtAnhSanPham.Text))
            {
                picAnh.Image = null;
                MessageBox.Show("Hình ảnh không tồn tại do file bị xóa hoặc dường dẫn không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSanPham.Focus();
                return;
            }
            else
            {
                picAnh.Image = Image.FromFile(richtxtAnhSanPham.Text);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSanPham.Focus();
                return;
            }
            if (cbMaNCU.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung ứng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaNCU.Focus();
                return;
            }
            if (txtnhasx.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nhà sản xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnhasx.Focus();
                return;
            }
            if (txtDonGiaNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaNhap.Focus();
                return;
            }
            if (txtDonGiaBanLe.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập dơn giá bán lẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaBanLe.Focus();
                return;
            }
            if (txtdongiabansi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán sỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdongiabansi.Focus();
                return;
            }
            if (txtdonvi.Text == "Vd: Chiếc, Cái, Hộp, ...")
            {
                MessageBox.Show("Bạn phải nhập đơn vị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdonvi.Focus();
                return;
            }


            LoadDataGridView();
            ResetValues();

            DANGNHAP.thaotac += "Thêm, ";

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (sp.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtMaSanPham.Text == "Mã sản phẩm sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtTenSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSanPham.Focus();
                return;
            }
            if (cbMaNCU.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung ứng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbMaNCU.Focus();
                return;
            }
            if (txtnhasx.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nhà sản xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtnhasx.Focus();
                return;
            }
            if (txtDonGiaNhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaNhap.Focus();
                return;
            }
            if (txtDonGiaBanLe.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập dơn giá bán lẻ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGiaBanLe.Focus();
                return;
            }
            if (txtdongiabansi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán sỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdongiabansi.Focus();
                return;
            }
            if (txtdonvi.Text == "Vd: Chiếc, Cái, Hộp, ...")
            {
                MessageBox.Show("Bạn phải nhập đơn vị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtdonvi.Focus();
                return;
            }

            Random rand = new Random();
            string anhsp = Application.StartupPath + @"\Images Resource\" + "Image_" + rand.Next(1, 1000000000) + ".jpg";
            File.Copy(richtxtAnhSanPham.Text, anhsp);

            DTO_SP SP = new DTO_SP(txtMaSanPham.Text, cbMaNCU.Text, txtTenSanPham.Text, dtpNgaySX.Text, dtpngayhh.Text, dtpngaynhap.Text, txtnhasx.Text, BUS_HDB.ConvertToFloatType(txtSoLuongNhap.Text), BUS_HDB.ConvertToFloatType(txtDonGiaNhap.Text), BUS_HDB.ConvertToFloatType(txtDonGiaBanLe.Text), BUS_HDB.ConvertToFloatType(txtdongiabansi.Text), txtdonvi.Text, txtgiamgia.Text, anhsp);
            BUS_SP.suaSP(SP);

            LoadDataGridView();
            ResetValues();

            DANGNHAP.thaotac += "Sửa, ";

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (sp.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSanPham.Text == "Mã sản phẩm sẽ tự động thêm!")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Xoá sản phẩm sẽ xoá tất cả các thông tin của sản phẩm có trên các hoá đơn nhập, xuất và trên bảng thống kê hàng tồn. Bạn có chắc chắn muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //xóa sản phẩm trên bảng hàng tồn chi tiết
                BUS_SP.RunDelSQLOnHTCT(txtMaSanPham.Text);
                //xóa sản phẩm trên bảng hóa đơn bán chi tiết
                BUS_SP.RunDelSQLOnHDBCT(txtMaSanPham.Text);
                //xóa sản phẩm trên bảng hóa đơn nhập chi tiết
                BUS_SP.RunDelSQLOnHDNCT(txtMaSanPham.Text);

                BUS_SP.RunDelSQL(txtMaSanPham.Text);
                LoadDataGridView();
                ResetValues();

                DANGNHAP.thaotac += "Xoá, ";

            }
        }
        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                richtxtAnhSanPham.Text = dlgOpen.FileName;
            }
        }
        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (txtTenSanPham.Text == string.Empty && cbMaNCU.Text == string.Empty)
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DTO_SP sp = new DTO_SP();
            sp.Mancu = cbMaNCU.Text;
            sp.Tensp = txtTenSanPham.Text;
            DataTable dt = BUS_SP.timkiemSP(sp.Mancu, sp.Tensp);
            DGVSanPham.DataSource = dt;

            if (dt.Rows.Count == 0)
            {
                lblkqtksp.Text = "Không có bản ghi nào thoả mãn điều kiện tìm kiếm!";
            }
            else
            {
                lblkqtksp.Text = "Có " + dt.Rows.Count + " bản ghi nào thoả mãn điều kiện tìm kiếm!";
            }
            DANGNHAP.thaotac += "Tìm kiếm, ";

        }
        private void btnHienthi_Click(object sender, EventArgs e)
        {
            DGVSanPham.DataSource = BUS_SP.hienthisp();
            ResetValues();
        }
        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSoLuongNhap_Leave(object sender, EventArgs e)
        {
            if (txtSoLuongNhap.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtSoLuongNhap.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoLuongNhap.Focus();
                }
                else
                {
                    if (a <= 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải > 0 !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSoLuongNhap.Focus();
                    }
                    else
                    {
                        txtSoLuongNhap.Text = BUS_HDB.FormatNumber(a.ToString());
                    }

                }
            }
        }

        private void txtDonGiaNhap_Leave(object sender, EventArgs e)
        {
            if (txtDonGiaNhap.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtDonGiaNhap.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDonGiaNhap.Focus();
                }
                else
                {
                    if (a <= 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải > 0 !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDonGiaNhap.Focus();
                    }
                    else
                    {
                        if(txtdongiabansi.Text != "")
                        {
                            if(a >= Int64.Parse(BUS_HDB.ConvertToFloatType(txtdongiabansi.Text)))
                            {
                                MessageBox.Show("Đơn giá nhập không được lớn hơn hoặc bằng đơn giá bán!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtDonGiaNhap.Focus();
                                return;
                            }
                            else
                            {
                                txtDonGiaNhap.Text = BUS_HDB.FormatNumber(a.ToString());
                            }
                        }
                        else
                        {
                            txtDonGiaNhap.Text = BUS_HDB.FormatNumber(a.ToString());
                        }
                        if (txtDonGiaBanLe.Text != "")
                        {
                            if (a >= Int64.Parse(BUS_HDB.ConvertToFloatType(txtDonGiaBanLe.Text)))
                            {
                                MessageBox.Show("Đơn giá nhập không được lớn hơn hoặc bằng đơn giá bán!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtDonGiaNhap.Focus();
                                return;
                            }
                            else
                            {
                                txtDonGiaNhap.Text = BUS_HDB.FormatNumber(a.ToString());
                            }
                        }
                        else
                        {
                            txtDonGiaNhap.Text = BUS_HDB.FormatNumber(a.ToString());
                        }
                    }
                }
            }
        }

        private void txtDonGiaBanLe_Leave(object sender, EventArgs e)
        {
            if (txtDonGiaBanLe.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtDonGiaBanLe.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDonGiaBanLe.Focus();
                }
                else
                {
                    if (a < 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0 !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtDonGiaBanLe.Focus();
                    }
                    else
                    {
                        if (txtDonGiaNhap.Text != "")
                        {
                            if (a <= Int64.Parse(BUS_HDB.ConvertToFloatType(txtDonGiaNhap.Text)))
                            {
                                MessageBox.Show("Đơn giá bán không được bé hơn hoặc bằng đơn giá nhập!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtDonGiaBanLe.Focus();
                                return;
                            }
                            else
                            {
                                txtDonGiaBanLe.Text = BUS_HDB.FormatNumber(a.ToString());
                            }
                        }
                        else
                        {
                            txtDonGiaBanLe.Text = BUS_HDB.FormatNumber(a.ToString());
                        }
                        if (txtdongiabansi.Text != "")
                        {
                            if (a <= Int64.Parse(BUS_HDB.ConvertToFloatType(txtdongiabansi.Text)))
                            {
                                MessageBox.Show("Đơn giá bán lẻ không được nhỏ hơn hoặc bằng đơn giá bán sỉ!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtdongiabansi.Focus();
                                return;
                            }
                            else
                            {
                                txtDonGiaBanLe.Text = BUS_HDB.FormatNumber(a.ToString());
                            }
                        }
                        else
                        {
                            txtDonGiaBanLe.Text = BUS_HDB.FormatNumber(a.ToString());

                        }
                    }

                }
            }
        }

        private void txtDonGiaBanSi_Leave(object sender, EventArgs e)
        {
            if (txtdongiabansi.Text != "")
            {
                Int64 a = 0;
                if (!Int64.TryParse(BUS_HDB.ConvertToFloatType(txtdongiabansi.Text), out a))
                {
                    MessageBox.Show("Giá trị phải là số!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtdongiabansi.Focus();
                }
                else
                {
                    if (a < 0)
                    {
                        MessageBox.Show("Giá trị hợp lệ phải >= 0 !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtdongiabansi.Focus();
                    }
                    else
                    {
                        if (txtDonGiaNhap.Text != "")
                        {
                            if (a <= Int64.Parse(BUS_HDB.ConvertToFloatType( txtDonGiaNhap.Text)))
                            {
                                MessageBox.Show("Đơn giá bán không được bé hơn hoặc bằng đơn giá nhập!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtdongiabansi.Focus();
                                return;
                            }
                            else
                            {
                                txtdongiabansi.Text = BUS_HDB.FormatNumber(a.ToString());
                            }
                        }
                        else
                        {
                            txtdongiabansi.Text = BUS_HDB.FormatNumber(a.ToString());
                        }
                        if (txtDonGiaBanLe.Text != "")
                        {
                            if (a >= Int64.Parse(BUS_HDB.ConvertToFloatType(txtDonGiaBanLe.Text)))
                            {
                                MessageBox.Show("Đơn giá bán sỉ không được lớn hơn hoặc bằng đơn giá bán lẻ!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtdongiabansi.Focus();
                                return;
                            }
                            else
                            {
                                txtdongiabansi.Text = BUS_HDB.FormatNumber(a.ToString());
                            }
                        }
                        else
                        {
                            txtdongiabansi.Text = BUS_HDB.FormatNumber(a.ToString());
                        }
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
                }
            }
        }

        private void SANPHAM_FormClosed(object sender, FormClosedEventArgs e)
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

        private void btnundo_Click(object sender, EventArgs e)
        {

        }

        private void btnredo_Click(object sender, EventArgs e)
        {

        }

        private void cbMaNCU_Leave(object sender, EventArgs e)
        {
            if (cbMaNCU.Text.Trim().Length != 0)
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
                            if (cbMaNCU.Text.Trim() == dr[0].ToString())
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
                            cbMaNCU.Focus();
                            return;
                        }
                    }
                }
            }
        }

        private void txtdonvi_Enter(object sender, EventArgs e)
        {
            if(txtdonvi.Text == "Vd: Chiếc, Cái, Hộp, ...")
            {
                txtdonvi.Text = string.Empty;
                txtdonvi.ForeColor = Color.Black;
            }
        }

        private void txtdonvi_Leave(object sender, EventArgs e)
        {
            if (txtdonvi.Text.Trim().Length == 0)
            {
                txtdonvi.Text = "Vd: Chiếc, Cái, Hộp, ...";
                txtdonvi.ForeColor = Color.Gray;
            }
        }

        private void btnxemttnacu_Click(object sender, EventArgs e)
        {
            DataTable dt = BUS_SP.hienthincucuthe(cbMaNCU.Text);
            if(cbMaNCU.Text.Trim().Length > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    MessageBox.Show("Mã nhà cung ứng: " + dr[0].ToString() + "." + Environment.NewLine + "Tên nhà cung ứng: " + dr[1].ToString() + "." + Environment.NewLine + "Địa chỉ: " + dr[2].ToString() + "." + Environment.NewLine + "SĐT: " + dr[3].ToString() + "." + Environment.NewLine + "Quy mô nhà cung ứng: " + dr[4].ToString() + ".", "Thông tin nhà cung ứng!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Mã nhà cung ứng không chính xác. Hãy nhập đúng mã để xem đầy đủ thông tin!", "Information!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Hãy nhập mã nhà cung úng để xem đầy đủ thông tin!", "Information!");
                return;
            }
        }

    }
}
