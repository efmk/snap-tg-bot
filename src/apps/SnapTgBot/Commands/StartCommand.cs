using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SnapTgBot.Commands;

internal sealed class StartCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _client;

    public StartCommand(ITelegramBotClient client)
    {
        _client = client;
    }

    public string Name => @"/start";

    public async Task Execute(Message message)
    {
        var text = BuildMessageText(message);
        await _client.SendTextMessageAsync(message.Chat, text, parseMode: ParseMode.Html);
    }

    private static string BuildMessageText(Message message)
    {
        var user = message.From;
        var sb = new StringBuilder();
        sb.AppendLine("Welcome to snap telegram bot!");
        sb.AppendLine($"You are <i>{user?.FirstName} {user?.LastName}</i>. Your ID is <b>{user?.Id}</b>.");
        return sb.ToString();
    }
}
