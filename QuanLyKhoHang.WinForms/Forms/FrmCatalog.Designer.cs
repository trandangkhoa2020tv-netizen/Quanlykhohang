namespace QuanLyKhoHang.Forms
{
    partial class FrmDanhMuc
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabDanhMuc;
        private TabPage tabNhaCungCap;
        private TabPage tabLoaiHang;
        private Panel pnlNhaCungCap;
        private DataGridView dgvNhaCungCap;
        private Label lblTitleNcc;
        private Label lblTenNcc;
        private Label lblDiaChiNcc;
        private Label lblSdtNcc;
        private Label lblEmailNcc;
        private Label lblGhiChuNcc;
        private TextBox txtTenNcc;
        private TextBox txtDiaChiNcc;
        private TextBox txtSdtNcc;
        private TextBox txtEmailNcc;
        private TextBox txtGhiChuNcc;
        private TextBox txtTimKiemNcc;
        private Button btnThemNcc;
        private Button btnSuaNcc;
        private Button btnXoaNcc;
        private Button btnLamMoiNcc;
        private Panel pnlLoaiHang;
        private DataGridView dgvLoaiHang;
        private Label lblTitleLoai;
        private Label lblTenLoai;
        private Label lblGhiChuLoai;
        private TextBox txtTenLoai;
        private TextBox txtGhiChuLoai;
        private TextBox txtTimKiemLoai;
        private Button btnThemLoai;
        private Button btnSuaLoai;
        private Button btnXoaLoai;
        private Button btnLamMoiLoai;

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
            tabDanhMuc = new TabControl();
            tabNhaCungCap = new TabPage();
            dgvNhaCungCap = new DataGridView();
            pnlNhaCungCap = new Panel();
            lblTitleNcc = new Label();
            lblTenNcc = new Label();
            txtTenNcc = new TextBox();
            lblDiaChiNcc = new Label();
            txtDiaChiNcc = new TextBox();
            lblSdtNcc = new Label();
            txtSdtNcc = new TextBox();
            lblEmailNcc = new Label();
            txtEmailNcc = new TextBox();
            lblGhiChuNcc = new Label();
            txtGhiChuNcc = new TextBox();
            btnThemNcc = new Button();
            btnSuaNcc = new Button();
            btnXoaNcc = new Button();
            btnLamMoiNcc = new Button();
            txtTimKiemNcc = new TextBox();
            tabLoaiHang = new TabPage();
            dgvLoaiHang = new DataGridView();
            pnlLoaiHang = new Panel();
            lblTitleLoai = new Label();
            lblTenLoai = new Label();
            txtTenLoai = new TextBox();
            lblGhiChuLoai = new Label();
            txtGhiChuLoai = new TextBox();
            btnThemLoai = new Button();
            btnSuaLoai = new Button();
            btnXoaLoai = new Button();
            btnLamMoiLoai = new Button();
            txtTimKiemLoai = new TextBox();
            tabDanhMuc.SuspendLayout();
            tabNhaCungCap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNhaCungCap).BeginInit();
            pnlNhaCungCap.SuspendLayout();
            tabLoaiHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiHang).BeginInit();
            pnlLoaiHang.SuspendLayout();
            SuspendLayout();
            // 
            // tabDanhMuc
            // 
            tabDanhMuc.Controls.Add(tabNhaCungCap);
            tabDanhMuc.Controls.Add(tabLoaiHang);
            tabDanhMuc.Dock = DockStyle.Fill;
            tabDanhMuc.Location = new Point(0, 0);
            tabDanhMuc.Margin = new Padding(4, 4, 4, 4);
            tabDanhMuc.Name = "tabDanhMuc";
            tabDanhMuc.Padding = new Point(12, 8);
            tabDanhMuc.SelectedIndex = 0;
            tabDanhMuc.Size = new Size(1748, 750);
            tabDanhMuc.TabIndex = 0;
            // 
            // tabNhaCungCap
            // 
            tabNhaCungCap.BackColor = Color.White;
            tabNhaCungCap.Controls.Add(dgvNhaCungCap);
            tabNhaCungCap.Controls.Add(pnlNhaCungCap);
            tabNhaCungCap.Location = new Point(4, 39);
            tabNhaCungCap.Margin = new Padding(4, 4, 4, 4);
            tabNhaCungCap.Name = "tabNhaCungCap";
            tabNhaCungCap.Padding = new Padding(10, 10, 10, 10);
            tabNhaCungCap.Size = new Size(1740, 707);
            tabNhaCungCap.TabIndex = 0;
            tabNhaCungCap.Text = "Nhà cung cấp";
            // 
            // dgvNhaCungCap
            // 
            dgvNhaCungCap.AllowUserToAddRows = false;
            dgvNhaCungCap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNhaCungCap.Dock = DockStyle.Fill;
            dgvNhaCungCap.Location = new Point(10, 304);
            dgvNhaCungCap.Margin = new Padding(4, 4, 4, 4);
            dgvNhaCungCap.Name = "dgvNhaCungCap";
            dgvNhaCungCap.ReadOnly = true;
            dgvNhaCungCap.RowHeadersWidth = 51;
            dgvNhaCungCap.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvNhaCungCap.Size = new Size(1720, 393);
            dgvNhaCungCap.TabIndex = 1;
            dgvNhaCungCap.CellClick += dgvNhaCungCap_CellClick;
            // 
            // pnlNhaCungCap
            // 
            pnlNhaCungCap.BackColor = Color.WhiteSmoke;
            pnlNhaCungCap.BorderStyle = BorderStyle.FixedSingle;
            pnlNhaCungCap.Controls.Add(lblTitleNcc);
            pnlNhaCungCap.Controls.Add(lblTenNcc);
            pnlNhaCungCap.Controls.Add(txtTenNcc);
            pnlNhaCungCap.Controls.Add(lblDiaChiNcc);
            pnlNhaCungCap.Controls.Add(txtDiaChiNcc);
            pnlNhaCungCap.Controls.Add(lblSdtNcc);
            pnlNhaCungCap.Controls.Add(txtSdtNcc);
            pnlNhaCungCap.Controls.Add(lblEmailNcc);
            pnlNhaCungCap.Controls.Add(txtEmailNcc);
            pnlNhaCungCap.Controls.Add(lblGhiChuNcc);
            pnlNhaCungCap.Controls.Add(txtGhiChuNcc);
            pnlNhaCungCap.Controls.Add(btnThemNcc);
            pnlNhaCungCap.Controls.Add(btnSuaNcc);
            pnlNhaCungCap.Controls.Add(btnXoaNcc);
            pnlNhaCungCap.Controls.Add(btnLamMoiNcc);
            pnlNhaCungCap.Controls.Add(txtTimKiemNcc);
            pnlNhaCungCap.Dock = DockStyle.Top;
            pnlNhaCungCap.Location = new Point(10, 10);
            pnlNhaCungCap.Margin = new Padding(4, 4, 4, 4);
            pnlNhaCungCap.Name = "pnlNhaCungCap";
            pnlNhaCungCap.Size = new Size(1720, 294);
            pnlNhaCungCap.TabIndex = 0;
            // 
            // lblTitleNcc
            // 
            lblTitleNcc.AutoSize = true;
            lblTitleNcc.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitleNcc.ForeColor = Color.FromArgb(30, 58, 138);
            lblTitleNcc.Location = new Point(25, 14);
            lblTitleNcc.Margin = new Padding(4, 0, 4, 0);
            lblTitleNcc.Name = "lblTitleNcc";
            lblTitleNcc.Size = new Size(384, 41);
            lblTitleNcc.TabIndex = 0;
            lblTitleNcc.Text = "QUẢN LÝ NHÀ CUNG CẤP";
            // 
            // lblTenNcc
            // 
            lblTenNcc.AutoSize = true;
            lblTenNcc.Location = new Point(25, 85);
            lblTenNcc.Margin = new Padding(4, 0, 4, 0);
            lblTenNcc.Name = "lblTenNcc";
            lblTenNcc.Size = new Size(127, 20);
            lblTenNcc.TabIndex = 1;
            lblTenNcc.Text = "Tên nhà cung cấp:";
            // 
            // txtTenNcc
            // 
            txtTenNcc.Location = new Point(188, 80);
            txtTenNcc.Margin = new Padding(4, 4, 4, 4);
            txtTenNcc.Name = "txtTenNcc";
            txtTenNcc.Size = new Size(349, 27);
            txtTenNcc.TabIndex = 2;
            // 
            // lblDiaChiNcc
            // 
            lblDiaChiNcc.AutoSize = true;
            lblDiaChiNcc.Location = new Point(25, 135);
            lblDiaChiNcc.Margin = new Padding(4, 0, 4, 0);
            lblDiaChiNcc.Name = "lblDiaChiNcc";
            lblDiaChiNcc.Size = new Size(58, 20);
            lblDiaChiNcc.TabIndex = 3;
            lblDiaChiNcc.Text = "Địa chỉ:";
            // 
            // txtDiaChiNcc
            // 
            txtDiaChiNcc.Location = new Point(188, 130);
            txtDiaChiNcc.Margin = new Padding(4, 4, 4, 4);
            txtDiaChiNcc.Name = "txtDiaChiNcc";
            txtDiaChiNcc.Size = new Size(349, 27);
            txtDiaChiNcc.TabIndex = 4;
            // 
            // lblSdtNcc
            // 
            lblSdtNcc.AutoSize = true;
            lblSdtNcc.Location = new Point(588, 85);
            lblSdtNcc.Margin = new Padding(4, 0, 4, 0);
            lblSdtNcc.Name = "lblSdtNcc";
            lblSdtNcc.Size = new Size(100, 20);
            lblSdtNcc.TabIndex = 5;
            lblSdtNcc.Text = "Số điện thoại:";
            // 
            // txtSdtNcc
            // 
            txtSdtNcc.Location = new Point(750, 80);
            txtSdtNcc.Margin = new Padding(4, 4, 4, 4);
            txtSdtNcc.Name = "txtSdtNcc";
            txtSdtNcc.Size = new Size(274, 27);
            txtSdtNcc.TabIndex = 6;
            // 
            // lblEmailNcc
            // 
            lblEmailNcc.AutoSize = true;
            lblEmailNcc.Location = new Point(588, 135);
            lblEmailNcc.Margin = new Padding(4, 0, 4, 0);
            lblEmailNcc.Name = "lblEmailNcc";
            lblEmailNcc.Size = new Size(49, 20);
            lblEmailNcc.TabIndex = 7;
            lblEmailNcc.Text = "Email:";
            // 
            // txtEmailNcc
            // 
            txtEmailNcc.Location = new Point(750, 130);
            txtEmailNcc.Margin = new Padding(4, 4, 4, 4);
            txtEmailNcc.Name = "txtEmailNcc";
            txtEmailNcc.Size = new Size(274, 27);
            txtEmailNcc.TabIndex = 8;
            // 
            // lblGhiChuNcc
            // 
            lblGhiChuNcc.AutoSize = true;
            lblGhiChuNcc.Location = new Point(25, 185);
            lblGhiChuNcc.Margin = new Padding(4, 0, 4, 0);
            lblGhiChuNcc.Name = "lblGhiChuNcc";
            lblGhiChuNcc.Size = new Size(61, 20);
            lblGhiChuNcc.TabIndex = 9;
            lblGhiChuNcc.Text = "Ghi chú:";
            // 
            // txtGhiChuNcc
            // 
            txtGhiChuNcc.Location = new Point(188, 180);
            txtGhiChuNcc.Margin = new Padding(4, 4, 4, 4);
            txtGhiChuNcc.Name = "txtGhiChuNcc";
            txtGhiChuNcc.Size = new Size(349, 27);
            txtGhiChuNcc.TabIndex = 10;
            // 
            // btnThemNcc
            // 
            btnThemNcc.Location = new Point(188, 235);
            btnThemNcc.Margin = new Padding(4, 4, 4, 4);
            btnThemNcc.Name = "btnThemNcc";
            btnThemNcc.Size = new Size(119, 44);
            btnThemNcc.TabIndex = 11;
            btnThemNcc.Text = "Thêm";
            btnThemNcc.UseVisualStyleBackColor = false;
            btnThemNcc.Click += btnThemNcc_Click;
            // 
            // btnSuaNcc
            // 
            btnSuaNcc.Location = new Point(325, 235);
            btnSuaNcc.Margin = new Padding(4, 4, 4, 4);
            btnSuaNcc.Name = "btnSuaNcc";
            btnSuaNcc.Size = new Size(119, 44);
            btnSuaNcc.TabIndex = 12;
            btnSuaNcc.Text = "Sửa";
            btnSuaNcc.UseVisualStyleBackColor = false;
            btnSuaNcc.Click += btnSuaNcc_Click;
            // 
            // btnXoaNcc
            // 
            btnXoaNcc.Location = new Point(462, 235);
            btnXoaNcc.Margin = new Padding(4, 4, 4, 4);
            btnXoaNcc.Name = "btnXoaNcc";
            btnXoaNcc.Size = new Size(119, 44);
            btnXoaNcc.TabIndex = 13;
            btnXoaNcc.Text = "Xóa";
            btnXoaNcc.UseVisualStyleBackColor = false;
            btnXoaNcc.Click += btnXoaNcc_Click;
            // 
            // btnLamMoiNcc
            // 
            btnLamMoiNcc.Location = new Point(600, 235);
            btnLamMoiNcc.Margin = new Padding(4, 4, 4, 4);
            btnLamMoiNcc.Name = "btnLamMoiNcc";
            btnLamMoiNcc.Size = new Size(119, 44);
            btnLamMoiNcc.TabIndex = 14;
            btnLamMoiNcc.Text = "Làm mới";
            btnLamMoiNcc.UseVisualStyleBackColor = false;
            btnLamMoiNcc.Click += btnLamMoiNcc_Click;
            // 
            // txtTimKiemNcc
            // 
            txtTimKiemNcc.Location = new Point(750, 238);
            txtTimKiemNcc.Margin = new Padding(4, 4, 4, 4);
            txtTimKiemNcc.Name = "txtTimKiemNcc";
            txtTimKiemNcc.Size = new Size(224, 27);
            txtTimKiemNcc.TabIndex = 15;
            // 
            // tabLoaiHang
            // 
            tabLoaiHang.BackColor = Color.White;
            tabLoaiHang.Controls.Add(dgvLoaiHang);
            tabLoaiHang.Controls.Add(pnlLoaiHang);
            tabLoaiHang.Location = new Point(4, 39);
            tabLoaiHang.Margin = new Padding(4, 4, 4, 4);
            tabLoaiHang.Name = "tabLoaiHang";
            tabLoaiHang.Padding = new Padding(10, 10, 10, 10);
            tabLoaiHang.Size = new Size(1740, 707);
            tabLoaiHang.TabIndex = 1;
            tabLoaiHang.Text = "Loại hàng";
            // 
            // dgvLoaiHang
            // 
            dgvLoaiHang.AllowUserToAddRows = false;
            dgvLoaiHang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoaiHang.Dock = DockStyle.Fill;
            dgvLoaiHang.Location = new Point(10, 267);
            dgvLoaiHang.Margin = new Padding(4, 4, 4, 4);
            dgvLoaiHang.Name = "dgvLoaiHang";
            dgvLoaiHang.ReadOnly = true;
            dgvLoaiHang.RowHeadersWidth = 51;
            dgvLoaiHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoaiHang.Size = new Size(1720, 430);
            dgvLoaiHang.TabIndex = 1;
            dgvLoaiHang.CellClick += dgvLoaiHang_CellClick;
            // 
            // pnlLoaiHang
            // 
            pnlLoaiHang.BackColor = Color.WhiteSmoke;
            pnlLoaiHang.BorderStyle = BorderStyle.FixedSingle;
            pnlLoaiHang.Controls.Add(lblTitleLoai);
            pnlLoaiHang.Controls.Add(lblTenLoai);
            pnlLoaiHang.Controls.Add(txtTenLoai);
            pnlLoaiHang.Controls.Add(lblGhiChuLoai);
            pnlLoaiHang.Controls.Add(txtGhiChuLoai);
            pnlLoaiHang.Controls.Add(btnThemLoai);
            pnlLoaiHang.Controls.Add(btnSuaLoai);
            pnlLoaiHang.Controls.Add(btnXoaLoai);
            pnlLoaiHang.Controls.Add(btnLamMoiLoai);
            pnlLoaiHang.Controls.Add(txtTimKiemLoai);
            pnlLoaiHang.Dock = DockStyle.Top;
            pnlLoaiHang.Location = new Point(10, 10);
            pnlLoaiHang.Margin = new Padding(4, 4, 4, 4);
            pnlLoaiHang.Name = "pnlLoaiHang";
            pnlLoaiHang.Size = new Size(1720, 257);
            pnlLoaiHang.TabIndex = 0;
            // 
            // lblTitleLoai
            // 
            lblTitleLoai.AutoSize = true;
            lblTitleLoai.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitleLoai.ForeColor = Color.FromArgb(30, 58, 138);
            lblTitleLoai.Location = new Point(25, 9);
            lblTitleLoai.Margin = new Padding(4, 0, 4, 0);
            lblTitleLoai.Name = "lblTitleLoai";
            lblTitleLoai.Size = new Size(321, 41);
            lblTitleLoai.TabIndex = 0;
            lblTitleLoai.Text = "QUẢN LÝ LOẠI HÀNG";
            // 
            // lblTenLoai
            // 
            lblTenLoai.AutoSize = true;
            lblTenLoai.Location = new Point(25, 85);
            lblTenLoai.Margin = new Padding(4, 0, 4, 0);
            lblTenLoai.Name = "lblTenLoai";
            lblTenLoai.Size = new Size(101, 20);
            lblTenLoai.TabIndex = 1;
            lblTenLoai.Text = "Tên loại hàng:";
            // 
            // txtTenLoai
            // 
            txtTenLoai.Location = new Point(188, 80);
            txtTenLoai.Margin = new Padding(4, 4, 4, 4);
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(449, 27);
            txtTenLoai.TabIndex = 2;
            // 
            // lblGhiChuLoai
            // 
            lblGhiChuLoai.AutoSize = true;
            lblGhiChuLoai.Location = new Point(25, 135);
            lblGhiChuLoai.Margin = new Padding(4, 0, 4, 0);
            lblGhiChuLoai.Name = "lblGhiChuLoai";
            lblGhiChuLoai.Size = new Size(61, 20);
            lblGhiChuLoai.TabIndex = 3;
            lblGhiChuLoai.Text = "Ghi chú:";
            // 
            // txtGhiChuLoai
            // 
            txtGhiChuLoai.Location = new Point(188, 130);
            txtGhiChuLoai.Margin = new Padding(4, 4, 4, 4);
            txtGhiChuLoai.Name = "txtGhiChuLoai";
            txtGhiChuLoai.Size = new Size(449, 27);
            txtGhiChuLoai.TabIndex = 4;
            // 
            // btnThemLoai
            // 
            btnThemLoai.Location = new Point(188, 198);
            btnThemLoai.Margin = new Padding(4, 4, 4, 4);
            btnThemLoai.Name = "btnThemLoai";
            btnThemLoai.Size = new Size(119, 44);
            btnThemLoai.TabIndex = 5;
            btnThemLoai.Text = "Thêm";
            btnThemLoai.UseVisualStyleBackColor = false;
            btnThemLoai.Click += btnThemLoai_Click;
            // 
            // btnSuaLoai
            // 
            btnSuaLoai.Location = new Point(325, 198);
            btnSuaLoai.Margin = new Padding(4, 4, 4, 4);
            btnSuaLoai.Name = "btnSuaLoai";
            btnSuaLoai.Size = new Size(119, 44);
            btnSuaLoai.TabIndex = 6;
            btnSuaLoai.Text = "Sửa";
            btnSuaLoai.UseVisualStyleBackColor = false;
            btnSuaLoai.Click += btnSuaLoai_Click;
            // 
            // btnXoaLoai
            // 
            btnXoaLoai.Location = new Point(462, 198);
            btnXoaLoai.Margin = new Padding(4, 4, 4, 4);
            btnXoaLoai.Name = "btnXoaLoai";
            btnXoaLoai.Size = new Size(119, 44);
            btnXoaLoai.TabIndex = 7;
            btnXoaLoai.Text = "Xóa";
            btnXoaLoai.UseVisualStyleBackColor = false;
            btnXoaLoai.Click += btnXoaLoai_Click;
            // 
            // btnLamMoiLoai
            // 
            btnLamMoiLoai.Location = new Point(600, 198);
            btnLamMoiLoai.Margin = new Padding(4, 4, 4, 4);
            btnLamMoiLoai.Name = "btnLamMoiLoai";
            btnLamMoiLoai.Size = new Size(119, 44);
            btnLamMoiLoai.TabIndex = 8;
            btnLamMoiLoai.Text = "Làm mới";
            btnLamMoiLoai.UseVisualStyleBackColor = false;
            btnLamMoiLoai.Click += btnLamMoiLoai_Click;
            // 
            // txtTimKiemLoai
            // 
            txtTimKiemLoai.Location = new Point(750, 200);
            txtTimKiemLoai.Margin = new Padding(4, 4, 4, 4);
            txtTimKiemLoai.Name = "txtTimKiemLoai";
            txtTimKiemLoai.Size = new Size(224, 27);
            txtTimKiemLoai.TabIndex = 9;
            // 
            // FrmDanhMuc
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1748, 750);
            Controls.Add(tabDanhMuc);
            Margin = new Padding(4, 4, 4, 4);
            Name = "FrmDanhMuc";
            StartPosition = FormStartPosition.Manual;
            Text = "Quản lý Danh mục";
            Load += FrmDanhMuc_Load;
            tabDanhMuc.ResumeLayout(false);
            tabNhaCungCap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNhaCungCap).EndInit();
            pnlNhaCungCap.ResumeLayout(false);
            pnlNhaCungCap.PerformLayout();
            tabLoaiHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLoaiHang).EndInit();
            pnlLoaiHang.ResumeLayout(false);
            pnlLoaiHang.PerformLayout();
            ResumeLayout(false);
        }
    }
}
