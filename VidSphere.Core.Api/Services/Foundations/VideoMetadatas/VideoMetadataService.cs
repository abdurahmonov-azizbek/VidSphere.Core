// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using VidSphere.Core.Api.Brokers.DateTimes;
using VidSphere.Core.Api.Brokers.Loggings;
using VidSphere.Core.Api.Brokers.Storages;
using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api
{
    public class VideoMetadataService : IVideoMetadataService
    {
        private readonly IStorageBroker storageBroker;

        public VideoMetadataService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await this.storageBroker.InsertVideoMetadataAsync(videoMetadata);
    }
}
