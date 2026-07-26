using System;

namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model phiếu nhập kho.
    /// Đây là phần thông tin chung của chứng từ, chi tiết hàng nằm ở ChiTietPhieuNhap.
    /// </summary>
    public class PhieuNhap
    {
        /// <summary>Mã phiếu nhập.</summary>
        public int MaPhieuNhap { get; set; }

        /// <summary>Mã nhà cung cấp của phiếu nhập.</summary>
        public int MaNhaCungCap { get; set; }

        /// <summary>Mã nhân viên lập phiếu.</summary>
        public int MaNhanVien { get; set; }

        /// <summary>Ngày nhập kho.</summary>
        public DateTime NgayNhap { get; set; } = DateTime.Now;

        /// <summary>Tổng tiền của toàn bộ phiếu.</summary>
        public decimal TongTien { get; set; }

        /// <summary>Ghi chú của phiếu.</summary>
        public string GhiChu { get; set; } = string.Empty;
    }
}
