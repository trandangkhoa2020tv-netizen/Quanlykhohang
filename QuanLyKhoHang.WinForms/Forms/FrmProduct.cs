using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using QuanLyKhoHang.ApiClients;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form quản lý danh mục hàng hóa.
    /// Người dùng có thể xem, thêm, sửa, xóa và tìm kiếm hàng hóa trong kho.
    /// </summary>
    public partial class FrmHangHoa : Form
    {
        private readonly HangHoaApiClient _hangHoaRepo = new HangHoaApiClient();
        private readonly LoaiHangApiClient _loaiHangRepo = new LoaiHangApiClient();
        private readonly NhaCungCapApiClient _nccRepo = new NhaCungCapApiClient();

        // Mã hàng đang được chọn trên DataGridView. Giá trị 0 nghĩa là chưa chọn dòng nào.
        private int _selectedId = 0;

        // Vai trò được truyền từ FrmMain để khóa/mở quyền thao tác.
        private readonly string _vaiTro;

        /// <summary>
        /// Constructor không tham số để WinForms Designer có thể khởi tạo form.
        /// </summary>
        public FrmHangHoa() : this(string.Empty)
        {
        }

        /// <summary>
        /// Khởi tạo form hàng hóa và nhận vai trò người dùng để phân quyền thao tác.
        /// </summary>
        public FrmHangHoa(string vaiTro)
        {
            _vaiTro = vaiTro;
            InitializeComponent();

            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            UiTheme.Apply(this);
            UiTheme.AddSearchButton(txtTimKiem, () => txtTimKiem_TextChanged(this, EventArgs.Empty));
        }

        /// <summary>
        /// Khi form mở: nạp dữ liệu hàng hóa, nạp combobox loại hàng/nhà cung cấp và áp dụng phân quyền.
        /// </summary>
        private void FrmHangHoa_Load(object sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            try
            {
                LoadDataGrid();
                LoadComboBoxes();
                btnXoa.Enabled = !string.Equals(_vaiTro, "NhanVien", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu hàng hóa. Vui lòng kiểm tra kết nối API/database.\nChi tiết: " + ex.Message,
                    "Không thể tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Nạp danh sách hàng hóa vào lưới.
        /// </summary>
        private void LoadDataGrid()
        {
            try
            {
                dgvHangHoa.DataSource = _hangHoaRepo.GetAll();
                if (dgvHangHoa.Columns.Count > 0)
                {
                    dgvHangHoa.Columns[0].Width = 70;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải danh sách hàng hóa.\nChi tiết: " + ex.Message,
                    "Không thể tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Sau khi lưới gắn dữ liệu, định dạng các cột số để dễ đọc.
        /// </summary>
        private void dgvHangHoa_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyNumberColumnFormat(4);
            ApplyNumberColumnFormat(5);
            ApplyNumberColumnFormat(6);
        }

        /// <summary>
        /// Áp dụng định dạng số tiếng Việt và căn phải cho một cột trong lưới hàng hóa.
        /// </summary>
        private void ApplyNumberColumnFormat(int columnIndex)
        {
            if (dgvHangHoa.Columns.Count <= columnIndex)
            {
                return;
            }

            DataGridViewCellStyle style = dgvHangHoa.Columns[columnIndex].DefaultCellStyle;
            style.FormatProvider = CultureInfo.GetCultureInfo("vi-VN");
            style.Format = "N0";
            style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Nạp danh sách loại hàng và nhà cung cấp vào combobox.
        /// </summary>
        private void LoadComboBoxes()
        {
            try
            {
                DataTable dtLoai = _loaiHangRepo.GetAll();
                UiTheme.BindComboBox(cbLoaiHang, dtLoai, 1, 0);

                DataTable dtNcc = _nccRepo.GetAll();
                UiTheme.BindComboBox(cbNhaCungCap, dtNcc, 1, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải loại hàng/nhà cung cấp.\nChi tiết: " + ex.Message,
                    "Không thể tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Thêm hàng hóa mới từ dữ liệu người dùng nhập trên form.
        /// </summary>
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenHang.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập tên hàng hóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _hangHoaRepo.Them(BuildHangHoaFromInput());
                MessageBox.Show("Thêm mới hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm hàng hóa: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi chọn một dòng trên lưới, đổ dữ liệu dòng đó lên các ô nhập để người dùng sửa/xóa.
        /// </summary>
        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvHangHoa.Rows[e.RowIndex];
            _selectedId = Convert.ToInt32(row.Cells[0].Value);
            txtTenHang.Text = row.Cells[1].Value?.ToString();
            cbLoaiHang.Text = row.Cells[2].Value?.ToString();
            cbNhaCungCap.Text = row.Cells[3].Value?.ToString();
            txtGiaNhap.Text = row.Cells[4].Value?.ToString();
            txtGiaBan.Text = row.Cells[5].Value?.ToString();
            txtSoLuong.Text = row.Cells[6].Value?.ToString();
            txtDVT.Text = row.Cells[7].Value?.ToString();
            txtGhiChu.Text = row.Cells[8].Value?.ToString();
        }

        /// <summary>
        /// Cập nhật hàng hóa đang được chọn.
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng từ bảng để sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                HangHoa hh = BuildHangHoaFromInput();
                hh.MaHangHoa = _selectedId;
                _hangHoaRepo.Sua(hh);
                MessageBox.Show("Cập nhật hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật hàng hóa: {ex.Message}", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xóa hàng hóa đang chọn. Nếu hàng đã có trong phiếu nhập/xuất, database sẽ không cho xóa.
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedId == 0)
            {
                MessageBox.Show("Vui lòng chọn một mặt hàng từ bảng để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa mặt hàng này khỏi danh mục?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _hangHoaRepo.Xoa(_selectedId);
                MessageBox.Show("Xóa hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                LoadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể xóa hàng hóa này vì đang được dùng trong chứng từ.\nChi tiết: {ex.Message}", "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Làm mới dữ liệu và xóa trạng thái chọn hiện tại.
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadDataGrid();
        }

        /// <summary>
        /// Tìm kiếm nhanh theo tên hàng, loại hàng hoặc nhà cung cấp trên dữ liệu đang hiển thị.
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvHangHoa.DataSource is not DataTable dt)
            {
                return;
            }

            if (dt.Columns.Count < 4)
            {
                return;
            }

            string tuKhoa = UiTheme.EscapeRowFilterValue(txtTimKiem.Text.Trim());
            if (string.IsNullOrEmpty(tuKhoa))
            {
                dt.DefaultView.RowFilter = string.Empty;
                return;
            }

            string tenHang = dt.Columns[1].ColumnName;
            string loaiHang = dt.Columns[2].ColumnName;
            string nhaCungCap = dt.Columns[3].ColumnName;
            dt.DefaultView.RowFilter = $"[{tenHang}] LIKE '%{tuKhoa}%' OR [{loaiHang}] LIKE '%{tuKhoa}%' OR [{nhaCungCap}] LIKE '%{tuKhoa}%'";
        }

        /// <summary>
        /// Gom dữ liệu từ các ô nhập thành model HangHoa để gửi xuống repository.
        /// </summary>
        private HangHoa BuildHangHoaFromInput()
        {
            return new HangHoa
            {
                TenHangHoa = txtTenHang.Text.Trim(),
                MaLoaiHang = Convert.ToInt32(cbLoaiHang.SelectedValue),
                MaNhaCungCap = Convert.ToInt32(cbNhaCungCap.SelectedValue),
                GiaNhap = string.IsNullOrWhiteSpace(txtGiaNhap.Text) ? 0 : Convert.ToDecimal(txtGiaNhap.Text),
                GiaBan = string.IsNullOrWhiteSpace(txtGiaBan.Text) ? 0 : Convert.ToDecimal(txtGiaBan.Text),
                SoLuongTon = string.IsNullOrWhiteSpace(txtSoLuong.Text) ? 0 : Convert.ToInt32(txtSoLuong.Text),
                DonViTinh = txtDVT.Text.Trim(),
                GhiChu = txtGhiChu.Text.Trim()
            };
        }

        /// <summary>
        /// Xóa nội dung các ô nhập và đưa form về trạng thái chưa chọn dòng nào.
        /// </summary>
        private void ClearInputs()
        {
            _selectedId = 0;
            txtTenHang.Clear();
            txtGiaNhap.Clear();
            txtGiaBan.Clear();
            txtSoLuong.Clear();
            txtDVT.Clear();
            txtGhiChu.Clear();
            if (cbLoaiHang.Items.Count > 0) cbLoaiHang.SelectedIndex = 0;
            if (cbNhaCungCap.Items.Count > 0) cbNhaCungCap.SelectedIndex = 0;
            txtTenHang.Focus();
        }
    }
}
