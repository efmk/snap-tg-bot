namespace SnapTgBot.Settings.Providers;

internal interface ISettingsProvider
{
    T GetSettings<T>();
}
