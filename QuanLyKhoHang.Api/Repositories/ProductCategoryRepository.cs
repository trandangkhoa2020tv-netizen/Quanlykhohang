using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository quản lý danh mục loại hàng.
    /// Loại hàng dùng để nhóm các mặt hàng trong kho.
    /// </summary>
    public class LoaiHangRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Lấy toàn bộ loại hàng để hiển thị trong lưới hoặc combobox.
        /// </summary>
        public DataTable GetAll()
        {
            string sql = "SELECT ma_loaihang AS \"Mã Loại\", ten_loaihang AS \"Tên Loại Hàng\", ghi_chu AS \"Ghi Chú\" FROM loaihang ORDER BY ma_loaihang DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm loại hàng mới.
        /// </summary>
        public int Them(LoaiHang lh)
        {
            string sql = "INSERT INTO loaihang (ten_loaihang, ghi_chu) VALUES (@ten, @ghichu)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", lh.TenLoaiHang),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(lh.GhiChu) ? DBNull.Value : lh.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Cập nhật loại hàng theo mã.
        /// </summary>
        public int Sua(LoaiHang lh)
        {
            string sql = "UPDATE loaihang SET ten_loaihang = @ten, ghi_chu = @ghichu WHERE ma_loaihang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", lh.MaLoaiHang),
                new NpgsqlParameter("@ten", lh.TenLoaiHang),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(lh.GhiChu) ? DBNull.Value : lh.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Xóa loại hàng. Nếu đang có hàng hóa thuộc loại này thì database sẽ chặn bằng khóa ngoại.
        /// </summary>
        public int Xoa(int maLoai)
        {
            string sql = "DELETE FROM loaihang WHERE ma_loaihang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", maLoai)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }
    }
}
