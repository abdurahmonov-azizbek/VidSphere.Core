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
        private readonly IDateTimebroker dateTimebroker;
        private readonly ILoggingBroker loggingBroker;

        public VideoMetadataService(
            IStorageBroker storageBroker,
            IDateTimebroker dateTimebroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.dateTimebroker = dateTimebroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata) =>
            throw new Exception();
    }
}
