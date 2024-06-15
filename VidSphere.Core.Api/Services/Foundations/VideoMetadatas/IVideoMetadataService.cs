// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Services.Foundations.VideoMetadatas
{
    public interface IVideoMetadataService
    {
        ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata); 
        IQueryable<VideoMetadata> RetrieveAllVideoMetadatas();
        ValueTask<VideoMetadata> RetrieveVideoMetadataByIdAsync(Guid videoMetadataId);
        ValueTask<VideoMetadata> ModifyVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}
