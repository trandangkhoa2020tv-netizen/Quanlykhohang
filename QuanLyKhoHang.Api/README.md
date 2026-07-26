# QuanLyKhoHang.Api

Backend ASP.NET Core Minimal API cua he thong Quan Ly Kho Hang. Project target `net9.0`, dung PostgreSQL qua Npgsql va khong dung MVC controller; route duoc chia theo cac file trong `Endpoints/`.

## Trach nhiem

- Dang nhap, phat va kiem tra JWT.
- Kiem tra API key tuy chon va phan quyen role `Admin`.
- Validate request, xu ly phieu nhap/xuat trong transaction.
- Quan ly hang hoa, danh muc, nha cung cap, khach hang, nhan vien va dashboard.
- Ghi audit log cho thao tac thay doi du lieu.
- Tra ve JSON cho WinForms va client khac.

## Cau truc

```text
Config/        ApiSettings va JwtSettings
Data/          Ket noi, maintenance va truy van database dung chung
DTOs/          Request/response DTO
Endpoints/     Minimal API route theo nghiep vu
Repositories/  SQL va transaction
Services/      Validation, rule nghiep vu, JWT, audit
Program.cs     Bootstrap, DI, middleware va map endpoint
```

## Chay API

Tu root solution:

```powershell
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Mac dinh API nghe tai `http://localhost:8088`. Kiem tra nhanh:

```powershell
Invoke-RestMethod http://localhost:8088/api/health
```

Trong Development, Swagger tai `http://localhost:8088/swagger`.

## Cau hinh

`appsettings.json` chi giu gia tri khong nhay cam. Dat secret va mat khau database qua bien moi truong hoac file `.env` dung cho tooling local:

```text
QLKH_DB_HOST=localhost
QLKH_DB_PORT=5432
QLKH_DB_NAME=quanlyhanghoa
QLKH_DB_USER=postgres
QLKH_DB_PASSWORD=
QLKH_JWT_SECRET=
QLKH_API_KEY=
```

`QLKH_JWT_SECRET` ghi de `JwtSettings.SecretKey`; `QLKH_API_KEY` ghi de `ApiSettings.ApiKey`. Khong dat secret trong `appsettings.json`, source control hoac README.

Khi chay bang `dotnet run`, dat cac bien can thiet trong shell hoac cau hinh Environment Variables cua launch profile. File `.env` khong duoc .NET nap tu dong; Docker Compose dung file nay khi chay container.

Trong production, API tu choi khoi dong neu:

- JWT bi tat, secret trong hoac ngan hon 32 ky tu.
- `RequireApiKey=true` nhung khong co `QLKH_API_KEY`.
- CORS cho phep moi origin.
- URL API khong dung HTTPS.
- Mat khau database dung gia tri demo bi cam.

## Middleware va gioi han

Thu tu xu ly chinh:

```text
CORS
-> kiem tra content type / body size
-> Swagger (Development)
-> API key (neu bat)
-> doc JWT
-> JWT guard cho route khong public
-> rate limiter
-> endpoint
```

- POST/PUT/PATCH duoi `/api` chi nhan JSON va toi da 256 KB.
- Global limiter: 120 request/phut theo user da dang nhap hoac IP.
- Login: 5 lan/15 phut.
- Cac route hang hoa co policy rieng cho doc/tao/sua/xoa.
- `429` duoc tra khi vuot gioi han.

Route public: `/`, `/api/health`, `/api/chuc-nang`, `/api/docs`, `/api/auth/login` va `/swagger` trong Development.

## Endpoint nhom

| Nhom | Duong dan chinh |
| --- | --- |
| He thong | `/api/health`, `/api/chuc-nang`, `/api/docs` |
| Xac thuc | `POST /api/auth/login` |
| Hang hoa | `/api/hang-hoa`, `/api/v2/hang-hoa` |
| Danh muc | `/api/loai-hang`, `/api/nha-cung-cap` |
| Doi tac | `/api/khach-hang`, `/api/nhan-vien` |
| Kho | `/api/ton-kho/thap` |
| Phieu nhap | `/api/phieu-nhap` va chi tiet |
| Phieu xuat | `/api/phieu-xuat` va chi tiet/thong tin |
| Dashboard | Route trong `DashboardEndpoints.cs` |

Route `/api/v2/...` tra DTO typed cho client moi. Cac route `/api/...` tuong thich voi client DataTable cua WinForms.

## Migration va seed

Mac dinh API khong sua schema database khi khoi dong. Chi trong Development:

```powershell
$env:QLKH_AUTO_MIGRATE = "1"
# Tuy chon: seed du lieu nghiep vu mau
$env:QLKH_SEED_DEMO_DATA = "1"
dotnet run --project QuanLyKhoHang.Api/QuanLyKhoHang.Api.csproj
```

Loi migration lam API dung khoi dong. Khong bat cac bien nay trong production; hay chay script SQL va quy trinh migration co kiem soat.

## Ma loi

| HTTP | Y nghia |
| --- | --- |
| `400` | Du lieu hoac rule nghiep vu khong hop le. |
| `401` | API key/JWT thieu, sai hoac het han. |
| `403` | Da xac thuc nhung khong du role. |
| `404` | Khong tim thay du lieu. |
| `413` | Payload lon hon 256 KB. |
| `415` | POST/PUT/PATCH khong gui JSON. |
| `429` | Vuot rate limit. |
| `500` | Loi he thong. |

## Quy tac phat trien

- Endpoint khong viet SQL truc tiep; dua logic vao service/repository.
- SQL co input phai dung `NpgsqlParameter`.
- Thay doi ton kho phai nam trong transaction.
- Endpoint thay doi du lieu can xem xet validation, phan quyen va audit log.
- Them endpoint can cap nhat test va WinForms client neu desktop su dung endpoint do.
