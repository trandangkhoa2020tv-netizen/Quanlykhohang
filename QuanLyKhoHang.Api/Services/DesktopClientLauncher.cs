using System.Diagnostics;

namespace QuanLyKhoHang.ApiServer.Services;

/// <summary>
/// Lớp hỗ trợ tự mở ứng dụng WinForms khi backend API được chạy trực tiếp.
/// </summary>
public static class DesktopClientLauncher
{
    private static readonly (string ProjectDirectory, string ExecutableName)[] DesktopCandidates =
    {
        ("QuanLyKhoHang.WinForms", "QuanLyKhoHang.WinForms.exe"),
        ("QuanLyKhoHang", "QuanLyKhoHang.exe")
    };

    /// <summary>
    /// Mở WinForms nếu API không được khởi động bởi chính ứng dụng desktop.
    /// </summary>
    public static void StartIfNeeded()
    {
        if (Environment.GetEnvironmentVariable("QUANLYKHOHANG_DISABLE_DESKTOP_LAUNCH") == "1")
        {
            return;
        }

        if (Environment.GetEnvironmentVariable("QUANLYKHOHANG_STARTED_BY_DESKTOP") == "1")
        {
            return;
        }

        string desktopExecutable = FindDesktopExecutable();
        if (string.IsNullOrEmpty(desktopExecutable))
        {
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = desktopExecutable,
                WorkingDirectory = Path.GetDirectoryName(desktopExecutable) ?? AppContext.BaseDirectory,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("Khong the mo giao dien QuanLyKhoHang: " + ex.Message);
        }
    }

    /// <summary>
    /// Tim file WinForms executable trong cac thu muc build Debug hoac Release.
    /// </summary>
    private static string FindDesktopExecutable()
    {
        DirectoryInfo directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory != null)
        {
            foreach ((string projectDirectory, string executableName) in DesktopCandidates)
            {
                foreach (string configuration in new[] { "Debug", "Release" })
                {
                    string desktopExecutable = Path.Combine(
                        directory.FullName,
                        projectDirectory,
                        "bin",
                        configuration,
                        "net9.0-windows",
                        executableName);

                    if (File.Exists(desktopExecutable))
                    {
                        return desktopExecutable;
                    }
                }
            }

            directory = directory.Parent;
        }

        return string.Empty;
    }
}
