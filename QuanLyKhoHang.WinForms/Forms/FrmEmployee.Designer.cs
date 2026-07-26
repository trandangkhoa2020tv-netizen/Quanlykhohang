namespace QuanLyKhoHang.Forms
{
    partial class FrmNhanVien
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTopControls; 
        private System.Windows.Forms.DataGridView dgvNhanVien; 
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtChucVu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblChucVu;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTopControls = new Panel();
            lblTitle = new Label();
            txtTen = new TextBox();
            txtDiaChi = new TextBox();
            txtSDT = new TextBox();
            txtEmail = new TextBox();
            txtChucVu = new TextBox();
            txtGhiChu = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblTen = new Label();
            lblDiaChi = new Label();
            lblSDT = new Label();
            lblEmail = new Label();
            lblChucVu = new Label();
            lblGhiChu = new Label();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            dgvNhanVien = new DataGridView();
            pnlTopControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
            SuspendLayout();
            // 
            // pnlTopControls
            // 
            pnlTopControls.BackColor = Color.WhiteSmoke;
            pnlTopControls.Controls.Add(lblTitle);
            pnlTopControls.Controls.Add(txtTen);
            pnlTopControls.Controls.Add(txtDiaChi);
            pnlTopControls.Controls.Add(txtSDT);
            pnlTopControls.Controls.Add(txtEmail);
            pnlTopControls.Controls.Add(txtChucVu);
            pnlTopControls.Controls.Add(txtGhiChu);
            pnlTopControls.Controls.Add(btnThem);
            pnlTopControls.Controls.Add(btnSua);
            pnlTopControls.Controls.Add(btnXoa);
            pnlTopControls.Controls.Add(btnLamMoi);
            pnlTopControls.Controls.Add(lblTen);
            pnlTopControls.Controls.Add(lblDiaChi);
            pnlTopControls.Controls.Add(lblSDT);
            pnlTopControls.Controls.Add(lblEmail);
            pnlTopControls.Controls.Add(lblChucVu);
            pnlTopControls.Controls.Add(lblGhiChu);
            pnlTopControls.Controls.Add(lblTimKiem);
            pnlTopControls.Controls.Add(txtTimKiem);
            pnlTopControls.Dock = DockStyle.Top;
            pnlTopControls.Location = new Point(0, 0);
            pnlTopControls.Margin = new Padding(4, 4, 4, 4);
            pnlTopControls.Name = "pnlTopControls";
            pnlTopControls.Size = new Size(1250, 258);
            pnlTopControls.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 58, 138);
            lblTitle.Location = new Point(25, 15);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(326, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ NHÂN VIÊN";
            // 
            // txtTen
            // 
            txtTen.Location = new Point(175, 80);
            txtTen.Margin = new Padding(4, 4, 4, 4);
            txtTen.Name = "txtTen";
            txtTen.Size = new Size(286, 27);
            txtTen.TabIndex = 1;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(175, 130);
            txtDiaChi.Margin = new Padding(4, 4, 4, 4);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(286, 27);
            txtDiaChi.TabIndex = 2;
            // 
            // txtSDT
            // 
            txtSDT.Location = new Point(625, 80);
            txtSDT.Margin = new Padding(4, 4, 4, 4);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(212, 27);
            txtSDT.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(625, 130);
            txtEmail.Margin = new Padding(4, 4, 4, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(212, 27);
            txtEmail.TabIndex = 4;
            // 
            // txtChucVu
            // 
            txtChucVu.Location = new Point(962, 80);
            txtChucVu.Margin = new Padding(4, 4, 4, 4);
            txtChucVu.Name = "txtChucVu";
            txtChucVu.Size = new Size(224, 27);
            txtChucVu.TabIndex = 5;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(962, 130);
            txtGhiChu.Margin = new Padding(4, 4, 4, 4);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.ScrollBars = ScrollBars.Vertical;
            txtGhiChu.Size = new Size(224, 58);
            txtGhiChu.TabIndex = 6;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(175, 198);
            btnThem.Margin = new Padding(4, 4, 4, 4);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(119, 44);
            btnThem.TabIndex = 7;
            btnThem.Text = "Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(312, 198);
            btnSua.Margin = new Padding(4, 4, 4, 4);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(119, 44);
            btnSua.TabIndex = 8;
            btnSua.Text = "Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(450, 198);
            btnXoa.Margin = new Padding(4, 4, 4, 4);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(119, 44);
            btnXoa.TabIndex = 9;
            btnXoa.Text = "Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(588, 198);
            btnLamMoi.Margin = new Padding(4, 4, 4, 4);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(119, 44);
            btnLamMoi.TabIndex = 10;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Location = new Point(25, 85);
            lblTen.Margin = new Padding(4, 0, 4, 0);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(102, 20);
            lblTen.TabIndex = 11;
            lblTen.Text = "Tên nhân viên:";
            // 
            // lblDiaChi
            // 
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(25, 135);
            lblDiaChi.Margin = new Padding(4, 0, 4, 0);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(58, 20);
            lblDiaChi.TabIndex = 12;
            lblDiaChi.Text = "Địa chỉ:";
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Location = new Point(500, 85);
            lblSDT.Margin = new Padding(4, 0, 4, 0);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(100, 20);
            lblSDT.TabIndex = 13;
            lblSDT.Text = "Số điện thoại:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(500, 135);
            lblEmail.Margin = new Padding(4, 0, 4, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 14;
            lblEmail.Text = "Email:";
            // 
            // lblChucVu
            // 
            lblChucVu.AutoSize = true;
            lblChucVu.Location = new Point(875, 85);
            lblChucVu.Margin = new Padding(4, 0, 4, 0);
            lblChucVu.Name = "lblChucVu";
            lblChucVu.Size = new Size(64, 20);
            lblChucVu.TabIndex = 15;
            lblChucVu.Text = "Chức vụ:";
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(875, 135);
            lblGhiChu.Margin = new Padding(4, 0, 4, 0);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(61, 20);
            lblGhiChu.TabIndex = 16;
            lblGhiChu.Text = "Ghi chú:";
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblTimKiem.ForeColor = Color.DarkBlue;
            lblTimKiem.Location = new Point(706, 208);
            lblTimKiem.Margin = new Padding(4, 0, 4, 0);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(138, 21);
            lblTimKiem.TabIndex = 17;
            lblTimKiem.Text = "Tìm kiếm nhanh:";
            lblTimKiem.Visible = false;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Font = new Font("Segoe UI", 10F);
            txtTimKiem.Location = new Point(852, 205);
            txtTimKiem.Margin = new Padding(4, 4, 4, 4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(224, 30);
            txtTimKiem.TabIndex = 18;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // dgvNhanVien
            // 
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNhanVien.ColumnHeadersHeight = 29;
            dgvNhanVien.Dock = DockStyle.Fill;
            dgvNhanVien.Location = new Point(0, 258);
            dgvNhanVien.Margin = new Padding(4, 4, 4, 4);
            dgvNhanVien.Name = "dgvNhanVien";
            dgvNhanVien.ReadOnly = true;
            dgvNhanVien.RowHeadersWidth = 51;
            dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhanVien.Size = new Size(1250, 492);
            dgvNhanVien.TabIndex = 0;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            // 
            // FrmNhanVien
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1250, 750);
            Controls.Add(dgvNhanVien);
            Controls.Add(pnlTopControls);
            Margin = new Padding(4, 4, 4, 4);
            Name = "FrmNhanVien";
            Text = "Quản Lý Nhân Viên";
            Load += FrmNhanVien_Load;
            pnlTopControls.ResumeLayout(false);
            pnlTopControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
            ResumeLayout(false);
        }
    }
}
