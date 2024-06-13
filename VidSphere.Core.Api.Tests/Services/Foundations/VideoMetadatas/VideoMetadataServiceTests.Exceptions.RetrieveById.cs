// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using VidSphere.Core.Api.Models.VideoMetadatas;
using VidSphere.Core.Api.Models.VideoMetadatas.Exceptions;

namespace VidSphere.Core.Api.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfSqlErrorOccursAndLogItAsync()
        {
            //given
            Guid someId = Guid.NewGuid();
            SqlException sqlException = GetSqlException();

            var failedVideoMetadataStorageException = new FailedVideoMetadataStorageException(
                message: "Failed Video Metadata storage error occured, please contact support.",
                innerException: sqlException);

            var expectedVideoMetadataDependencyException = new VideoMetadataDependencyException(
                message: "Video Metadata dependency exception error occured, please contact support.",
                innerException: failedVideoMetadataStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<VideoMetadata> retrieveVideoMetadataByIdTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(someId);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                await Assert.ThrowsAsync<VideoMetadataDependencyException>(
                    retrieveVideoMetadataByIdTask.AsTask);

            //then
            actualVideoMetadataDependencyException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedVideoMetadataDependencyException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
