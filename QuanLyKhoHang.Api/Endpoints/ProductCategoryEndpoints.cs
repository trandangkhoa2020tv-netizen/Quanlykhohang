using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD loại hàng.
/// </summary>
public static class LoaiHangEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách loại hàng.
    /// </summary>
    public static IEndpointRouteBuilder MapLoaiHangEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/loai-hang", (ILoaiHangService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/loai-hang", (ILoaiHangService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/loai-hang", (LoaiHang input, HttpContext context, ILoaiHangService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Them(input);
                auditLog.Record(context, "CREATE", "loaihang", null, input);
                return ApiResults.Created(affectedRows);
            }));

        app.MapPut("/api/loai-hang/{id:int}", (int id, LoaiHang input, HttpContext context, ILoaiHangService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Sua(id, input);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "UPDATE", "loaihang", id, input);
                }

                return ApiResults.Updated(affectedRows);
            }));

        app.MapDelete("/api/loai-hang/{id:int}", (int id, HttpContext context, ILoaiHangService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Xoa(id);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "DELETE", "loaihang", id, new { maLoaiHang = id });
                }

                return ApiResults.Deleted(affectedRows);
            }));

        return app;
    }
}
