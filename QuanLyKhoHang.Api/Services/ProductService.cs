using System.Data;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ CRUD hàng hóa.
/// </summary>
public interface IHangHoaService
{
    /// <summary>Lấy danh sách hàng hóa.</summary>
    DataTable GetAll();

    /// <summary>Lấy danh sách hàng hóa dạng DTO typed object cho API v2.</summary>
    List<HangHoaDto> GetDtos();

    /// <summary>Thêm một hàng hóa mới.</summary>
    int Them(HangHoa input);

    /// <summary>Cập nhật hàng hóa theo mã.</summary>
    int Sua(int id, HangHoa input);

    /// <summary>Xóa hàng hóa theo mã.</summary>
    int Xoa(int id);
}

/// <summary>
/// Service validate và điều phối nghiệp vụ hàng hóa trước khi gọi repository.
/// </summary>
public sealed class HangHoaService : IHangHoaService
{
    private readonly HangHoaRepository _repository;

    /// <summary>
    /// Khởi tạo service hàng hóa với repository tương ứng.
    /// </summary>
    public HangHoaService(HangHoaRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Lấy danh sách hàng hóa để trả về API.
    /// </summary>
    public DataTable GetAll() => _repository.GetAll();

    /// <summary>
    /// Lấy danh sách hàng hóa dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<HangHoaDto> GetDtos() => DataTableDtoMapper.ToHangHoaDtos(_repository.GetAll());

    /// <summary>
    /// Validate dữ liệu hàng hóa rồi thêm vào database.
    /// </summary>
    public int Them(HangHoa input)
    {
        Validate(input);
        return _repository.Them(input);
    }

    /// <summary>
    /// Gán mã hàng hóa từ route, validate dữ liệu rồi cập nhật database.
    /// </summary>
    public int Sua(int id, HangHoa input)
    {
        Validate(input);
        input.MaHangHoa = id;
        return _repository.Sua(input);
    }

    /// <summary>
    /// Xóa hàng hóa theo mã.
    /// </summary>
    public int Xoa(int id) => _repository.Xoa(id);

    /// <summary>
    /// Kiểm tra dữ liệu hàng hóa trước khi thêm hoặc cập nhật.
    /// </summary>
    private static void Validate(HangHoa input)
    {
        List<string> errors = ValidationHelper.RequireBody(input);
        if (errors.Count == 0)
        {
            ValidationHelper.RequireText(errors, input.TenHangHoa, "tenHangHoa", 255);
            ValidationHelper.RequirePositive(errors, input.MaLoaiHang, "maLoaiHang");
            ValidationHelper.RequirePositive(errors, input.MaNhaCungCap, "maNhaCungCap");
            ValidationHelper.RequireNonNegativeDecimal(errors, input.GiaNhap, "giaNhap");
            ValidationHelper.RequireNonNegativeDecimal(errors, input.GiaBan, "giaBan");
            ValidationHelper.RequireNonNegative(errors, input.SoLuongTon, "soLuongTon");
            ValidationHelper.OptionalMaxLength(errors, input.DonViTinh, "donViTinh", 50);
        }

        ValidationHelper.ThrowIfAny(errors);
    }
}
