using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.RateLimiting;
using QuanLyKhoHang.ApiServer;
using QuanLyKhoHang.ApiServer.Config;
using QuanLyKhoHang.ApiServer.Endpoints;
using QuanLyKhoHang.ApiServer.Services;
using QuanLyKhoHang.Data;
using QuanLyKhoHang.Repositories;
using System.Threading.RateLimiting;

const long MaxApiRequestBodySize = 256 * 1024;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình API từ appsettings.json và đặt URL backend sẽ lắng nghe.
ApiSettings apiSettings = builder.Configuration.GetSection("ApiSettings").Get<ApiSettings>() ?? new ApiSettings();
JwtSettings jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>() ?? new JwtSettings();
ApplySecurityEnvironmentOverrides(apiSettings, jwtSettings, builder.Environment);
ValidateSecurityConfiguration(apiSettings, jwtSettings, builder.Configuration, builder.Environment);
builder.WebHost.UseUrls(apiSettings.Url);
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = MaxApiRequestBodySize;
    options.Limits.MaxRequestLineSize = 8 * 1024;
});

    // Cho phép JSON request không phân biệt chữ hoa/thường ở tên property.
    builder.Services.Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.PropertyNameCaseInsensitive = true;
    });

    // Bật Swagger/OpenAPI để test và xem tài liệu API trực quan tại /swagger.
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsJsonAsync(
            new { message = "Qua nhieu request. Vui long thu lai sau." },
            cancellationToken);
    };

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            "global:" + GetRateLimitPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 120,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.AddPolicy("HangHoaRead", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetRateLimitPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 100,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.AddPolicy("CreateProduct", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetRateLimitPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.AddPolicy("UpdateProduct", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetRateLimitPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 20,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.AddPolicy("DeleteProduct", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetRateLimitPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));

    options.AddPolicy("Login", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetRateLimitPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(15),
                QueueLimit = 0
            }));
});

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Cấu hình CORS để client được phép gọi API theo danh sách AllowedOrigins.
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("ApiCors", policy =>
        {
            if (apiSettings.AllowsAnyOrigin())
            {
                policy.AllowAnyOrigin();
            }
            else
            {
                policy.WithOrigins(apiSettings.AllowedOrigins);
            }

            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
    });

    // Đăng ký repository: tầng thao tác trực tiếp với database.
    builder.Services.AddScoped<HangHoaRepository>();
    builder.Services.AddScoped<LoaiHangRepository>();
    builder.Services.AddScoped<NhaCungCapRepository>();
    builder.Services.AddScoped<KhachHangRepository>();
    builder.Services.AddScoped<NhanVienRepository>();
    builder.Services.AddScoped<PhieuNhapRepository>();
    builder.Services.AddScoped<PhieuXuatRepository>();
    builder.Services.AddScoped<TaiKhoanRepository>();
    builder.Services.AddScoped<DashboardRepository>();
    builder.Services.AddScoped<InventoryApiQueries>();

    // Đăng ký service: tầng xử lý nghiệp vụ và validate trước khi gọi repository.
    builder.Services.AddScoped<IAuthService, AuthService>();
    builder.Services.AddScoped<IHangHoaService, HangHoaService>();
    builder.Services.AddScoped<ILoaiHangService, LoaiHangService>();
    builder.Services.AddScoped<INhaCungCapService, NhaCungCapService>();
    builder.Services.AddScoped<IKhachHangService, KhachHangService>();
    builder.Services.AddScoped<INhanVienService, NhanVienService>();
    builder.Services.AddScoped<IKhoService, KhoService>();
    builder.Services.AddScoped<IPhieuNhapService, PhieuNhapService>();
    builder.Services.AddScoped<IPhieuXuatService, PhieuXuatService>();
    builder.Services.AddScoped<IDashboardService, DashboardService>();
    builder.Services.AddScoped<AuditLogService>();
    builder.Services.AddSingleton(jwtSettings);
    builder.Services.AddSingleton<JwtTokenService>();

    WebApplication app = builder.Build();

    // Đồng bộ sequence tự tăng để dữ liệu import sẵn không làm trùng khóa chính.
try
{
        bool runMigrations = builder.Environment.IsDevelopment()
            && string.Equals(Environment.GetEnvironmentVariable("QLKH_AUTO_MIGRATE"), "1", StringComparison.Ordinal);
        bool seedDemoData = runMigrations
            && string.Equals(Environment.GetEnvironmentVariable("QLKH_SEED_DEMO_DATA"), "1", StringComparison.Ordinal);

        if (runMigrations)
        {
            DatabaseMaintenance.EnsureRuntimeSchema(seedDemoData);
            DatabaseMaintenance.EnsureSerialSequences();
        }
    }
catch (Exception ex)
{
        throw new InvalidOperationException("Khong the chay migration database: " + ex.Message, ex);
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

    app.UseCors("ApiCors");
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/api") &&
        (HttpMethods.IsPost(context.Request.Method) ||
         HttpMethods.IsPut(context.Request.Method) ||
         HttpMethods.IsPatch(context.Request.Method)))
    {
        if (context.Request.ContentLength > MaxApiRequestBodySize)
        {
            context.Response.StatusCode = StatusCodes.Status413PayloadTooLarge;
            await context.Response.WriteAsJsonAsync(new { message = "Payload vuot qua kich thuoc toi da 256 KB." });
            return;
        }

        if (context.Request.ContentLength > 0 && !context.Request.HasJsonContentType())
        {
            context.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
            await context.Response.WriteAsJsonAsync(new { message = "API chi nhan request JSON." });
            return;
        }
    }

    await next();
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "QuanLyKhoHang API v1");
        options.RoutePrefix = "swagger";
    });
}

    // Nếu bật RequireApiKey, mọi request phải gửi key hợp lệ qua X-API-Key hoặc Authorization Bearer.
if (apiSettings.RequireApiKey)
{
        app.Use(async (context, next) =>
        {
            if (!ApiKeyValidator.HasValidApiKey(context.Request, apiSettings.ApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Khong co quyen truy cap API." });
                return;
            }

            await next();
    });
}

// Nếu bật JwtSettings.RequireJwt, các endpoint nghiệp vụ phải gửi Authorization: Bearer <token>.
app.Use(async (context, next) =>
{
    JwtTokenService tokenService = context.RequestServices.GetRequiredService<JwtTokenService>();
    string token = JwtTokenService.GetBearerToken(context.Request);
    if (tokenService.TryReadUser(token, out string username, out string role))
    {
        ApiAuthorization.SetAuthenticatedUser(context, username, role);
    }

    await next();
});

if (jwtSettings.RequireJwt)
{
    app.Use(async (context, next) =>
    {
        if (!JwtTokenService.IsPublicPath(context.Request.Path))
        {
            if (context.User?.Identity?.IsAuthenticated != true)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { message = "Token dang nhap khong hop le hoac da het han." });
                return;
            }
        }

        await next();
    });
}

// Đăng ký các nhóm route API theo từng chức năng.
app.UseRateLimiter();

    app.MapSystemEndpoints();
    app.MapAuthEndpoints();
    app.MapHangHoaEndpoints();
    app.MapLoaiHangEndpoints();
    app.MapNhaCungCapEndpoints();
    app.MapKhachHangEndpoints();
    app.MapNhanVienEndpoints();
    app.MapKhoEndpoints();
    app.MapPhieuNhapEndpoints();
    app.MapPhieuXuatEndpoints();
    app.MapDashboardEndpoints();

    // Nếu chạy API trực tiếp, tự mở WinForms để người dùng thấy giao diện desktop.
    app.Lifetime.ApplicationStarted.Register(DesktopClientLauncher.StartIfNeeded);

    app.Run();

static void ApplySecurityEnvironmentOverrides(
    ApiSettings apiSettings,
    JwtSettings jwtSettings,
    IWebHostEnvironment environment)
{
    jwtSettings.SecretKey = Environment.GetEnvironmentVariable("QLKH_JWT_SECRET") ?? string.Empty;
    apiSettings.ApiKey = Environment.GetEnvironmentVariable("QLKH_API_KEY") ?? string.Empty;

    if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey) && environment.IsDevelopment())
    {
        jwtSettings.SecretKey = Convert.ToBase64String(RandomNumberGenerator.GetBytes(48));
        Console.Error.WriteLine("QLKH_JWT_SECRET chua duoc dat; da tao secret tam thoi cho phien Development nay.");
    }
}

static void ValidateSecurityConfiguration(
    ApiSettings apiSettings,
    JwtSettings jwtSettings,
    IConfiguration configuration,
    IWebHostEnvironment environment)
{
    List<string> errors = new List<string>();
    if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey) || jwtSettings.SecretKey.Length < 32)
    {
        errors.Add("QLKH_JWT_SECRET phai duoc dat va co it nhat 32 ky tu.");
    }

    if (apiSettings.RequireApiKey && string.IsNullOrWhiteSpace(apiSettings.ApiKey))
    {
        errors.Add("QLKH_API_KEY phai duoc dat khi RequireApiKey = true.");
    }

    if (environment.IsDevelopment())
    {
        if (errors.Count > 0)
        {
            throw new InvalidOperationException("Cau hinh bao mat khong hop le: " + string.Join(" ", errors));
        }

        return;
    }

    string databasePassword = Environment.GetEnvironmentVariable("QLKH_DB_PASSWORD")
        ?? configuration["DatabaseSettings:Password"]
        ?? string.Empty;

    string demoDatabasePassword = Encoding.UTF8.GetString(Convert.FromBase64String("MTIzNA=="));
    if (string.Equals(databasePassword, demoDatabasePassword, StringComparison.Ordinal))
    {
        errors.Add("DatabaseSettings.Password/QLKH_DB_PASSWORD khong duoc dung mat khau demo.");
    }

    if (!jwtSettings.RequireJwt)
    {
        errors.Add("JwtSettings.RequireJwt phai bat trong moi truong production.");
    }

    if (apiSettings.AllowsAnyOrigin())
    {
        errors.Add("ApiSettings.AllowedOrigins phai gioi han origin trong production.");
    }

    if (!apiSettings.Url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
    {
        errors.Add("ApiSettings.Url phai dung HTTPS trong production.");
    }

    if (errors.Count > 0)
    {
        throw new InvalidOperationException("Cau hinh production khong an toan: " + string.Join(" ", errors));
    }
}

static string GetRateLimitPartitionKey(HttpContext context)
{
    string username = context.User?.Identity?.IsAuthenticated == true
        ? context.User.Identity.Name
        : string.Empty;

    if (!string.IsNullOrWhiteSpace(username))
    {
        return "user:" + username.Trim().ToLowerInvariant();
    }

    if (context.Request.Headers.TryGetValue("X-API-Key", out var apiKeyValues)
        && !string.IsNullOrWhiteSpace(apiKeyValues.ToString()))
    {
        return "api-key:" + HashPartitionValue(apiKeyValues.ToString());
    }

    string bearerToken = JwtTokenService.GetBearerToken(context.Request);
    if (!string.IsNullOrWhiteSpace(bearerToken))
    {
        return "bearer:" + HashPartitionValue(bearerToken);
    }

    return "ip:" + (context.Connection.RemoteIpAddress?.ToString() ?? "unknown");
}

static string HashPartitionValue(string value)
{
    byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(value));
    return Convert.ToHexString(hash).Substring(0, 16);
}
