using QuanLyKhoHang.Controls;

namespace QuanLyKhoHang.Forms
{
    partial class FrmThongKe
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlScroll;
        private TableLayoutPanel tlpPage;
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblTrangThai;
        private Button btnTaiLai;
        private TableLayoutPanel tlpCards;
        private Panel cardHangHoa;
        private Panel pnlAccentHangHoa;
        private Label lblHangHoaTitle;
        private Label lblHangHoaValue;
        private Label lblHangHoaNote;
        private Panel cardKhachHang;
        private Panel pnlAccentKhachHang;
        private Label lblKhachHangTitle;
        private Label lblKhachHangValue;
        private Label lblKhachHangNote;
        private Panel cardNhanVien;
        private Panel pnlAccentNhanVien;
        private Label lblNhanVienTitle;
        private Label lblNhanVienValue;
        private Label lblNhanVienNote;
        private Panel cardGiaTriKho;
        private Panel pnlAccentGiaTriKho;
        private Label lblGiaTriKhoTitle;
        private Label lblGiaTriKhoValue;
        private Label lblGiaTriKhoNote;
        private TableLayoutPanel tlpContent;
        private Panel pnlNhapXuat;
        private Label lblNhapXuatTitle;
        private Label lblNhapXuatSubtitle;
        private DashboardChart chartNhapXuat;
        private Panel pnlDanhMuc;
        private Label lblDanhMucTitle;
        private Label lblDanhMucSubtitle;
        private DashboardChart chartDanhMuc;
        private Panel pnlTonKho;
        private Label lblTonKhoTitle;
        private Label lblTonKhoSubtitle;
        private DashboardChart chartTonKho;
        private Panel pnlKhachHang;
        private Label lblKhachHangChartTitle;
        private Label lblKhachHangChartSubtitle;
        private DataGridView dgvKhachHang;
        private DataGridViewTextBoxColumn colTenKhachHang;
        private DataGridViewTextBoxColumn colSoLanMua;
        private DataGridViewTextBoxColumn colTongGiaTri;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlScroll = new Panel();
            tlpPage = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblTrangThai = new Label();
            btnTaiLai = new Button();
            tlpCards = new TableLayoutPanel();
            cardHangHoa = new Panel();
            pnlAccentHangHoa = new Panel();
            lblHangHoaTitle = new Label();
            lblHangHoaValue = new Label();
            lblHangHoaNote = new Label();
            cardKhachHang = new Panel();
            pnlAccentKhachHang = new Panel();
            lblKhachHangTitle = new Label();
            lblKhachHangValue = new Label();
            lblKhachHangNote = new Label();
            cardNhanVien = new Panel();
            pnlAccentNhanVien = new Panel();
            lblNhanVienTitle = new Label();
            lblNhanVienValue = new Label();
            lblNhanVienNote = new Label();
            cardGiaTriKho = new Panel();
            pnlAccentGiaTriKho = new Panel();
            lblGiaTriKhoTitle = new Label();
            lblGiaTriKhoValue = new Label();
            lblGiaTriKhoNote = new Label();
            tlpContent = new TableLayoutPanel();
            pnlNhapXuat = new Panel();
            lblNhapXuatSubtitle = new Label();
            lblNhapXuatTitle = new Label();
            chartNhapXuat = new DashboardChart();
            pnlDanhMuc = new Panel();
            lblDanhMucSubtitle = new Label();
            lblDanhMucTitle = new Label();
            chartDanhMuc = new DashboardChart();
            pnlTonKho = new Panel();
            lblTonKhoSubtitle = new Label();
            lblTonKhoTitle = new Label();
            chartTonKho = new DashboardChart();
            pnlKhachHang = new Panel();
            dgvKhachHang = new DataGridView();
            colTenKhachHang = new DataGridViewTextBoxColumn();
            colSoLanMua = new DataGridViewTextBoxColumn();
            colTongGiaTri = new DataGridViewTextBoxColumn();
            lblKhachHangChartSubtitle = new Label();
            lblKhachHangChartTitle = new Label();
            pnlScroll.SuspendLayout();
            tlpPage.SuspendLayout();
            pnlHeader.SuspendLayout();
            tlpCards.SuspendLayout();
            cardHangHoa.SuspendLayout();
            cardKhachHang.SuspendLayout();
            cardNhanVien.SuspendLayout();
            cardGiaTriKho.SuspendLayout();
            tlpContent.SuspendLayout();
            pnlNhapXuat.SuspendLayout();
            pnlDanhMuc.SuspendLayout();
            pnlTonKho.SuspendLayout();
            pnlKhachHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            SuspendLayout();
            // 
            // pnlScroll
            // 
            pnlScroll.AutoScroll = true;
            pnlScroll.BackColor = Color.FromArgb(248, 250, 252);
            pnlScroll.Controls.Add(tlpPage);
            pnlScroll.Dock = DockStyle.Fill;
            pnlScroll.Location = new Point(0, 0);
            pnlScroll.Name = "pnlScroll";
            pnlScroll.Size = new Size(940, 739);
            pnlScroll.TabIndex = 0;
            // 
            // tlpPage
            // 
            tlpPage.BackColor = Color.FromArgb(248, 250, 252);
            tlpPage.ColumnCount = 1;
            tlpPage.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPage.Controls.Add(pnlHeader, 0, 0);
            tlpPage.Controls.Add(tlpCards, 0, 1);
            tlpPage.Controls.Add(tlpContent, 0, 2);
            tlpPage.Dock = DockStyle.Top;
            tlpPage.Location = new Point(0, 0);
            tlpPage.Name = "tlpPage";
            tlpPage.Padding = new Padding(20, 12, 20, 20);
            tlpPage.RowCount = 3;
            tlpPage.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tlpPage.RowStyles.Add(new RowStyle(SizeType.Absolute, 132F));
            tlpPage.RowStyles.Add(new RowStyle(SizeType.Absolute, 710F));
            tlpPage.Size = new Size(919, 968);
            tlpPage.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTrangThai);
            pnlHeader.Controls.Add(btnTaiLai);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(20, 12);
            pnlHeader.Margin = new Padding(0, 0, 0, 8);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(879, 82);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 58, 138);
            lblTitle.Location = new Point(14, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(361, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THỐNG KÊ TỔNG QUAN";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 9.5F);
            lblSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblSubtitle.Location = new Point(2, 50);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(432, 21);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Theo dõi nhanh tình hình hàng hóa, nhập xuất và khách hàng";
            // 
            // lblTrangThai
            // 
            lblTrangThai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTrangThai.Font = new Font("Segoe UI", 8.5F);
            lblTrangThai.ForeColor = Color.FromArgb(100, 116, 139);
            lblTrangThai.Location = new Point(559, 54);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(180, 22);
            lblTrangThai.TabIndex = 2;
            lblTrangThai.Text = "Đang chờ tải dữ liệu";
            lblTrangThai.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnTaiLai
            // 
            btnTaiLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaiLai.BackColor = Color.FromArgb(37, 99, 235);
            btnTaiLai.Cursor = Cursors.Hand;
            btnTaiLai.FlatAppearance.BorderSize = 0;
            btnTaiLai.FlatStyle = FlatStyle.Flat;
            btnTaiLai.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnTaiLai.ForeColor = Color.White;
            btnTaiLai.Location = new Point(749, 10);
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Size = new Size(112, 38);
            btnTaiLai.TabIndex = 3;
            btnTaiLai.Text = "Làm mới";
            btnTaiLai.UseVisualStyleBackColor = false;
            btnTaiLai.Click += btnTaiLai_Click;
            // 
            // tlpCards
            // 
            tlpCards.ColumnCount = 4;
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlpCards.Controls.Add(cardHangHoa, 0, 0);
            tlpCards.Controls.Add(cardKhachHang, 1, 0);
            tlpCards.Controls.Add(cardNhanVien, 2, 0);
            tlpCards.Controls.Add(cardGiaTriKho, 3, 0);
            tlpCards.Dock = DockStyle.Fill;
            tlpCards.Location = new Point(20, 102);
            tlpCards.Margin = new Padding(0);
            tlpCards.Name = "tlpCards";
            tlpCards.RowCount = 1;
            tlpCards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpCards.Size = new Size(879, 132);
            tlpCards.TabIndex = 1;
            // 
            // cardHangHoa
            // 
            cardHangHoa.BackColor = Color.White;
            cardHangHoa.Controls.Add(pnlAccentHangHoa);
            cardHangHoa.Controls.Add(lblHangHoaTitle);
            cardHangHoa.Controls.Add(lblHangHoaValue);
            cardHangHoa.Controls.Add(lblHangHoaNote);
            cardHangHoa.Dock = DockStyle.Fill;
            cardHangHoa.Location = new Point(6, 6);
            cardHangHoa.Margin = new Padding(6);
            cardHangHoa.Name = "cardHangHoa";
            cardHangHoa.Size = new Size(207, 120);
            cardHangHoa.TabIndex = 0;
            cardHangHoa.Paint += DashboardPanel_Paint;
            // 
            // pnlAccentHangHoa
            // 
            pnlAccentHangHoa.BackColor = Color.FromArgb(37, 99, 235);
            pnlAccentHangHoa.Dock = DockStyle.Left;
            pnlAccentHangHoa.Location = new Point(0, 0);
            pnlAccentHangHoa.Name = "pnlAccentHangHoa";
            pnlAccentHangHoa.Size = new Size(5, 120);
            pnlAccentHangHoa.TabIndex = 0;
            // 
            // lblHangHoaTitle
            // 
            lblHangHoaTitle.AutoSize = true;
            lblHangHoaTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblHangHoaTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblHangHoaTitle.Location = new Point(20, 16);
            lblHangHoaTitle.Name = "lblHangHoaTitle";
            lblHangHoaTitle.Size = new Size(125, 21);
            lblHangHoaTitle.TabIndex = 1;
            lblHangHoaTitle.Text = "Tổng hàng hóa";
            // 
            // lblHangHoaValue
            // 
            lblHangHoaValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblHangHoaValue.AutoEllipsis = true;
            lblHangHoaValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblHangHoaValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblHangHoaValue.Location = new Point(20, 42);
            lblHangHoaValue.Name = "lblHangHoaValue";
            lblHangHoaValue.Size = new Size(172, 39);
            lblHangHoaValue.TabIndex = 2;
            lblHangHoaValue.Text = "—";
            // 
            // lblHangHoaNote
            // 
            lblHangHoaNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblHangHoaNote.AutoEllipsis = true;
            lblHangHoaNote.Font = new Font("Segoe UI", 8.5F);
            lblHangHoaNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblHangHoaNote.Location = new Point(21, 86);
            lblHangHoaNote.Name = "lblHangHoaNote";
            lblHangHoaNote.Size = new Size(171, 20);
            lblHangHoaNote.TabIndex = 3;
            lblHangHoaNote.Text = "Đang tải...";
            // 
            // cardKhachHang
            // 
            cardKhachHang.BackColor = Color.White;
            cardKhachHang.Controls.Add(pnlAccentKhachHang);
            cardKhachHang.Controls.Add(lblKhachHangTitle);
            cardKhachHang.Controls.Add(lblKhachHangValue);
            cardKhachHang.Controls.Add(lblKhachHangNote);
            cardKhachHang.Dock = DockStyle.Fill;
            cardKhachHang.Location = new Point(225, 6);
            cardKhachHang.Margin = new Padding(6);
            cardKhachHang.Name = "cardKhachHang";
            cardKhachHang.Size = new Size(207, 120);
            cardKhachHang.TabIndex = 1;
            cardKhachHang.Paint += DashboardPanel_Paint;
            // 
            // pnlAccentKhachHang
            // 
            pnlAccentKhachHang.BackColor = Color.FromArgb(139, 92, 246);
            pnlAccentKhachHang.Dock = DockStyle.Left;
            pnlAccentKhachHang.Location = new Point(0, 0);
            pnlAccentKhachHang.Name = "pnlAccentKhachHang";
            pnlAccentKhachHang.Size = new Size(5, 120);
            pnlAccentKhachHang.TabIndex = 0;
            // 
            // lblKhachHangTitle
            // 
            lblKhachHangTitle.AutoSize = true;
            lblKhachHangTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblKhachHangTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblKhachHangTitle.Location = new Point(20, 16);
            lblKhachHangTitle.Name = "lblKhachHangTitle";
            lblKhachHangTitle.Size = new Size(142, 21);
            lblKhachHangTitle.TabIndex = 1;
            lblKhachHangTitle.Text = "Tổng khách hàng";
            // 
            // lblKhachHangValue
            // 
            lblKhachHangValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblKhachHangValue.AutoEllipsis = true;
            lblKhachHangValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblKhachHangValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblKhachHangValue.Location = new Point(20, 42);
            lblKhachHangValue.Name = "lblKhachHangValue";
            lblKhachHangValue.Size = new Size(172, 39);
            lblKhachHangValue.TabIndex = 2;
            lblKhachHangValue.Text = "—";
            // 
            // lblKhachHangNote
            // 
            lblKhachHangNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblKhachHangNote.AutoEllipsis = true;
            lblKhachHangNote.Font = new Font("Segoe UI", 8.5F);
            lblKhachHangNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblKhachHangNote.Location = new Point(21, 86);
            lblKhachHangNote.Name = "lblKhachHangNote";
            lblKhachHangNote.Size = new Size(171, 20);
            lblKhachHangNote.TabIndex = 3;
            lblKhachHangNote.Text = "Đang tải...";
            // 
            // cardNhanVien
            // 
            cardNhanVien.BackColor = Color.White;
            cardNhanVien.Controls.Add(pnlAccentNhanVien);
            cardNhanVien.Controls.Add(lblNhanVienTitle);
            cardNhanVien.Controls.Add(lblNhanVienValue);
            cardNhanVien.Controls.Add(lblNhanVienNote);
            cardNhanVien.Dock = DockStyle.Fill;
            cardNhanVien.Location = new Point(444, 6);
            cardNhanVien.Margin = new Padding(6);
            cardNhanVien.Name = "cardNhanVien";
            cardNhanVien.Size = new Size(207, 120);
            cardNhanVien.TabIndex = 2;
            cardNhanVien.Paint += DashboardPanel_Paint;
            // 
            // pnlAccentNhanVien
            // 
            pnlAccentNhanVien.BackColor = Color.FromArgb(16, 185, 129);
            pnlAccentNhanVien.Dock = DockStyle.Left;
            pnlAccentNhanVien.Location = new Point(0, 0);
            pnlAccentNhanVien.Name = "pnlAccentNhanVien";
            pnlAccentNhanVien.Size = new Size(5, 120);
            pnlAccentNhanVien.TabIndex = 0;
            // 
            // lblNhanVienTitle
            // 
            lblNhanVienTitle.AutoSize = true;
            lblNhanVienTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNhanVienTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblNhanVienTitle.Location = new Point(20, 16);
            lblNhanVienTitle.Name = "lblNhanVienTitle";
            lblNhanVienTitle.Size = new Size(129, 21);
            lblNhanVienTitle.TabIndex = 1;
            lblNhanVienTitle.Text = "Tổng nhân viên";
            // 
            // lblNhanVienValue
            // 
            lblNhanVienValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblNhanVienValue.AutoEllipsis = true;
            lblNhanVienValue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblNhanVienValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblNhanVienValue.Location = new Point(20, 42);
            lblNhanVienValue.Name = "lblNhanVienValue";
            lblNhanVienValue.Size = new Size(172, 39);
            lblNhanVienValue.TabIndex = 2;
            lblNhanVienValue.Text = "—";
            // 
            // lblNhanVienNote
            // 
            lblNhanVienNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblNhanVienNote.AutoEllipsis = true;
            lblNhanVienNote.Font = new Font("Segoe UI", 8.5F);
            lblNhanVienNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblNhanVienNote.Location = new Point(21, 86);
            lblNhanVienNote.Name = "lblNhanVienNote";
            lblNhanVienNote.Size = new Size(171, 20);
            lblNhanVienNote.TabIndex = 3;
            lblNhanVienNote.Text = "Đang tải...";
            // 
            // cardGiaTriKho
            // 
            cardGiaTriKho.BackColor = Color.White;
            cardGiaTriKho.Controls.Add(pnlAccentGiaTriKho);
            cardGiaTriKho.Controls.Add(lblGiaTriKhoTitle);
            cardGiaTriKho.Controls.Add(lblGiaTriKhoValue);
            cardGiaTriKho.Controls.Add(lblGiaTriKhoNote);
            cardGiaTriKho.Dock = DockStyle.Fill;
            cardGiaTriKho.Location = new Point(663, 6);
            cardGiaTriKho.Margin = new Padding(6);
            cardGiaTriKho.Name = "cardGiaTriKho";
            cardGiaTriKho.Size = new Size(210, 120);
            cardGiaTriKho.TabIndex = 3;
            cardGiaTriKho.Paint += DashboardPanel_Paint;
            // 
            // pnlAccentGiaTriKho
            // 
            pnlAccentGiaTriKho.BackColor = Color.FromArgb(245, 158, 11);
            pnlAccentGiaTriKho.Dock = DockStyle.Left;
            pnlAccentGiaTriKho.Location = new Point(0, 0);
            pnlAccentGiaTriKho.Name = "pnlAccentGiaTriKho";
            pnlAccentGiaTriKho.Size = new Size(5, 120);
            pnlAccentGiaTriKho.TabIndex = 0;
            // 
            // lblGiaTriKhoTitle
            // 
            lblGiaTriKhoTitle.AutoSize = true;
            lblGiaTriKhoTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblGiaTriKhoTitle.ForeColor = Color.FromArgb(71, 85, 105);
            lblGiaTriKhoTitle.Location = new Point(20, 16);
            lblGiaTriKhoTitle.Name = "lblGiaTriKhoTitle";
            lblGiaTriKhoTitle.Size = new Size(119, 21);
            lblGiaTriKhoTitle.TabIndex = 1;
            lblGiaTriKhoTitle.Text = "Giá trị tồn kho";
            // 
            // lblGiaTriKhoValue
            // 
            lblGiaTriKhoValue.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblGiaTriKhoValue.AutoEllipsis = true;
            lblGiaTriKhoValue.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            lblGiaTriKhoValue.ForeColor = Color.FromArgb(15, 23, 42);
            lblGiaTriKhoValue.Location = new Point(20, 42);
            lblGiaTriKhoValue.Name = "lblGiaTriKhoValue";
            lblGiaTriKhoValue.Size = new Size(175, 39);
            lblGiaTriKhoValue.TabIndex = 2;
            lblGiaTriKhoValue.Text = "—";
            // 
            // lblGiaTriKhoNote
            // 
            lblGiaTriKhoNote.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblGiaTriKhoNote.AutoEllipsis = true;
            lblGiaTriKhoNote.Font = new Font("Segoe UI", 8.5F);
            lblGiaTriKhoNote.ForeColor = Color.FromArgb(100, 116, 139);
            lblGiaTriKhoNote.Location = new Point(21, 86);
            lblGiaTriKhoNote.Name = "lblGiaTriKhoNote";
            lblGiaTriKhoNote.Size = new Size(174, 20);
            lblGiaTriKhoNote.TabIndex = 3;
            lblGiaTriKhoNote.Text = "Đang tải...";
            // 
            // tlpContent
            // 
            tlpContent.ColumnCount = 2;
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpContent.Controls.Add(pnlNhapXuat, 0, 0);
            tlpContent.Controls.Add(pnlDanhMuc, 1, 0);
            tlpContent.Controls.Add(pnlTonKho, 0, 1);
            tlpContent.Controls.Add(pnlKhachHang, 1, 1);
            tlpContent.Dock = DockStyle.Fill;
            tlpContent.Location = new Point(20, 234);
            tlpContent.Margin = new Padding(0);
            tlpContent.Name = "tlpContent";
            tlpContent.RowCount = 2;
            tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpContent.Size = new Size(879, 714);
            tlpContent.TabIndex = 2;
            // 
            // pnlNhapXuat
            // 
            pnlNhapXuat.BackColor = Color.White;
            pnlNhapXuat.Controls.Add(chartNhapXuat);
            pnlNhapXuat.Controls.Add(lblNhapXuatSubtitle);
            pnlNhapXuat.Controls.Add(lblNhapXuatTitle);
            pnlNhapXuat.Dock = DockStyle.Fill;
            pnlNhapXuat.Location = new Point(6, 6);
            pnlNhapXuat.Margin = new Padding(6);
            pnlNhapXuat.Name = "pnlNhapXuat";
            pnlNhapXuat.Padding = new Padding(8);
            pnlNhapXuat.Size = new Size(427, 345);
            pnlNhapXuat.TabIndex = 0;
            pnlNhapXuat.Paint += DashboardPanel_Paint;
            // 
            // lblNhapXuatSubtitle
            // 
            lblNhapXuatSubtitle.Dock = DockStyle.Top;
            lblNhapXuatSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblNhapXuatSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblNhapXuatSubtitle.Location = new Point(8, 42);
            lblNhapXuatSubtitle.Name = "lblNhapXuatSubtitle";
            lblNhapXuatSubtitle.Padding = new Padding(16, 2, 8, 0);
            lblNhapXuatSubtitle.Size = new Size(411, 30);
            lblNhapXuatSubtitle.TabIndex = 1;
            lblNhapXuatSubtitle.Text = "Tổng số lượng phát sinh trong 12 tháng gần nhất";
            // 
            // lblNhapXuatTitle
            // 
            lblNhapXuatTitle.Dock = DockStyle.Top;
            lblNhapXuatTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblNhapXuatTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblNhapXuatTitle.Location = new Point(8, 8);
            lblNhapXuatTitle.Name = "lblNhapXuatTitle";
            lblNhapXuatTitle.Padding = new Padding(16, 10, 8, 0);
            lblNhapXuatTitle.Size = new Size(411, 34);
            lblNhapXuatTitle.TabIndex = 0;
            lblNhapXuatTitle.Text = "Nhập – xuất kho theo tháng";
            //
            // chartNhapXuat
            //
            chartNhapXuat.Dock = DockStyle.Fill;
            chartNhapXuat.Location = new Point(8, 72);
            chartNhapXuat.Name = "chartNhapXuat";
            chartNhapXuat.Size = new Size(411, 265);
            chartNhapXuat.TabIndex = 2;
            //
            // pnlDanhMuc
            //
            pnlDanhMuc.BackColor = Color.White;
            pnlDanhMuc.Controls.Add(chartDanhMuc);
            pnlDanhMuc.Controls.Add(lblDanhMucSubtitle);
            pnlDanhMuc.Controls.Add(lblDanhMucTitle);
            pnlDanhMuc.Dock = DockStyle.Fill;
            pnlDanhMuc.Location = new Point(445, 6);
            pnlDanhMuc.Margin = new Padding(6);
            pnlDanhMuc.Name = "pnlDanhMuc";
            pnlDanhMuc.Padding = new Padding(8);
            pnlDanhMuc.Size = new Size(428, 345);
            pnlDanhMuc.TabIndex = 1;
            pnlDanhMuc.Paint += DashboardPanel_Paint;
            // 
            // lblDanhMucSubtitle
            // 
            lblDanhMucSubtitle.Dock = DockStyle.Top;
            lblDanhMucSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblDanhMucSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblDanhMucSubtitle.Location = new Point(8, 42);
            lblDanhMucSubtitle.Name = "lblDanhMucSubtitle";
            lblDanhMucSubtitle.Padding = new Padding(16, 2, 8, 0);
            lblDanhMucSubtitle.Size = new Size(412, 30);
            lblDanhMucSubtitle.TabIndex = 1;
            lblDanhMucSubtitle.Text = "Tỷ lệ số mặt hàng đang hoạt động";
            // 
            // lblDanhMucTitle
            // 
            lblDanhMucTitle.Dock = DockStyle.Top;
            lblDanhMucTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDanhMucTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblDanhMucTitle.Location = new Point(8, 8);
            lblDanhMucTitle.Name = "lblDanhMucTitle";
            lblDanhMucTitle.Padding = new Padding(16, 10, 8, 0);
            lblDanhMucTitle.Size = new Size(412, 34);
            lblDanhMucTitle.TabIndex = 0;
            lblDanhMucTitle.Text = "Hàng hóa theo danh mục";
            //
            // chartDanhMuc
            //
            chartDanhMuc.Dock = DockStyle.Fill;
            chartDanhMuc.Location = new Point(8, 72);
            chartDanhMuc.Name = "chartDanhMuc";
            chartDanhMuc.Size = new Size(412, 265);
            chartDanhMuc.TabIndex = 2;
            //
            // pnlTonKho
            //
            pnlTonKho.BackColor = Color.White;
            pnlTonKho.Controls.Add(chartTonKho);
            pnlTonKho.Controls.Add(lblTonKhoSubtitle);
            pnlTonKho.Controls.Add(lblTonKhoTitle);
            pnlTonKho.Dock = DockStyle.Fill;
            pnlTonKho.Location = new Point(6, 363);
            pnlTonKho.Margin = new Padding(6);
            pnlTonKho.Name = "pnlTonKho";
            pnlTonKho.Padding = new Padding(8);
            pnlTonKho.Size = new Size(427, 345);
            pnlTonKho.TabIndex = 2;
            pnlTonKho.Paint += DashboardPanel_Paint;
            // 
            // lblTonKhoSubtitle
            // 
            lblTonKhoSubtitle.Dock = DockStyle.Top;
            lblTonKhoSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblTonKhoSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblTonKhoSubtitle.Location = new Point(8, 42);
            lblTonKhoSubtitle.Name = "lblTonKhoSubtitle";
            lblTonKhoSubtitle.Padding = new Padding(16, 2, 8, 0);
            lblTonKhoSubtitle.Size = new Size(411, 30);
            lblTonKhoSubtitle.TabIndex = 1;
            lblTonKhoSubtitle.Text = "Các mặt hàng có số lượng tồn cao nhất";
            // 
            // lblTonKhoTitle
            // 
            lblTonKhoTitle.Dock = DockStyle.Top;
            lblTonKhoTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTonKhoTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblTonKhoTitle.Location = new Point(8, 8);
            lblTonKhoTitle.Name = "lblTonKhoTitle";
            lblTonKhoTitle.Padding = new Padding(16, 10, 8, 0);
            lblTonKhoTitle.Size = new Size(411, 34);
            lblTonKhoTitle.TabIndex = 0;
            lblTonKhoTitle.Text = "Sản phẩm tồn nhiều nhất";
            //
            // chartTonKho
            //
            chartTonKho.Dock = DockStyle.Fill;
            chartTonKho.Location = new Point(8, 72);
            chartTonKho.Name = "chartTonKho";
            chartTonKho.Size = new Size(411, 265);
            chartTonKho.TabIndex = 2;
            //
            // pnlKhachHang
            //
            pnlKhachHang.BackColor = Color.White;
            pnlKhachHang.Controls.Add(dgvKhachHang);
            pnlKhachHang.Controls.Add(lblKhachHangChartSubtitle);
            pnlKhachHang.Controls.Add(lblKhachHangChartTitle);
            pnlKhachHang.Dock = DockStyle.Fill;
            pnlKhachHang.Location = new Point(445, 363);
            pnlKhachHang.Margin = new Padding(6);
            pnlKhachHang.Name = "pnlKhachHang";
            pnlKhachHang.Padding = new Padding(8);
            pnlKhachHang.Size = new Size(428, 345);
            pnlKhachHang.TabIndex = 3;
            pnlKhachHang.Paint += DashboardPanel_Paint;
            // 
            // dgvKhachHang
            // 
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.AllowUserToDeleteRows = false;
            dgvKhachHang.AllowUserToResizeRows = false;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.BackgroundColor = Color.White;
            dgvKhachHang.BorderStyle = BorderStyle.None;
            dgvKhachHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKhachHang.AutoGenerateColumns = false;
            dgvKhachHang.Columns.AddRange(new DataGridViewColumn[] { colTenKhachHang, colSoLanMua, colTongGiaTri });
            dgvKhachHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(239, 246, 255);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvKhachHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvKhachHang.ColumnHeadersHeight = 34;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 8.5F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(51, 65, 85);
            dataGridViewCellStyle2.Padding = new Padding(4, 0, 4, 0);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(30, 64, 175);
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvKhachHang.DefaultCellStyle = dataGridViewCellStyle2;
            dgvKhachHang.Dock = DockStyle.Fill;
            dgvKhachHang.EnableHeadersVisualStyles = false;
            dgvKhachHang.GridColor = Color.FromArgb(226, 232, 240);
            dgvKhachHang.Location = new Point(8, 72);
            dgvKhachHang.MultiSelect = false;
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.RowHeadersVisible = false;
            dgvKhachHang.RowHeadersWidth = 51;
            dgvKhachHang.RowTemplate.Height = 30;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.Size = new Size(412, 265);
            dgvKhachHang.TabIndex = 2;
            // 
            // colTenKhachHang
            // 
            colTenKhachHang.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenKhachHang.DataPropertyName = "TenKhachHang";
            colTenKhachHang.HeaderText = "Khách hàng";
            colTenKhachHang.MinimumWidth = 120;
            colTenKhachHang.Name = "colTenKhachHang";
            colTenKhachHang.ReadOnly = true;
            // 
            // colSoLanMua
            // 
            colSoLanMua.DataPropertyName = "SoLanMua";
            colSoLanMua.HeaderText = "Số lần mua";
            colSoLanMua.MinimumWidth = 80;
            colSoLanMua.Name = "colSoLanMua";
            colSoLanMua.ReadOnly = true;
            colSoLanMua.Width = 95;
            // 
            // colTongGiaTri
            // 
            colTongGiaTri.DataPropertyName = "TongGiaTri";
            colTongGiaTri.HeaderText = "Tổng giá trị";
            colTongGiaTri.MinimumWidth = 100;
            colTongGiaTri.Name = "colTongGiaTri";
            colTongGiaTri.ReadOnly = true;
            colTongGiaTri.Width = 120;
            // 
            // lblKhachHangChartSubtitle
            // 
            lblKhachHangChartSubtitle.Dock = DockStyle.Top;
            lblKhachHangChartSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblKhachHangChartSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblKhachHangChartSubtitle.Location = new Point(8, 42);
            lblKhachHangChartSubtitle.Name = "lblKhachHangChartSubtitle";
            lblKhachHangChartSubtitle.Padding = new Padding(16, 2, 8, 0);
            lblKhachHangChartSubtitle.Size = new Size(412, 30);
            lblKhachHangChartSubtitle.TabIndex = 1;
            lblKhachHangChartSubtitle.Text = "Xếp hạng theo tổng giá trị phiếu xuất";
            // 
            // lblKhachHangChartTitle
            // 
            lblKhachHangChartTitle.Dock = DockStyle.Top;
            lblKhachHangChartTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblKhachHangChartTitle.ForeColor = Color.FromArgb(15, 23, 42);
            lblKhachHangChartTitle.Location = new Point(8, 8);
            lblKhachHangChartTitle.Name = "lblKhachHangChartTitle";
            lblKhachHangChartTitle.Padding = new Padding(16, 10, 8, 0);
            lblKhachHangChartTitle.Size = new Size(412, 34);
            lblKhachHangChartTitle.TabIndex = 0;
            lblKhachHangChartTitle.Text = "Khách hàng mua nhiều";
            // 
            // FrmThongKe
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(940, 739);
            Controls.Add(pnlScroll);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmThongKe";
            Text = "Thống Kê";
            Load += FrmThongKe_Load;
            pnlScroll.ResumeLayout(false);
            tlpPage.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tlpCards.ResumeLayout(false);
            cardHangHoa.ResumeLayout(false);
            cardHangHoa.PerformLayout();
            cardKhachHang.ResumeLayout(false);
            cardKhachHang.PerformLayout();
            cardNhanVien.ResumeLayout(false);
            cardNhanVien.PerformLayout();
            cardGiaTriKho.ResumeLayout(false);
            cardGiaTriKho.PerformLayout();
            tlpContent.ResumeLayout(false);
            pnlNhapXuat.ResumeLayout(false);
            pnlDanhMuc.ResumeLayout(false);
            pnlTonKho.ResumeLayout(false);
            pnlKhachHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            ResumeLayout(false);
        }
    }
}
