﻿using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VidSphere.Core.Api.Models.Photos;
using VidSphere.Core.Api.Models.Photos.Exceptions;
using VidSphere.Core.Api.Services.Foundations.Photos;

namespace VidSphere.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : RESTFulController
    {
        private readonly IPhotoService photoService;

        public PhotoController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            if (file == null || !ValidatePhoto(file))
            {
                throw new InvalidPhotoExceptions("Photo is not valid format.");
            }

            using var stream = file.OpenReadStream();
            var blobUri = await photoService.AddPhotoAsync(stream, file.FileName, file.ContentType);

            var photo = new Photo
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                BlobUri = blobUri,
                UploadedDate = DateTime.UtcNow
            };

            return Ok(photo);
        }

        private bool ValidatePhoto(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return file.Length > 0 && file.Length <= 50 * 1024 * 1024 && allowedExtensions.Contains(extension);
        }
    }
}
