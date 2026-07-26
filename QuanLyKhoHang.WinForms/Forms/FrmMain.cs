using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form chính sau khi đăng nhập.
    /// Form này điều phối menu trái, phân quyền theo UserSession và nhúng các form nghiệp vụ vào vùng nội dung.
    /// </summary>
    public partial class FrmMain : Form
    {
        // Form con hiện tại đang được nhúng trong panel nội dung.
        private Form activeForm = null;
        private readonly Color activeMenuBackColor = Color.FromArgb(30, 112, 235);
        private readonly Color inactiveMenuBackColor = Color.White;
        private readonly Color inactiveMenuTextColor = Color.FromArgb(43, 54, 73);
        private const int SidebarExpandedWidth = 260;
        private const int SidebarCollapsedWidth = 76;
        private bool sidebarCollapsed;
        private bool sidebarTargetCollapsed;
        private Button activeMenuButton;
        private Bitmap toggleMenuIcon;

        private enum SidebarIcon
        {
            Products,
            Customer,
            Employees,
            Import,
            Export,
            Categories,
            Statistics
        }

        /// <summary>
        /// Khởi tạo form chính và mở ở chế độ toàn màn hình.
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();

            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            InitializeSidebarNavigation();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Khởi tạo thông tin hiển thị, biểu tượng và hỗ trợ truy cập cho menu bên trái.
        /// </summary>
        private void InitializeSidebarNavigation()
        {
            // Dùng bitmap tự vẽ để không phụ thuộc font hỗ trợ ký tự "☰".
            toggleMenuIcon = CreateHamburgerIcon(Color.FromArgb(30, 112, 235));
            btnToggleMenu.Image = toggleMenuIcon;
            btnToggleMenu.ImageAlign = ContentAlignment.MiddleCenter;

            foreach ((Button button, string text, SidebarIcon icon) in GetSidebarItems())
            {
                button.Tag = text;
                button.AccessibleName = text;
                button.TextImageRelation = TextImageRelation.ImageBeforeText;
            }

            sidebarCollapsed = false;
            sidebarTargetCollapsed = false;
            ApplySidebarState(false);
        }

        /// <summary>
        /// Danh sách nút cùng tên và loại biểu tượng tương ứng.
        /// </summary>
        private (Button Button, string Text, SidebarIcon Icon)[] GetSidebarItems()
        {
            return new[]
            {
                (btnHangHoa, "Hàng Hóa", SidebarIcon.Products),
                (btnKhachHang, "Khách Hàng", SidebarIcon.Customer),
                (btnNhanVien, "Nhân Viên", SidebarIcon.Employees),
                (btnNhapKho, "Nhập Kho", SidebarIcon.Import),
                (btnXuatKho, "Xuất Kho", SidebarIcon.Export),
                (btnDanhMuc, "Danh Mục", SidebarIcon.Categories),
                (btnThongKe, "Thống Kê", SidebarIcon.Statistics)
            };
        }

        /// <summary>
        /// Bắt đầu thu gọn hoặc mở rộng sidebar.
        /// </summary>
        private void btnToggleMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Stop();
            sidebarTargetCollapsed = !sidebarCollapsed;
            sidebarCollapsed = sidebarTargetCollapsed;
            ApplySidebarState(sidebarCollapsed);
            btnToggleMenu.Enabled = true;
            pnlContent.PerformLayout();
        }

        /// <summary>
        /// Áp dụng trạng thái cuối cùng của sidebar sau khi hiệu ứng hoàn tất.
        /// </summary>
        private void ApplySidebarState(bool collapsed)
        {
            pnlSidebar.Width = collapsed ? SidebarCollapsedWidth : SidebarExpandedWidth;
            ApplySidebarPresentation(collapsed);
        }

        /// <summary>
        /// Căn chỉnh logo, nút menu, tooltip và phần chân sidebar theo trạng thái hiện tại.
        /// </summary>
        private void ApplySidebarPresentation(bool collapsed)
        {
            lblBrandText.Visible = !collapsed;
            lblVersion.Visible = !collapsed;
            pnlSidebarFooter.Visible = !collapsed;

            lblBrandIcon.Location = collapsed
                ? new Point(14, 12)
                : new Point(24, 12);

            btnToggleMenu.Location = collapsed
                ? new Point(18, 74)
                : new Point(19, 74);
            btnToggleMenu.Visible = true;
            btnToggleMenu.Text = string.Empty;
            btnToggleMenu.AccessibleName = collapsed ? "Mở rộng menu" : "Thu gọn menu";
            sidebarToolTip.SetToolTip(
                btnToggleMenu,
                collapsed ? "Mở rộng thanh menu" : "Thu gọn thanh menu");

            flowLayoutPanel1.Padding = collapsed
                ? new Padding(12, 8, 12, 8)
                : new Padding(20, 8, 20, 8);

            foreach ((Button button, string text, SidebarIcon _) in GetSidebarItems())
            {
                button.Size = collapsed ? new Size(52, 44) : new Size(220, 44);
                button.Padding = collapsed ? Padding.Empty : new Padding(14, 0, 10, 0);
                button.Text = collapsed ? string.Empty : text;
                button.TextAlign = collapsed
                    ? ContentAlignment.MiddleCenter
                    : ContentAlignment.MiddleLeft;
                button.ImageAlign = collapsed
                    ? ContentAlignment.MiddleCenter
                    : ContentAlignment.MiddleLeft;
                sidebarToolTip.SetToolTip(button, collapsed ? text : string.Empty);
            }

            pnlBrand.Controls.SetChildIndex(btnToggleMenu, 0);
            btnToggleMenu.BringToFront();
            RefreshSidebarIcons();
        }

        /// <summary>
        /// Tạo biểu tượng hamburger bằng GDI+ để biểu tượng luôn hiển thị ổn định trên mọi máy.
        /// </summary>
        private static Bitmap CreateHamburgerIcon(Color color)
        {
            Bitmap bitmap = new Bitmap(26, 24);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using Pen pen = new Pen(color, 2.4F)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round
            };

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.DrawLine(pen, 3, 5, 23, 5);
            graphics.DrawLine(pen, 3, 12, 23, 12);
            graphics.DrawLine(pen, 3, 19, 23, 19);

            return bitmap;
        }

        /// <summary>
        /// Vẽ lại icon để luôn có độ tương phản đúng với nút đang được chọn.
        /// </summary>
        private void RefreshSidebarIcons()
        {
            foreach ((Button button, string _, SidebarIcon icon) in GetSidebarItems())
            {
                Color iconColor = button == activeMenuButton
                    ? Color.White
                    : inactiveMenuTextColor;
                Image oldImage = button.Image;
                button.Image = CreateSidebarIcon(icon, iconColor);
                oldImage?.Dispose();
            }
        }

        /// <summary>
        /// Tạo icon nét đơn giản bằng GDI+, không phụ thuộc emoji hoặc font ngoài.
        /// </summary>
        private static Bitmap CreateSidebarIcon(SidebarIcon icon, Color color)
        {
            Bitmap bitmap = new Bitmap(26, 24);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using Pen pen = new Pen(color, 1.9F)
            {
                StartCap = System.Drawing.Drawing2D.LineCap.Round,
                EndCap = System.Drawing.Drawing2D.LineCap.Round,
                LineJoin = System.Drawing.Drawing2D.LineJoin.Round
            };
            using SolidBrush brush = new SolidBrush(color);

            switch (icon)
            {
                case SidebarIcon.Products:
                    graphics.DrawRectangle(pen, 4, 7, 16, 13);
                    graphics.DrawLine(pen, 4, 7, 12, 3);
                    graphics.DrawLine(pen, 12, 3, 20, 7);
                    graphics.DrawLine(pen, 12, 3, 12, 20);
                    break;

                case SidebarIcon.Customer:
                    graphics.DrawEllipse(pen, 9, 3, 6, 6);
                    graphics.DrawArc(pen, 5, 10, 14, 11, 195, 150);
                    break;

                case SidebarIcon.Employees:
                    graphics.DrawEllipse(pen, 5, 5, 5, 5);
                    graphics.DrawEllipse(pen, 14, 5, 5, 5);
                    graphics.DrawArc(pen, 2, 11, 11, 9, 195, 150);
                    graphics.DrawArc(pen, 11, 11, 11, 9, 195, 150);
                    break;

                case SidebarIcon.Import:
                    graphics.DrawLine(pen, 12, 3, 12, 15);
                    graphics.DrawLine(pen, 8, 11, 12, 15);
                    graphics.DrawLine(pen, 16, 11, 12, 15);
                    graphics.DrawLine(pen, 4, 19, 20, 19);
                    graphics.DrawLine(pen, 4, 19, 4, 16);
                    graphics.DrawLine(pen, 20, 19, 20, 16);
                    break;

                case SidebarIcon.Export:
                    graphics.DrawLine(pen, 12, 15, 12, 3);
                    graphics.DrawLine(pen, 8, 7, 12, 3);
                    graphics.DrawLine(pen, 16, 7, 12, 3);
                    graphics.DrawLine(pen, 4, 19, 20, 19);
                    graphics.DrawLine(pen, 4, 19, 4, 16);
                    graphics.DrawLine(pen, 20, 19, 20, 16);
                    break;

                case SidebarIcon.Categories:
                    Point[] folderPoints =
                    {
                        new Point(3, 7),
                        new Point(10, 7),
                        new Point(12, 10),
                        new Point(21, 10),
                        new Point(20, 20),
                        new Point(3, 20)
                    };
                    graphics.DrawPolygon(pen, folderPoints);
                    break;

                case SidebarIcon.Statistics:
                    graphics.FillRectangle(brush, 4, 13, 4, 7);
                    graphics.FillRectangle(brush, 10, 9, 4, 11);
                    graphics.FillRectangle(brush, 16, 4, 4, 16);
                    break;
            }

            return bitmap;
        }

        /// <summary>
        /// Giải phóng các bitmap icon được tạo trong lúc chạy.
        /// </summary>
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            foreach ((Button button, string _, SidebarIcon _) in GetSidebarItems())
            {
                button.Image?.Dispose();
                button.Image = null;
            }

            btnToggleMenu.Image = null;
            toggleMenuIcon?.Dispose();

            base.OnFormClosed(e);
        }

        /// <summary>
        /// Khi form chính mở: phân quyền menu, hiển thị thông tin tài khoản và mở mặc định form hàng hóa.
        /// </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            string vaiTroHienTai = UserSession.VaiTro;
            string taiKhoanHienTai = UserSession.TenTaiKhoan;

            if (vaiTroHienTai == "NhanVien")
            {
                // Nhân viên không được vào màn hình quản lý nhân sự.
                btnNhanVien.Visible = false;
                btnDanhMuc.Visible = false;
                btnThongKe.Visible = true;

                // Tách quyền chi tiết theo tài khoản mẫu.
                if (taiKhoanHienTai == "nhanvienkho")
                {
                    btnXuatKho.Visible = false;
                    btnNhapKho.Visible = true;
                }
                else if (taiKhoanHienTai == "nhanvienbanhang")
                {
                    btnNhapKho.Visible = false;
                    btnXuatKho.Visible = true;
                }
            }
            else
            {
                // Admin được xem toàn bộ chức năng.
                btnNhanVien.Visible = true;
                btnHangHoa.Visible = true;
                btnKhachHang.Visible = true;
                btnNhapKho.Visible = true;
                btnXuatKho.Visible = true;
                btnDanhMuc.Visible = true;
                btnThongKe.Visible = true;
            }

            btnUserMenu.Text = UserSession.TenTaiKhoan;
            cmsUser.Items[0].Text = $"Tài khoản: {UserSession.TenTaiKhoan}";
            cmsUser.Items[1].Text = $"Quyền hạn: {UserSession.VaiTro}";

            menuDangXuat.Text = "Đăng xuất";
            menuTaiKhoan.Enabled = false;
            menuChucVu.Enabled = false;

            btnHangHoa_Click(this, EventArgs.Empty);
        }

        /// <summary>
        /// Nhúng form con vào panel nội dung.
        /// Mỗi lần mở form mới sẽ đóng form cũ để tránh chồng giao diện và giữ bộ nhớ gọn.
        /// </summary>
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        /// <summary>
        /// Đăng xuất tài khoản hiện tại, xóa session và quay lại màn hình đăng nhập.
        /// </summary>
        private void menuDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất tài khoản hiện tại để chuyển đổi không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UserSession.TenTaiKhoan = string.Empty;
                UserSession.VaiTro = string.Empty;

                Hide();

                FrmDangNhap loginForm = new FrmDangNhap();
                loginForm.ShowDialog();

                Close();
            }
        }

        /// <summary>
        /// Hiển thị menu tài khoản tại vị trí nút người dùng.
        /// </summary>
        private void btnUserMenu_Click(object sender, EventArgs e)
        {
            int x = Math.Min(0, btnUserMenu.Width - cmsUser.Width);
            cmsUser.Show(btnUserMenu, new System.Drawing.Point(x, btnUserMenu.Height));
        }

        /// <summary>
        /// Đổi màu nút menu đang được chọn và trả các nút còn lại về trạng thái thường.
        /// </summary>
        private void SetActiveMenu(Button activeButton)
        {
            activeMenuButton = activeButton;
            Button[] menuButtons = { btnHangHoa, btnKhachHang, btnNhanVien, btnNhapKho, btnXuatKho, btnDanhMuc, btnThongKe };
            foreach (Button button in menuButtons)
            {
                button.BackColor = button == activeButton ? activeMenuBackColor : inactiveMenuBackColor;
                button.ForeColor = button == activeButton ? Color.White : inactiveMenuTextColor;
            }

            RefreshSidebarIcons();
        }

        /// <summary>Mở form quản lý hàng hóa.</summary>
        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnHangHoa);
            OpenChildForm(new FrmHangHoa(UserSession.VaiTro));
        }

        /// <summary>Mở trang quản lý nhà cung cấp và loại hàng.</summary>
        private void btnDanhMuc_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnDanhMuc);
            OpenChildForm(new FrmDanhMuc());
        }

        /// <summary>Mở trang thống kê tổng quan của hệ thống.</summary>
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnThongKe);
            OpenChildForm(new FrmThongKe());
        }


        /// <summary>Mở form quản lý khách hàng.</summary>
        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnKhachHang);
            OpenChildForm(new FrmKhachHang(UserSession.VaiTro));
        }

        /// <summary>Mở form quản lý nhân viên.</summary>
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnNhanVien);
            OpenChildForm(new FrmNhanVien());
        }

        /// <summary>Mở form lập phiếu nhập kho.</summary>
        private void btnNhapKho_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnNhapKho);
            OpenChildForm(new FrmNhapKho(UserSession.VaiTro));
        }

        /// <summary>Mở form lập phiếu xuất kho.</summary>
        private void btnXuatKho_Click(object sender, EventArgs e)
        {
            SetActiveMenu(btnXuatKho);
            OpenChildForm(new FrmXuatKho(UserSession.VaiTro));
        }

        /// <summary>
        /// Xu ly khi nguoi dung bam vao label phien ban; hien tai chua co thao tac can thuc hien.
        /// </summary>
        private void lblVersion_Click(object sender, EventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
