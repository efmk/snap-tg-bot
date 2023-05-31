namespace SnapTgBot.Settings;

internal sealed record SecuritySettings
{
    public IReadOnlyCollection<string> AnonymousCommands { get; set; } = Array.Empty<string>();

    public IReadOnlyCollection<long> AllowedUsers { get; set; } = Array.Empty<long>();
}
