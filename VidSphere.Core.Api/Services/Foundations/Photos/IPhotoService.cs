namespace VidSphere.Core.Api.Services.Foundations.Photos
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(Stream fileStream, string fileName, string contentType);
    }
}
