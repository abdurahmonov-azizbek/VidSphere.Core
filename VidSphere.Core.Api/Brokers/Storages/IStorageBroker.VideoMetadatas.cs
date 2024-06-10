// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata);
        IQueryable<VideoMetadata> SelectAllVideoMetadatas();
        ValueTask<VideoMetadata> SelectVideoMetadataByIdAsync(Guid videoMetadataId);
    }
}
