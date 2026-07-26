using System;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository quản lý danh mục nhân viên.
    /// Nhân viên được gắn với tài khoản và được chọn khi lập phiếu nhập/xuất.
    /// </summary>
    public class NhanVienRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Lấy toàn bộ nhân viên.
        /// </summary>
        public DataTable GetAll()
        {
            string sql = "SELECT ma_nhanvien AS \"Mã NV\", ten_nhanvien AS \"Tên Nhân Viên\", dia_chi_nv AS \"Địa Chỉ\", so_dien_thoai AS \"SĐT\", email AS \"Email\", chuc_vu AS \"Chức Vụ\", ghi_chu AS \"Ghi Chú\" FROM nhanvien WHERE is_deleted = false ORDER BY ma_nhanvien DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// Thêm nhân viên mới.
        /// </summary>
        public int Them(NhanVien nv)
        {
            string sql = "INSERT INTO nhanvien (ten_nhanvien, dia_chi_nv, so_dien_thoai, email, chuc_vu, ghi_chu) VALUES (@ten, @diachi, @sdt, @email, @chucvu, @ghichu)";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ten", nv.TenNhanVien),
                new NpgsqlParameter("@diachi", nv.DiaChiNV),
                new NpgsqlParameter("@sdt", nv.SoDienThoai),
                new NpgsqlParameter("@email", string.IsNullOrWhiteSpace(nv.Email) ? DBNull.Value : nv.Email),
                new NpgsqlParameter("@chucvu", nv.ChucVu),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(nv.GhiChu) ? DBNull.Value : nv.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Cập nhật nhân viên theo mã.
        /// </summary>
        public int Sua(NhanVien nv)
        {
            string sql = "UPDATE nhanvien SET ten_nhanvien = @ten, dia_chi_nv = @diachi, so_dien_thoai = @sdt, email = @email, chuc_vu = @chucvu, ghi_chu = @ghichu WHERE ma_nhanvien = @ma AND is_deleted = false";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@ma", nv.MaNhanVien),
                new NpgsqlParameter("@ten", nv.TenNhanVien),
                new NpgsqlParameter("@diachi", nv.DiaChiNV),
                new NpgsqlParameter("@sdt", nv.SoDienThoai),
                new NpgsqlParameter("@email", string.IsNullOrWhiteSpace(nv.Email) ? DBNull.Value : nv.Email),
                new NpgsqlParameter("@chucvu", nv.ChucVu),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(nv.GhiChu) ? DBNull.Value : nv.GhiChu)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Xóa nhân viên theo mã.
        /// </summary>
        public int Xoa(int maNv)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var disableAccountCmd = new NpgsqlCommand("UPDATE taikhoan SET trang_thai = false WHERE ma_nhanvien = @ma", conn, transaction))
                        {
                            disableAccountCmd.Parameters.AddWithValue("@ma", maNv);
                            disableAccountCmd.ExecuteNonQuery();
                        }

                        int affectedRows;
                        using (var softDeleteEmployeeCmd = new NpgsqlCommand("UPDATE nhanvien SET is_deleted = true WHERE ma_nhanvien = @ma AND is_deleted = false", conn, transaction))
                        {
                            softDeleteEmployeeCmd.Parameters.AddWithValue("@ma", maNv);
                            affectedRows = softDeleteEmployeeCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return affectedRows;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

    }
}
