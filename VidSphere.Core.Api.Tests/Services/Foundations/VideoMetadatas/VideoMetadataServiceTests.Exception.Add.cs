﻿using FluentAssertions;
using Microsoft.Data.SqlClient;
using Moq;
using STX.EFxceptions.Abstractions.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VidSphere.Core.Api.Models.VideoMetadatas;
using VidSphere.Core.Api.Models.VideoMetadatas.Exceptions;

namespace VidSphere.Core.Api.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            SqlException sqlException = GetSqlException();

            var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    "Failed Video Metadata storage error occured, please contact support.",
                        sqlException);

            var expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    "Video Metadata dependency exception error occured, please contact support.",
                        failedVideoMetadataStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset()).Throws(sqlException);

            //when
            ValueTask<VideoMetadata> AddVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                await Assert.ThrowsAsync<VideoMetadataDependencyException>(AddVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyException.Should().BeEquivalentTo(expectedVideoMetadataDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(expectedVideoMetadataDependencyException))),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowExceptionAddIfDublicateKeyErrorOccurs()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            string someString = GetRandomString();

            var duplicateKeyException = new DuplicateKeyException(someString);

            var alreadyExistVideoMetadataException =
                new AlreadyExistsVideoMetadataException(
                    "Video Metadata already exist, please try again.",
                        duplicateKeyException);

            var expectedVideoMetadataDependencyValidationException
                = new VideoMetadataDependencyValidationException(
                    "Video Metadata dependency error occured. Fix errors and try again.",
                        alreadyExistVideoMetadataException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset()).Throws(duplicateKeyException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataDependencyValidationException actualVideoMetadataDependencyValidationException =
                await Assert.ThrowsAnyAsync<VideoMetadataDependencyValidationException>(addVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyValidationException))),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
