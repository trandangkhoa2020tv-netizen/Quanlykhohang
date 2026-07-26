# QuanLyKhoHang.Tests

Project xUnit target `net9.0` de kiem tra cac rule khong can WinForms hay PostgreSQL that. Project reference `QuanLyKhoHang.Api` va `QuanLyKhoHang.Shared`.

## Test hien co

| File | Pham vi |
| --- | --- |
| `JwtTokenServiceTests.cs` | Tao, validate va doc username/role tu JWT. |
| `ServiceValidationTests.cs` | Validation cua service cho dang nhap, hang hoa, phieu nhap va phieu xuat. |
| `SecurityIntegrationTests.cs` | API key validator, authorization role va chan payload login qua lon. |

Test hien tai bao gom cac tinh huong nhu request dang nhap rong, hang hoa thieu ten/ton am, phieu khong co chi tiet, so luong khong hop le, JWT sai dinh dang, user khong du quyen va payload lon hon gioi han API.

## Chay test

Tu root repository:

```powershell
dotnet test QuanLyKhoHang.sln
```

Chi chay project test:

```powershell
dotnet test QuanLyKhoHang.Tests/QuanLyKhoHang.Tests.csproj
```

Sau khi da build/restore, chay nhanh:

```powershell
dotnet test QuanLyKhoHang.Tests/QuanLyKhoHang.Tests.csproj --no-build --no-restore
```

Exit code `0` va dong `Passed!` nghia la test thanh cong. Khi dat project test lam Startup Project trong Visual Studio, cua so console ket thuc ngay sau khi test xong la hanh vi binh thuong.

## Quy tac them test

- Moi test kiem tra mot hanh vi ro rang va doc lap.
- Unit test khong ket noi database that.
- Test can PostgreSQL phai duoc dat trong nhom integration rieng, dung database test tach biet va co cleanup.
- Test endpoint nhay cam can bao phu ca `401`, `403`, validation va rate limit neu phu hop.
- Khong dung secret, mat khau that hay du lieu production trong test.
