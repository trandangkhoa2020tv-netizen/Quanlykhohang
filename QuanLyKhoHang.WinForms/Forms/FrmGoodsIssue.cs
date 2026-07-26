using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using QuanLyKhoHang.ApiClients;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Forms
{
    /// <summary>
    /// Form lập phiếu xuất kho.
    /// Người dùng chọn khách hàng, nhân viên, thêm danh sách hàng xuất, hệ thống kiểm tra tồn kho rồi lưu phiếu.
    /// </summary>
    public partial class FrmXuatKho : Form
    {
        private readonly PhieuXuatApiClient _pxRepo = new PhieuXuatApiClient();
        private readonly KhachHangApiClient _khRepo = new KhachHangApiClient();
        private readonly NhanVienApiClient _nvRepo = new NhanVienApiClient();
        private readonly HangHoaApiClient _hhRepo = new HangHoaApiClient();

        private DataTable _dtChiTietLocal;
        private decimal _tongTienPhieu;

        // Vai trò được truyền từ FrmMain. Hiện form chưa khóa thao tác theo vai trò, nhưng vẫn giữ để mở rộng.
        private readonly string _vaiTro;

        // Mã phiếu đang chọn ở bảng lịch sử, dùng khi xuất Excel/PDF.
        private int _maPhieuDuocChon;

        /// <summary>
        /// Constructor không tham số để WinForms Designer có thể khởi tạo form.
        /// </summary>
        public FrmXuatKho() : this(string.Empty)
        {
        }

        /// <summary>
        /// Khởi tạo form xuất kho và nhận vai trò người dùng để phục vụ phân quyền.
        /// </summary>
        public FrmXuatKho(string vaiTro)
        {
            _vaiTro = vaiTro;
            InitializeComponent();

            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            UiTheme.Apply(this);
            UiTheme.AddSearchButton(txtTimKiem, () => txtTimKiem_TextChanged(this, EventArgs.Empty));
            UiTheme.PlaceSearchControlsBesideAddButton(pnlTop, txtTimKiem, btnThemMon);
        }

        /// <summary>
        /// Nạp dữ liệu combobox, tạo bảng chi tiết tạm và nạp lịch sử phiếu xuất.
        /// </summary>
        private void FrmXuatKho_Load(object sender, EventArgs e)
        {
            if (DesignTimeHelper.IsDesignMode)
            {
                return;
            }

            _dtChiTietLocal = TaoBangChiTietTam();
            dgvChiTiet.DataSource = _dtChiTietLocal;

            try
            {
                UiTheme.BindComboBox(cbKhachHang, _khRepo.GetAll(), 1, 0);
                UiTheme.BindComboBox(cbNhanVien, _nvRepo.GetAll(), 1, 0);
                UiTheme.BindComboBox(cbHangHoa, _hhRepo.GetAll(), 1, 0);
                HienThiLichSuPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể tải dữ liệu lập phiếu. Vui lòng kiểm tra kết nối API/database.\nChi tiết: " + ex.Message,
                    "Không thể tải dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Tạo bảng tạm lưu các mặt hàng người dùng đã thêm vào phiếu nhưng chưa lưu database.
        /// </summary>
        private static DataTable TaoBangChiTietTam()
        {
            DataTable table = new DataTable();
            table.Columns.Add("MaHang", typeof(int));
            table.Columns.Add("TenHang", typeof(string));
            table.Columns.Add("SoLuong", typeof(int));
            table.Columns.Add("DonGia", typeof(decimal));
            table.Columns.Add("ThanhTien", typeof(decimal));
            return table;
        }

        /// <summary>
        /// Nạp lịch sử phiếu xuất từ database lên lưới bên dưới.
        /// </summary>
        private void HienThiLichSuPhieu()
        {
            try
            {
                dgvLichSuPhieu.DataSource = _pxRepo.GetAllPhieuXuat();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the nap lich su phieu xuat: " + ex.Message, "Loi");
            }
        }

        /// <summary>
        /// Sau khi lưới chi tiết có dữ liệu, định dạng cột số lượng, đơn giá và thành tiền.
        /// </summary>
        private void dgvChiTiet_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyNumberColumnFormat(dgvChiTiet, 3);
            ApplyNumberColumnFormat(dgvChiTiet, 4);
        }

        /// <summary>
        /// Sau khi lưới lịch sử có dữ liệu, định dạng cột tổng tiền.
        /// </summary>
        private void dgvLichSuPhieu_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyNumberColumnFormat(dgvLichSuPhieu, 4);
        }

        /// <summary>
        /// Áp dụng định dạng số tiếng Việt và căn phải cho một cột DataGridView.
        /// </summary>
        private static void ApplyNumberColumnFormat(DataGridView grid, int columnIndex)
        {
            if (grid.Columns.Count <= columnIndex)
            {
                return;
            }

            DataGridViewCellStyle style = grid.Columns[columnIndex].DefaultCellStyle;
            style.FormatProvider = CultureInfo.GetCultureInfo("vi-VN");
            style.Format = "N0";
            style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        /// <summary>
        /// Thêm một mặt hàng vào bảng chi tiết tạm.
        /// Trước khi thêm sẽ kiểm tra số lượng muốn xuất không vượt tồn kho hiện tại.
        /// </summary>
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (cbHangHoa.SelectedValue == null)
            {
                return;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out int soLuongMuonThem) || soLuongMuonThem <= 0)
            {
                MessageBox.Show("So luong xuat phai la so nguyen lon hon 0.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text.Trim(), NumberStyles.Number, CultureInfo.CurrentCulture, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Don gia xuat khong hop le.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return;
            }

            int maHang = Convert.ToInt32(cbHangHoa.SelectedValue);
            int tonKho = LayTonKho(maHang);
            int daThem = _dtChiTietLocal.AsEnumerable()
                .Where(row => Convert.ToInt32(row["MaHang"]) == maHang)
                .Sum(row => Convert.ToInt32(row["SoLuong"]));

            if (daThem + soLuongMuonThem > tonKho)
            {
                MessageBox.Show($"Khong du ton kho. Ton hien tai: {tonKho}, da them vao phieu: {daThem}.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal thanhTien = soLuongMuonThem * donGia;
            _dtChiTietLocal.Rows.Add(maHang, cbHangHoa.Text, soLuongMuonThem, donGia, thanhTien);
            TinhTongTien();
            txtSoLuong.Clear();
            txtDonGia.Clear();
        }

        /// <summary>
        /// Lấy số lượng tồn hiện tại của một mã hàng từ danh sách hàng hóa.
        /// </summary>
        private int LayTonKho(int maHang)
        {
            DataRow row = _hhRepo.GetAll().AsEnumerable()
                .FirstOrDefault(item => Convert.ToInt32(item[0]) == maHang);

            if (row == null)
            {
                throw new InvalidOperationException("Khong tim thay hang hoa trong he thong.");
            }

            return Convert.ToInt32(row[6]);
        }

        /// <summary>
        /// Tính lại tổng tiền phiếu từ các dòng trong bảng chi tiết tạm.
        /// </summary>
        private void TinhTongTien()
        {
            _tongTienPhieu = 0;
            foreach (DataRow row in _dtChiTietLocal.Rows)
            {
                _tongTienPhieu += Convert.ToDecimal(row["ThanhTien"]);
            }

            lblTongTien.Text = string.Format(new CultureInfo("vi-VN"), "TỔNG TIỀN: {0:N0} VNĐ", _tongTienPhieu);
        }

        /// <summary>
        /// Ghi nhận mã phiếu xuất đang chọn để dùng khi xuất file.
        /// </summary>
        private void dgvLichSuPhieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _maPhieuDuocChon = Convert.ToInt32(dgvLichSuPhieu.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        /// <summary>
        /// Tìm kiếm nhanh trên lịch sử phiếu xuất.
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (dgvLichSuPhieu.DataSource is DataTable table)
            {
                table.DefaultView.RowFilter = BuildFilter(table, txtTimKiem.Text.Trim());
            }
        }

        /// <summary>
        /// Tạo chuỗi lọc DataView theo mã phiếu, khách hàng hoặc nhân viên lập.
        /// </summary>
        private static string BuildFilter(DataTable table, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return string.Empty;
            }

            if (table.Columns.Count < 3)
            {
                return string.Empty;
            }

            string value = UiTheme.EscapeRowFilterValue(keyword);
            string maPhieu = table.Columns[0].ColumnName;
            string doiTac = table.Columns[1].ColumnName;
            string nhanVien = table.Columns[2].ColumnName;
            return $"Convert([{maPhieu}], 'System.String') LIKE '%{value}%' OR [{doiTac}] LIKE '%{value}%' OR [{nhanVien}] LIKE '%{value}%'";
        }

        /// <summary>
        /// Xuất chi tiết phiếu xuất đang chọn ra Excel.
        /// </summary>
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (_maPhieuDuocChon == 0)
            {
                MessageBox.Show("Vui long chon mot phieu xuat truoc khi xuat file.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable thongTinPhieu = _pxRepo.GetThongTinPhieuXuat(_maPhieuDuocChon);
                DataTable chiTietPhieu = _pxRepo.GetChiTietTheoMaPhieu(_maPhieuDuocChon);
                QuanLyKhoHang.Reports.ExportExcel.ToPhieuXuatExcel(thongTinPhieu, chiTietPhieu, $"Bao_Cao_Phieu_Xuat_So_{_maPhieuDuocChon}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi xuat file Excel: " + ex.Message, "Loi");
            }
        }

        /// <summary>
        /// Xuất chi tiết phiếu xuất đang chọn ra PDF.
        /// </summary>
        private void btnPdf_Click(object sender, EventArgs e)
        {
            if (_maPhieuDuocChon == 0)
            {
                MessageBox.Show("Vui long chon mot phieu xuat truoc khi xuat file.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable thongTinPhieu = _pxRepo.GetThongTinPhieuXuat(_maPhieuDuocChon);
                DataTable chiTietPhieu = _pxRepo.GetChiTietTheoMaPhieu(_maPhieuDuocChon);
                QuanLyKhoHang.Reports.ExportPdf.ToPhieuXuatPdf(thongTinPhieu, chiTietPhieu, $"Hoa_Don_Phieu_Xuat_So_{_maPhieuDuocChon}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi xuat file PDF: " + ex.Message, "Loi");
            }
        }

        /// <summary>
        /// Hàm dùng chung cho xuất Excel/PDF.
        /// Lấy chi tiết phiếu theo mã đang chọn rồi gọi delegate xuất file tương ứng.
        /// </summary>
        private void XuatFile(Action<DataTable, string> exportAction, string filePrefix)
        {
            if (_maPhieuDuocChon == 0)
            {
                MessageBox.Show("Vui long chon mot phieu xuat truoc khi xuat file.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable chiTietPhieu = _pxRepo.GetChiTietTheoMaPhieu(_maPhieuDuocChon);
                exportAction(chiTietPhieu, $"{filePrefix}_So_{_maPhieuDuocChon}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi xuat file: " + ex.Message, "Loi");
            }
        }

        /// <summary>
        /// Lưu phiếu xuất bằng transaction trong repository.
        /// Repository sẽ kiểm tra và trừ tồn kho, nếu thiếu tồn sẽ rollback toàn bộ.
        /// </summary>
        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_dtChiTietLocal.Rows.Count == 0)
            {
                MessageBox.Show("Chua co mat hang nao trong phieu xuat.", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PhieuXuat phieuXuat = new PhieuXuat
                {
                    MaKhachHang = Convert.ToInt32(cbKhachHang.SelectedValue),
                    MaNhanVien = Convert.ToInt32(cbNhanVien.SelectedValue),
                    NgayXuat = DateTime.Now,
                    TongTien = _tongTienPhieu,
                    GhiChu = txtGhiChuPhieu.Text.Trim()
                };

                List<ChiTietPhieuXuat> chiTietList = new List<ChiTietPhieuXuat>();
                foreach (DataRow row in _dtChiTietLocal.Rows)
                {
                    chiTietList.Add(new ChiTietPhieuXuat
                    {
                        MaHangHoa = Convert.ToInt32(row["MaHang"]),
                        SoLuong = Convert.ToInt32(row["SoLuong"]),
                        DonGiaXuat = Convert.ToDecimal(row["DonGia"]),
                        ThanhTien = Convert.ToDecimal(row["ThanhTien"])
                    });
                }

                _pxRepo.LuuPhieuXuat(phieuXuat, chiTietList);

                MessageBox.Show("Da luu phieu xuat va cap nhat ton kho.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dtChiTietLocal.Rows.Clear();
                TinhTongTien();
                txtGhiChuPhieu.Clear();
                _maPhieuDuocChon = 0;
                HienThiLichSuPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi luu phieu xuat: " + ex.Message, "Loi");
            }
        }
    }
}
