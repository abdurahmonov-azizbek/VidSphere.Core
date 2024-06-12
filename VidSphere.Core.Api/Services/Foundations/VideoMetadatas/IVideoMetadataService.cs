using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Services.Foundations.VideoMetadatas
{
    public interface IVideoMetadataService
    {
        ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}
