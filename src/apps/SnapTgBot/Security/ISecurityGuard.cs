using Telegram.Bot.Types;

namespace SnapTgBot.Security;

internal interface ISecurityGuard
{
    void CheckSecurity(Message message);
}
