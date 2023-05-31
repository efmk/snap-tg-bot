using Telegram.Bot.Types;

namespace SnapTgBot.Commands;

internal interface ITelegramCommand
{
    string Name { get; }

    Task Execute(Message message);
}
