using Microsoft.Extensions.Configuration;

namespace SnapTgBot.Utils;

internal static class ConfigurationUtils
{
    public static string GetSettingsKey<T>() => typeof(T).Name.TrimSuffix("Settings");

    public static void BindToSettings(this IConfiguration configuration, string settingsKey, object settings) =>
        configuration.GetSection(settingsKey).Bind(settings);
}
