using System.Data;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ CRUD nhà cung cấp.
/// </summary>
public interface INhaCungCapService
{
    /// <summary>Lấy danh sách nhà cung cấp.</summary>
    DataTable GetAll();

    /// <summary>Lấy danh sách nhà cung cấp dạng DTO typed object cho API v2.</summary>
    List<NhaCungCapDto> GetDtos();

    /// <summary>Thêm một nhà cung cấp mới.</summary>
    int Them(NhaCungCap input);

    /// <summary>Cập nhật nhà cung cấp theo mã.</summary>
    int Sua(int id, NhaCungCap input);

    /// <summary>Xóa nhà cung cấp theo mã.</summary>
    int Xoa(int id);
}

/// <summary>
/// Service validate và điều phối nghiệp vụ nhà cung cấp trước khi gọi repository.
/// </summary>
public sealed class NhaCungCapService : INhaCungCapService
{
    private readonly NhaCungCapRepository _repository;

    /// <summary>
    /// Khởi tạo service nhà cung cấp với repository tương ứng.
    /// </summary>
    public NhaCungCapService(NhaCungCapRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lấy danh sách nhà cung cấp để trả về API.
    /// </summary>
    public DataTable GetAll() => _repository.GetAll();

    /// <summary>
    /// Lấy danh sách nhà cung cấp dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<NhaCungCapDto> GetDtos() => DataTableDtoMapper.ToNhaCungCapDtos(_repository.GetAll());

    /// <summary>
    /// Validate dữ liệu nhà cung cấp rồi thêm vào database.
    /// </summary>
    public int Them(NhaCungCap input)
    {
        Validate(input);
        return _repository.Them(input);
    }

    /// <summary>
    /// Gán mã nhà cung cấp từ route, validate dữ liệu rồi cập nhật database.
    /// </summary>
    public int Sua(int id, NhaCungCap input)
    {
        Validate(input);
        input.MaNhaCungCap = id;
        return _repository.Sua(input);
    }

    /// <summary>
    /// Xóa nhà cung cấp theo mã.
    /// </summary>
    public int Xoa(int id) => _repository.Xoa(id);

    /// <summary>
    /// Kiểm tra dữ liệu nhà cung cấp trước khi thêm hoặc cập nhật.
    /// </summary>
    private static void Validate(NhaCungCap input)
    {
        List<string> errors = ValidationHelper.RequireBody(input);
        if (errors.Count == 0)
        {
            ValidationHelper.RequireText(errors, input.TenNhaCungCap, "tenNhaCungCap", 255);
            ValidationHelper.OptionalMaxLength(errors, input.DiaChiNCC, "diaChiNCC", 500);
            ValidationHelper.OptionalMaxLength(errors, input.SoDienThoai, "soDienThoai", 20);
            ValidationHelper.OptionalEmail(errors, input.Email, "email", 100);
        }

        ValidationHelper.ThrowIfAny(errors);
    }
}
