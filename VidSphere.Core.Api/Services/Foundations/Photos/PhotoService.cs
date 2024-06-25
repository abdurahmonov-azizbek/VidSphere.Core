using VidSphere.Core.Api.Brokers.Blobs;
using VidSphere.Core.Api.Brokers.DateTimes;
using VidSphere.Core.Api.Brokers.Loggings;

namespace VidSphere.Core.Api.Services.Foundations.Photos
{
    public class PhotoService : IPhotoService
    {
        private readonly IBlobBroker blobBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimebroker dateTimeBroker;

        public PhotoService(
            IBlobBroker blobBroker,
            ILoggingBroker loggingBroker,
            IDateTimebroker dateTimeBroker)
        {
            this.blobBroker = blobBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async Task<string> AddPhotoAsync(Stream fileStream, string fileName, string contentType) =>
            await this.blobBroker.UploadPhotoAsync(fileStream, fileName, contentType);
    }
}
