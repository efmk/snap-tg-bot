using Microsoft.Extensions.Logging;
using SnapTgBot.Commands;
using SnapTgBot.Utils;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace SnapTgBot.Startup;

internal sealed class ConfigureTelegramBot : IStartupTask
{
    private readonly ITelegramBotClient _client;
    private readonly IUpdateHandler _updateHandler;
    private readonly IReadOnlyCollection<string> _knownCommands;
    private readonly ILogger _logger;

    public ConfigureTelegramBot(
        ITelegramBotClient client,
        IUpdateHandler updateHandler,
        IEnumerable<ITelegramCommand> commands,
        ILogger<ConfigureTelegramBot> logger)
    {
        _client = client;
        _updateHandler = updateHandler;
        _knownCommands = commands.Select(c => c.Name).ToList();
        _logger = logger;
    }

    public async Task Run()
    {
        _client.StartReceivingFromScratch(_updateHandler);

        await AssertTokenIsValid();
        await InitializeCommands();

        var botInfo = await _client.GetMeAsync();
        _logger.LogInformation("Telegram bot has been initialized: {Username}", botInfo.Username);
    }

    private async Task AssertTokenIsValid()
    {
        var isTokenValid = await _client.TestApiAsync();
        Guard.Assert(isTokenValid, "Telegram token must be valid");
    }

    private async Task InitializeCommands()
    {
        await _client.SetChatMenuButtonAsync(menuButton: new MenuButtonCommands());

        var commands = _knownCommands.Select(c => new BotCommand { Command = c, Description = c });
        await _client.SetMyCommandsAsync(commands);
    }
}
