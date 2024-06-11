// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Moq;
using Tynamix.ObjectFiller;
using VidSphere.Core.Api.Brokers.DateTimes;
using VidSphere.Core.Api.Brokers.Loggings;
using VidSphere.Core.Api.Brokers.Storages;
using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.videoMetadataService = new VideoMetadataService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static VideoMetadata CreateRandomVideoMetadata(DateTimeOffset dates) =>
            CreateVideoMetadataFiller(dates).Create();

        private static VideoMetadata CreateRandomVideoMetadata() =>
            CreateVideoMetadataFiller(GetRandomDateTimeOffset()).Create();

        private static Filler<VideoMetadata> CreateVideoMetadataFiller(DateTimeOffset dates)
        {
            var filler = new Filler<VideoMetadata>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }
    }
}
