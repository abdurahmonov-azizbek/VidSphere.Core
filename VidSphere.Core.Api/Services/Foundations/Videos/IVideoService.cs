using VidSphere.Core.Api.Models.Videos;

namespace VidSphere.Core.Api.Services.Foundations.Videos
{
    public interface IVideoService
    {
        Task<string> AddVideoAsync(Stream fileStream, string fileName, string contentType);
        Task<Stream> GetVideoStreamAsync(string fileName);
    }
}
