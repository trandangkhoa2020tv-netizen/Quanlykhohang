# QuanLyKhoHang.WinForms

The Windows desktop application for the Inventory Management System. The project targets `net9.0-windows` and uses WinForms. It only renders the UI and calls the backend; it does not access PostgreSQL directly.

## Features

- Sign-in and in-memory session storage.
- Management of products, customers, employees, and catalogs.
- Creation of goods receipts and goods issues, with history viewing.
- Overview dashboard.
- Receipt export to Excel/PDF.
- UI authorization based on the role returned by the API.

## Structure

```text
ApiClients/  HTTP clients, backend configuration, and local API launcher
Config/      Desktop configuration
Controls/    Custom controls, including dashboard charts
Forms/       Forms and Designer UI
Reports/     Excel/PDF export
sql/         Schema, migrations, sample data, and backup script
Program.cs   Application entry point
```

## Forms

| File | Screen |
| --- | --- |
| `FrmLogin` | Sign-in and JWT receipt from the API. |
| `FrmMain` | Main shell, sidebar, account menu, and navigation. |
| `FrmProduct` | Product management. |
| `FrmCustomer` | Customer management. |
| `FrmEmployee` | Employee management. |
| `FrmCatalog` | Product-category and supplier catalog. |
| `FrmGoodsReceipt` | Create and export goods receipts. |
| `FrmGoodsIssue` | Create and export goods issues. |
| `FrmDashboard` | Overview statistics. |
| `UiTheme` | Shared theme and layout. |

Older class names may be retained to avoid changing Designer contracts; file names use English.

## Backend configuration

Copy the template file:

```powershell
Copy-Item Config/appsettings.example.json Config/appsettings.json
```

Example:

```json
{
  "ApiBaseUrl": "http://localhost:8088",
  "AutoStartLocalApi": false
}
```

| Key | Meaning |
| --- | --- |
| `ApiBaseUrl` | API address. The Docker/local default is `http://localhost:8088`. |
| `AutoStartLocalApi` | `true` for local development only; the desktop application attempts to start the API when it is not ready. |

If the API enables `RequireApiKey`, set `ApiKey` in the local `Config/appsettings.json`. Do not commit a real API key.

## Sign-in and API-call flow

```text
FrmLogin
-> AuthApiClient
-> POST /api/auth/login
-> ApiHttpClient.SetBearerToken(token)
-> UserSession (username, role for the UI)
-> FrmMain
```

After sign-in, `ApiHttpClient` automatically adds the `Authorization: Bearer <jwt>` header. If an API key is configured, the client adds `X-API-Key`. The JWT remains in process memory only and is never written to a file.

## Run the application

Run the API first, or enable `AutoStartLocalApi` under the conditions above. From the solution root:

```powershell
dotnet run --project QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj
```

In Visual Studio, set `QuanLyKhoHang.WinForms` as the Startup Project and click Start. Do not set `QuanLyKhoHang.Tests` as the Startup Project when you want to open the UI; the test project completes and returns exit code `0`.

## Publish

```powershell
dotnet publish QuanLyKhoHang.WinForms/QuanLyKhoHang.WinForms.csproj `
  -c Release `
  -r win-x64 `
  --self-contained true
```

The published application still requires the API and PostgreSQL to be available at the configured URL.

## Development rules

- Forms call classes in `ApiClients/`; do not create `HttpClient` directly in a form.
- Do not write SQL in WinForms.
- Put processing logic in `*.cs`; change UI through the Designer when possible.
- When editing `*.Designer.cs`, check DPI behavior and rebuild to prevent layout issues.
- When changing API routes or column names, update API clients and DataGridView/ComboBox bindings.
- UI authorization improves the user experience only; the API is the mandatory protection layer.
