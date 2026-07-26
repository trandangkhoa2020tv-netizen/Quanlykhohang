using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiServer.Endpoints;

/// <summary>
/// Nhóm endpoint CRUD nhân viên.
/// </summary>
public static class NhanVienEndpoints
{
    /// <summary>
    /// Đăng ký route thêm, sửa, xóa, lấy danh sách nhân viên.
    /// </summary>
    public static IEndpointRouteBuilder MapNhanVienEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/nhan-vien", (INhanVienService service) =>
            ApiResults.Safe(() => ApiResults.OkTable(service.GetAll())));

        app.MapGet("/api/v2/nhan-vien", (INhanVienService service) =>
            ApiResults.Safe(() => Results.Ok(service.GetDtos())));

        app.MapPost("/api/nhan-vien", (NhanVien input, HttpContext context, INhanVienService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                IResult denied = ApiAuthorization.RequireAdmin(context);
                if (denied != null)
                {
                    return denied;
                }

                int affectedRows = service.Them(input);
                auditLog.Record(context, "CREATE", "nhanvien", null, input);
                return ApiResults.Created(affectedRows);
            }));

        app.MapPut("/api/nhan-vien/{id:int}", (int id, NhanVien input, HttpContext context, INhanVienService service, AuditLogService auditLog) =>
            ApiResults.Safe(() =>
            {
                IResult denied = ApiAuthorization.RequireAdmin(context);
                if (denied != null)
                {
                    return denied;
                }

                int affectedRows = service.Sua(id, input);
                if (affectedRows > 0)
                {
                    auditLog.Record(context, "UPDATE", "nhanvien", id, input);
                }

                return ApiResults.Updated(affectedRows);
            }));

        app.MapDelete("/api/nhan-vien/{id:int}", (int id, HttpContext context, INhanVienService service, AuditLogService auditLog) =>
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
                    auditLog.Record(context, "SOFT_DELETE", "nhanvien", id, new { maNhanVien = id });
                }

                return ApiResults.Deleted(affectedRows);
            }));

        return app;
    }
}
