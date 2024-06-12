using FluentAssertions;
using Moq;
// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using VidSphere.Core.Api.Models.VideoMetadatas;

namespace VidSphere.Core.Api.Tests.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public void ShouldReturnVideoMetadatas()
        {
            //given
            IQueryable<VideoMetadata> randomVideoMetadatas = CreateRandomVideoMetadatas();
            IQueryable<VideoMetadata> storageVideoMetadatas = randomVideoMetadatas;
            IQueryable<VideoMetadata> expectedVideoMetadatas = storageVideoMetadatas;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllVideoMetadatas()).Returns(storageVideoMetadatas);

            //when
            IQueryable<VideoMetadata> actualVideoMetadatas =
                this.videoMetadataService.RetrieveAllVideoMetadatas();

            //then
            actualVideoMetadatas.Should().BeEquivalentTo(expectedVideoMetadatas);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllVideoMetadatas(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
