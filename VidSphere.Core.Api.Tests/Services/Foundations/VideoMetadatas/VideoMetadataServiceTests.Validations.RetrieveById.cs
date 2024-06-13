// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using FluentAssertions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Moq;
using VidSphere.Core.Api.Models.VideoMetadatas;
using VidSphere.Core.Api.Models.VideoMetadatas.Exceptions;

namespace VidSphere.Core.Api.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            //given
            var invalidVideoMetadataId = Guid.Empty;
            var invalidVideoMetadataException = new InvalidVideoMetadataException(
                message: "Video Metadata is invalid.");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Id),
                values: "Id is required.");

            var expectedVideoMetadataValidationException = new VideoMetadataValidationException(
                message: "Video Metadata Validation Exception occured, fix the errors and try again.",
                innerException: invalidVideoMetadataException);

            //when
            ValueTask<VideoMetadata> retrieveVideoMetadataByIdTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(invalidVideoMetadataId);

            VideoMetadataValidationException actuallVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    retrieveVideoMetadataByIdTask.AsTask);

            //then
            actuallVideoMetadataValidationException.Should().BeEquivalentTo(expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataValidationException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()), Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfVideoMetadataIsNotFoundAndLogItAsync()
        {
            Guid someVideoMetadataId = Guid.NewGuid();
            VideoMetadata noVideoMetadata = null;

            var notFoundVideoMetadataException = new NotFoundVideoMetadataException(
                message: $"Couldn't find job with id: {someVideoMetadataId}.");

            var expectedVideoMetadataValidationException = new VideoMetadataValidationException(
                message: "Video Metadata Validation Exception occured, fix the errors and try again.",
                innerException: notFoundVideoMetadataException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noVideoMetadata);

            //when 
            ValueTask<VideoMetadata> retrieveVideoMetadataByIdTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(someVideoMetadataId);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    retrieveVideoMetadataByIdTask.AsTask);

            //then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(expectedVideoMetadataValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataValidationException))), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
