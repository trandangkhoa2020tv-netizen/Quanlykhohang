using System;

namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model phiếu xuất kho.
    /// Đây là phần thông tin chung của chứng từ, chi tiết hàng nằm ở ChiTietPhieuXuat.
    /// </summary>
    public class PhieuXuat
    {
        /// <summary>Mã phiếu xuất.</summary>
        public int MaPhieuXuat { get; set; }

        /// <summary>Mã khách hàng nhận hàng.</summary>
        public int MaKhachHang { get; set; }

        /// <summary>Mã nhân viên lập phiếu.</summary>
        public int MaNhanVien { get; set; }

        /// <summary>Ngày xuất kho.</summary>
        public DateTime NgayXuat { get; set; } = DateTime.Now;

        /// <summary>Tổng tiền của toàn bộ phiếu.</summary>
        public decimal TongTien { get; set; }

        /// <summary>Ghi chú của phiếu.</summary>
        public string GhiChu { get; set; } = string.Empty;
    }
}
