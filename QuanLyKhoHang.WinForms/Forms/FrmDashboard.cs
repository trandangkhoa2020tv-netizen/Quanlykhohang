using System.Globalization;
using QuanLyKhoHang.ApiClients;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Trang thống kê tổng quan được nhúng trong vùng nội dung của form chính.
    /// </summary>
    public partial class FrmThongKe : Form
    {
        private static readonly CultureInfo VietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
        private readonly DashboardApiClient _apiClient = new DashboardApiClient();
        private readonly float _regularCardsRowHeight;
        private readonly int _regularPageHeight;
        private bool _isLoading;
        private bool? _compactCards;

        public FrmThongKe()
        {
            InitializeComponent();

            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            _regularCardsRowHeight = tlpPage.RowStyles[1].Height;
            _regularPageHeight = tlpPage.Height;
            ApplySimpleDashboardAppearance();
            ConfigureCustomerGridFormats();
            pnlScroll.Resize += (_, _) => ApplyResponsiveLayout();
            ApplyResponsiveLayout();
        }

        private async void FrmThongKe_Load(object sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            await LoadDashboardAsync();
        }

        private async void btnTaiLai_Click(object sender, EventArgs e)
        {
            await LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;
            btnTaiLai.Enabled = false;
            btnTaiLai.Text = "Đang tải...";
            lblTrangThai.Text = "Đang cập nhật dữ liệu";

            try
            {
                DashboardData data = await Task.Run(_apiClient.GetDashboard);
                if (IsDisposed)
                {
                    return;
                }

                BindDashboard(data ?? new DashboardData());
                lblTrangThai.Text = $"Cập nhật lúc {DateTime.Now:HH:mm}";
            }
            catch (Exception ex)
            {
                if (!IsDisposed)
                {
                    lblTrangThai.Text = "Không thể tải dữ liệu";
                    MessageBox.Show(
                        ex.Message,
                        "Lỗi tải thống kê",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (!IsDisposed)
                {
                    _isLoading = false;
                    btnTaiLai.Enabled = true;
                    btnTaiLai.Text = "Làm mới";
                }
            }
        }

        private void BindDashboard(DashboardData data)
        {
            DashboardSummary summary = data.Summary ?? new DashboardSummary();

            lblHangHoaValue.Text = summary.TongHangHoa.ToString("N0", VietnameseCulture);
            lblKhachHangValue.Text = summary.TongKhachHang.ToString("N0", VietnameseCulture);
            lblNhanVienValue.Text = summary.TongNhanVien.ToString("N0", VietnameseCulture);
            lblGiaTriKhoValue.Text = $"{summary.GiaTriTonKho.ToString("N0", VietnameseCulture)} đ";

            lblHangHoaNote.Text = summary.SoHangSapHet > 0
                ? $"{summary.SoHangSapHet.ToString("N0", VietnameseCulture)} mặt hàng sắp hết"
                : "Tồn kho đang ổn định";
            lblHangHoaNote.ForeColor = summary.SoHangSapHet > 0
                ? Color.FromArgb(220, 38, 38)
                : Color.FromArgb(22, 163, 74);
            lblKhachHangNote.Text = $"{summary.TongXuatKho.ToString("N0", VietnameseCulture)} đơn vị hàng đã xuất";
            lblNhanVienNote.Text = "Nhân sự đang hoạt động";
            lblGiaTriKhoNote.Text =
                $"Nhập {summary.TongNhapKho.ToString("N0", VietnameseCulture)} • " +
                $"Xuất {summary.TongXuatKho.ToString("N0", VietnameseCulture)} đơn vị";

            lblTonKhoSubtitle.Text = summary.SoHangSapHet > 0
                ? $"⚠ {summary.SoHangSapHet.ToString("N0", VietnameseCulture)} sản phẩm sắp hết hàng"
                : "✓ Không có cảnh báo tồn kho";
            lblTonKhoSubtitle.ForeColor = summary.SoHangSapHet > 0
                ? Color.FromArgb(220, 38, 38)
                : Color.FromArgb(22, 163, 74);

            chartNhapXuat.SetMonthlyData(data.NhapXuatTheoThang);
            chartDanhMuc.SetCategoryData(data.HangTheoDanhMuc);
            chartTonKho.SetStockData(data.TonKhoNhieu);
            dgvKhachHang.DataSource = null;
            dgvKhachHang.DataSource = data.KhachHangMuaNhieu ?? new List<DashboardCustomerPoint>();
            dgvKhachHang.ClearSelection();
        }

        private void ConfigureCustomerGridFormats()
        {
            DataGridViewColumn purchaseCountColumn = dgvKhachHang.Columns["colSoLanMua"];
            DataGridViewColumn totalValueColumn = dgvKhachHang.Columns["colTongGiaTri"];

            if (purchaseCountColumn == null || totalValueColumn == null)
            {
                return;
            }

            purchaseCountColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            purchaseCountColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            purchaseCountColumn.DefaultCellStyle.Format = "N0";
            purchaseCountColumn.DefaultCellStyle.FormatProvider = VietnameseCulture;

            totalValueColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalValueColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            totalValueColumn.DefaultCellStyle.Format = "#,##0 'đ'";
            totalValueColumn.DefaultCellStyle.FormatProvider = VietnameseCulture;
        }

        private void ApplySimpleDashboardAppearance()
        {
            BackColor = SystemColors.Control;
            pnlScroll.BackColor = SystemColors.Control;
            tlpPage.BackColor = SystemColors.Control;

            Panel[] summaryPanels = { cardHangHoa, cardKhachHang, cardNhanVien, cardGiaTriKho };
            foreach (Panel panel in summaryPanels)
            {
                panel.BackColor = SystemColors.Control;
                panel.BorderStyle = BorderStyle.FixedSingle;
            }

            pnlAccentHangHoa.Visible = false;
            pnlAccentKhachHang.Visible = false;
            pnlAccentNhanVien.Visible = false;
            pnlAccentGiaTriKho.Visible = false;

            Label[] summaryValues = { lblHangHoaValue, lblKhachHangValue, lblNhanVienValue, lblGiaTriKhoValue };
            foreach (Label label in summaryValues)
            {
                label.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                label.ForeColor = Color.FromArgb(30, 64, 175);
            }

            Panel[] dataPanels = { pnlNhapXuat, pnlDanhMuc, pnlTonKho, pnlKhachHang };
            foreach (Panel panel in dataPanels)
            {
                panel.BackColor = Color.White;
                panel.BorderStyle = BorderStyle.FixedSingle;
            }

            btnTaiLai.BackColor = Color.White;
            btnTaiLai.ForeColor = SystemColors.ControlText;
            btnTaiLai.FlatAppearance.BorderColor = SystemColors.ControlDark;
            btnTaiLai.FlatAppearance.BorderSize = 1;
        }

        private void DashboardPanel_Paint(object sender, PaintEventArgs e)
        {
            if (sender is not Panel panel)
            {
                return;
            }

            using Pen borderPen = new Pen(SystemColors.ControlDark);
            e.Graphics.DrawRectangle(
                borderPen,
                0,
                0,
                Math.Max(0, panel.ClientSize.Width - 1),
                Math.Max(0, panel.ClientSize.Height - 1));
        }

        private void ApplyResponsiveLayout()
        {
            bool useCompactCards = pnlScroll.ClientSize.Width < 1200;
            if (_compactCards == useCompactCards)
            {
                return;
            }

            _compactCards = useCompactCards;
            tlpPage.SuspendLayout();
            tlpCards.SuspendLayout();

            tlpCards.ColumnStyles.Clear();
            tlpCards.RowStyles.Clear();

            if (useCompactCards)
            {
                tlpCards.ColumnCount = 2;
                tlpCards.RowCount = 2;
                tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
                tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));

                tlpCards.SetCellPosition(cardHangHoa, new TableLayoutPanelCellPosition(0, 0));
                tlpCards.SetCellPosition(cardKhachHang, new TableLayoutPanelCellPosition(1, 0));
                tlpCards.SetCellPosition(cardNhanVien, new TableLayoutPanelCellPosition(0, 1));
                tlpCards.SetCellPosition(cardGiaTriKho, new TableLayoutPanelCellPosition(1, 1));

                tlpPage.RowStyles[1].Height = _regularCardsRowHeight * 2F;
                tlpPage.Height = _regularPageHeight + (int)Math.Ceiling(_regularCardsRowHeight);
            }
            else
            {
                tlpCards.ColumnCount = 4;
                tlpCards.RowCount = 1;
                for (int index = 0; index < 4; index++)
                {
                    tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
                }

                tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                tlpCards.SetCellPosition(cardHangHoa, new TableLayoutPanelCellPosition(0, 0));
                tlpCards.SetCellPosition(cardKhachHang, new TableLayoutPanelCellPosition(1, 0));
                tlpCards.SetCellPosition(cardNhanVien, new TableLayoutPanelCellPosition(2, 0));
                tlpCards.SetCellPosition(cardGiaTriKho, new TableLayoutPanelCellPosition(3, 0));

                tlpPage.RowStyles[1].Height = _regularCardsRowHeight;
                tlpPage.Height = _regularPageHeight;
            }

            tlpCards.ResumeLayout(true);
            tlpPage.ResumeLayout(true);
        }
    }
}
