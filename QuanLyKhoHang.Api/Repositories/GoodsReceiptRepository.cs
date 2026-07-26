using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository xử lý nghiệp vụ nhập kho.
    /// Bao gồm lấy lịch sử phiếu nhập, lấy chi tiết phiếu, tạo phiếu và cộng tồn kho.
    /// </summary>
    public class PhieuNhapRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Lấy danh sách phiếu nhập để hiển thị lịch sử trên form nhập kho.
        /// </summary>
        public DataTable GetAll()
        {
            string sql = @"SELECT p.ma_phieunhap AS ""Mã Phiếu"", n.ten_nhacungcap AS ""Nhà Cung Cấp"",
                           nv.ten_nhanvien AS ""Nhân Viên Lập"", p.ngay_nhap AS ""Ngày Nhập"",
                           p.tong_tien AS ""Tổng Tiền"", p.ghi_chu AS ""Ghi Chú""
                           FROM phieunhap p
                           JOIN nhacungcap n ON p.ma_nhacungcap = n.ma_nhacungcap
                           JOIN nhanvien nv ON p.ma_nhanvien = nv.ma_nhanvien
                           ORDER BY p.ma_phieunhap DESC";
            return _dbHelper.ExecuteQuery(sql);
        }

        /// <summary>
        /// Tạo phiếu nhập đơn lẻ và trả về mã phiếu mới.
        /// Hàm này giữ lại để tương thích, luồng mới nên dùng LuuPhieuNhap để có transaction.
        /// </summary>
        public int ThemPhieuNhap(PhieuNhap pn)
        {
            string sql = @"INSERT INTO phieunhap (ma_nhacungcap, ma_nhanvien, tong_tien, ghi_chu)
                           VALUES (@mancc, @manv, @tongtien, @ghichu) RETURNING ma_phieunhap";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@mancc", pn.MaNhaCungCap),
                new NpgsqlParameter("@manv", pn.MaNhanVien),
                new NpgsqlParameter("@tongtien", pn.TongTien),
                new NpgsqlParameter("@ghichu", string.IsNullOrWhiteSpace(pn.GhiChu) ? DBNull.Value : pn.GhiChu)
            };
            object result = _dbHelper.ExecuteScalar(sql, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// Thêm chi tiết phiếu nhập và cộng số lượng tồn.
        /// Hàm này giữ lại để tương thích, luồng mới nên dùng LuuPhieuNhap để tránh ghi nửa chừng.
        /// </summary>
        public int ThemChiTiet(ChiTietPhieuNhap ct)
        {
            string sql = @"INSERT INTO chitietphieunhap (ma_phieunhap, ma_hanghoa, so_luong, don_gia_nhap, thanh_tien)
                           VALUES (@mapn, @mahang, @soluong, @dongia, @thanhtien);
                           UPDATE hanghoa SET so_luong_ton = so_luong_ton + @soluong
                           WHERE ma_hanghoa = @mahang AND is_deleted = false;";
            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@mapn", ct.MaPhieuNhap),
                new NpgsqlParameter("@mahang", ct.MaHangHoa),
                new NpgsqlParameter("@soluong", ct.SoLuong),
                new NpgsqlParameter("@dongia", ct.DonGiaNhap),
                new NpgsqlParameter("@thanhtien", ct.ThanhTien)
            };
            return _dbHelper.ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// Alias rõ nghĩa cho GetAll(), dùng tại form và API.
        /// </summary>
        public DataTable GetAllPhieuNhap()
        {
            return GetAll();
        }

        /// <summary>
        /// Lấy danh sách mặt hàng thuộc một phiếu nhập để xuất Excel/PDF hoặc xem chi tiết.
        /// </summary>
        public DataTable GetChiTietTheoMaPhieu(int maPhieu)
        {
            string sql = @"SELECT hh.ten_hanghoa AS ""Tên Mặt Hàng"",
                           ct.so_luong AS ""Số Lượng"",
                           ct.don_gia_nhap AS ""Đơn Giá"",
                           ct.thanh_tien AS ""Thành Tiền""
                           FROM chitietphieunhap ct
                           JOIN hanghoa hh ON ct.ma_hanghoa = hh.ma_hanghoa
                           WHERE ct.ma_phieunhap = @maphieu";

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@maphieu", maPhieu)
            };

            return _dbHelper.ExecuteQuery(sql, parameters);
        }

        /// <summary>
        /// Lưu trọn vẹn phiếu nhập trong một transaction:
        /// 1. Tạo phiếu nhập.
        /// 2. Thêm từng dòng chi tiết.
        /// 3. Cộng tồn kho tương ứng.
        /// Nếu bất kỳ bước nào lỗi thì rollback toàn bộ.
        /// </summary>
        public int LuuPhieuNhap(PhieuNhap pn, IEnumerable<ChiTietPhieuNhap> chiTietList)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int maPhieuNhap = TaoPhieuNhap(conn, transaction, pn);

                        foreach (ChiTietPhieuNhap ct in chiTietList)
                        {
                            ct.MaPhieuNhap = maPhieuNhap;
                            ThemChiTietTrongTransaction(conn, transaction, ct);
                        }

                        transaction.Commit();
                        return maPhieuNhap;
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
        /// Tạo bản ghi phiếu nhập trong transaction và lấy mã phiếu vừa sinh.
        /// </summary>
        private int TaoPhieuNhap(NpgsqlConnection conn, NpgsqlTransaction transaction, PhieuNhap pn)
        {
            const string sql = @"INSERT INTO phieunhap (ma_nhacungcap, ma_nhanvien, tong_tien, ghi_chu)
                                 VALUES (@mancc, @manv, @tongtien, @ghichu)
                                 RETURNING ma_phieunhap";

            using (var cmd = new NpgsqlCommand(sql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@mancc", pn.MaNhaCungCap);
                cmd.Parameters.AddWithValue("@manv", pn.MaNhanVien);
                cmd.Parameters.AddWithValue("@tongtien", pn.TongTien);
                cmd.Parameters.AddWithValue("@ghichu", string.IsNullOrWhiteSpace(pn.GhiChu) ? DBNull.Value : pn.GhiChu);

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        /// <summary>
        /// Thêm một dòng chi tiết phiếu nhập và cộng tồn kho trong cùng transaction.
        /// </summary>
        private void ThemChiTietTrongTransaction(NpgsqlConnection conn, NpgsqlTransaction transaction, ChiTietPhieuNhap ct)
        {
            const string chiTietSql = @"INSERT INTO chitietphieunhap (ma_phieunhap, ma_hanghoa, so_luong, don_gia_nhap, thanh_tien)
                                        VALUES (@mapn, @mahang, @soluong, @dongia, @thanhtien)";

            using (var chiTietCmd = new NpgsqlCommand(chiTietSql, conn, transaction))
            {
                chiTietCmd.Parameters.AddWithValue("@mapn", ct.MaPhieuNhap);
                chiTietCmd.Parameters.AddWithValue("@mahang", ct.MaHangHoa);
                chiTietCmd.Parameters.AddWithValue("@soluong", ct.SoLuong);
                chiTietCmd.Parameters.AddWithValue("@dongia", ct.DonGiaNhap);
                chiTietCmd.Parameters.AddWithValue("@thanhtien", ct.ThanhTien);
                chiTietCmd.ExecuteNonQuery();
            }

            const string congTonSql = @"UPDATE hanghoa
                                        SET so_luong_ton = so_luong_ton + @soluong
                                        WHERE ma_hanghoa = @mahang AND is_deleted = false";

            using (var congTonCmd = new NpgsqlCommand(congTonSql, conn, transaction))
            {
                congTonCmd.Parameters.AddWithValue("@mahang", ct.MaHangHoa);
                congTonCmd.Parameters.AddWithValue("@soluong", ct.SoLuong);

                if (congTonCmd.ExecuteNonQuery() != 1)
                {
                    throw new InvalidOperationException($"Hang hoa ma {ct.MaHangHoa} khong ton tai hoac da bi xoa.");
                }
            }
        }
    }
}
