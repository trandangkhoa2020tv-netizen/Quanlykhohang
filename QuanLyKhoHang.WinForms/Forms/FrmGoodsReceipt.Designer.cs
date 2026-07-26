namespace QuanLyKhoHang.Forms
{
    partial class FrmNhapKho
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.GroupBox groupPhieu;
        private System.Windows.Forms.GroupBox groupHang;
        private System.Windows.Forms.ComboBox cbNCC;
        private System.Windows.Forms.ComboBox cbNhanVien;
        private System.Windows.Forms.ComboBox cbHangHoa;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.TextBox txtGhiChuPhieu;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.DataGridView dgvLichSuPhieu;
        private System.Windows.Forms.Button btnThemMon;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblNhaCungCap;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblGhiChuPhieu;
        private System.Windows.Forms.Label lblHangHoa;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblLichSuTitle;
        private System.Windows.Forms.Label lblTimKiem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.groupPhieu = new System.Windows.Forms.GroupBox();
            this.groupHang = new System.Windows.Forms.GroupBox();
            this.cbNCC = new System.Windows.Forms.ComboBox();
            this.cbNhanVien = new System.Windows.Forms.ComboBox();
            this.cbHangHoa = new System.Windows.Forms.ComboBox();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.txtGhiChuPhieu = new System.Windows.Forms.TextBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.dgvLichSuPhieu = new System.Windows.Forms.DataGridView();
            this.btnThemMon = new System.Windows.Forms.Button();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnPdf = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblNhaCungCap = new System.Windows.Forms.Label();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblGhiChuPhieu = new System.Windows.Forms.Label();
            this.lblHangHoa = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.lblLichSuTitle = new System.Windows.Forms.Label();
            this.lblTimKiem = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuPhieu)).BeginInit();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.groupPhieu.SuspendLayout();
            this.groupHang.SuspendLayout();
            this.SuspendLayout();

            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 330;
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.groupPhieu, this.groupHang, this.lblTimKiem, this.txtTimKiem
            });

            this.lblTitle.Text = "LẬP PHIẾU NHẬP KHO";
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.AutoSize = true;
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 58, 138);

            this.groupPhieu.Text = "Thông tin chung";
            this.groupPhieu.Location = new System.Drawing.Point(20, 68);
            this.groupPhieu.Size = new System.Drawing.Size(940, 112);
            this.groupPhieu.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.groupPhieu.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblNhaCungCap, this.cbNCC, this.lblNhanVien, this.cbNhanVien,
                this.lblGhiChuPhieu, this.txtGhiChuPhieu
            });

            this.lblNhaCungCap.Text = "Nhà cung cấp:";
            this.lblNhaCungCap.Location = new System.Drawing.Point(15, 28);
            this.lblNhaCungCap.AutoSize = true;

            this.cbNCC.Location = new System.Drawing.Point(135, 30);
            this.cbNCC.Size = new System.Drawing.Size(280, 27);
            this.cbNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblNhanVien.Text = "Nhân viên lập:";
            this.lblNhanVien.Location = new System.Drawing.Point(455, 28);
            this.lblNhanVien.AutoSize = true;

            this.cbNhanVien.Location = new System.Drawing.Point(590, 30);
            this.cbNhanVien.Size = new System.Drawing.Size(280, 27);
            this.cbNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblGhiChuPhieu.Text = "Ghi chú phiếu:";
            this.lblGhiChuPhieu.Location = new System.Drawing.Point(15, 68);
            this.lblGhiChuPhieu.AutoSize = true;

            this.txtGhiChuPhieu.Location = new System.Drawing.Point(135, 70);
            this.txtGhiChuPhieu.Size = new System.Drawing.Size(755, 27);

            this.groupHang.Text = "Chi tiết mặt hàng";
            this.groupHang.Location = new System.Drawing.Point(20, 190);
            this.groupHang.Size = new System.Drawing.Size(940, 82);
            this.groupHang.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.groupHang.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblHangHoa, this.cbHangHoa, this.lblSoLuong, this.txtSoLuong,
                this.lblDonGia, this.txtDonGia, this.btnThemMon
            });

            this.lblHangHoa.Text = "Mặt hàng:";
            this.lblHangHoa.Location = new System.Drawing.Point(18, 33);
            this.lblHangHoa.AutoSize = true;

            this.cbHangHoa.Location = new System.Drawing.Point(115, 35);
            this.cbHangHoa.Size = new System.Drawing.Size(240, 27);
            this.cbHangHoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblSoLuong.Text = "Số lượng:";
            this.lblSoLuong.Location = new System.Drawing.Point(380, 33);
            this.lblSoLuong.AutoSize = true;

            this.txtSoLuong.Location = new System.Drawing.Point(465, 35);
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(90, 27);

            this.lblDonGia.Text = "Đơn giá nhập:";
            this.lblDonGia.Location = new System.Drawing.Point(573, 33);
            this.lblDonGia.AutoSize = true;

            this.txtDonGia.Location = new System.Drawing.Point(690, 35);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(90, 27);

            this.btnThemMon.Text = "Thêm hàng";
            this.btnThemMon.Name = "btnThemMon";
            this.btnThemMon.Location = new System.Drawing.Point(790, 31);
            this.btnThemMon.Size = new System.Drawing.Size(100, 36);
            this.btnThemMon.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnThemMon.ForeColor = System.Drawing.Color.White;
            this.btnThemMon.Click += new System.EventHandler(this.btnThemMon_Click);

            this.lblTimKiem.Text = "Tìm nhanh phiếu:";
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Visible = false;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(500, 286);
            this.lblTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;

            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Location = new System.Drawing.Point(500, 282);
            this.txtTimKiem.Size = new System.Drawing.Size(175, 30);
            this.txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Height = 64;
            this.pnlBottom.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(16, 10, 16, 10);
            this.pnlBottom.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTongTien, this.btnLuuPhieu, this.btnExcel, this.btnPdf
            });

            this.lblTongTien.Text = "TỔNG TIỀN: 0 VNĐ";
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.Green;
            this.lblTongTien.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTongTien.AutoSize = false;
            this.lblTongTien.Width = 360;

            this.btnLuuPhieu.Text = "Lưu phiếu nhập";
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Width = 164;
            this.btnLuuPhieu.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);

            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Width = 112;
            this.btnExcel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);

            this.btnPdf.Text = "Xuất PDF";
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Width = 104;
            this.btnPdf.BackColor = System.Drawing.Color.IndianRed;
            this.btnPdf.ForeColor = System.Drawing.Color.White;
            this.btnPdf.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);

            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.Height = 125;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvChiTiet_DataBindingComplete);

            this.lblLichSuTitle.Text = "  Lịch sử phiếu nhập đã lưu (click chọn dòng để xuất file)";
            this.lblLichSuTitle.Name = "lblLichSuTitle";
            this.lblLichSuTitle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblLichSuTitle.BackColor = System.Drawing.Color.FromArgb(239, 246, 255);
            this.lblLichSuTitle.ForeColor = System.Drawing.Color.FromArgb(30, 58, 138);
            this.lblLichSuTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLichSuTitle.Height = 40;
            this.lblLichSuTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.dgvLichSuPhieu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLichSuPhieu.Name = "dgvLichSuPhieu";
            this.dgvLichSuPhieu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLichSuPhieu.AllowUserToAddRows = false;
            this.dgvLichSuPhieu.ReadOnly = true;
            this.dgvLichSuPhieu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSuPhieu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSuPhieu_CellClick);
            this.dgvLichSuPhieu.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvLichSuPhieu_DataBindingComplete);

            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(980, 680);
            this.Controls.Add(this.dgvLichSuPhieu);
            this.Controls.Add(this.lblLichSuTitle);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.pnlTop);
            this.Name = "FrmNhapKho";
            this.Text = "Lập Phiếu Nhập Kho";
            this.Load += new System.EventHandler(this.FrmNhapKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSuPhieu)).EndInit();
            this.groupHang.ResumeLayout(false);
            this.groupHang.PerformLayout();
            this.groupPhieu.ResumeLayout(false);
            this.groupPhieu.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
