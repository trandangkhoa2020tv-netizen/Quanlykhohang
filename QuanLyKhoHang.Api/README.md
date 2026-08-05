# QuanLyKhoHang.Api

The ASP.NET Core Minimal API backend for the Inventory Management System. The project targets `net9.0`, uses PostgreSQL through Npgsql, and does not use MVC controllers; routes are organized in the `Endpoints/` files.

## Responsibilities

- Sign-in, JWT issuance, and JWT validation.
- Optional API-key validation and `Admin` role authorization.
- Request validation and transactional processing of goods receipts/issues.
- Management of products, categories, suppliers, customers, employees, and the dashboard.
- Audit logging for data-changing operations.
- JSON responses for WinForms and other clients.

## Structure

```text
Config/        ApiSettings and JwtSettings
Data/          Shared database connection, maintenance, and query utilities
DTOs/          Request/response DTOs
Endpoints/     Minimal API routes grouped by business area
Repositories/  SQL and transactions
Services/      Validation, business rules, JWT, and audit logging
Program.cs     Bootstrap, DI, middleware, and endpoint mapping
```

## Run the API

From the solution root:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

By default, the API listens on `http://localhost:8088`. Quick check:

```powershell
Invoke-RestMethod http://localhost:8088/api/health
```

In Development, Swagger is available at `http://localhost:8088/swagger`.

## Configuration

`appsettings.json` contains only non-sensitive values. Set secrets and the database password through environment variables or a `.env` file used by local tooling:

```text
QLKH_DB_HOST=localhost
QLKH_DB_PORT=5432
QLKH_DB_NAME=quanlyhanghoa
QLKH_DB_USER=postgres
QLKH_DB_PASSWORD=
QLKH_JWT_SECRET=
QLKH_API_KEY=
```

`QLKH_JWT_SECRET` overrides `JwtSettings.SecretKey`; `QLKH_API_KEY` overrides `ApiSettings.ApiKey`. Do not put secrets in `appsettings.json`, source control, or a README.

When using `dotnet run`, set the required variables in the shell or in the launch profile's Environment Variables configuration. .NET does not load `.env` files automatically; Docker Compose uses this file when it runs containers.

In production, the API refuses to start if:

- JWT is disabled, or its secret is empty or shorter than 32 characters.
- `RequireApiKey=true` but `QLKH_API_KEY` is absent.
- CORS allows every origin.
- The API URL does not use HTTPS.
- The database password uses a prohibited demo value.

## Middleware and limits

Main processing order:

```text
CORS
-> content-type / body-size validation
-> Swagger (Development)
-> API key (when enabled)
-> JWT parsing
-> JWT guard for non-public routes
-> rate limiter
-> endpoint
```

- POST/PUT/PATCH requests under `/api` accept JSON only and are limited to 256 KB.
- Global limiter: 120 requests/minute per signed-in user or IP address.
- Sign-in: 5 attempts/15 minutes.
- Product routes have separate policies for read/create/update/delete.
- `429` is returned when a rate limit is exceeded.

Public routes: `/`, `/api/health`, `/api/chuc-nang`, `/api/docs`, `/api/auth/login`, and `/swagger` in Development.

## Endpoint groups

| Group | Main routes |
| --- | --- |
| System | `/api/health`, `/api/chuc-nang`, `/api/docs` |
| Authentication | `POST /api/auth/login` |
| Products | `/api/hang-hoa`, `/api/v2/hang-hoa` |
| Catalog | `/api/loai-hang`, `/api/nha-cung-cap` |
| Partners | `/api/khach-hang`, `/api/nhan-vien` |
| Inventory | `/api/ton-kho/thap` |
| Goods receipts | `/api/phieu-nhap` and details |
| Goods issues | `/api/phieu-xuat` and details/information |
| Dashboard | Routes in `DashboardEndpoints.cs` |

Routes under `/api/v2/...` return typed DTOs for newer clients. The `/api/...` routes are compatible with the WinForms DataTable client.

## Migration and seeding

By default, the API does not modify the database schema at startup. In Development only:

```powershell
$env:QLKH_AUTO_MIGRATE = "1"
# Optional: seed sample business data
$env:QLKH_SEED_DEMO_DATA = "1"
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

A migration failure prevents the API from starting. Do not enable these variables in production; run SQL scripts and a controlled migration process instead.

## Error codes

| HTTP | Meaning |
| --- | --- |
| `400` | Invalid data or business rule. |
| `401` | Missing, invalid, or expired API key/JWT. |
| `403` | Authenticated, but missing the required role. |
| `404` | Data was not found. |
| `413` | Payload exceeds 256 KB. |
| `415` | A POST/PUT/PATCH request did not send JSON. |
| `429` | Rate limit exceeded. |
| `500` | System error. |

## Development rules

- Endpoints must not write SQL directly; place logic in services/repositories.
- SQL with input must use `NpgsqlParameter`.
- Inventory changes must be performed within a transaction.
- Data-changing endpoints require consideration of validation, authorization, and audit logging.
- When adding an endpoint, update tests and the WinForms client if the desktop application uses it.
