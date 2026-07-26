using System;
using System.Data;
using System.Security.Cryptography;
using Npgsql;
using QuanLyKhoHang.Data;

namespace QuanLyKhoHang.Repositories
{
    /// <summary>
    /// Repository kiểm tra tài khoản đăng nhập.
    /// Form đăng nhập dùng lớp này để xác thực username/password và lấy vai trò phân quyền.
    /// </summary>
    public class TaiKhoanRepository
    {
        private readonly DatabaseHelper _dbHelper = new DatabaseHelper();

        /// <summary>
        /// Kiểm tra đăng nhập.
        /// Nếu database có cột trang_thai thì chỉ cho tài khoản đang hoạt động đăng nhập.
        /// Nếu database cũ chưa có cột này thì vẫn đăng nhập theo user/password để tránh lỗi runtime.
        /// </summary>
        public string CheckLogin(string username, string password)
        {
            string sql = @"SELECT vai_tro, mat_khau
                           FROM taikhoan
                           WHERE ten_taikhoan = @username";

            if (HasTrangThaiColumn())
            {
                sql += " AND trang_thai = true";
            }

            NpgsqlParameter[] parameters = {
                new NpgsqlParameter("@username", username)
            };

            DataTable result = _dbHelper.ExecuteQuery(sql, parameters);
            if (result.Rows.Count == 0)
            {
                return string.Empty;
            }

            DataRow row = result.Rows[0];
            string storedPassword = Convert.ToString(row["mat_khau"]) ?? string.Empty;
            if (!VerifyPassword(password, storedPassword))
            {
                return string.Empty;
            }

            return Convert.ToString(row["vai_tro"]) ?? string.Empty;
        }

        /// <summary>
        /// Kiem tra mat khau PBKDF2, khong chap nhan plain text hoac SHA-256.
        /// </summary>
        private static bool VerifyPassword(string password, string storedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedPassword))
            {
                return false;
            }

            if (!storedPassword.StartsWith("pbkdf2$", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return VerifyPbkdf2(password, storedPassword);
        }

        /// <summary>
        /// Kiểm tra mật khẩu dạng PBKDF2 theo định dạng pbkdf2$iterations$salt$hash.
        /// </summary>
        private static bool VerifyPbkdf2(string password, string storedPassword)
        {
            string[] parts = storedPassword.Split('$');
            if (parts.Length != 4 || !int.TryParse(parts[1], out int iterations) || iterations <= 0)
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expectedHash = Convert.FromBase64String(parts[3]);

                byte[] actualHash = Rfc2898DeriveBytes.Pbkdf2(
                    password,
                    salt,
                    iterations,
                    HashAlgorithmName.SHA256,
                    expectedHash.Length);

                return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra schema hiện tại có cột trang_thai trong bảng taikhoan hay chưa.
        /// Hàm này giúp chương trình tương thích database cũ.
        /// </summary>
        private bool HasTrangThaiColumn()
        {
            const string sql = @"SELECT COUNT(*)
                                 FROM information_schema.columns
                                 WHERE table_schema = 'public'
                                   AND table_name = 'taikhoan'
                                   AND column_name = 'trang_thai'";

            object result = _dbHelper.ExecuteScalar(sql);
            return Convert.ToInt32(result) > 0;
        }
    }
}
