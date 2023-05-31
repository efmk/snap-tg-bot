using SnapTgBot.Utils;
using Microsoft.Extensions.Logging;
using SnapTgBot.Commands;
using SnapTgBot.Security;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SnapTgBot.Messaging;

internal sealed class TelegramMessageHandler : ITelegramMessageHandler
{
    private readonly ISecurityGuard _securityGuard;
    private readonly ITelegramBotClient _client;
    private readonly IReadOnlyDictionary<string, ITelegramCommand> _commands;
    private readonly ILogger _logger;

    public TelegramMessageHandler(
        ISecurityGuard securityGuard,
        ITelegramBotClient client,
        IEnumerable<ITelegramCommand> commands,
        ILogger<TelegramMessageHandler> logger)
    {
        _securityGuard = securityGuard;
        _client = client;
        _commands = commands.ToDictionary(c => c.Name);
        _logger = logger;
    }

    public async Task Handle(Message message)
    {
        try
        {
            _securityGuard.CheckSecurity(message);

            if (!message.IsCommand())
            {
                _logger.LogWarning(
                    $"Unrecognized message has been received. " +
                    $"User name: \"{message.From?.Username}\", Message: \"{message.Text}\"");
                return;
            }

            await HandleCommand(message);
        }
        catch (UnauthorizedAccessException)
        {
            _logger.LogWarning(
                $"Unknown user has messaged to the bot. " +
                $"User name: \"{message.From?.Username}\", Message: \"{message.Text}\"");

            await ReplyWithMessage(message, "You are unknown user, I won't speak with you.");
        }
    }

    private async Task HandleCommand(Message message)
    {
        if (!_commands.TryGetValue(message.Text ?? string.Empty, out var command))
        {
            await ReplyWithMessage(message, "Unknown command");
            return;
        }

        await command.Execute(message);
    }

    private async Task ReplyWithMessage(Message message, string text) =>
        await _client.SendTextMessageAsync(
            message.Chat,
            text,
            replyToMessageId: message.MessageId);
}
