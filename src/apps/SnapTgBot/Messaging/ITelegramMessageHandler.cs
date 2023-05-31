using Telegram.Bot.Types;

namespace SnapTgBot.Messaging;

internal interface ITelegramMessageHandler
{
    Task Handle(Message message);
}
