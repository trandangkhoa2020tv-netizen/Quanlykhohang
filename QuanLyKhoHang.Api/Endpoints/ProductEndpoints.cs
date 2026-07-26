using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD hàng hóa.
/// </summary>
public static class HangHoaEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách hàng hóa.
    /// </summary>
    public static IEndpointRouteBuilder MapHangHoaEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/hang-hoa", (IHangHoaService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())))
            .RequireRateLimiting("HangHoaRead");

        app.MapGet("/api/v2/hang-hoa", (IHangHoaService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())))
            .RequireRateLimiting("HangHoaRead");

        app.MapPost("/api/hang-hoa", (HangHoa input, HttpContext context, IHangHoaService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Them(input);
                auditLog.Record(context, "CREATE", "hanghoa", null, input);
                return ApiResults.Created(affectedRows);
            }))
            .RequireRateLimiting("CreateProduct");

        app.MapPut("/api/hang-hoa/{id:int}", (int id, HangHoa input, HttpContext context, IHangHoaService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Sua(id, input);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "UPDATE", "hanghoa", id, input);
                }

                return ApiResults.Updated(affectedRows);
            }))
            .RequireRateLimiting("UpdateProduct");

        app.MapDelete("/api/hang-hoa/{id:int}", (int id, HttpContext context, IHangHoaService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                IResult denied = ApiAuthorization.RequireAdmin(context);
                if (denied != null)
                {
                    return denied;
                }

                int affectedRows = service.Xoa(id);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "SOFT_DELETE", "hanghoa", id, new { maHangHoa = id });
                }

                return ApiResults.Deleted(affectedRows);
            }))
            .RequireRateLimiting("DeleteProduct");
        
        return app;
    }
}
