using VidSphere.Core.Api.Brokers.Blobs;
using VidSphere.Core.Api.Brokers.DateTimes;
using VidSphere.Core.Api.Brokers.Loggings;
using VidSphere.Core.Api.Models.Videos;

namespace VidSphere.Core.Api.Services.Foundations.Videos
{
    public class VideoService : IVideoService
    {
        private readonly IBlobBroker blobBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimebroker dateTimeBroker;

        public VideoService(
            IBlobBroker blobBroker,
            ILoggingBroker loggingBroker,
            IDateTimebroker dateTimeBroker)
        {
            this.blobBroker = blobBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async Task<string> AddVideoAsync(Stream fileStream, string fileName, string contentType) =>
            await this.blobBroker.UploadVideoAsync(fileStream, fileName, contentType);

        public async Task<Stream> GetVideoStreamAsync(string fileName) =>
            await this.blobBroker.GetStreamAsync(fileName);
    }
}
