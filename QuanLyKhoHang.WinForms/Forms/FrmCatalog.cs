using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using QuanLyKhoHang.ApiClients;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Trang quản lý nhà cung cấp và loại hàng dùng chung cho các combobox danh mục.
    /// </summary>
    public partial class FrmDanhMuc : Form
    {
        private readonly NhaCungCapApiClient _nccRepo = new NhaCungCapApiClient();
        private readonly LoaiHangApiClient _loaiHangRepo = new LoaiHangApiClient();

        private int _selectedNccId;
        private int _selectedLoaiId;

        public FrmDanhMuc()
        {
            InitializeComponent();

            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            UiTheme.Apply(this);
            UiTheme.AddSearchButton(txtTimKiemNcc, () => LocDuLieu(dgvNhaCungCap, txtTimKiemNcc));
            UiTheme.AddSearchButton(txtTimKiemLoai, () => LocDuLieu(dgvLoaiHang, txtTimKiemLoai));
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            LoadNhaCungCap();
            LoadLoaiHang();
        }

        private void LoadNhaCungCap()
        {
            try
            {
                dgvNhaCungCap.DataSource = _nccRepo.GetAll();
                dgvNhaCungCap.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowLoadError("nhà cung cấp", ex);
            }
        }

        private void LoadLoaiHang()
        {
            try
            {
                dgvLoaiHang.DataSource = _loaiHangRepo.GetAll();
                dgvLoaiHang.ClearSelection();
            }
            catch (Exception ex)
            {
                ShowLoadError("loại hàng", ex);
            }
        }

        private static void ShowLoadError(string danhMuc, Exception ex)
        {
            MessageBox.Show(
                $"Không thể tải danh sách {danhMuc}.\nChi tiết: {ex.Message}",
                "Không thể tải dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private void btnThemNcc_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenNcc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNcc.Focus();
                return;
            }

            try
            {
                _nccRepo.Them(BuildNhaCungCap());
                MessageBox.Show("Thêm nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearNhaCungCap();
                LoadNhaCungCap();
            }
            catch (Exception ex)
            {
                ShowSaveError("thêm nhà cung cấp", ex);
            }
        }

        private void btnSuaNcc_Click(object sender, EventArgs e)
        {
            if (_selectedNccId == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenNcc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNcc.Focus();
                return;
            }

            try
            {
                NhaCungCap nhaCungCap = BuildNhaCungCap();
                nhaCungCap.MaNhaCungCap = _selectedNccId;
                _nccRepo.Sua(nhaCungCap);
                MessageBox.Show("Cập nhật nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearNhaCungCap();
                LoadNhaCungCap();
            }
            catch (Exception ex)
            {
                ShowSaveError("cập nhật nhà cung cấp", ex);
            }
        }

        private void btnXoaNcc_Click(object sender, EventArgs e)
        {
            if (_selectedNccId == 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa nhà cung cấp này? Nếu đang được dùng trong phiếu nhập, hệ thống sẽ từ chối xóa.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _nccRepo.Xoa(_selectedNccId);
                MessageBox.Show("Xóa nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearNhaCungCap();
                LoadNhaCungCap();
            }
            catch (Exception ex)
            {
                ShowSaveError("xóa nhà cung cấp", ex);
            }
        }

        private void btnLamMoiNcc_Click(object sender, EventArgs e)
        {
            ClearNhaCungCap();
            LoadNhaCungCap();
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvNhaCungCap.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = dgvNhaCungCap.Rows[e.RowIndex];
            if (row.Cells.Count < 6 || row.Cells[0].Value == null || row.Cells[0].Value == DBNull.Value)
            {
                return;
            }

            _selectedNccId = Convert.ToInt32(row.Cells[0].Value);
            txtTenNcc.Text = CellText(row, 1);
            txtDiaChiNcc.Text = CellText(row, 2);
            txtSdtNcc.Text = CellText(row, 3);
            txtEmailNcc.Text = CellText(row, 4);
            txtGhiChuNcc.Text = CellText(row, 5);
        }

        private void btnThemLoai_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            try
            {
                _loaiHangRepo.Them(BuildLoaiHang());
                MessageBox.Show("Thêm loại hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearLoaiHang();
                LoadLoaiHang();
            }
            catch (Exception ex)
            {
                ShowSaveError("thêm loại hàng", ex);
            }
        }

        private void btnSuaLoai_Click(object sender, EventArgs e)
        {
            if (_selectedLoaiId == 0)
            {
                MessageBox.Show("Vui lòng chọn loại hàng cần sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại hàng.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLoai.Focus();
                return;
            }

            try
            {
                LoaiHang loaiHang = BuildLoaiHang();
                loaiHang.MaLoaiHang = _selectedLoaiId;
                _loaiHangRepo.Sua(loaiHang);
                MessageBox.Show("Cập nhật loại hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearLoaiHang();
                LoadLoaiHang();
            }
            catch (Exception ex)
            {
                ShowSaveError("cập nhật loại hàng", ex);
            }
        }

        private void btnXoaLoai_Click(object sender, EventArgs e)
        {
            if (_selectedLoaiId == 0)
            {
                MessageBox.Show("Vui lòng chọn loại hàng cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                    "Bạn có chắc muốn xóa loại hàng này? Nếu đang được dùng trong hàng hóa, hệ thống sẽ từ chối xóa.",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                _loaiHangRepo.Xoa(_selectedLoaiId);
                MessageBox.Show("Xóa loại hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearLoaiHang();
                LoadLoaiHang();
            }
            catch (Exception ex)
            {
                ShowSaveError("xóa loại hàng", ex);
            }
        }

        private void btnLamMoiLoai_Click(object sender, EventArgs e)
        {
            ClearLoaiHang();
            LoadLoaiHang();
        }

        private void dgvLoaiHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLoaiHang.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = dgvLoaiHang.Rows[e.RowIndex];
            if (row.Cells.Count < 3 || row.Cells[0].Value == null || row.Cells[0].Value == DBNull.Value)
            {
                return;
            }

            _selectedLoaiId = Convert.ToInt32(row.Cells[0].Value);
            txtTenLoai.Text = CellText(row, 1);
            txtGhiChuLoai.Text = CellText(row, 2);
        }

        private NhaCungCap BuildNhaCungCap()
        {
            return new NhaCungCap
            {
                TenNhaCungCap = txtTenNcc.Text.Trim(),
                DiaChiNCC = txtDiaChiNcc.Text.Trim(),
                SoDienThoai = txtSdtNcc.Text.Trim(),
                Email = txtEmailNcc.Text.Trim(),
                GhiChu = txtGhiChuNcc.Text.Trim()
            };
        }

        private LoaiHang BuildLoaiHang()
        {
            return new LoaiHang
            {
                TenLoaiHang = txtTenLoai.Text.Trim(),
                GhiChu = txtGhiChuLoai.Text.Trim()
            };
        }

        private void ClearNhaCungCap()
        {
            _selectedNccId = 0;
            txtTenNcc.Clear();
            txtDiaChiNcc.Clear();
            txtSdtNcc.Clear();
            txtEmailNcc.Clear();
            txtGhiChuNcc.Clear();
            dgvNhaCungCap.ClearSelection();
        }

        private void ClearLoaiHang()
        {
            _selectedLoaiId = 0;
            txtTenLoai.Clear();
            txtGhiChuLoai.Clear();
            dgvLoaiHang.ClearSelection();
        }

        private static string CellText(DataGridViewRow row, int index)
        {
            object value = row.Cells[index].Value;
            return value == null || value == DBNull.Value ? string.Empty : Convert.ToString(value) ?? string.Empty;
        }

        private static void ShowSaveError(string action, Exception ex)
        {
            MessageBox.Show(
                $"Không thể {action}.\nChi tiết: {ex.Message}",
                "Lỗi thao tác",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void LocDuLieu(DataGridView grid, TextBox searchBox)
        {
            if (grid.DataSource is not DataTable table || table.Columns.Count == 0)
            {
                return;
            }

            string keyword = UiTheme.EscapeRowFilterValue(searchBox.Text.Trim());
            if (string.IsNullOrEmpty(keyword))
            {
                table.DefaultView.RowFilter = string.Empty;
                return;
            }

            List<string> expressions = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                string columnReference = $"[{column.ColumnName}]";
                if (column.DataType != typeof(string))
                {
                    columnReference = $"Convert({columnReference}, 'System.String')";
                }

                expressions.Add($"{columnReference} LIKE '%{keyword}%'");
            }

            table.DefaultView.RowFilter = string.Join(" OR ", expressions);
        }

    }
}
