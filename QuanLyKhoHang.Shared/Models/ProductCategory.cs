namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model loại hàng, dùng để phân nhóm hàng hóa.
    /// </summary>
    public class LoaiHang
    {
        /// <summary>Mã loại hàng.</summary>
        public int MaLoaiHang { get; set; }

        /// <summary>Tên loại hàng.</summary>
        public string TenLoaiHang { get; set; } = string.Empty;

        /// <summary>Ghi chú thêm.</summary>
        public string GhiChu { get; set; } = string.Empty;
    }
}
