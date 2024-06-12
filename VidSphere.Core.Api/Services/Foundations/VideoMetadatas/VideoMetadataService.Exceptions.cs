using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private delegate ValueTask<VideoMetadata> ReturningVideoMetadataFunction();

        private async ValueTask<VideoMetadata> TryCatch(ReturningVideoMetadataFunction returningVideoMetadataFunction)
        {
            return await returningVideoMetadataFunction();
        }
    }
}
