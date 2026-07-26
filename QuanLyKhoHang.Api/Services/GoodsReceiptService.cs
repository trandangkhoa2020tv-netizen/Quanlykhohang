using System.Data;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ nhập kho.
/// </summary>
public interface IPhieuNhapService
{
    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu nhập.
    /// </summary>
    DataTable GetAllPhieuNhap();

    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu nhập dạng DTO typed object cho API v2.
    /// </summary>
    List<PhieuNhapDto> GetAllPhieuNhapDtos();

    /// <summary>
    /// Lấy danh sách chi tiết hàng hóa của một phiếu nhập.
    /// </summary>
    DataTable GetChiTietTheoMaPhieu(int id);

    /// <summary>
    /// Lấy chi tiết phiếu nhập dạng DTO typed object cho API v2.
    /// </summary>
    List<ChiTietPhieuNhapDto> GetChiTietDtosTheoMaPhieu(int id);

    /// <summary>
    /// Lưu phiếu nhập kèm danh sách chi tiết và cập nhật tồn kho.
    /// </summary>
    int LuuPhieuNhap(LuuPhieuNhapRequest input);
}

/// <summary>
/// Service validate dữ liệu nhập kho rồi gọi repository lưu trong transaction.
/// </summary>
public sealed class PhieuNhapService : IPhieuNhapService
{
    private readonly PhieuNhapRepository _phieuNhapRepository;

    /// <summary>
    /// Khởi tạo service nhập kho với repository phiếu nhập.
    /// </summary>
    public PhieuNhapService(PhieuNhapRepository phieuNhapRepository)
    {
        _phieuNhapRepository = phieuNhapRepository;
    }

    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu nhập từ repository.
    /// </summary>
    public DataTable GetAllPhieuNhap() => _phieuNhapRepository.GetAllPhieuNhap();

    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu nhập dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<PhieuNhapDto> GetAllPhieuNhapDtos()
    {
        return DataTableDtoMapper.ToPhieuNhapDtos(_phieuNhapRepository.GetAllPhieuNhap());
    }

    /// <summary>
    /// Lấy chi tiết một phiếu nhập theo mã phiếu.
    /// </summary>
    public DataTable GetChiTietTheoMaPhieu(int id) => _phieuNhapRepository.GetChiTietTheoMaPhieu(id);

    /// <summary>
    /// Lấy chi tiết một phiếu nhập dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<ChiTietPhieuNhapDto> GetChiTietDtosTheoMaPhieu(int id)
    {
        return DataTableDtoMapper.ToChiTietPhieuNhapDtos(_phieuNhapRepository.GetChiTietTheoMaPhieu(id));
    }

    /// <summary>
    /// Validate phiếu nhập rồi lưu phiếu và chi tiết trong một transaction.
    /// </summary>
    public int LuuPhieuNhap(LuuPhieuNhapRequest input)
    {
        Validate(input);
        return _phieuNhapRepository.LuuPhieuNhap(input.PhieuNhap, input.ChiTietList);
    }

    /// <summary>
    /// Kiểm tra phiếu nhập phải có thông tin phiếu, nhà cung cấp, nhân viên và chi tiết hàng.
    /// </summary>
    private static void Validate(LuuPhieuNhapRequest input)
    {
        List<string> errors = new List<string>();
        if (input?.PhieuNhap == null)
        {
            errors.Add("Thong tin phieu nhap khong duoc de trong.");
            ValidationHelper.ThrowIfAny(errors);
        }

        ValidationHelper.RequirePositive(errors, input.PhieuNhap.MaNhaCungCap, "maNhaCungCap");
        ValidationHelper.RequirePositive(errors, input.PhieuNhap.MaNhanVien, "maNhanVien");
        ValidateChiTiet(errors, input.ChiTietList);
        ValidationHelper.ThrowIfAny(errors);
        NormalizeTotals(input);
    }

    /// <summary>
    /// Kiểm tra danh sách chi tiết phiếu nhập không được rỗng.
    /// </summary>
    private static void ValidateChiTiet(List<string> errors, List<ChiTietPhieuNhap> chiTietList)
    {
        if (chiTietList == null || chiTietList.Count == 0)
        {
            errors.Add("Phieu phai co it nhat mot mat hang.");
            return;
        }

        for (int i = 0; i < chiTietList.Count; i++)
        {
            ChiTietPhieuNhap chiTiet = chiTietList[i];
            string prefix = "chiTietList[" + i + "].";
            if (chiTiet == null)
            {
                errors.Add(prefix + "khong duoc de trong.");
                continue;
            }

            ValidationHelper.RequirePositive(errors, chiTiet.MaHangHoa, prefix + "maHangHoa");
            ValidationHelper.RequirePositive(errors, chiTiet.SoLuong, prefix + "soLuong");
            ValidationHelper.RequireNonNegativeDecimal(errors, chiTiet.DonGiaNhap, prefix + "donGiaNhap");
        }
    }

    /// <summary>
    /// Tinh lai thanh tien tung dong va tong tien phieu nhap truoc khi luu.
    /// </summary>
    private static void NormalizeTotals(LuuPhieuNhapRequest input)
    {
        decimal tongTien = 0;
        foreach (ChiTietPhieuNhap chiTiet in input.ChiTietList)
        {
            chiTiet.ThanhTien = chiTiet.SoLuong * chiTiet.DonGiaNhap;
            tongTien += chiTiet.ThanhTien;
        }

        input.PhieuNhap.TongTien = tongTien;
    }
}
