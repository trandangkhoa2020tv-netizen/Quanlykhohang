# QuanLyKhoHang.Shared

Class library target `net9.0`, chua model dung chung cho `QuanLyKhoHang.Api`, `QuanLyKhoHang.WinForms` va test. Project nay khong chua UI, SQL, HTTP client hay logic truy cap database.

## Models

| File | Du lieu |
| --- | --- |
| `Account.cs` | Tai khoan va thong tin quyen. |
| `Product.cs` | Hang hoa va ton kho. |
| `ProductCategory.cs` | Loai hang. |
| `Supplier.cs` | Nha cung cap. |
| `Customer.cs` | Khach hang. |
| `Employee.cs` | Nhan vien. |
| `GoodsReceipt.cs` / `GoodsReceiptDetail.cs` | Phieu nhap va chi tiet. |
| `GoodsIssue.cs` / `GoodsIssueDetail.cs` | Phieu xuat va chi tiet. |
| `DashboardModels.cs` | DTO tong hop cho dashboard. |
| `UserSession.cs` | Trang thai user dung cho desktop sau dang nhap. |

## Nguyen tac

- Giữ model don gian, khong tham chieu WinForms, ASP.NET Core hay Npgsql.
- Thay doi model co the anh huong API, client va serialization; build toan solution sau moi thay doi.
- Khong dat password, API key, connection string hoac logic bao mat vao Shared.
- DTO request/response chi dung rieng cho API nen de trong `QuanLyKhoHang.Api/DTOs`.

## Build

Project duoc build tu dong khi build solution:

```powershell
dotnet build QuanLyKhoHang.sln
```
