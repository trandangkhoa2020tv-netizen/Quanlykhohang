namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model đại diện một dòng chi tiết trong phiếu xuất kho.
    /// Mỗi dòng cho biết xuất mặt hàng nào, số lượng bao nhiêu và đơn giá xuất.
    /// </summary>
    public class ChiTietPhieuXuat
    {
        /// <summary>Mã chi tiết phiếu xuất, tương ứng cột ma_chitiet nếu cần dùng.</summary>
        public int MaChiTietPX { get; set; }

        /// <summary>Mã phiếu xuất cha.</summary>
        public int MaPhieuXuat { get; set; }

        /// <summary>Mã hàng hóa được xuất.</summary>
        public int MaHangHoa { get; set; }

        /// <summary>Số lượng xuất.</summary>
        public int SoLuong { get; set; }

        /// <summary>Đơn giá xuất tại thời điểm lập phiếu.</summary>
        public decimal DonGiaXuat { get; set; }

        /// <summary>Thành tiền của dòng chi tiết, thường bằng SoLuong * DonGiaXuat.</summary>
        public decimal ThanhTien { get; set; }
    }
}
