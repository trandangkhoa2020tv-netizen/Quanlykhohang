namespace QuanLyKhoHang.Models
{
    /// <summary>
    /// Model hàng hóa trong kho.
    /// Dữ liệu tương ứng bảng hanghoa trong PostgreSQL.
    /// </summary>
    public class HangHoa
    {
        /// <summary>Mã định danh hàng hóa.</summary>
        public int MaHangHoa { get; set; }

        /// <summary>Tên hàng hóa.</summary>
        public string TenHangHoa { get; set; } = string.Empty;

        /// <summary>Mã loại hàng liên kết bảng loaihang.</summary>
        public int MaLoaiHang { get; set; }

        /// <summary>Mã nhà cung cấp liên kết bảng nhacungcap.</summary>
        public int MaNhaCungCap { get; set; }

        /// <summary>Giá nhập tham khảo.</summary>
        public decimal GiaNhap { get; set; }

        /// <summary>Giá bán tham khảo.</summary>
        public decimal GiaBan { get; set; }

        /// <summary>Số lượng tồn hiện tại trong kho.</summary>
        public int SoLuongTon { get; set; }

        /// <summary>Đơn vị tính như Cái, Hộp, Kg.</summary>
        public string DonViTinh { get; set; } = string.Empty;

        /// <summary>Ghi chú thêm cho mặt hàng.</summary>
        public string GhiChu { get; set; } = string.Empty;
    }
}
