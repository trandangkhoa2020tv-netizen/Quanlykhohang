using System.Data;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ CRUD nhân viên.
/// </summary>
public interface INhanVienService
{
    /// <summary>Lấy danh sách nhân viên.</summary>
    DataTable GetAll();

    /// <summary>Lấy danh sách nhân viên dạng DTO typed object cho API v2.</summary>
    List<NhanVienDto> GetDtos();

    /// <summary>Thêm một nhân viên mới.</summary>
    int Them(NhanVien input);

    /// <summary>Cập nhật nhân viên theo mã.</summary>
    int Sua(int id, NhanVien input);

    /// <summary>Xóa nhân viên theo mã.</summary>
    int Xoa(int id);
}

/// <summary>
/// Service validate và điều phối nghiệp vụ nhân viên trước khi gọi repository.
/// </summary>
public sealed class NhanVienService : INhanVienService
{
    private readonly NhanVienRepository _repository;

    /// <summary>
    /// Khởi tạo service nhân viên với repository tương ứng.
    /// </summary>
    public NhanVienService(NhanVienRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lấy danh sách nhân viên để trả về API.
    /// </summary>
    public DataTable GetAll() => _repository.GetAll();

    /// <summary>
    /// Lấy danh sách nhân viên dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<NhanVienDto> GetDtos() => DataTableDtoMapper.ToNhanVienDtos(_repository.GetAll());

    /// <summary>
    /// Validate dữ liệu nhân viên rồi thêm vào database.
    /// </summary>
    public int Them(NhanVien input)
    {
        Validate(input);
        return _repository.Them(input);
    }

    /// <summary>
    /// Gán mã nhân viên từ route, validate dữ liệu rồi cập nhật database.
    /// </summary>
    public int Sua(int id, NhanVien input)
    {
        Validate(input);
        input.MaNhanVien = id;
        return _repository.Sua(input);
    }

    /// <summary>
    /// Xóa nhân viên theo mã; repository sẽ kiểm tra chứng từ nhập/xuất liên quan.
    /// </summary>
    public int Xoa(int id) => _repository.Xoa(id);

    /// <summary>
    /// Kiểm tra dữ liệu nhân viên trước khi thêm hoặc cập nhật.
    /// </summary>
    private static void Validate(NhanVien input)
    {
        List<string> errors = ValidationHelper.RequireBody(input);
        if (errors.Count == 0)
        {
            ValidationHelper.RequireText(errors, input.TenNhanVien, "tenNhanVien", 255);
            ValidationHelper.OptionalMaxLength(errors, input.DiaChiNV, "diaChiNV", 500);
            ValidationHelper.OptionalMaxLength(errors, input.SoDienThoai, "soDienThoai", 20);
            ValidationHelper.OptionalEmail(errors, input.Email, "email", 100);
            ValidationHelper.OptionalMaxLength(errors, input.ChucVu, "chucVu", 100);
        }

        ValidationHelper.ThrowIfAny(errors);
    }
}
