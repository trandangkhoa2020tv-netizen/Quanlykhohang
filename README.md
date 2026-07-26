# Quan Ly Kho Hang

He thong quan ly kho hang cho desktop Windows, gom ung dung WinForms, ASP.NET Core Minimal API va PostgreSQL.

## Thanh phan

| Project | Muc dich |
| --- | --- |
| `QuanLyKhoHang.WinForms` | Ung dung desktop: dang nhap, quan ly danh muc, lap phieu nhap/xuat, dashboard va xuat Excel/PDF. |
| `QuanLyKhoHang.Api` | Backend HTTP: xac thuc JWT, phan quyen, validate, xu ly nghiep vu, audit log va PostgreSQL. |
| `QuanLyKhoHang.Shared` | Model va DTO dung chung giua desktop va API. |
| `QuanLyKhoHang.Tests` | xUnit test cho validation, JWT va cac middleware bao mat. |

Tat ca project dang target `.NET 9`; WinForms target `net9.0-windows`.

## Kien truc

```text
WinForms
  -> ApiClients
  -> HTTP JSON (X-API-Key neu bat, Bearer JWT sau dang nhap)
  -> QuanLyKhoHang.Api
  -> Services / Repositories
  -> PostgreSQL
```

WinForms khong ket noi truc tiep den PostgreSQL. Mọi kiem tra quyen va nghiep vu quan trong deu nam o API.

## Yeu cau

- .NET SDK 9.0
- PostgreSQL 17 hoac tuong thich
- Visual Studio 2022 co workload **Desktop development with .NET** (neu dung IDE)
- Docker Desktop (tuy chon, cho API va PostgreSQL container)

## Khoi dong local

1. Tao database PostgreSQL `quanlyhanghoa` va chay schema:

   ```powershell
   psql -U postgres -d quanlyhanghoa -f QuanLyKhoHang.WinForms/sql/create_tables.sql
   ```

2. Dat bien moi truong cho phien PowerShell chay API:

   ```powershell
   $env:QLKH_DB_PASSWORD = "<database-password>"
   $env:QLKH_JWT_SECRET = "<secret-at-least-32-characters>"
   ```

   `QLKH_JWT_SECRET` can dai it nhat 32 ky tu. File `.env` duoc Docker Compose va script backup su dung, nhung `dotnet run` khong tu dong nap file nay.

3. Khoi phuc package va build:

   ```powershell
   dotnet restore QuanLyKhoHang.sln
   dotnet build QuanLyKhoHang.sln
   ```

4. Chay API:

   ```powershell
   dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
   ```

5. Chay desktop o terminal khac:

   ```powershell
   dotnet run --project QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj
   ```

API mac dinh nghe tai `http://localhost:8088`. Swagger chi co trong Development tai `http://localhost:8088/swagger`.

Schema va `sample_data.sql` khong tao tai khoan dang nhap mac dinh. Hay tao tai khoan dau tien bang quy trinh quan tri database cua don vi, voi mat khau PBKDF2 hop le; khong them mat khau plain text vao script hay tai lieu.

Chi voi local development, WinForms co the tu khoi dong API khi `Config/appsettings.json` dat `AutoStartLocalApi` la `true` va `ApiBaseUrl` la `http://localhost:8088`.

## Docker

Docker chay API va PostgreSQL; WinForms van chay tren Windows host.

```powershell
Copy-Item .env.example .env
# Dat QLKH_DB_PASSWORD va QLKH_JWT_SECRET trong .env
docker compose up -d --build
```

| Dich vu | Dia chi tren may host |
| --- | --- |
| API | `http://localhost:8088` |
| PostgreSQL | `localhost:5432` |

Trong Docker, API ket noi PostgreSQL qua `postgres:5432`. Neu PostgreSQL local dang dung cong `5432`, hay doi port publish trong `docker-compose.yml`.

## Cau hinh va bao mat

File [`.env.example`](.env.example) liet ke cac bien mo rong:

```text
QLKH_DB_HOST, QLKH_DB_PORT, QLKH_DB_NAME, QLKH_DB_USER, QLKH_DB_PASSWORD
QLKH_JWT_SECRET
QLKH_API_KEY
QLKH_AUTO_MIGRATE
QLKH_SEED_DEMO_DATA
```

- `QLKH_JWT_SECRET`: bat buoc trong production, toi thieu 32 ky tu. Development khong dat bien nay se tao secret tam thoi cho phien chay.
- `QLKH_API_KEY`: bat buoc khi `ApiSettings.RequireApiKey=true`; desktop gui key qua `X-API-Key`.
- `QLKH_AUTO_MIGRATE=1`: chi cho Development, cho phep migration runtime.
- `QLKH_SEED_DEMO_DATA=1`: chi co tac dung khi da bat auto migrate; khong dung cho production.

API gioi han request body 256 KB, yeu cau JSON cho POST/PUT/PATCH API, rate-limit login 5 lan/15 phut va co global rate limit 120 request/phut theo user/IP. Endpoint nghiep vu dung JWT; cac thao tac nhay cam con kiem tra role `Admin`.

## Database

SQL nam tai `QuanLyKhoHang.WinForms/sql/`:

| File | Muc dich |
| --- | --- |
| `create_tables.sql` | Tao schema, rang buoc, index va audit log cho database moi. |
| `sample_data.sql` | Du lieu nghiep vu mau; khong chua tai khoan/mat khau mau. |
| `sync_existing_database.sql` | Dong bo schema database cu. |
| `migrate_add_trang_thai.sql` | Migration trang thai tai khoan cho database cu. |
| `backup_database.ps1` | Backup bang `pg_dump`, doc thong tin tu bien moi truong. |

Database production phai duoc backup truoc khi chay migration. API khong tu dong migrate hoac seed tru khi bat ro rang cac bien Development o tren.

## Test

```powershell
dotnet test QuanLyKhoHang.sln
```

Test hien tai khong can PostgreSQL that. Xem [README cua project test](QuanLyKhoHang.Tests/README.md) de biet pham vi va cach chay chi tiet.

## Cau truc nhanh

```text
QuanLyKhoHang.sln
QuanLyKhoHang.Api/        Backend Minimal API
QuanLyKhoHang.WinForms/   Desktop WinForms
QuanLyKhoHang.Shared/     Model dung chung
QuanLyKhoHang.Tests/      xUnit tests
docker-compose.yml        API + PostgreSQL cho local Docker
.env.example              Mau bien moi truong, khong chua secret
```

Tai lieu chi tiet nam o README cua tung project.
