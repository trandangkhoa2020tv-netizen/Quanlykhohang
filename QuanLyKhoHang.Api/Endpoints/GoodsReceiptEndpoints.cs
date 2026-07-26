using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.ApiServer.Services;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint xử lý nghiệp vụ nhập kho.
/// </summary>
public static class PhieuNhapEndpoints
{
    /// <summary>
    /// Đăng ký các route phiếu nhập vào ứng dụng Minimal API.
    /// </summary>
    public static IEndpointRouteBuilder MapPhieuNhapEndpoints(this IEndpointRouteBuilder app)
    {
        // Lấy lịch sử phiếu nhập.
        app.MapGet("/api/phieu-nhap", (IPhieuNhapService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAllPhieuNhap())));

        app.MapGet("/api/v2/phieu-nhap", (IPhieuNhapService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetAllPhieuNhapDtos())));

        // Lấy chi tiết các mặt hàng thuộc một phiếu nhập.
        app.MapGet("/api/phieu-nhap/{id:int}/chi-tiet", (int id, IPhieuNhapService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetChiTietTheoMaPhieu(id))));

        app.MapGet("/api/v2/phieu-nhap/{id:int}/chi-tiet", (int id, IPhieuNhapService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetChiTietDtosTheoMaPhieu(id))));

        // Tạo phiếu nhập, thêm chi tiết và cộng tồn kho trong transaction.
        app.MapPost("/api/phieu-nhap", (LuuPhieuNhapRequest input, HttpContext context, IPhieuNhapService service, AuditLogService auditLog) => ApiResults.Safe(() =>
        {
            int maPhieu = service.LuuPhieuNhap(input);
            auditLog.Record(context, "CREATE", "phieunhap", maPhieu, input);
            return Results.Ok(new { message = "Da luu phieu nhap va cap nhat ton kho.", maPhieu });
        }));

        return app;
    }
}
