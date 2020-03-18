using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using BUS;

namespace QLBHDT
{
    public partial class MAIN : Form
    {
        private NHANVIEN nv;
        private KHACHHANG kh;
        private SANPHAM sp;
        private NCU ncu;
        private NHOMND_NHOMQH nnd_qh;
        private NGUOIDUNG_QH nd;
        private HDBAN hdb;
        private HDNHAP hdn;
        private CONGNO cn;
        private DOANHTHU dt;
        private HANGTON ht;
        private QLTRUYCAP qltc;

        private List<Form> f = new List<Form>();

        public MAIN()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        public static bool tkhdb, tkhdn, tkkh, tknv, tksp, tkncu, xemquyen;

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

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void MAIN_FormClosed(object sender, FormClosedEventArgs e)
        {
            mnuStartpage_Click(sender, e);
            DANGNHAP dn = new DANGNHAP();
            if (DANGNHAP.ghinhomk == true)
            {
                dn.txtPass.Text = DANGNHAP.pass;
                dn.txtUsers.Text = DANGNHAP.user;
                dn.ckbgn.Checked = true;
                dn.btndangnhap.Enabled = true;
            }

            BUS_TC.CapnhatTTTruyCap(DANGNHAP.matc, DANGNHAP.danhmuctc, DANGNHAP.thaotac);

            dn.Show();
        }

        private void mnuStartpage_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in f)
            {
                if(childForm.IsDisposed == false)
                {
                    childForm.Close();
                }
            }

            if(f.Count > 0)
            {
                f.Clear();
            }
        }

        private void mnunhanvien_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý nhân viên | ";
            DANGNHAP.thaotac += "Xem, ";
            if (nv == null || nv.IsDisposed == true)
            {
                nv = new NHANVIEN();
                nv.Show();
                f.Add(nv);
            }
        }

        private void mnuKhachhang_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý khách hàng | ";
            DANGNHAP.thaotac += "Xem, ";
            if (kh == null || kh.IsDisposed == true)
            {
                kh = new KHACHHANG();
                kh.Show();
                f.Add(kh);
            }
        }

        private void mnudoimk_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Đổi mật khẩu | ";
            DANGNHAP.thaotac += "Đổi mật khẩu, ";
            DOI_MK dmk = new DOI_MK();
            dmk.ShowDialog();
        }

        private void MAIN_Load(object sender, EventArgs e)
        {
            mnunguoidung.Enabled = false;
            mnunhanvien.Enabled = false;
            mnuKhachhang.Enabled = false;
            mnuSanpham.Enabled = false;
            mnuhoadonban.Enabled = false;
            mnuhoadonnhap.Enabled = false;
            mnunhacungung.Enabled = false;
            mnund.Enabled = false;
            mnunhomnd_qh.Enabled = false;
            mnutruycap.Enabled = false;

            string[] danhmuctmp = DANGNHAP.Danhmuc.Split('|');
            for (int j = 0; j < danhmuctmp.Length; ++j)
            {
                if (danhmuctmp[j].Trim() == "Quản lý nhân viên")
                {
                    mnunhanvien.Enabled = false;
                }
                else if (danhmuctmp[j].Trim() == "Quản lý khách hàng")
                {
                    mnuKhachhang.Enabled = true;
                }
                else if (danhmuctmp[j].Trim() == "Quản lý nhà cung ứng")
                {
                    mnunhacungung.Enabled = true;
                }
                else if (danhmuctmp[j].Trim() == "Quản lý sản phẩm")
                {
                    mnuSanpham.Enabled = true;
                }
                else if (danhmuctmp[j].Trim() == "Quản lý hoá đơn")
                {
                    if (DANGNHAP.IdNV.Substring(0, 3) == "NQT")
                    {
                        mnuhoadonban.Enabled = true;
                        mnuhoadonnhap.Enabled = true;
                    }
                    if (DANGNHAP.IdNV.Substring(0, 3) == "NBH")
                    {
                        mnuhoadonban.Enabled = true;
                    }
                    if (DANGNHAP.IdNV.Substring(0, 3) == "NKT")
                    {
                        mnuhoadonban.Enabled = true;
                        mnuhoadonnhap.Enabled = true;
                    }
                    if (DANGNHAP.IdNV.Substring(0, 3) == "NTK")
                    {
                        mnuhoadonnhap.Enabled = true;
                    }
                }
                else if (danhmuctmp[j].Trim() == "Thống kê, báo cáo")
                {
                    if (DANGNHAP.IdNV.Substring(0, 3) == "NQT")
                    {
                        mnucongno.Enabled = true;
                        mnudoanhthu.Enabled = true;
                        mnuhangton.Enabled = true;
                    }
                    if (DANGNHAP.IdNV.Substring(0, 3) == "NKT")
                    {
                        mnucongno.Enabled = true;
                        mnudoanhthu.Enabled = true;
                    }
                    if (DANGNHAP.IdNV.Substring(0, 3) == "NTK")
                    {
                        mnuhangton.Enabled = true;
                    }
                }
                else if (danhmuctmp[j].Trim() == "Quản trị hệ thống (quản lý người dùng)")
                {
                    mnunguoidung.Enabled = true;
                    mnunhanvien.Enabled = true;
                    mnuKhachhang.Enabled = true;
                    mnuSanpham.Enabled = true;
                    mnuhoadonban.Enabled = true;
                    mnuhoadonnhap.Enabled = true;
                    mnunhacungung.Enabled = true;
                    mnunhomnd_qh.Enabled = true;
                    mnund.Enabled = true;
                    mnutruycap.Enabled = true;
                }
            }
        }

        private void mnuhoadonnhap_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý hoá đơn nhập | ";
            DANGNHAP.thaotac += "Xem, ";
            if (hdn == null || hdn.IsDisposed == true)
            {
                hdn = new HDNHAP();
                //hdb.Getdataincb += NCU_Getdataincb;
                hdn.Show();
                f.Add(hdn);
            }
        }

        private void mnuhoadonban_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý hoá đơn bán | ";
            DANGNHAP.thaotac += "Xem, ";
            if (hdb == null || hdb.IsDisposed == true)
            {
                hdb = new HDBAN();
                //hdb.Getdataincb += NCU_Getdataincb;
                hdb.Show();
                f.Add(hdb);
            }
        }

        private void mnuSanpham_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý sản phẩm | ";
            DANGNHAP.thaotac += "Xem, ";
            if (sp == null || sp.IsDisposed == true)
            {
                sp = new SANPHAM();
                sp.Show();
                f.Add(sp);
            }
        }

        private void mnunhacungung_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý nhà cung ứng | ";
            DANGNHAP.thaotac += "Xem, ";
            if (ncu == null || ncu.IsDisposed == true)
            {
                ncu = new NCU();
                ncu.Getdataincb += NCU_Getdataincb;
                ncu.Show();
                f.Add(ncu);
            }

        }

        private void NCU_Getdataincb(ComboBox cb)
        {
            if(sp != null)
            {
                if (cb.Items.Count > 0)
                {
                    if (sp.cbMaNCU.Items.Count > 0)
                    {
                        sp.cbMaNCU.Items.Clear();
                        for (int i = 0; i < cb.Items.Count; ++i)
                        {
                            sp.cbMaNCU.Items.Add(cb.Items[i]);
                        }
                    }
                }
                else
                {
                    sp.cbMaNCU.Items.Clear();
                }
            }
        }

        private void mnund_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý người dùng và quyền hạn | ";
            DANGNHAP.thaotac += "Xem, ";
            if (nd == null || nd.IsDisposed == true)
            {
                nd = new NGUOIDUNG_QH();
                //nd.Getdataincb += NCU_Getdataincb;
                nd.Show();
                f.Add(nd);
            }
        }

        private void mnunhomnd_qh_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý nhóm người dùng và nhóm quyền hạn | ";
            DANGNHAP.thaotac += "Xem, ";
            if (nnd_qh == null || nnd_qh.IsDisposed == true)
            {
                nnd_qh = new NHOMND_NHOMQH();
                nnd_qh.Show();
                f.Add(nnd_qh);
            }
        }

        private void mnuhangton_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Thống kê hàng tồn | ";
            DANGNHAP.thaotac += "Xem, ";
            if (ht == null || ht.IsDisposed == true)
            {
                ht = new HANGTON();
                ht.Show();
                f.Add(ht);
            }

        }

        private void mnudoanhthu_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Thống kê doanh thu | ";
            DANGNHAP.thaotac += "Xem, ";
            if (dt == null || dt.IsDisposed == true)
            {
                dt = new DOANHTHU();
                dt.Show();
                f.Add(dt);
            }
        }

        private void mnucongno_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Thống kê công nợ | ";
            DANGNHAP.thaotac += "Xem, ";
            if (cn == null || cn.IsDisposed == true)
            {
                cn = new CONGNO();
                cn.Show();
                f.Add(cn);
            }
        }

        private void mnutkhdban_Click(object sender, EventArgs e)
        {
            tkhdb = true;
            mnuhoadonban_Click(sender, e);
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            DANGNHAP dn = new DANGNHAP();
            if (DANGNHAP.ghinhomk == true)
            {
                dn.txtPass.Text = DANGNHAP.pass;
                dn.txtUsers.Text = DANGNHAP.user;
                dn.ckbgn.Checked = true;
                dn.btndangnhap.Enabled = true;
            }
            dn.Show();
            Close();
        }

        private void mnutruycap_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Quản lý truy cập | ";
            DANGNHAP.thaotac += "Xem, ";
            if (ncu == null || ncu.IsDisposed == true)
            {
                qltc = new QLTRUYCAP();
                qltc.Show();
                f.Add(qltc);
            }
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

        private void mnuDoigiaodien_Click(object sender, EventArgs e)
        {

        }

        private void mnugoithieu_Click(object sender, EventArgs e)
        {
            GIOITHIEU gt = new GIOITHIEU();
            gt.ShowDialog();
        }

        private void mnuhuongdan_Click(object sender, EventArgs e)
        {
            HUONGDAN hd = new HUONGDAN();
            hd.ShowDialog();
        }

        private void mnuStatistics_DropDownOpened(object sender, EventArgs e)
        {
            mnuStatistics.ForeColor = Color.Black;
        }

        private void mnuStatistics_DropDownClosed(object sender, EventArgs e)
        {
            mnuStatistics.ForeColor = Color.White;
        }

        private void fileMenu_DropDownClosed(object sender, EventArgs e)
        {
            fileMenu.ForeColor = Color.White;
        }

        private void fileMenu_DropDownOpened(object sender, EventArgs e)
        {
            fileMenu.ForeColor = Color.Black;
        }

        private void windowsMenu_DropDownClosed(object sender, EventArgs e)
        {
            windowsMenu.ForeColor = Color.White;
        }

        private void windowsMenu_DropDownOpened(object sender, EventArgs e)
        {
            windowsMenu.ForeColor = Color.Black;
        }

        private void mnumanage_DropDownClosed(object sender, EventArgs e)
        {
            mnumanage.ForeColor = Color.White;
        }

        private void mnumanage_DropDownOpened(object sender, EventArgs e)
        {
            mnumanage.ForeColor = Color.Black;
        }

        private void mnusearch_DropDownClosed(object sender, EventArgs e)
        {
            mnusearch.ForeColor = Color.White;
        }

        private void mnusearch_DropDownOpened(object sender, EventArgs e)
        {
            mnusearch.ForeColor = Color.Black;
        }

        private void helpMenu_DropDownClosed(object sender, EventArgs e)
        {
            helpMenu.ForeColor = Color.White;
        }

        private void helpMenu_DropDownOpened(object sender, EventArgs e)
        {
            helpMenu.ForeColor = Color.Black;
        }

        private void mnugoithieu_MouseEnter(object sender, EventArgs e)
        {
            mnugoithieu.ForeColor = Color.Black;
        }

        private void mnugoithieu_MouseLeave(object sender, EventArgs e)
        {
            mnugoithieu.ForeColor = Color.White;
        }

        private void mnuhuongdan_MouseEnter(object sender, EventArgs e)
        {
            mnuhuongdan.ForeColor = Color.Black;
        }

        private void mnuhuongdan_MouseLeave(object sender, EventArgs e)
        {
            mnuhuongdan.ForeColor = Color.White;
        }

        private void mnutkhoadon_MouseEnter(object sender, EventArgs e)
        {
            mnutkhoadon.ForeColor = Color.Black;
        }

        private void mnutkhoadon_MouseLeave(object sender, EventArgs e)
        {
            mnutkhoadon.ForeColor = Color.White;
        }

        private void mnutkhdban_MouseEnter(object sender, EventArgs e)
        {
            mnutkhdban.ForeColor = Color.Black;
        }

        private void mnutkhdban_MouseLeave(object sender, EventArgs e)
        {
            mnutkhdban.ForeColor = Color.White;
        }

        private void mnutkhdnhap_MouseEnter(object sender, EventArgs e)
        {
            mnutkhdnhap.ForeColor = Color.Black;
        }

        private void mnutkhdnhap_MouseLeave(object sender, EventArgs e)
        {
            mnutkhdnhap.ForeColor = Color.White;
        }

        private void mnutkkhachhang_MouseEnter(object sender, EventArgs e)
        {
            mnutkkhachhang.ForeColor = Color.Black;
        }

        private void mnutkkhachhang_MouseLeave(object sender, EventArgs e)
        {
            mnutkkhachhang.ForeColor = Color.White;
        }

        private void mnutksanpham_MouseEnter(object sender, EventArgs e)
        {
            mnutksanpham.ForeColor = Color.Black;
        }

        private void mnutksanpham_MouseLeave(object sender, EventArgs e)
        {
            mnutksanpham.ForeColor = Color.White;
        }

        private void mnutknhacu_MouseEnter(object sender, EventArgs e)
        {
            mnutknhacu.ForeColor = Color.Black;
        }

        private void mnutknhacu_MouseLeave(object sender, EventArgs e)
        {
            mnutknhacu.ForeColor = Color.White;
        }

        private void mnuhangton_MouseEnter(object sender, EventArgs e)
        {
            mnuhangton.ForeColor = Color.Black;
        }

        private void mnuhangton_MouseLeave(object sender, EventArgs e)
        {
            mnuhangton.ForeColor = Color.White;
        }

        private void mnudoanhthu_MouseEnter(object sender, EventArgs e)
        {
            mnudoanhthu.ForeColor = Color.Black;
        }

        private void mnudoanhthu_MouseLeave(object sender, EventArgs e)
        {
            mnudoanhthu.ForeColor = Color.White;
        }

        private void mnucongno_MouseEnter(object sender, EventArgs e)
        {
            mnucongno.ForeColor = Color.Black;
        }

        private void mnucongno_MouseLeave(object sender, EventArgs e)
        {
            mnucongno.ForeColor = Color.White;
        }

        private void mnunguoidung_MouseEnter(object sender, EventArgs e)
        {
            mnunguoidung.ForeColor = Color.Black;
        }

        private void mnunguoidung_MouseLeave(object sender, EventArgs e)
        {
            mnunguoidung.ForeColor = Color.White;
        }

        private void mnund_MouseEnter(object sender, EventArgs e)
        {
            mnund.ForeColor = Color.Black;
        }

        private void mnund_MouseLeave(object sender, EventArgs e)
        {
            mnund.ForeColor = Color.White;
        }

        private void mnunhomnd_qh_MouseEnter(object sender, EventArgs e)
        {
            mnunhomnd_qh.ForeColor = Color.Black;
        }

        private void mnunhomnd_qh_MouseLeave(object sender, EventArgs e)
        {
            mnunhomnd_qh.ForeColor = Color.White;
        }

        private void mnutruycap_MouseEnter(object sender, EventArgs e)
        {
            mnutruycap.ForeColor = Color.Black;
        }

        private void mnutruycap_MouseLeave(object sender, EventArgs e)
        {
            mnutruycap.ForeColor = Color.White;
        }

        private void mnunhanvien_MouseEnter(object sender, EventArgs e)
        {
            mnunhanvien.ForeColor = Color.Black;
        }

        private void mnunhanvien_MouseLeave(object sender, EventArgs e)
        {
            mnunhanvien.ForeColor = Color.White;
        }

        private void mnuKhachhang_MouseEnter(object sender, EventArgs e)
        {
            mnuKhachhang.ForeColor = Color.Black;
        }

        private void mnuKhachhang_MouseLeave(object sender, EventArgs e)
        {
            mnuKhachhang.ForeColor = Color.White;
        }

        private void mnuHD_MouseEnter(object sender, EventArgs e)
        {
            mnuHD.ForeColor = Color.Black;
        }

        private void mnuHD_MouseLeave(object sender, EventArgs e)
        {
            mnuHD.ForeColor = Color.White;
        }

        private void mnuhoadonnhap_MouseEnter(object sender, EventArgs e)
        {
            mnuhoadonnhap.ForeColor = Color.Black;
        }

        private void mnuhoadonnhap_MouseLeave(object sender, EventArgs e)
        {
            mnuhoadonnhap.ForeColor = Color.White;
        }

        private void mnuhoadonban_MouseEnter(object sender, EventArgs e)
        {
            mnuhoadonban.ForeColor = Color.Black;
        }

        private void mnuhoadonban_MouseLeave(object sender, EventArgs e)
        {
            mnuhoadonban.ForeColor = Color.White;
        }

        private void mnuSanpham_MouseEnter(object sender, EventArgs e)
        {
            mnuSanpham.ForeColor = Color.Black;
        }

        private void mnuSanpham_MouseLeave(object sender, EventArgs e)
        {
            mnuSanpham.ForeColor = Color.White;
        }

        private void mnunhacungung_MouseEnter(object sender, EventArgs e)
        {
            mnunhacungung.ForeColor = Color.Black;
        }

        private void mnunhacungung_MouseLeave(object sender, EventArgs e)
        {
            mnunhacungung.ForeColor = Color.White;
        }

        private void mnuDoigiaodien_MouseEnter(object sender, EventArgs e)
        {
            mnuDoigiaodien.ForeColor = Color.Black;
        }

        private void mnuDoigiaodien_MouseLeave(object sender, EventArgs e)
        {
            mnuDoigiaodien.ForeColor = Color.White;
        }

        private void mnuTaiKhoan_MouseEnter(object sender, EventArgs e)
        {
            mnuTaiKhoan.ForeColor = Color.Black;
        }

        private void mnuTaiKhoan_MouseLeave(object sender, EventArgs e)
        {
            mnuTaiKhoan.ForeColor = Color.White;
        }

        private void mnuDangXuat_MouseEnter(object sender, EventArgs e)
        {
            mnuDangXuat.ForeColor = Color.Black;
        }

        private void mnuDangXuat_MouseLeave(object sender, EventArgs e)
        {
            mnuDangXuat.ForeColor = Color.White;
        }

        private void mnudoimk_MouseEnter(object sender, EventArgs e)
        {
            mnudoimk.ForeColor = Color.Black;
        }

        private void mnudoimk_MouseLeave(object sender, EventArgs e)
        {
            mnudoimk.ForeColor = Color.White;
        }

        private void mnuxemquyen_MouseEnter(object sender, EventArgs e)
        {
            mnuxemquyen.ForeColor = Color.Black;
        }

        private void mnuxemquyen_MouseLeave(object sender, EventArgs e)
        {
            mnuxemquyen.ForeColor = Color.White;
        }

        private void mnuStartpage_MouseEnter(object sender, EventArgs e)
        {
            mnuStartpage.ForeColor = Color.Black;
        }

        private void mnuStartpage_MouseLeave(object sender, EventArgs e)
        {
            mnuStartpage.ForeColor = Color.White;
        }

        private void exitToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.Black;
        }

        private void exitToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            exitToolStripMenuItem.ForeColor = Color.White;
        }

        private void mnuxemquyen_Click(object sender, EventArgs e)
        {
            DANGNHAP.danhmuctc += DateTime.Now + " ; Xem quyền người dùng | ";
            DANGNHAP.thaotac += "Xem, ";
            xemquyen = true;
            XEMQUYEN xq = new XEMQUYEN();
            xq.ShowDialog();
        }

        private void mnutkhdnhap_Click(object sender, EventArgs e)
        {
            tkhdn = true;
            mnuhoadonnhap_Click(sender, e);
        }

        private void mnutkkhachhang_Click(object sender, EventArgs e)
        {
            tkkh = true;
            mnuKhachhang_Click(sender, e);
        }

        private void mnutknhanvien_Click(object sender, EventArgs e)
        {
            tknv = true;
            mnunhanvien_Click(sender, e);
        }

        private void mnutksanpham_Click(object sender, EventArgs e)
        {
            tksp = true;
            mnuSanpham_Click(sender, e);
        }

        private void mnutknhacu_Click(object sender, EventArgs e)
        {
            tkncu = true;
            mnunhacungung_Click(sender, e);
        }
    }
}
