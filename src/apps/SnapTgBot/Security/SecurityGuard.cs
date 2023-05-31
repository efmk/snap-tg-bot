using SnapTgBot.Settings;
using SnapTgBot.Utils;
using Telegram.Bot.Types;

namespace SnapTgBot.Security;

internal sealed class SecurityGuard : ISecurityGuard
{
    private readonly SecuritySettings _settings;

    public SecurityGuard(SecuritySettings settings)
    {
        _settings = settings;
    }

    public void CheckSecurity(Message message)
    {
        if (message.IsCommand() && _settings.AnonymousCommands.Contains(message.Text))
        {
            return;
        }

        CheckAllowedUsers(message.From);
    }

    private void CheckAllowedUsers(User? user)
    {
        if (user is null || _settings.AllowedUsers.Contains(user.Id) == false)
        {
            throw new UnauthorizedAccessException($"User '{user}' is not authorized.");
        }
    }
}
