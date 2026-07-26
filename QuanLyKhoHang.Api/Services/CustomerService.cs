using System.Data;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ CRUD khách hàng.
/// </summary>
public interface IKhachHangService
{
    /// <summary>Lấy danh sách khách hàng.</summary>
    DataTable GetAll();

    /// <summary>Lấy danh sách khách hàng dạng DTO typed object cho API v2.</summary>
    List<KhachHangDto> GetDtos();

    /// <summary>Thêm một khách hàng mới.</summary>
    int Them(KhachHang input);

    /// <summary>Cập nhật khách hàng theo mã.</summary>
    int Sua(int id, KhachHang input);

    /// <summary>Xóa khách hàng theo mã.</summary>
    int Xoa(int id);
}

/// <summary>
/// Service validate và điều phối nghiệp vụ khách hàng trước khi gọi repository.
/// </summary>
public sealed class KhachHangService : IKhachHangService
{
    private readonly KhachHangRepository _repository;

    /// <summary>
    /// Khởi tạo service khách hàng với repository tương ứng.
    /// </summary>
    public KhachHangService(KhachHangRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lấy danh sách khách hàng để trả về API.
    /// </summary>
    public DataTable GetAll() => _repository.GetAll();

    /// <summary>
    /// Lấy danh sách khách hàng dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<KhachHangDto> GetDtos() => DataTableDtoMapper.ToKhachHangDtos(_repository.GetAll());

    /// <summary>
    /// Validate dữ liệu khách hàng rồi thêm vào database.
    /// </summary>
    public int Them(KhachHang input)
    {
        Validate(input);
        return _repository.Them(input);
    }

    /// <summary>
    /// Gán mã khách hàng từ route, validate dữ liệu rồi cập nhật database.
    /// </summary>
    public int Sua(int id, KhachHang input)
    {
        Validate(input);
        input.MaKhachHang = id;
        return _repository.Sua(input);
    }

    /// <summary>
    /// Xóa khách hàng theo mã.
    /// </summary>
    public int Xoa(int id) => _repository.Xoa(id);

    /// <summary>
    /// Kiểm tra dữ liệu khách hàng trước khi thêm hoặc cập nhật.
    /// </summary>
    private static void Validate(KhachHang input)
    {
        List<string> errors = ValidationHelper.RequireBody(input);
        if (errors.Count == 0)
        {
            ValidationHelper.RequireText(errors, input.TenKhachHang, "tenKhachHang", 255);
            ValidationHelper.OptionalMaxLength(errors, input.DiaChiKH, "diaChiKH", 500);
            ValidationHelper.OptionalMaxLength(errors, input.SoDienThoai, "soDienThoai", 20);
            ValidationHelper.OptionalEmail(errors, input.Email, "email", 100);
        }

        ValidationHelper.ThrowIfAny(errors);
    }
}
