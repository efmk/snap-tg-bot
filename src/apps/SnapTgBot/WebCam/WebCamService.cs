using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using SnapTgBot.Settings;

namespace SnapTgBot.WebCam;

internal sealed class WebCamService : IWebCamService
{
    private static readonly object Lock = new();

    private readonly WebCamSettings _settings;

    public WebCamService(WebCamSettings settings)
    {
        _settings = settings;
    }

    public byte[] TakePicture()
    {
        lock (Lock)
        {
            using var videoCapture = CreateVideoCapture();
            return videoCapture.QueryFrame().ToImage<Bgr, byte>().ToJpegData();
        }
    }

    private VideoCapture CreateVideoCapture()
    {
        var capture = new VideoCapture(camIndex: 0, GetApi());

        capture.Set(CapProp.FrameWidth, _settings.ImageWidth);
        capture.Set(CapProp.FrameHeight, _settings.ImageHeight);

        return capture;
    }

    private static VideoCapture.API GetApi() =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? VideoCapture.API.DShow
            : VideoCapture.API.Any;
}
