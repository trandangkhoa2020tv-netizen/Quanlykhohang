using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository quản lý bảng hanghoa.
    /// Tầng Form và API gọi lớp này để lấy danh sách, thêm, sửa, xóa hàng hóa.
    /// </summary>
    public class HangHoaRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Lấy danh sách hàng hóa kèm tên loại hàng và tên nhà cung cấp để hiển thị lên lưới.
        /// </summary>
        public DataTable GetAll()
        {
            string sql = @"SELECT h.ma_hanghoa AS ""Mã Hàng"", h.ten_hanghoa AS ""Tên Hàng Hóa"",
                           l.ten_loaihang AS ""Loại Hàng"", n.ten_nhacungcap AS ""Nhà Cung Cấp"",
                           h.gia_nhap AS ""Giá Nhập"", h.gia_ban AS ""Giá Bán"",
                           h.so_luong_ton AS ""Tồn Kho"", h.don_vi_tinh AS ""ĐVT"", h.ghi_chu AS ""Ghi Chú""
                           FROM hanghoa h
                           JOIN loaihang l ON h.ma_loaihang = l.ma_loaihang
                           JOIN nhacungcap n ON h.ma_nhacungcap = n.ma_nhacungcap
                           WHERE COALESCE(h.is_deleted, false) = false
                           ORDER BY h.ma_hanghoa DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm một mặt hàng mới vào kho.
        /// </summary>
        public int Them(HangHoa hh)
        {
            string sql = @"INSERT INTO hanghoa (ten_hanghoa, ma_loaihang, ma_nhacungcap, gia_nhap, gia_ban, so_luong_ton, don_vi_tinh, ghi_chu, is_deleted)
                           VALUES (@ten, @maloai, @mancc, @gianhap, @giaban, @ton, @dvt, @ghichu, false)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", hh.TenHangHoa),
                new NpgsqlParameter("@maloai", hh.MaLoaiHang),
                new NpgsqlParameter("@mancc", hh.MaNhaCungCap),
                new NpgsqlParameter("@gianhap", hh.GiaNhap),
                new NpgsqlParameter("@giaban", hh.GiaBan),
                new NpgsqlParameter("@ton", hh.SoLuongTon),
                new NpgsqlParameter("@dvt", hh.DonViTinh),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(hh.GhiChu) ? DBNull.Value : hh.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Cập nhật thông tin mặt hàng theo mã hàng hóa.
        /// </summary>
        public int Sua(HangHoa hh)
        {
            string sql = @"UPDATE hanghoa SET ten_hanghoa = @ten, ma_loaihang = @maloai, ma_nhacungcap = @mancc,
                           gia_nhap = @gianhap, gia_ban = @giaban, so_luong_ton = @ton, don_vi_tinh = @dvt, ghi_chu = @ghichu
                           WHERE ma_hanghoa = @ma AND COALESCE(is_deleted, false) = false";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", hh.MaHangHoa),
                new NpgsqlParameter("@ten", hh.TenHangHoa),
                new NpgsqlParameter("@maloai", hh.MaLoaiHang),
                new NpgsqlParameter("@mancc", hh.MaNhaCungCap),
                new NpgsqlParameter("@gianhap", hh.GiaNhap),
                new NpgsqlParameter("@giaban", hh.GiaBan),
                new NpgsqlParameter("@ton", hh.SoLuongTon),
                new NpgsqlParameter("@dvt", hh.DonViTinh),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(hh.GhiChu) ? DBNull.Value : hh.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Xóa mặt hàng theo mã. Nếu hàng đã nằm trong chứng từ nhập/xuất thì database sẽ chặn bằng khóa ngoại.
        /// </summary>
        public int Xoa(int maHang)
        {
            string sql = "UPDATE hanghoa SET is_deleted = true WHERE ma_hanghoa = @ma AND COALESCE(is_deleted, false) = false";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", maHang)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

   
        
    }
}
