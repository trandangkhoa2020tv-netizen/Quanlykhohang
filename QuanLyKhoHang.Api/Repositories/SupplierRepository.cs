using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository quản lý danh mục nhà cung cấp.
    /// Nhà cung cấp được dùng khi lập phiếu nhập kho.
    /// </summary>
    public class NhaCungCapRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Lấy toàn bộ nhà cung cấp.
        /// </summary>
        public DataTable GetAll()
        {
            string sql = "SELECT ma_nhacungcap AS \"Mã NCC\", ten_nhacungcap AS \"Tên Nhà Cung Cấp\", dia_chi_ncc AS \"Địa Chỉ\", so_dien_thoai AS \"SĐT\", email AS \"Email\", ghi_chu AS \"Ghi Chú\" FROM nhacungcap ORDER BY ma_nhacungcap DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm nhà cung cấp mới.
        /// </summary>
        public int Them(NhaCungCap ncc)
        {
            string sql = "INSERT INTO nhacungcap (ten_nhacungcap, dia_chi_ncc, so_dien_thoai, email, ghi_chu) VALUES (@ten, @diachi, @sdt, @email, @ghichu)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", ncc.TenNhaCungCap),
                new NpgsqlParameter("@diachi", ncc.DiaChiNCC),
                new NpgsqlParameter("@sdt", ncc.SoDienThoai),
                new NpgsqlParameter("@email", string.IsNullOrWhiteSpace(ncc.Email) ? DBNull.Value : ncc.Email),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(ncc.GhiChu) ? DBNull.Value : ncc.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Cập nhật nhà cung cấp theo mã.
        /// </summary>
        public int Sua(NhaCungCap ncc)
        {
            string sql = "UPDATE nhacungcap SET ten_nhacungcap = @ten, dia_chi_ncc = @diachi, so_dien_thoai = @sdt, email = @email, ghi_chu = @ghichu WHERE ma_nhacungcap = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", ncc.MaNhaCungCap),
                new NpgsqlParameter("@ten", ncc.TenNhaCungCap),
                new NpgsqlParameter("@diachi", ncc.DiaChiNCC),
                new NpgsqlParameter("@sdt", ncc.SoDienThoai),
                new NpgsqlParameter("@email", string.IsNullOrWhiteSpace(ncc.Email) ? DBNull.Value : ncc.Email),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(ncc.GhiChu) ? DBNull.Value : ncc.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Xóa nhà cung cấp theo mã.
        /// </summary>
        public int Xoa(int maNcc)
        {
            string sql = "DELETE FROM nhacungcap WHERE ma_nhacungcap = @ma";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", maNcc)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }
    }
}
