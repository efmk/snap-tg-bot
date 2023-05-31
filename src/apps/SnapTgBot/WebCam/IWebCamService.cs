namespace SnapTgBot.WebCam;

/// <summary>
/// Represents the web cam service interface.
/// </summary>
public interface IWebCamService
{
    /// <summary>
    /// Takes a picture using specified settings.
    /// </summary>
    /// <returns>Image byte array</returns>
    byte[] TakePicture();
}
