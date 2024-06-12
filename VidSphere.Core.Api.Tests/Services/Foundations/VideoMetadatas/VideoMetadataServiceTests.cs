// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Microsoft.Data.SqlClient;
using Moq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Tynamix.ObjectFiller;
using VidSphere.Core.Api.Brokers.DateTimes;
using VidSphere.Core.Api.Brokers.Loggings;
using VidSphere.Core.Api.Brokers.Storages;
using VidSphere.Core.Api.Models.VideoMetadatas;
using VidSphere.Core.Api.Services.Foundations.VideoMetadatas;
using Xeptions;

namespace VidSphere.Core.Api.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<IDateTimebroker> dateTimeBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimebroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.videoMetadataService =
                new VideoMetadataService(
                    storageBroker: storageBrokerMock.Object,
                    dateTimeBroker: dateTimeBrokerMock.Object,
                    loggingBroker: loggingBrokerMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue().ToString();

        private static int GetRandomNumber() =>
        new IntRange(min: 2, max: 10).GetValue();
        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetUninitializedObject(typeof(SqlException));

        private Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expecteedException) =>
            actualException => actualException.SameExceptionAs(expecteedException);

        private static VideoMetadata CreateRandomVideoMetadata() =>
            CreateVideoMetadataFiller(date: GetRandomDateTimeOffset()).Create();

        private static IQueryable<VideoMetadata> CreateRandomVideoMetadatas()
        {
            return CreateVideoMetadataFiller(date: GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static VideoMetadata CreateRandomVideoMetadata(DateTimeOffset dates) =>
                CreateVideoMetadataFiller(date: dates).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<VideoMetadata> CreateVideoMetadataFiller(DateTimeOffset date)
        {
            var filler = new Filler<VideoMetadata>();
            filler.Setup().OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
