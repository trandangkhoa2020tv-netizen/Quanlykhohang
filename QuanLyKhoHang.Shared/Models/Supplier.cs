namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model nhà cung cấp, dùng khi lập phiếu nhập kho.
    /// </summary>
    public class NhaCungCap
    {
        /// <summary>Mã nhà cung cấp.</summary>
        public int MaNhaCungCap { get; set; }

        /// <summary>Tên nhà cung cấp.</summary>
        public string TenNhaCungCap { get; set; } = string.Empty;

        /// <summary>Địa chỉ nhà cung cấp.</summary>
        public string DiaChiNCC { get; set; } = string.Empty;

        /// <summary>Số điện thoại nhà cung cấp.</summary>
        public string SoDienThoai { get; set; } = string.Empty;

        /// <summary>Email nhà cung cấp.</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Ghi chú thêm.</summary>
        public string GhiChu { get; set; } = string.Empty;
    }
}
