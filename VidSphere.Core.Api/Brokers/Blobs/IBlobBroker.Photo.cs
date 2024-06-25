namespace VidSphere.Core.Api.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<string> UploadPhotoAsync(Stream fileStream, string fileName, string contentType);
    }
}
