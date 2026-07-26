namespace QuanLyKhoHang.Forms
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlShell;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlBrand;
        private System.Windows.Forms.Label lblBrandIcon;
        private System.Windows.Forms.Label lblBrandText;
        private System.Windows.Forms.Button btnToggleMenu;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel pnlSidebarFooter;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlHeaderLine;
        private System.Windows.Forms.Panel pnlSidebarLine;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnHangHoa;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnNhanVien;
        private System.Windows.Forms.Button btnNhapKho;
        private System.Windows.Forms.Button btnXuatKho;
        private System.Windows.Forms.Button btnDanhMuc;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnUserMenu;
        private System.Windows.Forms.ContextMenuStrip cmsUser;
        private System.Windows.Forms.Timer sidebarTimer;
        private System.Windows.Forms.ToolTip sidebarToolTip;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlShell = new Panel();
            pnlContent = new Panel();
            pnlHeader = new Panel();
            btnUserMenu = new Button();
            pnlHeaderLine = new Panel();
            pnlSidebar = new Panel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btnHangHoa = new Button();
            btnKhachHang = new Button();
            btnNhanVien = new Button();
            btnNhapKho = new Button();
            btnXuatKho = new Button();
            btnDanhMuc = new Button();
            btnThongKe = new Button();
            pnlSidebarFooter = new Panel();
            lblVersion = new Label();
            pnlBrand = new Panel();
            lblBrandIcon = new Label();
            lblBrandText = new Label();
            btnToggleMenu = new Button();
            pnlSidebarLine = new Panel();
            cmsUser = new ContextMenuStrip(components);
            menuTaiKhoan = new ToolStripMenuItem();
            menuChucVu = new ToolStripMenuItem();
            menuDangXuat = new ToolStripMenuItem();
            sidebarTimer = new System.Windows.Forms.Timer(components);
            sidebarToolTip = new ToolTip(components);
            pnlShell.SuspendLayout();
            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            pnlSidebarFooter.SuspendLayout();
            pnlBrand.SuspendLayout();
            cmsUser.SuspendLayout();
            SuspendLayout();
            // 
            // pnlShell
            // 
            pnlShell.BackColor = Color.FromArgb(245, 247, 251);
            pnlShell.Controls.Add(pnlContent);
            pnlShell.Controls.Add(pnlHeader);
            pnlShell.Controls.Add(pnlSidebar);
            pnlShell.Dock = DockStyle.Fill;
            pnlShell.Location = new Point(0, 0);
            pnlShell.Margin = new Padding(4);
            pnlShell.Name = "pnlShell";
            pnlShell.Size = new Size(1500, 875);
            pnlShell.TabIndex = 1;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(240, 242, 245);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(325, 75);
            pnlContent.Margin = new Padding(4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1175, 800);
            pnlContent.TabIndex = 0;
            pnlContent.Paint += pnlContent_Paint;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnUserMenu);
            pnlHeader.Controls.Add(pnlHeaderLine);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(325, 0);
            pnlHeader.Margin = new Padding(4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1175, 75);
            pnlHeader.TabIndex = 1;
            // 
            // btnUserMenu
            // 
            btnUserMenu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnUserMenu.BackColor = Color.White;
            btnUserMenu.Cursor = Cursors.Hand;
            btnUserMenu.FlatAppearance.BorderColor = Color.FromArgb(224, 229, 238);
            btnUserMenu.FlatStyle = FlatStyle.Flat;
            btnUserMenu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUserMenu.ForeColor = Color.FromArgb(30, 41, 59);
            btnUserMenu.Location = new Point(1025, 16);
            btnUserMenu.Margin = new Padding(4);
            btnUserMenu.Name = "btnUserMenu";
            btnUserMenu.Size = new Size(130, 42);
            btnUserMenu.TabIndex = 0;
            btnUserMenu.Text = "Admin";
            btnUserMenu.UseVisualStyleBackColor = false;
            btnUserMenu.Click += btnUserMenu_Click;
            // 
            // pnlHeaderLine
            // 
            pnlHeaderLine.BackColor = Color.FromArgb(224, 229, 238);
            pnlHeaderLine.Dock = DockStyle.Bottom;
            pnlHeaderLine.Location = new Point(0, 74);
            pnlHeaderLine.Margin = new Padding(4);
            pnlHeaderLine.Name = "pnlHeaderLine";
            pnlHeaderLine.Size = new Size(1175, 1);
            pnlHeaderLine.TabIndex = 1;
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.White;
            pnlSidebar.Controls.Add(flowLayoutPanel1);
            pnlSidebar.Controls.Add(pnlSidebarFooter);
            pnlSidebar.Controls.Add(pnlBrand);
            pnlSidebar.Controls.Add(pnlSidebarLine);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(4);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(325, 875);
            pnlSidebar.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.Controls.Add(btnHangHoa);
            flowLayoutPanel1.Controls.Add(btnKhachHang);
            flowLayoutPanel1.Controls.Add(btnNhanVien);
            flowLayoutPanel1.Controls.Add(btnNhapKho);
            flowLayoutPanel1.Controls.Add(btnXuatKho);
            flowLayoutPanel1.Controls.Add(btnDanhMuc);
            flowLayoutPanel1.Controls.Add(btnThongKe);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 150);
            flowLayoutPanel1.Margin = new Padding(4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(25, 10, 25, 10);
            flowLayoutPanel1.Size = new Size(324, 587);
            flowLayoutPanel1.TabIndex = 0;
            flowLayoutPanel1.WrapContents = false;
            // 
            // btnHangHoa
            // 
            btnHangHoa.BackColor = Color.FromArgb(30, 112, 235);
            btnHangHoa.Cursor = Cursors.Hand;
            btnHangHoa.FlatAppearance.BorderSize = 0;
            btnHangHoa.FlatStyle = FlatStyle.Flat;
            btnHangHoa.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnHangHoa.ForeColor = Color.White;
            btnHangHoa.Location = new Point(25, 10);
            btnHangHoa.Margin = new Padding(0, 0, 0, 12);
            btnHangHoa.Name = "btnHangHoa";
            btnHangHoa.Padding = new Padding(28, 0, 0, 0);
            btnHangHoa.Size = new Size(275, 55);
            btnHangHoa.TabIndex = 0;
            btnHangHoa.Text = "Hàng Hóa";
            btnHangHoa.TextAlign = ContentAlignment.MiddleLeft;
            btnHangHoa.UseVisualStyleBackColor = false;
            btnHangHoa.Click += btnHangHoa_Click;
            // 
            // btnKhachHang
            // 
            btnKhachHang.BackColor = Color.White;
            btnKhachHang.Cursor = Cursors.Hand;
            btnKhachHang.FlatAppearance.BorderSize = 0;
            btnKhachHang.FlatStyle = FlatStyle.Flat;
            btnKhachHang.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnKhachHang.ForeColor = Color.FromArgb(43, 54, 73);
            btnKhachHang.Location = new Point(25, 77);
            btnKhachHang.Margin = new Padding(0, 0, 0, 12);
            btnKhachHang.Name = "btnKhachHang";
            btnKhachHang.Padding = new Padding(28, 0, 0, 0);
            btnKhachHang.Size = new Size(275, 55);
            btnKhachHang.TabIndex = 1;
            btnKhachHang.Text = "Khách Hàng";
            btnKhachHang.TextAlign = ContentAlignment.MiddleLeft;
            btnKhachHang.UseVisualStyleBackColor = false;
            btnKhachHang.Click += btnKhachHang_Click;
            // 
            // btnNhanVien
            // 
            btnNhanVien.BackColor = Color.White;
            btnNhanVien.Cursor = Cursors.Hand;
            btnNhanVien.FlatAppearance.BorderSize = 0;
            btnNhanVien.FlatStyle = FlatStyle.Flat;
            btnNhanVien.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNhanVien.ForeColor = Color.FromArgb(43, 54, 73);
            btnNhanVien.Location = new Point(25, 144);
            btnNhanVien.Margin = new Padding(0, 0, 0, 12);
            btnNhanVien.Name = "btnNhanVien";
            btnNhanVien.Padding = new Padding(28, 0, 0, 0);
            btnNhanVien.Size = new Size(275, 55);
            btnNhanVien.TabIndex = 2;
            btnNhanVien.Text = "Nhân Viên";
            btnNhanVien.TextAlign = ContentAlignment.MiddleLeft;
            btnNhanVien.UseVisualStyleBackColor = false;
            btnNhanVien.Click += btnNhanVien_Click;
            // 
            // btnNhapKho
            // 
            btnNhapKho.BackColor = Color.White;
            btnNhapKho.Cursor = Cursors.Hand;
            btnNhapKho.FlatAppearance.BorderSize = 0;
            btnNhapKho.FlatStyle = FlatStyle.Flat;
            btnNhapKho.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNhapKho.ForeColor = Color.FromArgb(43, 54, 73);
            btnNhapKho.Location = new Point(25, 211);
            btnNhapKho.Margin = new Padding(0, 0, 0, 12);
            btnNhapKho.Name = "btnNhapKho";
            btnNhapKho.Padding = new Padding(28, 0, 0, 0);
            btnNhapKho.Size = new Size(275, 55);
            btnNhapKho.TabIndex = 3;
            btnNhapKho.Text = "Nhập Kho";
            btnNhapKho.TextAlign = ContentAlignment.MiddleLeft;
            btnNhapKho.UseVisualStyleBackColor = false;
            btnNhapKho.Click += btnNhapKho_Click;
            // 
            // btnXuatKho
            // 
            btnXuatKho.BackColor = Color.White;
            btnXuatKho.Cursor = Cursors.Hand;
            btnXuatKho.FlatAppearance.BorderSize = 0;
            btnXuatKho.FlatStyle = FlatStyle.Flat;
            btnXuatKho.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnXuatKho.ForeColor = Color.FromArgb(43, 54, 73);
            btnXuatKho.Location = new Point(25, 278);
            btnXuatKho.Margin = new Padding(0, 0, 0, 12);
            btnXuatKho.Name = "btnXuatKho";
            btnXuatKho.Padding = new Padding(28, 0, 0, 0);
            btnXuatKho.Size = new Size(275, 55);
            btnXuatKho.TabIndex = 4;
            btnXuatKho.Text = "Xuất Kho";
            btnXuatKho.TextAlign = ContentAlignment.MiddleLeft;
            btnXuatKho.UseVisualStyleBackColor = false;
            btnXuatKho.Click += btnXuatKho_Click;
            // 
            // btnDanhMuc
            // 
            btnDanhMuc.BackColor = Color.White;
            btnDanhMuc.Cursor = Cursors.Hand;
            btnDanhMuc.FlatAppearance.BorderSize = 0;
            btnDanhMuc.FlatStyle = FlatStyle.Flat;
            btnDanhMuc.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDanhMuc.ForeColor = Color.FromArgb(43, 54, 73);
            btnDanhMuc.Location = new Point(25, 345);
            btnDanhMuc.Margin = new Padding(0, 0, 0, 12);
            btnDanhMuc.Name = "btnDanhMuc";
            btnDanhMuc.Padding = new Padding(28, 0, 0, 0);
            btnDanhMuc.Size = new Size(275, 55);
            btnDanhMuc.TabIndex = 5;
            btnDanhMuc.Text = "Danh Mục";
            btnDanhMuc.TextAlign = ContentAlignment.MiddleLeft;
            btnDanhMuc.UseVisualStyleBackColor = false;
            btnDanhMuc.Click += btnDanhMuc_Click;
            // 
            // btnThongKe
            // 
            btnThongKe.BackColor = Color.White;
            btnThongKe.Cursor = Cursors.Hand;
            btnThongKe.FlatAppearance.BorderSize = 0;
            btnThongKe.FlatStyle = FlatStyle.Flat;
            btnThongKe.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThongKe.ForeColor = Color.FromArgb(43, 54, 73);
            btnThongKe.Location = new Point(25, 412);
            btnThongKe.Margin = new Padding(0, 0, 0, 12);
            btnThongKe.Name = "btnThongKe";
            btnThongKe.Padding = new Padding(28, 0, 0, 0);
            btnThongKe.Size = new Size(275, 55);
            btnThongKe.TabIndex = 6;
            btnThongKe.Text = "Thống Kê";
            btnThongKe.TextAlign = ContentAlignment.MiddleLeft;
            btnThongKe.UseVisualStyleBackColor = false;
            btnThongKe.Click += btnThongKe_Click;
            // 
            // pnlSidebarFooter
            // 
            pnlSidebarFooter.BackColor = Color.White;
            pnlSidebarFooter.Controls.Add(lblVersion);
            pnlSidebarFooter.Dock = DockStyle.Bottom;
            pnlSidebarFooter.Location = new Point(0, 737);
            pnlSidebarFooter.Margin = new Padding(4);
            pnlSidebarFooter.Name = "pnlSidebarFooter";
            pnlSidebarFooter.Size = new Size(324, 138);
            pnlSidebarFooter.TabIndex = 1;
            // 
            // lblVersion
            // 
            lblVersion.BackColor = Color.FromArgb(248, 250, 253);
            lblVersion.BorderStyle = BorderStyle.FixedSingle;
            lblVersion.Font = new Font("Segoe UI", 8.5F);
            lblVersion.ForeColor = Color.FromArgb(105, 116, 135);
            lblVersion.Location = new Point(45, 45);
            lblVersion.Margin = new Padding(4, 0, 4, 0);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new Size(193, 56);
            lblVersion.TabIndex = 0;
            lblVersion.Text = "Phiên bản 1.0\r\n© 2026 Quản Lý Kho Hàng";
            lblVersion.TextAlign = ContentAlignment.MiddleCenter;
            lblVersion.Click += lblVersion_Click;
            // 
            // pnlBrand
            // 
            pnlBrand.BackColor = Color.White;
            pnlBrand.Controls.Add(lblBrandIcon);
            pnlBrand.Controls.Add(lblBrandText);
            pnlBrand.Controls.Add(btnToggleMenu);
            pnlBrand.Dock = DockStyle.Top;
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Margin = new Padding(4);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.Size = new Size(324, 150);
            pnlBrand.TabIndex = 2;
            // 
            // lblBrandIcon
            // 
            lblBrandIcon.BackColor = Color.FromArgb(30, 112, 235);
            lblBrandIcon.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblBrandIcon.ForeColor = Color.White;
            lblBrandIcon.Location = new Point(30, 15);
            lblBrandIcon.Margin = new Padding(4, 0, 4, 0);
            lblBrandIcon.Name = "lblBrandIcon";
            lblBrandIcon.Size = new Size(60, 60);
            lblBrandIcon.TabIndex = 0;
            lblBrandIcon.Text = "QL";
            lblBrandIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblBrandText
            // 
            lblBrandText.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBrandText.ForeColor = Color.FromArgb(30, 112, 235);
            lblBrandText.Location = new Point(105, 20);
            lblBrandText.Margin = new Padding(4, 0, 4, 0);
            lblBrandText.Name = "lblBrandText";
            lblBrandText.Size = new Size(200, 78);
            lblBrandText.TabIndex = 1;
            lblBrandText.Text = "Quản Lý\r\nKho Hàng";
            lblBrandText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnToggleMenu
            // 
            btnToggleMenu.AccessibleDescription = "Thu gọn hoặc mở rộng thanh menu bên trái";
            btnToggleMenu.AccessibleName = "Thu gọn menu";
            btnToggleMenu.BackColor = Color.FromArgb(239, 246, 255);
            btnToggleMenu.Cursor = Cursors.Hand;
            btnToggleMenu.FlatAppearance.BorderColor = Color.FromArgb(30, 112, 235);
            btnToggleMenu.FlatAppearance.MouseDownBackColor = Color.FromArgb(219, 234, 254);
            btnToggleMenu.FlatAppearance.MouseOverBackColor = Color.FromArgb(239, 246, 255);
            btnToggleMenu.FlatStyle = FlatStyle.Flat;
            btnToggleMenu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnToggleMenu.ForeColor = Color.FromArgb(30, 112, 235);
            btnToggleMenu.Location = new Point(24, 92);
            btnToggleMenu.Margin = new Padding(4);
            btnToggleMenu.Name = "btnToggleMenu";
            btnToggleMenu.Size = new Size(50, 50);
            btnToggleMenu.TabIndex = 2;
            btnToggleMenu.UseVisualStyleBackColor = false;
            btnToggleMenu.Click += btnToggleMenu_Click;
            // 
            // pnlSidebarLine
            // 
            pnlSidebarLine.BackColor = Color.FromArgb(224, 229, 238);
            pnlSidebarLine.Dock = DockStyle.Right;
            pnlSidebarLine.Location = new Point(324, 0);
            pnlSidebarLine.Margin = new Padding(4);
            pnlSidebarLine.Name = "pnlSidebarLine";
            pnlSidebarLine.Size = new Size(1, 875);
            pnlSidebarLine.TabIndex = 3;
            // 
            // cmsUser
            // 
            cmsUser.BackColor = Color.White;
            cmsUser.Font = new Font("Segoe UI", 9F);
            cmsUser.ImageScalingSize = new Size(16, 16);
            cmsUser.Items.AddRange(new ToolStripItem[] { menuTaiKhoan, menuChucVu, menuDangXuat });
            cmsUser.Name = "cmsUser";
            cmsUser.Size = new Size(220, 104);
            // 
            // menuTaiKhoan
            // 
            menuTaiKhoan.Name = "menuTaiKhoan";
            menuTaiKhoan.Size = new Size(219, 32);
            menuTaiKhoan.Text = "Tai khoan";
            // 
            // menuChucVu
            // 
            menuChucVu.Name = "menuChucVu";
            menuChucVu.Size = new Size(219, 32);
            menuChucVu.Text = "Quyen han";
            // 
            // menuDangXuat
            // 
            menuDangXuat.Name = "menuDangXuat";
            menuDangXuat.BackColor = Color.FromArgb(254, 242, 242);
            menuDangXuat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            menuDangXuat.ForeColor = Color.FromArgb(185, 28, 28);
            menuDangXuat.Size = new Size(219, 32);
            menuDangXuat.Text = "Đăng xuất";
            menuDangXuat.Click += menuDangXuat_Click;
            // 
            // sidebarTimer
            // 
            sidebarTimer.Interval = 15;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1500, 875);
            Controls.Add(pnlShell);
            Margin = new Padding(4);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Quản Lý Kho Hàng - Dashboard";
            Load += FrmMain_Load;
            pnlShell.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlSidebar.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            pnlSidebarFooter.ResumeLayout(false);
            pnlBrand.ResumeLayout(false);
            cmsUser.ResumeLayout(false);
            ResumeLayout(false);
        }
        private ToolStripMenuItem menuTaiKhoan;
        private ToolStripMenuItem menuChucVu;
        private ToolStripMenuItem menuDangXuat;
    }
}
