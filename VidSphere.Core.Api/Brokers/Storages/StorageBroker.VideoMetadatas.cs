// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<VideoMetadata> VideoMetadatas { get; set; }

        public async ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await InsertAsync<VideoMetadata>(videoMetadata);

        public IQueryable<VideoMetadata> SelectAllVideoMetadatas() =>
            SelectAll<VideoMetadata>();

        public async ValueTask<VideoMetadata> SelectVideoMetadataByIdAsync(Guid videoMetadataId) =>
            await SelectAsync<VideoMetadata>(videoMetadataId);

        public async ValueTask<VideoMetadata> UpdateVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await UpdateAsync<VideoMetadata>(videoMetadata);

        public async ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await DeleteAsync<VideoMetadata>(videoMetadata);
    }
}