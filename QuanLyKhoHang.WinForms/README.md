# QuanLyKhoHang.WinForms

Ung dung desktop Windows cua he thong Quan Ly Kho Hang. Project target `net9.0-windows` va dung WinForms. Ung dung chi hien thi giao dien va goi backend; khong truy cap PostgreSQL truc tiep.

## Chuc nang

- Dang nhap va luu phien trong bo nho.
- Quan ly hang hoa, khach hang, nhan vien va danh muc.
- Lap phieu nhap kho, phieu xuat kho va xem lich su.
- Dashboard tong quan.
- Xuat phieu ra Excel/PDF.
- Phan quyen UI theo role API tra ve.

## Cau truc

```text
ApiClients/  HTTP client, cau hinh backend va local API launcher
Config/      Cau hinh desktop
Controls/    Custom controls, bao gom chart dashboard
Forms/       Form va giao dien Designer
Reports/     Xuat Excel/PDF
sql/         Schema, migration, sample data va backup script
Program.cs   Diem khoi dong ung dung
```

## Forms

| File | Man hinh |
| --- | --- |
| `FrmLogin` | Dang nhap, nhan JWT tu API. |
| `FrmMain` | Shell chinh, sidebar, menu tai khoan va dieu huong. |
| `FrmProduct` | Quan ly hang hoa. |
| `FrmCustomer` | Quan ly khach hang. |
| `FrmEmployee` | Quan ly nhan vien. |
| `FrmCatalog` | Danh muc loai hang va nha cung cap. |
| `FrmGoodsReceipt` | Lap va xuat phieu nhap. |
| `FrmGoodsIssue` | Lap va xuat phieu xuat. |
| `FrmDashboard` | Thong ke tong quan. |
| `UiTheme` | Theme va bo cuc dung chung. |

Ten class cu co the van giu de tranh thay doi contract cua Designer; ten file da dung tieng Anh.

## Cau hinh backend

Sao chep file mau:

```powershell
Copy-Item Config/appsettings.example.json Config/appsettings.json
```

Vi du:

```json
{
  "ApiBaseUrl": "http://localhost:8088",
  "AutoStartLocalApi": false
}
```

| Key | Y nghia |
| --- | --- |
| `ApiBaseUrl` | Dia chi API. Docker/local mac dinh: `http://localhost:8088`. |
| `AutoStartLocalApi` | `true` chi cho development local; desktop se thu tu khoi dong API neu API chua san sang. |

Neu API bat `RequireApiKey`, dat `ApiKey` trong `Config/appsettings.json` tren may local. Khong commit API key that.

## Luong dang nhap va goi API

```text
FrmLogin
-> AuthApiClient
-> POST /api/auth/login
-> ApiHttpClient.SetBearerToken(token)
-> UserSession (username, role cho UI)
-> FrmMain
```

Sau khi dang nhap, `ApiHttpClient` tu them header `Authorization: Bearer <jwt>`. Neu cau hinh API key, client them `X-API-Key`. JWT chi duoc giu trong bo nho cua tien trinh, khong ghi ra file.

## Chay ung dung

Chay API truoc, hoac bat `AutoStartLocalApi` theo dieu kien o tren. Tu root solution:

```powershell
dotnet run --project QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj
```

Trong Visual Studio, dat `QuanLyKhoHang.WinForms` lam Startup Project va bam Start. Khong dat `QuanLyKhoHang.Tests` lam Startup Project neu muon mo giao dien; project test chay xong se tra exit code `0`.

## Publish

```powershell
dotnet publish QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj `
  -c Release `
  -r win-x64 `
  --self-contained true
```

Ban publish van can API va PostgreSQL dang hoat dong o URL da cau hinh.

## Quy tac phat trien

- Form goi lop trong `ApiClients/`; khong tao `HttpClient` truc tiep trong form.
- Khong viet SQL trong WinForms.
- Logic xu ly dat trong `*.cs`; thay doi giao dien bang Designer khi co the.
- Khi sua `*.Designer.cs`, kiem tra DPI va build lai de tranh loi layout.
- Khi doi route/ten cot API, cap nhat API client va binding cua DataGridView/ComboBox.
- Kiem tra phan quyen o UI chi de cai thien trai nghiem; API la lop bao ve bat buoc.
