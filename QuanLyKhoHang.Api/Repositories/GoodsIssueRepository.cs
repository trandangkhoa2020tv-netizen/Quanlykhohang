using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository xử lý nghiệp vụ xuất kho.
    /// Bao gồm lấy lịch sử phiếu xuất, lấy chi tiết phiếu, tạo phiếu và trừ tồn kho.
    /// </summary>
    public class PhieuXuatRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Lấy danh sách phiếu xuất để hiển thị lịch sử trên form xuất kho.
        /// </summary>
        public DataTable GetAll()
        {
            string sql = @"SELECT p.ma_phieuxuat AS ""Mã Phiếu"", k.ten_khachhang AS ""Khách Hàng"",
                           nv.ten_nhanvien AS ""Nhân Viên Lập"", p.ngay_xuat AS ""Ngày Xuất"",
                           p.tong_tien AS ""Tổng Tiền"", p.ghi_chu AS ""Ghi Chú""
                           FROM phieuxuat p
                           JOIN khachhang k ON p.ma_khachhang = k.ma_khachhang
                           JOIN nhanvien nv ON p.ma_nhanvien = nv.ma_nhanvien
                           ORDER BY p.ma_phieuxuat DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// Tạo phiếu xuất đơn lẻ và trả về mã phiếu mới.
        /// Hàm này giữ lại để tương thích, luồng mới nên dùng LuuPhieuXuat để có transaction.
        /// </summary>
        public int ThemPhieuXuat(PhieuXuat px)
        {
            string sql = @"INSERT INTO phieuxuat (ma_khachhang, ma_nhanvien, tong_tien, ghi_chu)
                           VALUES (@makh, @manv, @tongtien, @ghichu) RETURNING ma_phieuxuat";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@makh", px.MaKhachHang),
                new NpgsqlParameter("@manv", px.MaNhanVien),
                new NpgsqlParameter("@tongtien", px.TongTien),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(px.GhiChu) ? DBNull.Value : px.GhiChu)
            };
            object result = _dbHelper.ExecuteScalar(sql, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Thêm chi tiết phiếu xuất và trừ tồn kho.
        /// Hàm này giữ lại để tương thích, luồng mới nên dùng LuuPhieuXuat để tránh ghi nửa chừng.
        /// </summary>
        public int ThemChiTiet(ChiTietPhieuXuat ct)
        {
            string sql = @"INSERT INTO chitietphieuxuat (ma_phieuxuat, ma_hanghoa, so_luong, don_gia_xuat, thanh_tien)
                           VALUES (@mapx, @mahang, @soluong, @dongia, @thanhtien);
                           UPDATE hanghoa SET so_luong_ton = so_luong_ton - @soluong
                           WHERE ma_hanghoa = @mahang AND is_deleted = false;";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@mapx", ct.MaPhieuXuat),
                new NpgsqlParameter("@mahang", ct.MaHangHoa),
                new NpgsqlParameter("@soluong", ct.SoLuong),
                new NpgsqlParameter("@dongia", ct.DonGiaXuat),
                new NpgsqlParameter("@thanhtien", ct.ThanhTien)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Alias rõ nghĩa cho GetAll(), dùng tại form và API.
        /// </summary>
        public DataTable GetAllPhieuXuat()
        {
            return GetAll();
        }

        /// <summary>
        /// Lấy danh sách mặt hàng thuộc một phiếu xuất để xuất Excel/PDF hoặc xem chi tiết.
        /// </summary>
        public DataTable GetChiTietTheoMaPhieu(int maPhieu)
        {
            string sql = @"SELECT hh.ten_hanghoa AS ""Tên Mặt Hàng"",
                           ct.so_luong AS ""Số Lượng"",
                           hh.don_vi_tinh AS ""Đơn Vị"",
                           ct.don_gia_xuat AS ""Đơn Giá"",
                           ct.thanh_tien AS ""Thành Tiền""
                           FROM chitietphieuxuat ct
                           JOIN hanghoa hh ON ct.ma_hanghoa = hh.ma_hanghoa
                           WHERE ct.ma_phieuxuat = @maphieu";

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@maphieu", maPhieu)
            };

            return _dbHelper.ExecuteQuery(sql, parameters);
        }

        /// <summary>
        /// Lấy thông tin chung của một phiếu xuất để in hóa đơn/PDF.
        /// </summary>
        public DataTable GetThongTinPhieuXuat(int maPhieu)
        {
            string sql = @"SELECT p.ma_phieuxuat AS ""Mã Phiếu"",
                           k.ten_khachhang AS ""Khách Hàng"",
                           k.so_dien_thoai AS ""Số Điện Thoại"",
                           k.dia_chi_kh AS ""Địa Chỉ"",
                           nv.ten_nhanvien AS ""Nhân Viên Lập"",
                           p.ngay_xuat AS ""Ngày Xuất"",
                           p.tong_tien AS ""Tổng Tiền"",
                           p.ghi_chu AS ""Ghi Chú""
                           FROM phieuxuat p
                           JOIN khachhang k ON p.ma_khachhang = k.ma_khachhang
                           JOIN nhanvien nv ON p.ma_nhanvien = nv.ma_nhanvien
                           WHERE p.ma_phieuxuat = @maphieu";

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@maphieu", maPhieu)
            };

            return _dbHelper.ExecuteQuery(sql, parameters);
        }

        /// <summary>
        /// Lưu trọn vẹn phiếu xuất trong một transaction:
        /// 1. Tạo phiếu xuất.
        /// 2. Kiểm tra và trừ tồn kho từng mặt hàng.
        /// 3. Thêm chi tiết phiếu xuất.
        /// Nếu thiếu tồn hoặc lỗi database thì rollback toàn bộ.
        /// </summary>
        public int LuuPhieuXuat(PhieuXuat px, IEnumerable<ChiTietPhieuXuat> chiTietList)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int maPhieuXuat = TaoPhieuXuat(conn, transaction, px);

                        foreach (ChiTietPhieuXuat ct in chiTietList)
                        {
                            ct.MaPhieuXuat = maPhieuXuat;
                            ThemChiTietTrongTransaction(conn, transaction, ct);
                        }

                        transaction.Commit();
                        return maPhieuXuat;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Tạo bản ghi phiếu xuất trong transaction và lấy mã phiếu vừa sinh.
        /// </summary>
        private int TaoPhieuXuat(NpgsqlConnection conn, NpgsqlTransaction transaction, PhieuXuat px)
        {
            const string sql = @"INSERT INTO phieuxuat (ma_khachhang, ma_nhanvien, tong_tien, ghi_chu)
                                 VALUES (@makh, @manv, @tongtien, @ghichu)
                                 RETURNING ma_phieuxuat";

            using (var cmd = new NpgsqlCommand(sql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@makh", px.MaKhachHang);
                cmd.Parameters.AddWithValue("@manv", px.MaNhanVien);
                cmd.Parameters.AddWithValue("@tongtien", px.TongTien);
                cmd.Parameters.AddWithValue("@ghichu", string.IsNullOrWhiteSpace(px.GhiChu) ? DBNull.Value : px.GhiChu);

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        /// <summary>
        /// Trừ tồn kho có điều kiện rồi thêm chi tiết phiếu xuất.
        /// Điều kiện so_luong_ton >= @soluong giúp chống xuất âm kể cả khi nhiều người thao tác cùng lúc.
        /// </summary>
        private void ThemChiTietTrongTransaction(NpgsqlConnection conn, NpgsqlTransaction transaction, ChiTietPhieuXuat ct)
        {
            const string truTonSql = @"UPDATE hanghoa
                                       SET so_luong_ton = so_luong_ton - @soluong
                                       WHERE ma_hanghoa = @mahang AND is_deleted = false AND so_luong_ton >= @soluong";

            using (var truTonCmd = new NpgsqlCommand(truTonSql, conn, transaction))
            {
                truTonCmd.Parameters.AddWithValue("@mahang", ct.MaHangHoa);
                truTonCmd.Parameters.AddWithValue("@soluong", ct.SoLuong);

                if (truTonCmd.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException($"Hàng hóa mã {ct.MaHangHoa} không đủ tồn kho để xuất.");
                }
            }

            const string chiTietSql = @"INSERT INTO chitietphieuxuat (ma_phieuxuat, ma_hanghoa, so_luong, don_gia_xuat, thanh_tien)
                                        VALUES (@mapx, @mahang, @soluong, @dongia, @thanhtien)";

            using (var chiTietCmd = new NpgsqlCommand(chiTietSql, conn, transaction))
            {
                chiTietCmd.Parameters.AddWithValue("@mapx", ct.MaPhieuXuat);
                chiTietCmd.Parameters.AddWithValue("@mahang", ct.MaHangHoa);
                chiTietCmd.Parameters.AddWithValue("@soluong", ct.SoLuong);
                chiTietCmd.Parameters.AddWithValue("@dongia", ct.DonGiaXuat);
                chiTietCmd.Parameters.AddWithValue("@thanhtien", ct.ThanhTien);
                chiTietCmd.ExecuteNonQuery();
            }
        }
    }
}
