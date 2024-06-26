﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Moq;
using VidSphere.Core.Api.Models.VideoMetadatas.Exceptions;
using VidSphere.Core.Api.Models.VideoMetadatas;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace VidSphere.Core.Api.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRemoveByIdIfSqlErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            var sqlException = GetSqlException();

            var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    message: "Failed Video Metadata storage error occured, please contact support.",
                    innerException: sqlException);

            var expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    message: "Video Metadata dependency exception error occured, please contact support.",
                    innerException: failedVideoMetadataStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            // when
            ValueTask<VideoMetadata> removeVideoMetadataTask =
                this.videoMetadataService.RemoveVideoMetadataByIdAsync(someId);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                await Assert.ThrowsAsync<VideoMetadataDependencyException>(
                    removeVideoMetadataTask.AsTask);

            // then
            actualVideoMetadataDependencyException.Should().BeEquivalentTo(
                expectedVideoMetadataDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedVideoMetadataDependencyException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnRemoveByIdIfDatabaseUpdateConcurrencyErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedVideoMetadataException =
                new LockedVideoMetadataException(
                    message: "Video Metadata is locked, please try again.",
                    innerException: dbUpdateConcurrencyException);

            var expectedVideoMetadataDependencyValidationException =
                new VideoMetadataDependencyValidationException(
                    message: "Video Metadata dependency error occured. Fix errors and try again.",
                    innerException: lockedVideoMetadataException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(dbUpdateConcurrencyException);

            // when
            ValueTask<VideoMetadata> removeVideoMetadataTask =
                this.videoMetadataService.RemoveVideoMetadataByIdAsync(someId);

            VideoMetadataDependencyValidationException actualVideoMetadataDependencyValidationException =
                await Assert.ThrowsAsync<VideoMetadataDependencyValidationException>(
                    removeVideoMetadataTask.AsTask);

            // then
            actualVideoMetadataDependencyValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataDependencyValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataDependencyValidationException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRemoveByIdIfServiceErrorOccursAndLogItAsync()
        {
            // given
            Guid someId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedVideoMetadataServiceException =
                new FailedVideoMetadataServiceException(
                    message: "Unexpected error of Video Metadata occured",
                    innerException: serviceException);

            var expectedVideoMetadataDependencyServiceException =
                new VideoMetadataDependencyServiceException(
                    message: "Unexpected service error occured. Contact support.",
                    innerException: failedVideoMetadataServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            // when
            ValueTask<VideoMetadata> removeVideoMetadataTask =
                this.videoMetadataService.RemoveVideoMetadataByIdAsync(someId);

            VideoMetadataDependencyServiceException actualVideoMetadataDependencyServiceException =
                await Assert.ThrowsAsync<VideoMetadataDependencyServiceException>(
                    removeVideoMetadataTask.AsTask);

            // then
            actualVideoMetadataDependencyServiceException.Should().BeEquivalentTo(
                expectedVideoMetadataDependencyServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataDependencyServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
