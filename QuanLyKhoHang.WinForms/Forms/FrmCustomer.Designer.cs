namespace QuanLyKhoHang.Forms
{
    partial class FrmKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTopControls; 
        private System.Windows.Forms.DataGridView dgvKhachHang; 
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtEmail;
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
            txtGhiChu = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblTen = new Label();
            lblDiaChi = new Label();
            lblSDT = new Label();
            lblEmail = new Label();
            lblGhiChu = new Label();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            dgvKhachHang = new DataGridView();
            pnlTopControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
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
            pnlTopControls.Controls.Add(txtGhiChu);
            pnlTopControls.Controls.Add(btnThem);
            pnlTopControls.Controls.Add(btnSua);
            pnlTopControls.Controls.Add(btnXoa);
            pnlTopControls.Controls.Add(btnLamMoi);
            pnlTopControls.Controls.Add(lblTen);
            pnlTopControls.Controls.Add(lblDiaChi);
            pnlTopControls.Controls.Add(lblSDT);
            pnlTopControls.Controls.Add(lblEmail);
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
            lblTitle.Size = new Size(358, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ KHÁCH HÀNG";
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
            txtSDT.Size = new Size(224, 27);
            txtSDT.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(625, 130);
            txtEmail.Margin = new Padding(4, 4, 4, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(224, 27);
            txtEmail.TabIndex = 4;
            // 
            // txtGhiChu
            // 
            txtGhiChu.Location = new Point(962, 80);
            txtGhiChu.Margin = new Padding(4, 4, 4, 4);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.ScrollBars = ScrollBars.Vertical;
            txtGhiChu.Size = new Size(224, 83);
            txtGhiChu.TabIndex = 5;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(175, 198);
            btnThem.Margin = new Padding(4, 4, 4, 4);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(119, 44);
            btnThem.TabIndex = 6;
            btnThem.Text = "Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(312, 198);
            btnSua.Margin = new Padding(4, 4, 4, 4);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(119, 44);
            btnSua.TabIndex = 7;
            btnSua.Text = "Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(450, 198);
            btnXoa.Margin = new Padding(4, 4, 4, 4);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(119, 44);
            btnXoa.TabIndex = 8;
            btnXoa.Text = "Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(588, 198);
            btnLamMoi.Margin = new Padding(4, 4, 4, 4);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(119, 44);
            btnLamMoi.TabIndex = 9;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Location = new Point(25, 85);
            lblTen.Margin = new Padding(4, 0, 4, 0);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(114, 20);
            lblTen.TabIndex = 10;
            lblTen.Text = "Tên khách hàng:";
            // 
            // lblDiaChi
            // 
            lblDiaChi.AutoSize = true;
            lblDiaChi.Location = new Point(25, 135);
            lblDiaChi.Margin = new Padding(4, 0, 4, 0);
            lblDiaChi.Name = "lblDiaChi";
            lblDiaChi.Size = new Size(58, 20);
            lblDiaChi.TabIndex = 11;
            lblDiaChi.Text = "Địa chỉ:";
            // 
            // lblSDT
            // 
            lblSDT.AutoSize = true;
            lblSDT.Location = new Point(500, 85);
            lblSDT.Margin = new Padding(4, 0, 4, 0);
            lblSDT.Name = "lblSDT";
            lblSDT.Size = new Size(100, 20);
            lblSDT.TabIndex = 12;
            lblSDT.Text = "Số điện thoại:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(500, 135);
            lblEmail.Margin = new Padding(4, 0, 4, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(49, 20);
            lblEmail.TabIndex = 13;
            lblEmail.Text = "Email:";
            // 
            // lblGhiChu
            // 
            lblGhiChu.AutoSize = true;
            lblGhiChu.Location = new Point(875, 85);
            lblGhiChu.Margin = new Padding(4, 0, 4, 0);
            lblGhiChu.Name = "lblGhiChu";
            lblGhiChu.Size = new Size(61, 20);
            lblGhiChu.TabIndex = 14;
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
            lblTimKiem.TabIndex = 15;
            lblTimKiem.Text = "Tìm kiếm nhanh:";
            lblTimKiem.Visible = false;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Font = new Font("Segoe UI", 10F);
            txtTimKiem.Location = new Point(852, 204);
            txtTimKiem.Margin = new Padding(4, 4, 4, 4);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(224, 30);
            txtTimKiem.TabIndex = 16;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // dgvKhachHang
            // 
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhachHang.ColumnHeadersHeight = 29;
            dgvKhachHang.Dock = DockStyle.Fill;
            dgvKhachHang.Location = new Point(0, 258);
            dgvKhachHang.Margin = new Padding(4, 4, 4, 4);
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.ReadOnly = true;
            dgvKhachHang.RowHeadersWidth = 51;
            dgvKhachHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhachHang.Size = new Size(1250, 492);
            dgvKhachHang.TabIndex = 0;
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
            // 
            // FrmKhachHang
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1250, 750);
            Controls.Add(dgvKhachHang);
            Controls.Add(pnlTopControls);
            Margin = new Padding(4, 4, 4, 4);
            Name = "FrmKhachHang";
            Text = "Quản Lý Khách Hàng";
            Load += FrmKhachHang_Load;
            pnlTopControls.ResumeLayout(false);
            pnlTopControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            ResumeLayout(false);
        }
    }
}
