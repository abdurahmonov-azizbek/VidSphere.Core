namespace VidSphere.Core.Api.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<string> UploadVideoAsync(Stream fileStream, string fileName, string contentType);
        Task<IEnumerable<string>> GetAllVideoFilesAsync();
        Task<Stream> GetStreamAsync(string fileName);
    }
}
