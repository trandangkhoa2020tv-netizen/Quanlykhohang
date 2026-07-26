using System.Text.Json;

namespace QuanLyKhoHang.ApiClients
{
    /// <summary>
    /// Cau hinh ket noi tu WinForms toi backend API.
    /// </summary>
    public sealed class ApiClientSettings
    {
        private const int LocalDevelopmentApiPort = 8088;

        /// <summary>Dia chi goc cua API server.</summary>
        public string BaseUrl { get; private set; } = "http://localhost:8088";

        /// <summary>API key gui kem request neu backend bat xac thuc bang API key.</summary>
        public string ApiKey { get; private set; } = string.Empty;

        /// <summary>Cho phep WinForms tu khoi dong API local khi dung port phat trien mac dinh.</summary>
        public bool AutoStartLocalApi { get; private set; } = true;

        /// <summary>Doc cau hinh tu appsettings, ho tro ca format moi va format cu.</summary>
        public static ApiClientSettings Load()
        {
            ApiClientSettings settings = new ApiClientSettings();

            try
            {
                string configPath = Path.Combine(AppContext.BaseDirectory, "Config", "appsettings.json");
                if (!File.Exists(configPath))
                {
                    configPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
                }

                if (File.Exists(configPath))
                {
                    using FileStream stream = File.OpenRead(configPath);
                    using JsonDocument document = JsonDocument.Parse(stream);

                    if (document.RootElement.TryGetProperty("ApiClientSettings", out JsonElement api))
                    {
                        ApplySettingsSection(settings, api);
                    }

                    ApplyRootSettings(settings, document.RootElement);
                }
            }
            catch
            {
                // Use defaults and environment variables when the local config is unavailable.
            }

            string apiKey = Environment.GetEnvironmentVariable("QLKH_API_KEY");
            if (!string.IsNullOrWhiteSpace(apiKey))
            {
                settings.ApiKey = apiKey;
            }

            return settings;
        }

        public bool CanAutoStartLocalApi()
        {
            if (!AutoStartLocalApi)
            {
                return false;
            }

            if (!Uri.TryCreate(BaseUrl, UriKind.Absolute, out Uri uri))
            {
                return false;
            }

            return uri.IsLoopback && uri.Port == LocalDevelopmentApiPort;
        }

        private static void ApplyRootSettings(ApiClientSettings settings, JsonElement root)
        {
            if (root.TryGetProperty("ApiBaseUrl", out JsonElement apiBaseUrl))
            {
                settings.BaseUrl = apiBaseUrl.GetString() ?? settings.BaseUrl;
            }

            if (root.TryGetProperty("AutoStartLocalApi", out JsonElement autoStartLocalApi)
                && IsBoolean(autoStartLocalApi))
            {
                settings.AutoStartLocalApi = autoStartLocalApi.GetBoolean();
            }
        }

        private static void ApplySettingsSection(ApiClientSettings settings, JsonElement api)
        {
            if (api.TryGetProperty("BaseUrl", out JsonElement baseUrl))
            {
                settings.BaseUrl = baseUrl.GetString() ?? settings.BaseUrl;
            }

            if (api.TryGetProperty("ApiBaseUrl", out JsonElement apiBaseUrl))
            {
                settings.BaseUrl = apiBaseUrl.GetString() ?? settings.BaseUrl;
            }

            if (api.TryGetProperty("AutoStartLocalApi", out JsonElement autoStartLocalApi)
                && IsBoolean(autoStartLocalApi))
            {
                settings.AutoStartLocalApi = autoStartLocalApi.GetBoolean();
            }
        }

        private static bool IsBoolean(JsonElement value)
        {
            return value.ValueKind == JsonValueKind.True || value.ValueKind == JsonValueKind.False;
        }
    }
}
