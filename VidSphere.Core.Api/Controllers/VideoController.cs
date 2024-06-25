using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VidSphere.Core.Api.Models.Videos.Exceptions;
using VidSphere.Core.Api.Models.Videos;
using VidSphere.Core.Api.Services.Foundations.Videos;

namespace VidSphere.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : RESTFulController
    {
        private readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if (file == null || !ValidateVideo(file))
            {
                throw new InvalidVideoExceptions("Video is not valid format.");
            }

            using var stream = file.OpenReadStream();
            var blobUri = await videoService.AddVideoAsync(stream, file.FileName, file.ContentType);

            var video = new Video
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                BlobUri = blobUri,
                UploadedDate = DateTime.UtcNow
            };

            return Ok(video);
        }

        [HttpGet("video/{fileName}")]
        public async Task<IActionResult> GetVideo(string fileName)
        {
            var stream = await this.videoService.GetVideoStreamAsync(fileName);

            if (stream == null)
            {
                return NotFound();
            }

            string contentType = fileName.EndsWith(".mp4") ? "video/mp4" :
                                 fileName.EndsWith(".avi") ? "video/x-msvideo" :
                                 "application/octet-stream";

            return File(stream, contentType);
        }
        private bool ValidateVideo(IFormFile file)
        {
            var allowedExtensions = new[] { ".mp4", ".avi", ".mov" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return file.Length > 0 && file.Length <= 50 * 1024 * 1024 && allowedExtensions.Contains(extension);
        }
    }
}
