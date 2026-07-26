using System.Diagnostics;
using System.Net.Http;

namespace QuanLyKhoHang.ApiClients
{
    /// <summary>
    /// Tự kiểm tra và khởi động backend API khi người dùng mở ứng dụng WinForms.
    /// </summary>
    internal static class ApiServerLauncher
    {
        private static Process _startedProcess;

        /// <summary>
        /// Bảo đảm API server đang chạy; nếu chưa chạy thì tìm project/dll API và khởi động bằng dotnet.
        /// </summary>
        public static void EnsureStarted()
        {
            ApiClientSettings settings = ApiClientSettings.Load();
            if (IsHealthy(settings.BaseUrl))
            {
                return;
            }

            if (!settings.CanAutoStartLocalApi())
            {
                throw CreateApiUnavailableException(settings.BaseUrl);
            }

            ProcessStartInfo startInfo = CreateStartInfo();
            _startedProcess = Process.Start(startInfo);

            for (int i = 0; i < 30; i++)
            {
                Thread.Sleep(300);
                if (IsHealthy(settings.BaseUrl))
                {
                    return;
                }

                if (_startedProcess != null && _startedProcess.HasExited)
                {
                    break;
                }
            }

            throw CreateApiUnavailableException(settings.BaseUrl);
        }

        /// <summary>
        /// Dừng process API do ứng dụng desktop tự khởi động khi WinForms thoát.
        /// </summary>
        public static void StopIfStartedByApp()
        {
            try
            {
                if (_startedProcess != null && !_startedProcess.HasExited)
                {
                    _startedProcess.Kill(entireProcessTree: true);
                    _startedProcess.Dispose();
                }
            }
            catch
            {
                // Khong chan viec thoat WinForms neu API da tu dung.
            }
        }

        /// <summary>
        /// Gọi endpoint health để kiểm tra API đã sẵn sàng nhận request hay chưa.
        /// </summary>
        private static bool IsHealthy(string baseUrl)
        {
            try
            {
                using HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
                Uri healthUrl = new Uri(new Uri(EnsureTrailingSlash(baseUrl)), "api/health");
                using HttpResponseMessage response = client.GetAsync(healthUrl).GetAwaiter().GetResult();
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private static InvalidOperationException CreateApiUnavailableException(string baseUrl)
        {
            return new InvalidOperationException("Khong ket noi duoc API tai " + baseUrl + ". Hay kiem tra Docker/API server dang chay hoac cau hinh ApiBaseUrl.");
        }

        /// <summary>
        /// Tạo thông tin khởi động API, ưu tiên DLL đã build rồi mới chạy trực tiếp project.
        /// </summary>
        private static ProcessStartInfo CreateStartInfo()
        {
            string apiDirectory = FindApiDirectory();
            string debugDll = Path.Combine(apiDirectory, "bin", "Debug", "net9.0", "QuanLyKhoHang.Api.dll");
            string releaseDll = Path.Combine(apiDirectory, "bin", "Release", "net9.0", "QuanLyKhoHang.Api.dll");
            string projectFile = Path.Combine(apiDirectory, "QuanLyKhoHang.Api.csproj");

            if (File.Exists(debugDll))
            {
                return CreateDotnetStartInfo(apiDirectory, $"\"{debugDll}\"");
            }

            if (File.Exists(releaseDll))
            {
                return CreateDotnetStartInfo(apiDirectory, $"\"{releaseDll}\"");
            }

            if (File.Exists(projectFile))
            {
                return CreateDotnetStartInfo(apiDirectory, $"run --project \"{projectFile}\"");
            }

            throw new FileNotFoundException("Khong tim thay backend QuanLyKhoHang.Api.");
        }

        /// <summary>
        /// Tạo ProcessStartInfo chạy lệnh dotnet trong thư mục backend.
        /// </summary>
        private static ProcessStartInfo CreateDotnetStartInfo(string workingDirectory, string arguments)
        {
            return new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = arguments,
                WorkingDirectory = workingDirectory,
                UseShellExecute = false,
                CreateNoWindow = true,
                Environment =
                {
                    // API duoc tu khoi dong boi WinForms chi danh cho local dev.
                    ["ASPNETCORE_ENVIRONMENT"] = "Development",
                    ["QUANLYKHOHANG_STARTED_BY_DESKTOP"] = "1"
                }
            };
        }

        /// <summary>
        /// Tìm thư mục project QuanLyKhoHang.Api bằng cách duyệt từ thư mục chạy hiện tại lên các thư mục cha.
        /// </summary>
        private static string FindApiDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo(AppContext.BaseDirectory);
            while (directory != null)
            {
                string apiDirectory = Path.Combine(directory.FullName, "QuanLyKhoHang.Api");
                if (Directory.Exists(apiDirectory))
                {
                    return apiDirectory;
                }

                directory = directory.Parent;
            }

            string localApiDirectory = Path.Combine(AppContext.BaseDirectory, "QuanLyKhoHang.Api");
            if (Directory.Exists(localApiDirectory))
            {
                return localApiDirectory;
            }

            throw new DirectoryNotFoundException("Khong tim thay thu muc QuanLyKhoHang.Api.");
        }

        /// <summary>
        /// Bảo đảm URL gốc kết thúc bằng dấu gạch chéo trước khi ghép đường dẫn con.
        /// </summary>
        private static string EnsureTrailingSlash(string value)
        {
            return value.EndsWith("/", StringComparison.Ordinal) ? value : value + "/";
        }
    }
}
