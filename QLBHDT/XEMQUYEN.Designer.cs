namespace QLBHDT
{
    partial class XEMQUYEN
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label12 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabxacnhanquyen = new System.Windows.Forms.TabControl();
            this.tabqlnv = new System.Windows.Forms.TabPage();
            this.clbqlnv = new System.Windows.Forms.CheckedListBox();
            this.tabqlkh = new System.Windows.Forms.TabPage();
            this.clbqlkh = new System.Windows.Forms.CheckedListBox();
            this.tabqlncu = new System.Windows.Forms.TabPage();
            this.clbqlncu = new System.Windows.Forms.CheckedListBox();
            this.tabqlsp = new System.Windows.Forms.TabPage();
            this.clbqlsp = new System.Windows.Forms.CheckedListBox();
            this.tabqlhd = new System.Windows.Forms.TabPage();
            this.clbqlhd = new System.Windows.Forms.CheckedListBox();
            this.tabqltkbc = new System.Windows.Forms.TabPage();
            this.clbqltkbc = new System.Windows.Forms.CheckedListBox();
            this.tabqlnd = new System.Windows.Forms.TabPage();
            this.clbquantri = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkqlncu = new System.Windows.Forms.CheckBox();
            this.chkQuantri = new System.Windows.Forms.CheckBox();
            this.chktkbc = new System.Windows.Forms.CheckBox();
            this.chkqlhd = new System.Windows.Forms.CheckBox();
            this.chkqlsp = new System.Windows.Forms.CheckBox();
            this.chkqlkh = new System.Windows.Forms.CheckBox();
            this.chkqlnv = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnok = new System.Windows.Forms.Button();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabxacnhanquyen.SuspendLayout();
            this.tabqlnv.SuspendLayout();
            this.tabqlkh.SuspendLayout();
            this.tabqlncu.SuspendLayout();
            this.tabqlsp.SuspendLayout();
            this.tabqlhd.SuspendLayout();
            this.tabqltkbc.SuspendLayout();
            this.tabqlnd.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label12);
            this.panel5.Controls.Add(this.btnClose);
            this.panel5.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(740, 24);
            this.panel5.TabIndex = 129;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label12.Location = new System.Drawing.Point(0, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 15);
            this.label12.TabIndex = 72;
            this.label12.Text = "[] Xem Quyền";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Red;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::QLBHDT.Properties.Resources.cross;
            this.btnClose.Location = new System.Drawing.Point(710, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(28, 22);
            this.btnClose.TabIndex = 71;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(740, 452);
            this.panel1.TabIndex = 130;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tabxacnhanquyen);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox3.Location = new System.Drawing.Point(267, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(471, 395);
            this.groupBox3.TabIndex = 135;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Quyền cụ thể cho từng danh mục";
            // 
            // tabxacnhanquyen
            // 
            this.tabxacnhanquyen.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabxacnhanquyen.Controls.Add(this.tabqlnv);
            this.tabxacnhanquyen.Controls.Add(this.tabqlkh);
            this.tabxacnhanquyen.Controls.Add(this.tabqlncu);
            this.tabxacnhanquyen.Controls.Add(this.tabqlsp);
            this.tabxacnhanquyen.Controls.Add(this.tabqlhd);
            this.tabxacnhanquyen.Controls.Add(this.tabqltkbc);
            this.tabxacnhanquyen.Controls.Add(this.tabqlnd);
            this.tabxacnhanquyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabxacnhanquyen.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabxacnhanquyen.Location = new System.Drawing.Point(3, 19);
            this.tabxacnhanquyen.Name = "tabxacnhanquyen";
            this.tabxacnhanquyen.SelectedIndex = 0;
            this.tabxacnhanquyen.Size = new System.Drawing.Size(465, 373);
            this.tabxacnhanquyen.TabIndex = 0;
            // 
            // tabqlnv
            // 
            this.tabqlnv.AutoScroll = true;
            this.tabqlnv.BackColor = System.Drawing.SystemColors.Control;
            this.tabqlnv.Controls.Add(this.clbqlnv);
            this.tabqlnv.Location = new System.Drawing.Point(4, 27);
            this.tabqlnv.Name = "tabqlnv";
            this.tabqlnv.Padding = new System.Windows.Forms.Padding(3);
            this.tabqlnv.Size = new System.Drawing.Size(457, 342);
            this.tabqlnv.TabIndex = 0;
            this.tabqlnv.Text = "Quản lý nhân viên";
            // 
            // clbqlnv
            // 
            this.clbqlnv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.clbqlnv.CheckOnClick = true;
            this.clbqlnv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbqlnv.ForeColor = System.Drawing.SystemColors.Window;
            this.clbqlnv.FormattingEnabled = true;
            this.clbqlnv.Items.AddRange(new object[] {
            "Xem (đọc).",
            "Thêm (tạo) bản ghi.",
            "Sửa (cập nhật) bản ghi.",
            "Xoá (huỷ) bản ghi."});
            this.clbqlnv.Location = new System.Drawing.Point(3, 3);
            this.clbqlnv.Name = "clbqlnv";
            this.clbqlnv.Size = new System.Drawing.Size(451, 336);
            this.clbqlnv.TabIndex = 0;
            // 
            // tabqlkh
            // 
            this.tabqlkh.AutoScroll = true;
            this.tabqlkh.BackColor = System.Drawing.SystemColors.Control;
            this.tabqlkh.Controls.Add(this.clbqlkh);
            this.tabqlkh.Location = new System.Drawing.Point(4, 27);
            this.tabqlkh.Name = "tabqlkh";
            this.tabqlkh.Padding = new System.Windows.Forms.Padding(3);
            this.tabqlkh.Size = new System.Drawing.Size(457, 342);
            this.tabqlkh.TabIndex = 6;
            this.tabqlkh.Text = "Quản lý khách hàng";
            // 
            // clbqlkh
            // 
            this.clbqlkh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.clbqlkh.CheckOnClick = true;
            this.clbqlkh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbqlkh.ForeColor = System.Drawing.SystemColors.Window;
            this.clbqlkh.FormattingEnabled = true;
            this.clbqlkh.Items.AddRange(new object[] {
            "Xem (đọc).",
            "Thêm (tạo) bản ghi.",
            "Sửa (cập nhật) bản ghi.",
            "Xoá (huỷ) bản ghi."});
            this.clbqlkh.Location = new System.Drawing.Point(3, 3);
            this.clbqlkh.Name = "clbqlkh";
            this.clbqlkh.Size = new System.Drawing.Size(451, 336);
            this.clbqlkh.TabIndex = 1;
            // 
            // tabqlncu
            // 
            this.tabqlncu.BackColor = System.Drawing.SystemColors.Control;
            this.tabqlncu.Controls.Add(this.clbqlncu);
            this.tabqlncu.Location = new System.Drawing.Point(4, 27);
            this.tabqlncu.Name = "tabqlncu";
            this.tabqlncu.Size = new System.Drawing.Size(457, 342);
            this.tabqlncu.TabIndex = 11;
            this.tabqlncu.Text = "Quản lý nhà cung ứng";
            // 
            // clbqlncu
            // 
            this.clbqlncu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.clbqlncu.CheckOnClick = true;
            this.clbqlncu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbqlncu.ForeColor = System.Drawing.SystemColors.Window;
            this.clbqlncu.FormattingEnabled = true;
            this.clbqlncu.Items.AddRange(new object[] {
            "Xem (đọc).",
            "Thêm (tạo) bản ghi.",
            "Sửa (cập nhật) bản ghi.",
            "Xoá (huỷ) bản ghi."});
            this.clbqlncu.Location = new System.Drawing.Point(0, 0);
            this.clbqlncu.Name = "clbqlncu";
            this.clbqlncu.Size = new System.Drawing.Size(457, 342);
            this.clbqlncu.TabIndex = 3;
            // 
            // tabqlsp
            // 
            this.tabqlsp.AutoScroll = true;
            this.tabqlsp.BackColor = System.Drawing.SystemColors.Control;
            this.tabqlsp.Controls.Add(this.clbqlsp);
            this.tabqlsp.Location = new System.Drawing.Point(4, 27);
            this.tabqlsp.Name = "tabqlsp";
            this.tabqlsp.Padding = new System.Windows.Forms.Padding(3);
            this.tabqlsp.Size = new System.Drawing.Size(457, 342);
            this.tabqlsp.TabIndex = 7;
            this.tabqlsp.Text = "Quản lý sản phẩm";
            // 
            // clbqlsp
            // 
            this.clbqlsp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.clbqlsp.CheckOnClick = true;
            this.clbqlsp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbqlsp.ForeColor = System.Drawing.SystemColors.Window;
            this.clbqlsp.FormattingEnabled = true;
            this.clbqlsp.Items.AddRange(new object[] {
            "Xem (đọc).",
            "Thêm (tạo) bản ghi.",
            "Sửa (cập nhật) bản ghi.",
            "Xoá (huỷ) bản ghi."});
            this.clbqlsp.Location = new System.Drawing.Point(3, 3);
            this.clbqlsp.Name = "clbqlsp";
            this.clbqlsp.Size = new System.Drawing.Size(451, 336);
            this.clbqlsp.TabIndex = 1;
            // 
            // tabqlhd
            // 
            this.tabqlhd.AutoScroll = true;
            this.tabqlhd.BackColor = System.Drawing.SystemColors.Control;
            this.tabqlhd.Controls.Add(this.clbqlhd);
            this.tabqlhd.Location = new System.Drawing.Point(4, 27);
            this.tabqlhd.Name = "tabqlhd";
            this.tabqlhd.Padding = new System.Windows.Forms.Padding(3);
            this.tabqlhd.Size = new System.Drawing.Size(457, 342);
            this.tabqlhd.TabIndex = 8;
            this.tabqlhd.Text = "Quản lý hoá đơn";
            // 
            // clbqlhd
            // 
            this.clbqlhd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.clbqlhd.CheckOnClick = true;
            this.clbqlhd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbqlhd.ForeColor = System.Drawing.SystemColors.Window;
            this.clbqlhd.FormattingEnabled = true;
            this.clbqlhd.Items.AddRange(new object[] {
            "Xem (đọc).",
            "Thêm (tạo) bản ghi.",
            "Sửa (cập nhật) bản ghi.",
            "Xoá (huỷ) bản ghi."});
            this.clbqlhd.Location = new System.Drawing.Point(3, 3);
            this.clbqlhd.Name = "clbqlhd";
            this.clbqlhd.Size = new System.Drawing.Size(451, 336);
            this.clbqlhd.TabIndex = 1;
            // 
            // tabqltkbc
            // 
            this.tabqltkbc.AutoScroll = true;
            this.tabqltkbc.BackColor = System.Drawing.SystemColors.Control;
            this.tabqltkbc.Controls.Add(this.clbqltkbc);
            this.tabqltkbc.Location = new System.Drawing.Point(4, 27);
            this.tabqltkbc.Name = "tabqltkbc";
            this.tabqltkbc.Padding = new System.Windows.Forms.Padding(3);
            this.tabqltkbc.Size = new System.Drawing.Size(457, 342);
            this.tabqltkbc.TabIndex = 9;
            this.tabqltkbc.Text = "Thống kê, báo cáo";
            // 
            // clbqltkbc
            // 
            this.clbqltkbc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.clbqltkbc.CheckOnClick = true;
            this.clbqltkbc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbqltkbc.ForeColor = System.Drawing.SystemColors.Window;
            this.clbqltkbc.FormattingEnabled = true;
            this.clbqltkbc.Items.AddRange(new object[] {
            "Xem (đọc).",
            "Thêm (tạo) bản ghi.",
            "Sửa (cập nhật) bản ghi.",
            "Xoá (huỷ) bản ghi."});
            this.clbqltkbc.Location = new System.Drawing.Point(3, 3);
            this.clbqltkbc.Name = "clbqltkbc";
            this.clbqltkbc.Size = new System.Drawing.Size(451, 336);
            this.clbqltkbc.TabIndex = 1;
            // 
            // tabqlnd
            // 
            this.tabqlnd.AutoScroll = true;
            this.tabqlnd.BackColor = System.Drawing.SystemColors.Control;
            this.tabqlnd.Controls.Add(this.clbquantri);
            this.tabqlnd.Location = new System.Drawing.Point(4, 27);
            this.tabqlnd.Name = "tabqlnd";
            this.tabqlnd.Padding = new System.Windows.Forms.Padding(3);
            this.tabqlnd.Size = new System.Drawing.Size(457, 342);
            this.tabqlnd.TabIndex = 10;
            this.tabqlnd.Text = "Quản trị hệ thống (quản lý người dùng)";
            this.tabqlnd.ToolTipText = "Quản trị hệ thống (quản lý người dùng)";
            // 
            // clbquantri
            // 
            this.clbquantri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.clbquantri.CheckOnClick = true;
            this.clbquantri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbquantri.ForeColor = System.Drawing.SystemColors.Window;
            this.clbquantri.FormattingEnabled = true;
            this.clbquantri.Items.AddRange(new object[] {
            "Xem (đọc).",
            "Thêm (tạo) bản ghi.",
            "Sửa (cập nhật) bản ghi.",
            "Xoá (huỷ) bản ghi."});
            this.clbquantri.Location = new System.Drawing.Point(3, 3);
            this.clbquantri.Name = "clbquantri";
            this.clbquantri.Size = new System.Drawing.Size(451, 336);
            this.clbquantri.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkqlncu);
            this.groupBox4.Controls.Add(this.chkQuantri);
            this.groupBox4.Controls.Add(this.chktkbc);
            this.groupBox4.Controls.Add(this.chkqlhd);
            this.groupBox4.Controls.Add(this.chkqlsp);
            this.groupBox4.Controls.Add(this.chkqlkh);
            this.groupBox4.Controls.Add(this.chkqlnv);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Window;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 395);
            this.groupBox4.TabIndex = 134;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Danh mục được phép truy cập";
            // 
            // chkqlncu
            // 
            this.chkqlncu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkqlncu.AutoSize = true;
            this.chkqlncu.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkqlncu.Location = new System.Drawing.Point(18, 76);
            this.chkqlncu.Name = "chkqlncu";
            this.chkqlncu.Size = new System.Drawing.Size(148, 19);
            this.chkqlncu.TabIndex = 12;
            this.chkqlncu.Text = "Quản lý nhà cung ứng.";
            this.chkqlncu.UseVisualStyleBackColor = true;
            // 
            // chkQuantri
            // 
            this.chkQuantri.AutoSize = true;
            this.chkQuantri.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkQuantri.Location = new System.Drawing.Point(17, 180);
            this.chkQuantri.Name = "chkQuantri";
            this.chkQuantri.Size = new System.Drawing.Size(244, 19);
            this.chkQuantri.TabIndex = 11;
            this.chkQuantri.Text = "Quản trị hệ thống (quản lý người dùng).";
            this.chkQuantri.UseVisualStyleBackColor = true;
            // 
            // chktkbc
            // 
            this.chktkbc.AutoSize = true;
            this.chktkbc.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chktkbc.Location = new System.Drawing.Point(18, 154);
            this.chktkbc.Name = "chktkbc";
            this.chktkbc.Size = new System.Drawing.Size(128, 19);
            this.chktkbc.TabIndex = 10;
            this.chktkbc.Text = "Thống kê, báo cáo.";
            this.chktkbc.UseVisualStyleBackColor = true;
            // 
            // chkqlhd
            // 
            this.chkqlhd.AutoSize = true;
            this.chkqlhd.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkqlhd.Location = new System.Drawing.Point(18, 128);
            this.chkqlhd.Name = "chkqlhd";
            this.chkqlhd.Size = new System.Drawing.Size(120, 19);
            this.chkqlhd.TabIndex = 9;
            this.chkqlhd.Text = "Quản lý hoá đơn.";
            this.chkqlhd.UseVisualStyleBackColor = true;
            // 
            // chkqlsp
            // 
            this.chkqlsp.AutoSize = true;
            this.chkqlsp.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkqlsp.Location = new System.Drawing.Point(18, 102);
            this.chkqlsp.Name = "chkqlsp";
            this.chkqlsp.Size = new System.Drawing.Size(128, 19);
            this.chkqlsp.TabIndex = 8;
            this.chkqlsp.Text = "Quản lý sản phẩm.";
            this.chkqlsp.UseVisualStyleBackColor = true;
            // 
            // chkqlkh
            // 
            this.chkqlkh.AutoSize = true;
            this.chkqlkh.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkqlkh.Location = new System.Drawing.Point(18, 50);
            this.chkqlkh.Name = "chkqlkh";
            this.chkqlkh.Size = new System.Drawing.Size(137, 19);
            this.chkqlkh.TabIndex = 7;
            this.chkqlkh.Text = "Quản lý khách hàng.";
            this.chkqlkh.UseVisualStyleBackColor = true;
            // 
            // chkqlnv
            // 
            this.chkqlnv.AutoSize = true;
            this.chkqlnv.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkqlnv.Location = new System.Drawing.Point(18, 24);
            this.chkqlnv.Name = "chkqlnv";
            this.chkqlnv.Size = new System.Drawing.Size(129, 19);
            this.chkqlnv.TabIndex = 6;
            this.chkqlnv.Text = "Quản lý nhân viên.";
            this.chkqlnv.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 395);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(738, 55);
            this.panel2.TabIndex = 133;
            // 
            // btnok
            // 
            this.btnok.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnok.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnok.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnok.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnok.ForeColor = System.Drawing.Color.White;
            this.btnok.Location = new System.Drawing.Point(316, 11);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(107, 33);
            this.btnok.TabIndex = 5;
            this.btnok.Text = "OK";
            this.btnok.UseVisualStyleBackColor = false;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.panel5;
            this.bunifuDragControl1.Vertical = true;
            // 
            // XEMQUYEN
            // 
            this.AcceptButton = this.btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 476);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XEMQUYEN";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem quyền hạn";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.XEMQUYEN_FormClosed);
            this.Load += new System.EventHandler(this.XEMQUYEN_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tabxacnhanquyen.ResumeLayout(false);
            this.tabqlnv.ResumeLayout(false);
            this.tabqlkh.ResumeLayout(false);
            this.tabqlncu.ResumeLayout(false);
            this.tabqlsp.ResumeLayout(false);
            this.tabqlhd.ResumeLayout(false);
            this.tabqltkbc.ResumeLayout(false);
            this.tabqlnd.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabxacnhanquyen;
        private System.Windows.Forms.TabPage tabqlnv;
        private System.Windows.Forms.CheckedListBox clbqlnv;
        private System.Windows.Forms.TabPage tabqlkh;
        private System.Windows.Forms.CheckedListBox clbqlkh;
        private System.Windows.Forms.TabPage tabqlncu;
        private System.Windows.Forms.CheckedListBox clbqlncu;
        private System.Windows.Forms.TabPage tabqlsp;
        private System.Windows.Forms.CheckedListBox clbqlsp;
        private System.Windows.Forms.TabPage tabqlhd;
        private System.Windows.Forms.CheckedListBox clbqlhd;
        private System.Windows.Forms.TabPage tabqltkbc;
        private System.Windows.Forms.CheckedListBox clbqltkbc;
        private System.Windows.Forms.TabPage tabqlnd;
        private System.Windows.Forms.CheckedListBox clbquantri;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkqlncu;
        private System.Windows.Forms.CheckBox chkQuantri;
        private System.Windows.Forms.CheckBox chktkbc;
        private System.Windows.Forms.CheckBox chkqlhd;
        private System.Windows.Forms.CheckBox chkqlsp;
        private System.Windows.Forms.CheckBox chkqlkh;
        private System.Windows.Forms.CheckBox chkqlnv;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnok;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
    }
}