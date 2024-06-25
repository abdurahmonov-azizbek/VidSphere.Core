namespace VidSphere.Core.Api.Brokers.Blobs
{
    public partial class BlobBroker
    {
        public async Task<string> UploadPhotoAsync(Stream fileStream, string fileName, string contentType) =>
            await UploadAsync(fileStream, fileName, contentType);
    }
}
