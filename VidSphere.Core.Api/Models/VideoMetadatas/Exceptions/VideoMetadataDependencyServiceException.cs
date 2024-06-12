// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataDependencyServiceException : Xeption
    {
        public VideoMetadataDependencyServiceException(string message, Xeption innerException)
            :base(message, innerException)
        { }
    }
}
