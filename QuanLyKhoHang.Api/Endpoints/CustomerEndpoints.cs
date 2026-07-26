using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD khách hàng.
/// </summary>
public static class KhachHangEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách khách hàng.
    /// </summary>
    public static IEndpointRouteBuilder MapKhachHangEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/khach-hang", (IKhachHangService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/khach-hang", (IKhachHangService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/khach-hang", (KhachHang input, HttpContext context, IKhachHangService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Them(input);
                auditLog.Record(context, "CREATE", "khachhang", null, input);
                return ApiResults.Created(affectedRows);
            }));

        app.MapPut("/api/khach-hang/{id:int}", (int id, KhachHang input, HttpContext context, IKhachHangService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Sua(id, input);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "UPDATE", "khachhang", id, input);
                }

                return ApiResults.Updated(affectedRows);
            }));

        app.MapDelete("/api/khach-hang/{id:int}", (int id, HttpContext context, IKhachHangService service, AuditLogService auditLog) =>
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
                    auditLog.Record(context, "DELETE", "khachhang", id, new { maKhachHang = id });
                }

                return ApiResults.Deleted(affectedRows);
            }));

        return app;
    }
}
