namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model nhân viên trong hệ thống.
    /// Nhân viên có thể được liên kết với tài khoản đăng nhập và được chọn khi lập phiếu.
    /// </summary>
    public class NhanVien
    {
        /// <summary>Mã nhân viên.</summary>
        public int MaNhanVien { get; set; }

        /// <summary>Tên nhân viên.</summary>
        public string TenNhanVien { get; set; } = string.Empty;

        /// <summary>Địa chỉ nhân viên.</summary>
        public string DiaChiNV { get; set; } = string.Empty;

        /// <summary>Số điện thoại nhân viên.</summary>
        public string SoDienThoai { get; set; } = string.Empty;

        /// <summary>Email nhân viên.</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Chức vụ hoặc vị trí công việc.</summary>
        public string ChucVu { get; set; } = string.Empty;

        /// <summary>Ghi chú thêm.</summary>
        public string GhiChu { get; set; } = string.Empty;
    }
}
