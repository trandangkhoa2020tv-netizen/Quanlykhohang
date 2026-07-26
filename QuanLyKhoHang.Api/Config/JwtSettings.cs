namespace QuanLyKhoHang.ApiServer.Config;

/// <summary>
/// Cấu hình phát hành và kiểm tra JWT cho API.
/// </summary>
public sealed class JwtSettings
{
    /// <summary>
    /// Bật/tắt yêu cầu JWT cho các endpoint nghiệp vụ.
    /// </summary>
    public bool RequireJwt { get; set; }

    /// <summary>
    /// Tên issuer ghi trong token.
    /// </summary>
    public string Issuer { get; set; } = "QuanLyKhoHang.Api";

    /// <summary>
    /// Tên audience ghi trong token.
    /// </summary>
    public string Audience { get; set; } = "QuanLyKhoHang.WinForms";

    /// <summary>
    /// Khóa ký HMAC SHA-256. Khi triển khai thật nên đổi sang chuỗi bí mật dài và không commit lên git.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Thời gian sống của token tính bằng phút.
    /// </summary>
    public int ExpirationMinutes { get; set; } = 480;
}
