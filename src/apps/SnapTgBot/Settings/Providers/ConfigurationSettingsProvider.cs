using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using SnapTgBot.Utils;

namespace SnapTgBot.Settings.Providers;

internal sealed class ConfigurationSettingsProvider : ISettingsProvider
{
    private readonly IConfiguration _configuration;
    private readonly ConcurrentDictionary<string, object> _settingsCache = new();

    public ConfigurationSettingsProvider(IConfiguration configuration)
    {
        _configuration = configuration;

        ChangeToken.OnChange(_configuration.GetReloadToken, OnConfigurationChanged);
    }

    public T GetSettings<T>()
    {
        var settingsKey = ConfigurationUtils.GetSettingsKey<T>();
        var cacheKey = settingsKey;
        return (T)_settingsCache.GetOrAdd(
            cacheKey,
            _ =>
            {
                var settings = _configuration.Get<T>()!;
                _configuration.BindToSettings(settingsKey, settings);
                return settings;
            });
    }

    private void OnConfigurationChanged() => _settingsCache.Clear();
}
