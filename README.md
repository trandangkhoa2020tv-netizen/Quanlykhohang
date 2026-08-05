# Inventory Management System

An inventory management system for Windows desktop, consisting of a WinForms application, an ASP.NET Core Minimal API, and PostgreSQL.

## Components

| Project | Purpose |
| --- | --- |
| `QuanLyKhoHang.WinForms` | Desktop application: sign-in, master-data management, goods receipt/issue creation, dashboard, and Excel/PDF export. |
| `QuanLyKhoHang.Api` | HTTP backend: JWT authentication, authorization, validation, business processing, audit logging, and PostgreSQL access. |
| `QuanLyKhoHang.Shared` | Models and DTOs shared by the desktop application and API. |
| `QuanLyKhoHang.Tests` | xUnit tests for validation, JWT, and security middleware. |

All projects target `.NET 9`; WinForms targets `net9.0-windows`.

## Architecture

```text
WinForms
  -> ApiClients
  -> HTTP JSON (X-API-Key when enabled; Bearer JWT after sign-in)
  -> QuanLyKhoHang.Api
  -> Services / Repositories
  -> PostgreSQL
```

WinForms does not connect directly to PostgreSQL. All authorization checks and critical business logic reside in the API.

## Requirements

- .NET SDK 9.0
- PostgreSQL 17 or compatible
- Visual Studio 2022 with the **Desktop development with .NET** workload (if using an IDE)
- Docker Desktop (optional, for the API and PostgreSQL containers)

## Run locally

1. Create the `quanlyhanghoa` PostgreSQL database and run the schema:

   ```powershell
   psql -U postgres -d quanlyhanghoa -f QuanLyKhoHang.WinForms/sql/create_tables.sql
   ```

2. Set environment variables for the PowerShell session that runs the API:

   ```powershell
   $env:QLKH_DB_PASSWORD = "<database-password>"
   $env:QLKH_JWT_SECRET = "<secret-at-least-32-characters>"
   ```

   `QLKH_JWT_SECRET` must be at least 32 characters. Docker Compose and the backup script use the `.env` file, but `dotnet run` does not load it automatically.

3. Restore packages and build:

   ```powershell
   dotnet restore QuanLyKhoHang.sln
   dotnet build QuanLyKhoHang.sln
   ```

4. Run the API:

   ```powershell
   dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
   ```

5. Run the desktop application in another terminal:

   ```powershell
   dotnet run --project QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj
   ```

By default, the API listens on `http://localhost:8088`. Swagger is available only in Development at `http://localhost:8088/swagger`.

The schema and `sample_data.sql` do not create a default sign-in account. Create the first account through your organization's database-administration process, using a valid PBKDF2 password hash; do not add plain-text passwords to scripts or documentation.

For local development only, WinForms can start the API itself when `Config/appsettings.json` sets `AutoStartLocalApi` to `true` and `ApiBaseUrl` to `http://localhost:8088`.

## Docker

Docker runs the API and PostgreSQL; WinForms still runs on the Windows host.

```powershell
Copy-Item .env.example .env
# Set QLKH_DB_PASSWORD and QLKH_JWT_SECRET in .env
docker compose up -d --build
```

| Service | Host address |
| --- | --- |
| API | `http://localhost:8088` |
| PostgreSQL | `localhost:5432` |

Inside Docker, the API connects to PostgreSQL through `postgres:5432`. If local PostgreSQL already uses port `5432`, change the published port in `docker-compose.yml`.

## Configuration and security

[`.env.example`](.env.example) lists the available environment variables:

```text
QLKH_DB_HOST, QLKH_DB_PORT, QLKH_DB_NAME, QLKH_DB_USER, QLKH_DB_PASSWORD
QLKH_JWT_SECRET
QLKH_API_KEY
QLKH_AUTO_MIGRATE
QLKH_SEED_DEMO_DATA
```

- `QLKH_JWT_SECRET`: required in production and at least 32 characters long. In Development, if it is not set, a temporary secret is generated for the running session.
- `QLKH_API_KEY`: required when `ApiSettings.RequireApiKey=true`; the desktop client sends it in `X-API-Key`.
- `QLKH_AUTO_MIGRATE=1`: Development only; allows runtime migrations.
- `QLKH_SEED_DEMO_DATA=1`: takes effect only when automatic migration is enabled; do not use in production.

The API limits request bodies to 256 KB, requires JSON for POST/PUT/PATCH API calls, rate-limits sign-in to 5 attempts per 15 minutes, and applies a global limit of 120 requests per minute per user/IP. Business endpoints use JWT; sensitive operations also require the `Admin` role.

## Database

SQL files are in `QuanLyKhoHang.WinForms/sql/`:

| File | Purpose |
| --- | --- |
| `create_tables.sql` | Creates the schema, constraints, indexes, and audit log for a new database. |
| `sample_data.sql` | Sample business data; does not contain sample accounts or passwords. |
| `sync_existing_database.sql` | Synchronizes an existing database schema. |
| `migrate_add_trang_thai.sql` | Account-status migration for existing databases. |
| `backup_database.ps1` | Creates a backup with `pg_dump`, reading settings from environment variables. |

Back up a production database before running migrations. The API does not migrate or seed automatically unless the Development variables above are explicitly enabled.

## Tests

```powershell
dotnet test QuanLyKhoHang.sln
```

The current tests do not require a real PostgreSQL instance. See the [test project README](QuanLyKhoHang.Tests/README.md) for coverage and detailed instructions.

## Quick structure

```text
QuanLyKhoHang.sln
QuanLyKhoHang.Api/        Minimal API backend
QuanLyKhoHang.WinForms/   WinForms desktop application
QuanLyKhoHang.Shared/     Shared models
QuanLyKhoHang.Tests/      xUnit tests
docker-compose.yml        API + PostgreSQL for local Docker
.env.example              Environment-variable template without secrets
```

Detailed documentation is available in each project's README.
