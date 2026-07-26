using System.Data;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.Models;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Hợp đồng xử lý nghiệp vụ xuất kho.
/// </summary>
public interface IPhieuXuatService
{
    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu xuất.
    /// </summary>
    DataTable GetAllPhieuXuat();

    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu xuất dạng DTO typed object cho API v2.
    /// </summary>
    List<PhieuXuatDto> GetAllPhieuXuatDtos();

    /// <summary>
    /// Lấy danh sách chi tiết hàng hóa của một phiếu xuất.
    /// </summary>
    DataTable GetChiTietTheoMaPhieu(int id);

    /// <summary>
    /// Lấy chi tiết phiếu xuất dạng DTO typed object cho API v2.
    /// </summary>
    List<ChiTietPhieuXuatDto> GetChiTietDtosTheoMaPhieu(int id);

    /// <summary>
    /// Lấy thông tin tổng quát của một phiếu xuất để in hoặc xuất báo cáo.
    /// </summary>
    DataTable GetThongTinPhieuXuat(int id);

    /// <summary>
    /// Lấy thông tin tổng quát phiếu xuất dạng DTO typed object cho API v2.
    /// </summary>
    List<ThongTinPhieuXuatDto> GetThongTinPhieuXuatDtos(int id);

    /// <summary>
    /// Lưu phiếu xuất kèm danh sách chi tiết và cập nhật tồn kho.
    /// </summary>
    int LuuPhieuXuat(LuuPhieuXuatRequest input);
}

/// <summary>
/// Service validate dữ liệu xuất kho rồi gọi repository lưu trong transaction.
/// </summary>
public sealed class PhieuXuatService : IPhieuXuatService
{
    private readonly PhieuXuatRepository _phieuXuatRepository;

    /// <summary>
    /// Khởi tạo service xuất kho với repository phiếu xuất.
    /// </summary>
    public PhieuXuatService(PhieuXuatRepository phieuXuatRepository)
    {
        _phieuXuatRepository = phieuXuatRepository;
    }

    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu xuất từ repository.
    /// </summary>
    public DataTable GetAllPhieuXuat() => _phieuXuatRepository.GetAllPhieuXuat();

    /// <summary>
    /// Lấy toàn bộ lịch sử phiếu xuất dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<PhieuXuatDto> GetAllPhieuXuatDtos()
    {
        return DataTableDtoMapper.ToPhieuXuatDtos(_phieuXuatRepository.GetAllPhieuXuat());
    }

    /// <summary>
    /// Lấy chi tiết một phiếu xuất theo mã phiếu.
    /// </summary>
    public DataTable GetChiTietTheoMaPhieu(int id) => _phieuXuatRepository.GetChiTietTheoMaPhieu(id);

    /// <summary>
    /// Lấy chi tiết một phiếu xuất dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<ChiTietPhieuXuatDto> GetChiTietDtosTheoMaPhieu(int id)
    {
        return DataTableDtoMapper.ToChiTietPhieuXuatDtos(_phieuXuatRepository.GetChiTietTheoMaPhieu(id));
    }

    /// <summary>
    /// Lấy thông tin chung của phiếu xuất để in hóa đơn hoặc xuất file.
    /// </summary>
    public DataTable GetThongTinPhieuXuat(int id) => _phieuXuatRepository.GetThongTinPhieuXuat(id);

    /// <summary>
    /// Lấy thông tin chung của phiếu xuất dạng DTO typed object cho endpoint /api/v2.
    /// </summary>
    public List<ThongTinPhieuXuatDto> GetThongTinPhieuXuatDtos(int id)
    {
        return DataTableDtoMapper.ToThongTinPhieuXuatDtos(_phieuXuatRepository.GetThongTinPhieuXuat(id));
    }

    /// <summary>
    /// Validate phiếu xuất rồi lưu phiếu và chi tiết trong một transaction.
    /// </summary>
    public int LuuPhieuXuat(LuuPhieuXuatRequest input)
    {
        Validate(input);
        return _phieuXuatRepository.LuuPhieuXuat(input.PhieuXuat, input.ChiTietList);
    }

    /// <summary>
    /// Kiểm tra phiếu xuất phải có thông tin phiếu, khách hàng, nhân viên và chi tiết hàng.
    /// </summary>
    private static void Validate(LuuPhieuXuatRequest input)
    {
        List<string> errors = new List<string>();
        if (input?.PhieuXuat == null)
        {
            errors.Add("Thong tin phieu xuat khong duoc de trong.");
            ValidationHelper.ThrowIfAny(errors);
        }

        ValidationHelper.RequirePositive(errors, input.PhieuXuat.MaKhachHang, "maKhachHang");
        ValidationHelper.RequirePositive(errors, input.PhieuXuat.MaNhanVien, "maNhanVien");
        ValidateChiTiet(errors, input.ChiTietList);
        ValidationHelper.ThrowIfAny(errors);
        NormalizeTotals(input);
    }

    /// <summary>
    /// Kiểm tra danh sách chi tiết phiếu xuất không được rỗng.
    /// </summary>
    private static void ValidateChiTiet(List<string> errors, List<ChiTietPhieuXuat> chiTietList)
    {
        if (chiTietList == null || chiTietList.Count == 0)
        {
            errors.Add("Phieu phai co it nhat mot mat hang.");
            return;
        }

        for (int i = 0; i < chiTietList.Count; i++)
        {
            ChiTietPhieuXuat chiTiet = chiTietList[i];
            string prefix = "chiTietList[" + i + "].";
            if (chiTiet == null)
            {
                errors.Add(prefix + "khong duoc de trong.");
                continue;
            }

            ValidationHelper.RequirePositive(errors, chiTiet.MaHangHoa, prefix + "maHangHoa");
            ValidationHelper.RequirePositive(errors, chiTiet.SoLuong, prefix + "soLuong");
            ValidationHelper.RequireNonNegativeDecimal(errors, chiTiet.DonGiaXuat, prefix + "donGiaXuat");
        }
    }

    /// <summary>
    /// Tinh lai thanh tien tung dong va tong tien phieu xuat truoc khi luu.
    /// </summary>
    private static void NormalizeTotals(LuuPhieuXuatRequest input)
    {
        decimal tongTien = 0;
        foreach (ChiTietPhieuXuat chiTiet in input.ChiTietList)
        {
            chiTiet.ThanhTien = chiTiet.SoLuong * chiTiet.DonGiaXuat;
            tongTien += chiTiet.ThanhTien;
        }

        input.PhieuXuat.TongTien = tongTien;
    }
}
