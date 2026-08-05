# QuanLyKhoHang.Tests

An xUnit project targeting `net9.0` that verifies rules which do not require WinForms or a real PostgreSQL instance. The project references `QuanLyKhoHang.Api` and `QuanLyKhoHang.Shared`.

## Current tests

| File | Coverage |
| --- | --- |
| `JwtTokenServiceTests.cs` | Creates, validates, and reads usernames/roles from JWTs. |
| `ServiceValidationTests.cs` | Service validation for sign-in, products, goods receipts, and goods issues. |
| `SecurityIntegrationTests.cs` | API-key validation, role authorization, and rejection of oversized sign-in payloads. |

The current tests include cases such as empty sign-in requests, products with missing names or negative stock, documents without details, invalid quantities, malformed JWTs, unauthorized users, and payloads exceeding the API limit.

## Run tests

From the repository root:

```powershell
dotnet test QuanLyKhoHang.sln
```

Run only the test project:

```powershell
dotnet test QuanLyKhoHang.Tests/QuanLyKhoHang.Tests.csproj
```

After building/restoring, run quickly:

```powershell
dotnet test QuanLyKhoHang.Tests/QuanLyKhoHang.Tests.csproj --no-build --no-restore
```

Exit code `0` and the `Passed!` line mean the tests succeeded. If the test project is set as the Startup Project in Visual Studio, the console window closing immediately after the tests complete is normal behavior.

## Rules for adding tests

- Each test verifies one clear, independent behavior.
- Unit tests must not connect to a real database.
- Tests that require PostgreSQL must be placed in a separate integration-test group, use an isolated test database, and perform cleanup.
- Tests for sensitive endpoints should cover `401`, `403`, validation, and rate limiting where applicable.
- Do not use secrets, real passwords, or production data in tests.
