using System.Data;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ CRUD loại hàng.
/// </summary>
public interface ILoaiHangService
{
    /// <summary>Lấy danh sách loại hàng.</summary>
    DataTable GetAll();

    /// <summary>Lấy danh sách loại hàng dạng DTO typed object cho API v2.</summary>
    List<LoaiHangDto> GetDtos();

    /// <summary>Thêm một loại hàng mới.</summary>
    int Them(LoaiHang input);

    /// <summary>Cập nhật loại hàng theo mã.</summary>
    int Sua(int id, LoaiHang input);

    /// <summary>Xóa loại hàng theo mã.</summary>
    int Xoa(int id);
}

/// <summary>
/// Service validate và điều phối nghiệp vụ loại hàng trước khi gọi repository.
/// </summary>
public sealed class LoaiHangService : ILoaiHangService
{
    private readonly LoaiHangRepository _repository;

    /// <summary>
    /// Khởi tạo service loại hàng với repository tương ứng.
    /// </summary>
    public LoaiHangService(LoaiHangRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lấy danh sách loại hàng để trả về API.
    /// </summary>
    public DataTable GetAll() => _repository.GetAll();

    /// <summary>
    /// Lấy danh sách loại hàng dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<LoaiHangDto> GetDtos() => DataTableDtoMapper.ToLoaiHangDtos(_repository.GetAll());

    /// <summary>
    /// Validate dữ liệu loại hàng rồi thêm vào database.
    /// </summary>
    public int Them(LoaiHang input)
    {
        Validate(input);
        return _repository.Them(input);
    }

    /// <summary>
    /// Gán mã loại hàng từ route, validate dữ liệu rồi cập nhật database.
    /// </summary>
    public int Sua(int id, LoaiHang input)
    {
        Validate(input);
        input.MaLoaiHang = id;
        return _repository.Sua(input);
    }

    /// <summary>
    /// Xóa loại hàng theo mã.
    /// </summary>
    public int Xoa(int id) => _repository.Xoa(id);

    /// <summary>
    /// Kiểm tra dữ liệu loại hàng trước khi thêm hoặc cập nhật.
    /// </summary>
    private static void Validate(LoaiHang input)
    {
        List<string> errors = ValidationHelper.RequireBody(input);
        if (errors.Count == 0)
        {
            ValidationHelper.RequireText(errors, input.TenLoaiHang, "tenLoaiHang", 100);
        }

        ValidationHelper.ThrowIfAny(errors);
    }
}
