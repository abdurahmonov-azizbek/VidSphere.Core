﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using VidSphere.Core.Api.Models.VideoMetadatas.Exceptions;
using VidSphere.Core.Api.Models.VideoMetadatas;
using VidSphere.Core.Api.Services.Foundations.VideoMetadatas;

namespace VidSphere.Core.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoMetadataController : RESTFulController
    {
        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataController(IVideoMetadataService videoMetadataService)
        {
            this.videoMetadataService = videoMetadataService;
        }

        [HttpPost]
        public async ValueTask<ActionResult<VideoMetadata>> PostVideoMetadataAsync(VideoMetadata videoMetadata)
        {
            try
            {
                VideoMetadata postedVideoMetadata =
                await this.videoMetadataService.AddVideoMetadataAsync(videoMetadata);

                return Created(postedVideoMetadata);
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
            {
                return BadRequest(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataDependencyValidationException videoMetadataDependencyValidationException)
                when (videoMetadataDependencyValidationException.InnerException is
                    AlreadyExistsVideoMetadataException)
            {
                return Conflict(videoMetadataDependencyValidationException.InnerException);
            }
            catch (VideoMetadataDependencyException videoMetadataDependencyException)
            {
                return InternalServerError(videoMetadataDependencyException);
            }
            catch (VideoMetadataDependencyServiceException videoMetadataDependencyServiceException)
            {
                return InternalServerError(videoMetadataDependencyServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<VideoMetadata>> GetAllVideoMetadatas()
        {
            try
            {
                IQueryable<VideoMetadata> gettingAllVideoMetadatas =
                this.videoMetadataService.RetrieveAllVideoMetadatas();

                return Ok(gettingAllVideoMetadatas);
            }
            catch (VideoMetadataDependencyException videoMetadataDependencyException)
            {
                return InternalServerError(videoMetadataDependencyException);
            }
            catch (VideoMetadataDependencyServiceException videoMetadataDependencyServiceException)
            {
                return InternalServerError(videoMetadataDependencyServiceException);
            }
        }

        [HttpGet("{videoMetadataId}")]
        public async ValueTask<ActionResult<VideoMetadata>> GetVideoMetadataByIdasync(Guid videoMetadataId)
        {
            try
            {
                VideoMetadata videoMetadata =
                    await this.videoMetadataService.RetrieveVideoMetadataByIdAsync(videoMetadataId);

                return Ok(videoMetadata);
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
                when (videoMetadataValidationException.InnerException is NotFoundVideoMetadataException)
            {
                return NotFound(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
            {
                return BadRequest(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataDependencyException videoMetadataDependencyException)
            {
                return InternalServerError(videoMetadataDependencyException);
            }
            catch (VideoMetadataDependencyServiceException videoMetadataDependencyServiceException)
            {
                return InternalServerError(videoMetadataDependencyServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<VideoMetadata>> PutVideoMetadataAsync(VideoMetadata videoMetadata)
        {
            try
            {
                VideoMetadata modifyVideoMetadata =
                    await this.videoMetadataService.ModifyVideoMetadataAsync(videoMetadata);

                return Ok(modifyVideoMetadata);
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
                when (videoMetadataValidationException.InnerException is NotFoundVideoMetadataException)
            {
                return NotFound(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
            {
                return BadRequest(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataDependencyValidationException videoMetadataDependencyValidationException)
            {
                return Conflict(videoMetadataDependencyValidationException.InnerException);
            }
            catch (VideoMetadataDependencyException videoMetadataDependencyException)
            {
                return InternalServerError(videoMetadataDependencyException.InnerException);
            }
            catch (VideoMetadataDependencyServiceException videoMetadataDependencyServiceException)
            {
                return InternalServerError(videoMetadataDependencyServiceException.InnerException);
            }
        }

        [HttpDelete]
        public async ValueTask<ActionResult<VideoMetadata>> DeleteVideoMetadataById(Guid videoMetadataId)
        {
            try
            {
                VideoMetadata deletedVideoMetadata =
                    await this.videoMetadataService.RemoveVideoMetadataByIdAsync(videoMetadataId);

                return deletedVideoMetadata;
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
                when (videoMetadataValidationException.InnerException is NotFoundVideoMetadataException)
            {
                return NotFound(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataValidationException videoMetadataValidationException)
            {
                return BadRequest(videoMetadataValidationException.InnerException);
            }
            catch (VideoMetadataDependencyValidationException videoMetadataDependencyValidationException)
            {
                return Conflict(videoMetadataDependencyValidationException.InnerException);
            }
            catch (VideoMetadataDependencyException videoMetadataDependencyException)
            {
                return InternalServerError(videoMetadataDependencyException.InnerException);
            }
            catch (VideoMetadataDependencyServiceException videoMetadataDependencyServiceException)
            {
                return InternalServerError(videoMetadataDependencyServiceException.InnerException);
            }
        }
    }
}
