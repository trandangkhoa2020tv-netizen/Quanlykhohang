using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD nhà cung cấp.
/// </summary>
public static class NhaCungCapEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách nhà cung cấp.
    /// </summary>
    public static IEndpointRouteBuilder MapNhaCungCapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/nha-cung-cap", (INhaCungCapService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/nha-cung-cap", (INhaCungCapService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/nha-cung-cap", (NhaCungCap input, HttpContext context, INhaCungCapService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Them(input);
                auditLog.Record(context, "CREATE", "nhacungcap", null, input);
                return ApiResults.Created(affectedRows);
            }));

        app.MapPut("/api/nha-cung-cap/{id:int}", (int id, NhaCungCap input, HttpContext context, INhaCungCapService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Sua(id, input);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "UPDATE", "nhacungcap", id, input);
                }

                return ApiResults.Updated(affectedRows);
            }));

        app.MapDelete("/api/nha-cung-cap/{id:int}", (int id, HttpContext context, INhaCungCapService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                int affectedRows = service.Xoa(id);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "DELETE", "nhacungcap", id, new { maNhaCungCap = id });
                }

                return ApiResults.Deleted(affectedRows);
            }));

        return app;
    }
}
