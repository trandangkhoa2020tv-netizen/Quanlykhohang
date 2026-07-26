namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model đại diện một dòng chi tiết trong phiếu nhập kho.
    /// Mỗi dòng cho biết nhập mặt hàng nào, số lượng bao nhiêu và đơn giá nhập.
    /// </summary>
    public class ChiTietPhieuNhap
    {
        /// <summary>Mã chi tiết phiếu nhập, tương ứng cột ma_chitiet nếu cần dùng.</summary>
        public int MaChiTietPN { get; set; }

        /// <summary>Mã phiếu nhập cha.</summary>
        public int MaPhieuNhap { get; set; }

        /// <summary>Mã hàng hóa được nhập.</summary>
        public int MaHangHoa { get; set; }

        /// <summary>Số lượng nhập.</summary>
        public int SoLuong { get; set; }

        /// <summary>Đơn giá nhập tại thời điểm lập phiếu.</summary>
        public decimal DonGiaNhap { get; set; }

        /// <summary>Thành tiền của dòng chi tiết, thường bằng SoLuong * DonGiaNhap.</summary>
        public decimal ThanhTien { get; set; }
    }
}
