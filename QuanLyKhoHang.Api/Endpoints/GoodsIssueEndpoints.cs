using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.ApiServer.Services;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint xử lý nghiệp vụ xuất kho.
/// </summary>
public static class PhieuXuatEndpoints
{
    /// <summary>
    /// Đăng ký các route phiếu xuất vào ứng dụng Minimal API.
    /// </summary>
    public static IEndpointRouteBuilder MapPhieuXuatEndpoints(this IEndpointRouteBuilder app)
    {
        // Lấy lịch sử phiếu xuất.
        app.MapGet("/api/phieu-xuat", (IPhieuXuatService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAllPhieuXuat())));

        app.MapGet("/api/v2/phieu-xuat", (IPhieuXuatService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetAllPhieuXuatDtos())));

        // Lấy chi tiết các mặt hàng thuộc một phiếu xuất.
        app.MapGet("/api/phieu-xuat/{id:int}/chi-tiet", (int id, IPhieuXuatService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetChiTietTheoMaPhieu(id))));

        app.MapGet("/api/v2/phieu-xuat/{id:int}/chi-tiet", (int id, IPhieuXuatService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetChiTietDtosTheoMaPhieu(id))));

        // Lấy thông tin chung của phiếu xuất để in hoặc xuất báo cáo.
        app.MapGet("/api/phieu-xuat/{id:int}/thong-tin", (int id, IPhieuXuatService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetThongTinPhieuXuat(id))));

        app.MapGet("/api/v2/phieu-xuat/{id:int}/thong-tin", (int id, IPhieuXuatService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetThongTinPhieuXuatDtos(id))));

        // Tạo phiếu xuất, kiểm tra tồn kho, trừ tồn và thêm chi tiết trong transaction.
        app.MapPost("/api/phieu-xuat", (LuuPhieuXuatRequest input, HttpContext context, IPhieuXuatService service, AuditLogService auditLog) => ApiResults.Safe(() =>
        {
            int maPhieu = service.LuuPhieuXuat(input);
            auditLog.Record(context, "CREATE", "phieuxuat", maPhieu, input);
            return Results.Ok(new { message = "Da luu phieu xuat va cap nhat ton kho.", maPhieu });
        }));

        return app;
    }
}
