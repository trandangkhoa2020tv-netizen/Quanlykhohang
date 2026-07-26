namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model khách hàng, dùng khi lập phiếu xuất kho.
    /// </summary>
    public class KhachHang
    {
        /// <summary>Mã khách hàng.</summary>
        public int MaKhachHang { get; set; }

        /// <summary>Tên khách hàng.</summary>
        public string TenKhachHang { get; set; } = string.Empty;

        /// <summary>Địa chỉ khách hàng.</summary>
        public string DiaChiKH { get; set; } = string.Empty;

        /// <summary>Số điện thoại khách hàng.</summary>
        public string SoDienThoai { get; set; } = string.Empty;

        /// <summary>Email khách hàng.</summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>Ghi chú thêm.</summary>
        public string GhiChu { get; set; } = string.Empty;
    }
}
