using System;
using System.Data;
using System.Windows.Forms;
using QuanLyKhoHang.ApiClients;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form quản lý danh mục khách hàng.
    /// Khách hàng được dùng trong nghiệp vụ xuất kho/bán hàng.
    /// </summary>
    public partial class FrmKhachHang : Form
    {
        private readonly KhachHangApiClient _khachHangRepo = new KhachHangApiClient();
        private int _selectedId = 0;
        private readonly string _vaiTro;

        /// <summary>
        /// Constructor không tham số để WinForms Designer có thể khởi tạo form.
        /// </summary>
        public FrmKhachHang() : this(string.Empty)
        {
        }

        /// <summary>
        /// Khởi tạo form khách hàng và nhận vai trò người dùng để giới hạn quyền xóa.
        /// </summary>
        public FrmKhachHang(string vaiTro)
        {
            _vaiTro = vaiTro;
            InitializeComponent();

            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            UiTheme.Apply(this);
            UiTheme.AddSearchButton(txtTimKiem, () => txtTimKiem_TextChanged(this, EventArgs.Empty));
            UiTheme.PlaceSearchControlsBesideAddButton(pnlTopControls, txtTimKiem, btnLamMoi);
        }

        /// <summary>
        /// Nạp dữ liệu khách hàng và khóa nút xóa nếu người dùng là nhân viên.
        /// </summary>
        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            LoadData();
            btnXoa.Enabled = !string.Equals(_vaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Nạp danh sách khách hàng lên lưới.
        /// </summary>
        private void LoadData()
        {
            try
            {
                dgvKhachHang.DataSource = _khachHangRepo.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách khách hàng.\nChi tiết: " + ex.Message,
                    "Không thể tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Thêm khách hàng mới từ dữ liệu nhập trên form.
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTen.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!");
                return;
            }

            try
            {
                _khachHangRepo.Them(BuildKhachHangFromInput());
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm khách hàng: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi chọn khách hàng trên lưới, đưa dữ liệu lên form để sửa/xóa.
        /// </summary>
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
            _selectedId = Convert.ToInt32(row.Cells[0].Value);
            txtTen.Text = row.Cells[1].Value?.ToString();
            txtDiaChi.Text = row.Cells[2].Value?.ToString();
            txtSDT.Text = row.Cells[3].Value?.ToString();
            txtEmail.Text = row.Cells[4].Value?.ToString();
            txtGhiChu.Text = row.Cells[5].Value?.ToString();
        }

        /// <summary>
        /// Cập nhật khách hàng đang được chọn.
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                return;
            }

            try
            {
                KhachHang kh = BuildKhachHangFromInput();
                kh.MaKhachHang = _selectedId;
                _khachHangRepo.Sua(kh);
                MessageBox.Show("Cập nhật khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật khách hàng: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa khách hàng đang được chọn.
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng từ bảng để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa khách hàng này khỏi danh mục?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _khachHangRepo.Xoa(_selectedId);
                MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa khách hàng: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm kiếm nhanh theo mã, tên, địa chỉ, số điện thoại, email hoặc ghi chú.
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvKhachHang.DataSource is not DataTable dt)
            {
                return;
            }

            if (dt.Columns.Count < 6)
            {
                return;
            }

            string tuKhoa = UiTheme.EscapeRowFilterValue(txtTimKiem.Text.Trim());
            if (string.IsNullOrEmpty(tuKhoa))
            {
                dt.DefaultView.RowFilter = string.Empty;
                return;
            }

            dt.DefaultView.RowFilter =
                $"Convert([{dt.Columns[0].ColumnName}], 'System.String') LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[1].ColumnName}] LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[2].ColumnName}] LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[3].ColumnName}] LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[4].ColumnName}] LIKE '%{tuKhoa}%' " +
                $"OR [{dt.Columns[5].ColumnName}] LIKE '%{tuKhoa}%'";
        }

        /// <summary>
        /// Làm mới danh sách và xóa trạng thái chọn.
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
        }

        /// <summary>
        /// Gom dữ liệu nhập liệu thành model KhachHang.
        /// </summary>
        private KhachHang BuildKhachHangFromInput()
        {
            return new KhachHang
            {
                TenKhachHang = txtTen.Text.Trim(),
                DiaChiKH = txtDiaChi.Text.Trim(),
                SoDienThoai = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim()
            };
        }

        /// <summary>
        /// Xóa các ô nhập trên form.
        /// </summary>
        private void ClearInputs()
        {
            _selectedId = 0;
            txtTen.Clear();
            txtDiaChi.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtGhiChu.Clear();
        }
    }
}
