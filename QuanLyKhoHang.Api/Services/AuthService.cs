using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ đăng nhập.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Kiểm tra thông tin đăng nhập và trả về vai trò nếu hợp lệ.
    /// </summary>
    string CheckLogin(LoginRequest input);
}

/// <summary>
/// Service xử lý đăng nhập trước khi gọi repository kiểm tra database.
/// </summary>
public sealed class AuthService : IAuthService
{
    private readonly TaiKhoanRepository _taiKhoanRepository;

    /// <summary>
    /// Khởi tạo service đăng nhập với repository tài khoản.
    /// </summary>
    public AuthService(TaiKhoanRepository taiKhoanRepository)
    {
        _taiKhoanRepository = taiKhoanRepository;
    }

    /// <summary>
    /// Kiểm tra username/password rỗng rồi gọi TaiKhoanRepository để xác thực.
    /// </summary>
    public string CheckLogin(LoginRequest input)
    {
        if (input == null || string.IsNullOrWhiteSpace(input.Username) || string.IsNullOrWhiteSpace(input.Password))
        {
            throw new InvalidOperationException("Vui long nhap ten tai khoan va mat khau.");
        }

        if (input.Username.Length > 100 || input.Password.Length > 256)
        {
            throw new InvalidOperationException("Thong tin dang nhap khong hop le.");
        }

        return _taiKhoanRepository.CheckLogin(input.Username.Trim(), input.Password);
    }
}
