using Microsoft.Extensions.Logging;
using SnapTgBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace SnapTgBot.Messaging;

internal sealed class TelegramUpdateHandler : IUpdateHandler
{
    private readonly ITelegramMessageHandler _messageHandler;
    private readonly ILogger _logger;

    public TelegramUpdateHandler(ITelegramMessageHandler messageHandler, ILogger<TelegramUpdateHandler> logger)
    {
        _messageHandler = messageHandler;
        _logger = logger;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is { } message)
        {
            await _messageHandler.Handle(message);
        }

    }

    public Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception has occurred during telegram polling");
        client.StartReceivingFromScratch(this);
        return Task.CompletedTask;
    }
}
