using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace QLBHDT
{
    public partial class PRINTPREVIEW : Form
    {
        public PRINTPREVIEW()
        {
            InitializeComponent();
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
        private void PRINTPREVIEW_Load(object sender, EventArgs e)
        {
            if (HDBAN.hdbprint == true)
            {
                DataTable dt = BUS.BUS_HDBCT.hienthiHDBCTLenBC(HDBAN.mahdb);
                ReportDataSource rds = new ReportDataSource();
                //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = "INHOADONBAN.rdlc";
                //Nếu có dữ liệu
                //Tạo nguồn dữ liệu cho báo cáo
                rds.Name = "HDBCT";
                rds.Value = dt;
                //Xóa dữ liệu của báo cáo cũ trong trường hợp người dùng thực hiện câu truy vấn khác
                reportViewer1.LocalReport.DataSources.Clear();
                //Add dữ liệu vào báo cáo
                reportViewer1.LocalReport.DataSources.Add(rds);

                dt = BUS.BUS_HDB.hienthiHDBcuthe(HDBAN.mahdb);
                DataRow row = dt.Rows[0];
                ReportParameter mahdb = new ReportParameter("MaHDB", HDBAN.mahdb);
                reportViewer1.LocalReport.SetParameters(mahdb);

                ReportParameter makh = new ReportParameter("MaKH", row[2].ToString());
                reportViewer1.LocalReport.SetParameters(makh);

                ReportParameter hinhthuctt = new ReportParameter("HinhthucTT", row[6].ToString());
                reportViewer1.LocalReport.SetParameters(hinhthuctt);

                ReportParameter dathanhtoan = new ReportParameter("dathanhtoan", row[7].ToString());
                reportViewer1.LocalReport.SetParameters(dathanhtoan);

                ReportParameter conno = new ReportParameter("conno", row[8].ToString());
                reportViewer1.LocalReport.SetParameters(conno);

                ReportParameter ngayin = new ReportParameter("Ngayin", DateTime.Now.ToString());
                reportViewer1.LocalReport.SetParameters(ngayin);

                dt = BUS.BUS_KH.hienthikhcuthe(row[2].ToString());
                DataRow r = dt.Rows[0];

                ReportParameter tenkh = new ReportParameter("TenKH", r[1].ToString());
                reportViewer1.LocalReport.SetParameters(tenkh);

                ReportParameter diachi = new ReportParameter("DiaChi", r[5].ToString());
                reportViewer1.LocalReport.SetParameters(diachi);

                ReportParameter sdt = new ReportParameter("SĐT", r[6].ToString());
                reportViewer1.LocalReport.SetParameters(sdt);

                dt = BUS.BUS_NV.hienthinvcuthe(row[1].ToString());
                r = dt.Rows[0];

                ReportParameter tennv = new ReportParameter("TenNV", r[1].ToString());
                reportViewer1.LocalReport.SetParameters(tennv);

                //Refresh lại báo cáo
                reportViewer1.RefreshReport();
                HDBAN.hdbprint = false;
            }

            if (HDNHAP.hdnprint == true)
            {
                DataTable dt = BUS.BUS_HDNCT.hienthiHDNCTLenBC(HDNHAP.mahdn);
                //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = "INHOADONNHAP.rdlc";

                //Nếu có dữ liệu
                //Tạo nguồn dữ liệu cho báo cáo
                ReportDataSource rds = new ReportDataSource();
                rds.Name = "HDNCT";
                rds.Value = dt;
                //Xóa dữ liệu của báo cáo cũ trong trường hợp người dùng thực hiện câu truy vấn khác
                reportViewer1.LocalReport.DataSources.Clear();
                //Add dữ liệu vào báo cáo
                reportViewer1.LocalReport.DataSources.Add(rds);

                dt = BUS.BUS_HDN.hienthiHDNcuthe(HDNHAP.mahdn);
                DataRow row = dt.Rows[0];

                ReportParameter mahdn = new ReportParameter("MaHDN", HDNHAP.mahdn);
                reportViewer1.LocalReport.SetParameters(mahdn);

                ReportParameter mancu = new ReportParameter("MaNCU", row[2].ToString());
                reportViewer1.LocalReport.SetParameters(mancu);

                ReportParameter ngayin = new ReportParameter("Ngayin", DateTime.Now.ToString());
                reportViewer1.LocalReport.SetParameters(ngayin);

                dt = BUS.BUS_NCU.hienthiNCUcuthe(row[2].ToString());
                DataRow r = dt.Rows[0];

                ReportParameter tenncu = new ReportParameter("TenNCU", r[1].ToString());
                reportViewer1.LocalReport.SetParameters(tenncu);

                ReportParameter diachi = new ReportParameter("DiaChi", r[2].ToString());
                reportViewer1.LocalReport.SetParameters(diachi);

                ReportParameter sdt = new ReportParameter("SĐT", r[3].ToString());
                reportViewer1.LocalReport.SetParameters(sdt);

                dt = BUS.BUS_NV.hienthinvcuthe(row[1].ToString());
                r = dt.Rows[0];

                ReportParameter tennvlap = new ReportParameter("TenNV", r[1].ToString());
                reportViewer1.LocalReport.SetParameters(tennvlap);

                //Refresh lại báo cáo
                reportViewer1.RefreshReport();
                HDNHAP.hdnprint = false;
            }

            if (DOANHTHU.dtprint == true)
            {
                DataTable dt = BUS.BUS_DT.hienthiDTcuthe(DOANHTHU.madt);
                //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = "DOANHTHU.rdlc";

                DataRow row = dt.Rows[0];
                ReportParameter madt = new ReportParameter("MaTK", DOANHTHU.madt);
                reportViewer1.LocalReport.SetParameters(madt);

                ReportParameter manv = new ReportParameter("MaNV", row[1].ToString());
                reportViewer1.LocalReport.SetParameters(manv);

                ReportParameter ngayin = new ReportParameter("Ngayin", DateTime.Now.ToString());
                reportViewer1.LocalReport.SetParameters(ngayin);

                ReportParameter ngaytk = new ReportParameter("NgayTK", row[5].ToString());
                reportViewer1.LocalReport.SetParameters(ngaytk);

                ReportParameter tongds = new ReportParameter("TongDS", row[2].ToString());
                reportViewer1.LocalReport.SetParameters(tongds);

                ReportParameter tongdt = new ReportParameter("TongDT", row[3].ToString());
                reportViewer1.LocalReport.SetParameters(tongdt);

                ReportParameter loinhuan = new ReportParameter("loinhuan", row[4].ToString());
                reportViewer1.LocalReport.SetParameters(loinhuan);

                ReportParameter thangtk = new ReportParameter("ThangTK", row[5].ToString().Substring(3, 8));
                reportViewer1.LocalReport.SetParameters(thangtk);

                dt = BUS.BUS_NV.hienthinvcuthe(row[1].ToString());
                DataRow dr = dt.Rows[0];

                ReportParameter tennv = new ReportParameter("TenNV", dr[1].ToString());
                reportViewer1.LocalReport.SetParameters(tennv);

                //Refresh lại báo cáo
                reportViewer1.RefreshReport();
                DOANHTHU.dtprint = false;
            }

            if (HANGTON.htprint == true)
            {
                //Xóa dữ liệu của báo cáo cũ trong trường hợp người dùng thực hiện câu truy vấn khác
                reportViewer1.LocalReport.DataSources.Clear();

                DataTable DT = BUS.BUS_HTCT.hienthiHTCTLenBC(HANGTON.maht);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "HTCT";
                rds.Value = DT;
                //Add dữ liệu vào báo cáo
                reportViewer1.LocalReport.DataSources.Add(rds);

                DataTable dt = BUS.BUS_HT.hienthiHTcuthe(HANGTON.maht);
                //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = "HANGTON.rdlc";

                DataRow row = dt.Rows[0];
                ReportParameter maht = new ReportParameter("MaTK", HANGTON.maht);
                reportViewer1.LocalReport.SetParameters(maht);

                ReportParameter manv = new ReportParameter("MaNV", row[1].ToString());
                reportViewer1.LocalReport.SetParameters(manv);

                ReportParameter ngayin = new ReportParameter("Ngayin", DateTime.Now.ToString());
                reportViewer1.LocalReport.SetParameters(ngayin);

                ReportParameter ngaytk = new ReportParameter("NgayTK", row[2].ToString());
                reportViewer1.LocalReport.SetParameters(ngaytk);

                ReportParameter thangtk = new ReportParameter("ThangTK", row[2].ToString().Substring(3, 8));
                reportViewer1.LocalReport.SetParameters(thangtk);

                dt = BUS.BUS_NV.hienthinvcuthe(row[1].ToString());
                DataRow dr = dt.Rows[0];

                ReportParameter tennv = new ReportParameter("TenNV", dr[1].ToString());
                reportViewer1.LocalReport.SetParameters(tennv);

                //Refresh lại báo cáo
                reportViewer1.RefreshReport();
                HANGTON.htprint = false;
            }

            if (CONGNO.cnprint == true)
            {
                //Xóa dữ liệu của báo cáo cũ trong trường hợp người dùng thực hiện câu truy vấn khác
                reportViewer1.LocalReport.DataSources.Clear();

                DataTable DT = BUS.BUS_CNCT.hienthiCNCTcuthe(CONGNO.macn);

                ReportDataSource rds = new ReportDataSource();
                rds.Name = "CNCT";
                rds.Value = DT;
                //Add dữ liệu vào báo cáo
                reportViewer1.LocalReport.DataSources.Add(rds);

                DataTable dt = BUS.BUS_CN.hienthiCNcuthe(CONGNO.macn);
                //Khai báo chế độ xử lý báo cáo, trong trường hợp này lấy báo cáo ở local
                reportViewer1.ProcessingMode = ProcessingMode.Local;
                //Đường dẫn báo cáo
                reportViewer1.LocalReport.ReportPath = "CONGNO.rdlc";

                DataRow row = dt.Rows[0];
                ReportParameter macn = new ReportParameter("MaTK", CONGNO.macn);
                reportViewer1.LocalReport.SetParameters(macn);

                ReportParameter manv = new ReportParameter("MaNV", row[1].ToString());
                reportViewer1.LocalReport.SetParameters(manv);

                ReportParameter ngayin = new ReportParameter("Ngayin", DateTime.Now.ToString());
                reportViewer1.LocalReport.SetParameters(ngayin);

                ReportParameter ngaytk = new ReportParameter("NgayTK", row[2].ToString());
                reportViewer1.LocalReport.SetParameters(ngaytk);

                ReportParameter thangtk = new ReportParameter("ThangTK", row[2].ToString().Substring(3, 8));
                reportViewer1.LocalReport.SetParameters(thangtk);

                dt = BUS.BUS_NV.hienthinvcuthe(row[1].ToString());
                DataRow dr = dt.Rows[0];

                ReportParameter tennv = new ReportParameter("TenNV", dr[1].ToString());
                reportViewer1.LocalReport.SetParameters(tennv);

                //Refresh lại báo cáo
                reportViewer1.RefreshReport();
                CONGNO.cnprint = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
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
    }
}
