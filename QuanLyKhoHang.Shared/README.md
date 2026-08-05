# QuanLyKhoHang.Shared

A class library targeting `net9.0` that contains models shared by `QuanLyKhoHang.Api`, `QuanLyKhoHang.WinForms`, and the tests. This project contains no UI, SQL, HTTP clients, or database-access logic.

## Models

| File | Data |
| --- | --- |
| `Account.cs` | Accounts and authorization information. |
| `Product.cs` | Products and inventory. |
| `ProductCategory.cs` | Product categories. |
| `Supplier.cs` | Suppliers. |
| `Customer.cs` | Customers. |
| `Employee.cs` | Employees. |
| `GoodsReceipt.cs` / `GoodsReceiptDetail.cs` | Goods receipts and their details. |
| `GoodsIssue.cs` / `GoodsIssueDetail.cs` | Goods issues and their details. |
| `DashboardModels.cs` | Aggregated DTOs for the dashboard. |
| `UserSession.cs` | User state used by the desktop application after sign-in. |

## Principles

- Keep models simple and independent of WinForms, ASP.NET Core, and Npgsql.
- Model changes can affect the API, client, and serialization; build the entire solution after every change.
- Do not put passwords, API keys, connection strings, or security logic in Shared.
- Keep API-specific request/response DTOs in `QuanLyKhoHang.Api/DTOs`.

## Build

The project is built automatically when the solution is built:

```powershell
dotnet build QuanLyKhoHang.sln
```
