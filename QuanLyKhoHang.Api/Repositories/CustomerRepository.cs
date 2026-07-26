using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository quản lý danh mục khách hàng.
    /// Khách hàng được dùng khi lập phiếu xuất kho.
    /// </summary>
    public class KhachHangRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Lấy toàn bộ khách hàng.
        /// </summary>
        public DataTable GetAll()
        {
            string sql = "SELECT ma_khachhang AS \"Mã KH\", ten_khachhang AS \"Tên Khách Hàng\", dia_chi_kh AS \"Địa Chỉ\", so_dien_thoai AS \"SĐT\", email AS \"Email\", ghi_chu AS \"Ghi Chú\" FROM khachhang ORDER BY ma_khachhang DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm khách hàng mới.
        /// </summary>
        public int Them(KhachHang kh)
        {
            string sql = "INSERT INTO khachhang (ten_khachhang, dia_chi_kh, so_dien_thoai, email, ghi_chu) VALUES (@ten, @diachi, @sdt, @email, @ghichu)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", kh.TenKhachHang),
                new NpgsqlParameter("@diachi", kh.DiaChiKH),
                new NpgsqlParameter("@sdt", kh.SoDienThoai),
                new NpgsqlParameter("@email", string.IsNullOrWhiteSpace(kh.Email) ? DBNull.Value : kh.Email),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(kh.GhiChu) ? DBNull.Value : kh.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Cập nhật khách hàng theo mã.
        /// </summary>
        public int Sua(KhachHang kh)
        {
            string sql = "UPDATE khachhang SET ten_khachhang = @ten, dia_chi_kh = @diachi, so_dien_thoai = @sdt, email = @email, ghi_chu = @ghichu WHERE ma_khachhang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", kh.MaKhachHang),
                new NpgsqlParameter("@ten", kh.TenKhachHang),
                new NpgsqlParameter("@diachi", kh.DiaChiKH),
                new NpgsqlParameter("@sdt", kh.SoDienThoai),
                new NpgsqlParameter("@email", string.IsNullOrWhiteSpace(kh.Email) ? DBNull.Value : kh.Email),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(kh.GhiChu) ? DBNull.Value : kh.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Xóa khách hàng theo mã.
        /// </summary>
        public int Xoa(int maKh)
        {
            string sql = "DELETE FROM khachhang WHERE ma_khachhang = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", maKh)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }
    }
}
