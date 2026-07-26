using Microsoft.AspNetCore.Http;
using QuanLyKhoHang.ApiServer.DTOs;
using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Repositories;

namespace QuanLyKhoHang.Tests;

public sealed class SecurityIntegrationTests
{
    [Fact]
    public void ApiKeyValidator_ShouldAcceptOnlyMatchingHeaderValue()
    {
        DefaultHttpContext validContext = new DefaultHttpContext();
        validContext.Request.Headers["X-API-Key"] = "test-api-key";

        DefaultHttpContext invalidContext = new DefaultHttpContext();
        invalidContext.Request.Headers["X-API-Key"] = "wrong-key";

        Assert.True(ApiKeyValidator.HasValidApiKey(validContext.Request, "test-api-key"));
        Assert.False(ApiKeyValidator.HasValidApiKey(invalidContext.Request, "test-api-key"));
    }

    [Fact]
    public void RequireAdmin_ShouldRejectNonAdminUser()
    {
        DefaultHttpContext context = new DefaultHttpContext();
        ApiAuthorization.SetAuthenticatedUser(context, "staff", "NhanVien");

        IResult result = ApiAuthorization.RequireAdmin(context);

        Assert.NotNull(result);
        IStatusCodeHttpResult statusCodeResult = Assert.IsAssignableFrom<IStatusCodeHttpResult>(result);
        Assert.Equal(StatusCodes.Status403Forbidden, statusCodeResult.StatusCode);
    }

    [Fact]
    public void RequireAdmin_ShouldAllowAdminUser()
    {
        DefaultHttpContext context = new DefaultHttpContext();
        ApiAuthorization.SetAuthenticatedUser(context, "admin", "Admin");

        IResult result = ApiAuthorization.RequireAdmin(context);

        Assert.Null(result);
        Assert.True(context.User.Identity?.IsAuthenticated);
        Assert.Equal("admin", context.User.Identity?.Name);
    }

    [Fact]
    public void AuthService_ShouldRejectOversizedCredentialsBeforeDatabaseAccess()
    {
        AuthService service = new AuthService(new TaiKhoanRepository());

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(() =>
            service.CheckLogin(new LoginRequest
            {
                Username = new string('u', 101),
                Password = new string('p', 257)
            }));

        Assert.Contains("khong hop le", exception.Message);
    }
}
