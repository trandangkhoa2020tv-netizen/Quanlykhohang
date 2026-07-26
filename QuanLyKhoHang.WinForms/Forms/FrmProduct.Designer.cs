namespace QuanLyKhoHang.Forms
{
    partial class FrmHangHoa
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTopControls; // Khung bám đỉnh để gom cụm điều khiển
        private System.Windows.Forms.DataGridView dgvHangHoa;
        private System.Windows.Forms.TextBox txtTenHang;
        private System.Windows.Forms.ComboBox cbLoaiHang;
        private System.Windows.Forms.ComboBox cbNhaCungCap;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtDVT;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTenHang;
        private System.Windows.Forms.Label lblLoaiHang;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.Label lblSoLuongTon;
        private System.Windows.Forms.Label lblDonViTinh;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;

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
            this.pnlTopControls = new System.Windows.Forms.Panel();
            this.dgvHangHoa = new System.Windows.Forms.DataGridView();
            this.txtTenHang = new System.Windows.Forms.TextBox();
            this.cbLoaiHang = new System.Windows.Forms.ComboBox();
            this.cbNhaCungCap = new System.Windows.Forms.ComboBox();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtDVT = new System.Windows.Forms.TextBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTenHang = new System.Windows.Forms.Label();
            this.lblLoaiHang = new System.Windows.Forms.Label();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.lblSoLuongTon = new System.Windows.Forms.Label();
            this.lblDonViTinh = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.lblTimKiem = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvHangHoa)).BeginInit();
            this.pnlTopControls.SuspendLayout();
            this.SuspendLayout();

            // pnlTopControls (Định vị thanh nhập liệu bám đỉnh cố định)
            this.pnlTopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopControls.Height = 236;
            this.pnlTopControls.Name = "pnlTopControls";
            this.pnlTopControls.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTopControls.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.txtTenHang, this.cbLoaiHang, this.cbNhaCungCap,
                this.txtGiaNhap, this.txtGiaBan, this.txtSoLuong, this.txtDVT, this.txtGhiChu,
                this.btnThem, this.btnSua, this.btnXoa, this.btnLamMoi,
                this.lblTenHang, this.lblLoaiHang, this.lblNhaCungCap, this.lblGiaNhap,
                this.lblGiaBan, this.lblSoLuongTon, this.lblDonViTinh, this.lblGhiChu,
                this.lblTimKiem
            });

            // lblTitle
            this.lblTitle.Text = "QUẢN LÝ DANH MỤC HÀNG HÓA";
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.AutoSize = true;
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 58, 138);

            this.lblTenHang.Text = "Tên hàng:";
            this.lblTenHang.Location = new System.Drawing.Point(20, 68);
            this.lblTenHang.AutoSize = true;

            this.lblLoaiHang.Text = "Loại hàng:";
            this.lblLoaiHang.Location = new System.Drawing.Point(20, 108);
            this.lblLoaiHang.AutoSize = true;

            this.lblNhaCungCap.Text = "Nhà cung cấp:";
            this.lblNhaCungCap.Location = new System.Drawing.Point(20, 148);
            this.lblNhaCungCap.AutoSize = true;

            this.lblGiaNhap.Text = "Giá nhập:";
            this.lblGiaNhap.Location = new System.Drawing.Point(380, 68);
            this.lblGiaNhap.AutoSize = true;

            this.lblGiaBan.Text = "Giá bán:";
            this.lblGiaBan.Location = new System.Drawing.Point(380, 108);
            this.lblGiaBan.AutoSize = true;

            this.lblSoLuongTon.Text = "Số lượng tồn:";
            this.lblSoLuongTon.Location = new System.Drawing.Point(380, 148);
            this.lblSoLuongTon.AutoSize = true;

            this.lblDonViTinh.Text = "Đơn vị tính:";
            this.lblDonViTinh.Location = new System.Drawing.Point(680, 68);
            this.lblDonViTinh.AutoSize = true;

            this.lblGhiChu.Text = "Ghi chú:";
            this.lblGhiChu.Location = new System.Drawing.Point(680, 108);
            this.lblGhiChu.AutoSize = true;

            // Tọa độ các ô nhập dữ liệu (Inputs Layout)
            this.txtTenHang.Location = new System.Drawing.Point(130, 64); this.txtTenHang.Size = new System.Drawing.Size(220, 27);
            this.cbLoaiHang.Location = new System.Drawing.Point(130, 104); this.cbLoaiHang.Size = new System.Drawing.Size(220, 27); this.cbLoaiHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNhaCungCap.Location = new System.Drawing.Point(130, 144); this.cbNhaCungCap.Size = new System.Drawing.Size(220, 27); this.cbNhaCungCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            
            this.txtGiaNhap.Location = new System.Drawing.Point(480, 64); this.txtGiaNhap.Size = new System.Drawing.Size(170, 27);
            this.txtGiaNhap.Name = "txtGiaNhap";
            this.txtGiaBan.Location = new System.Drawing.Point(480, 104); this.txtGiaBan.Size = new System.Drawing.Size(170, 27);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtSoLuong.Location = new System.Drawing.Point(480, 144); this.txtSoLuong.Size = new System.Drawing.Size(170, 27);
            this.txtSoLuong.Name = "txtSoLuong";
            
            // Giữ kích thước cố định để ô Đơn vị tính và Ghi chú không kéo dài theo màn hình.
            this.txtDVT.Location = new System.Drawing.Point(770, 64); this.txtDVT.Size = new System.Drawing.Size(180, 27);
            this.txtGhiChu.Location = new System.Drawing.Point(770, 104); this.txtGhiChu.Size = new System.Drawing.Size(180, 67); this.txtGhiChu.Multiline = true;
            this.txtGhiChu.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;

            // ======================================================================
            // ĐÃ SỬA: ĐƯA CỤM NÚT BẤM VỀ PHONG CÁCH CỔ ĐIỂN GIỐNG KHÁCH HÀNG BAN ĐẦU

            this.btnThem.Text = "Thêm"; 
            this.btnThem.Name = "btnThem";
            this.btnThem.Location = new System.Drawing.Point(130, 188);
            this.btnThem.Size = new System.Drawing.Size(95, 35); 
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Text = "Sửa"; 
            this.btnSua.Name = "btnSua";
            this.btnSua.Location = new System.Drawing.Point(240, 188);
            this.btnSua.Size = new System.Drawing.Size(95, 35); 
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Text = "Xóa"; 
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Location = new System.Drawing.Point(350, 188);
            this.btnXoa.Size = new System.Drawing.Size(95, 35); 
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Location = new System.Drawing.Point(460, 188);
            this.btnLamMoi.Size = new System.Drawing.Size(95, 35); 
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // ======================================================================

            // thêm TextBox tìm kiếm nhanh ở góc trên bên phải
            // 2. Dán đoạn cấu hình này vào trong InitializeComponent(), ngay trên đoạn cấu hình dgvHangHoa:
           // 1. Cấu hình Nhãn "Tìm kiếm nhanh:"
            this.lblTimKiem.Text = "Tìm kiếm nhanh:";
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Visible = false;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTimKiem.AutoSize = true;
            // Tọa độ mới: Cách cụm nút bấm bên trái một khoảng vừa vặn, không bị đè chữ
            this.lblTimKiem.Location = new System.Drawing.Point(565, 194);
            this.lblTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;

            // 2. Cấu hình Ô TextBox txtTimKiem
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            // Tọa độ mới: Nằm ngay sau nhãn chữ, thẳng hàng với hàng nút bấm
            this.txtTimKiem.Location = new System.Drawing.Point(620, 190);
            this.txtTimKiem.Size = new System.Drawing.Size(180, 27);
            this.txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // ======================================================================
            // QUAN TRỌNG: PHẢI CÓ 2 DÒNG NÀY ĐỂ NẠP CONTROL VÀO PANEL (TRÁNH BỊ ẨN)
            this.pnlTopControls.Controls.Add(this.txtTimKiem);
            // ======================================================================
            // dgvHangHoa (Ép bảng lưới bung rộng chiếm 100% không gian trống còn lại bên dưới)
            this.dgvHangHoa.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHangHoa.Name = "dgvHangHoa";
            this.dgvHangHoa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHangHoa.AllowUserToAddRows = false;
            this.dgvHangHoa.ReadOnly = true;
            this.dgvHangHoa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHangHoa.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHangHoa_CellClick);

            this.dgvHangHoa.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvHangHoa_DataBindingComplete);

            // FrmHangHoa Form Properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.dgvHangHoa);
            this.Controls.Add(this.pnlTopControls); 
            this.Name = "FrmHangHoa";
            this.Text = "Quản Lý Hàng Hóa";
            this.Load += new System.EventHandler(this.FrmHangHoa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangHoa)).EndInit();
            this.pnlTopControls.ResumeLayout(false);
            this.pnlTopControls.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
