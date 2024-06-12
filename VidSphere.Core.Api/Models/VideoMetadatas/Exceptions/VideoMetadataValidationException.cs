// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataValidationException : Xeption
    {
        public VideoMetadataValidationException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
