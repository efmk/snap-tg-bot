using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SnapTgBot.Utils;

internal static class TelegramExtensions
{
    public static bool IsCommand(this Message message) => message.Text?.StartsWith("/") == true;

    public static void StartReceivingFromScratch(this ITelegramBotClient client, IUpdateHandler updateHandler) =>
        client.StartReceiving(
            updateHandler,
            new ReceiverOptions
            {
                ThrowPendingUpdates = true,
                AllowedUpdates = new[]
                {
                    UpdateType.Message
                }
            });
}
