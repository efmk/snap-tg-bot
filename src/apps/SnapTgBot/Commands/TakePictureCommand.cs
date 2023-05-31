using SnapTgBot.WebCam;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace SnapTgBot.Commands;

internal sealed class TakePictureCommand : ITelegramCommand
{
    private readonly ITelegramBotClient _client;
    private readonly IWebCamService _webCamService;

    public TakePictureCommand(
        ITelegramBotClient client,
        IWebCamService webCamService)
    {
        _client = client;
        _webCamService = webCamService;
    }

    public string Name => @"/takepicture";

    public async Task Execute(Message message)
    {
        var picture = _webCamService.TakePicture();
        var fileName = $"Picture_{DateTime.Now:O}";
        var fileStream = new InputFileStream(new MemoryStream(picture), fileName);
        await _client.SendPhotoAsync(message.Chat, fileStream);
    }
}
