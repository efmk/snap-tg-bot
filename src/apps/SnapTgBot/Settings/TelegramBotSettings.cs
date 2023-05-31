namespace SnapTgBot.Settings;

internal sealed record TelegramBotSettings
{
    public string AccessToken { get; set; } = string.Empty;
}
