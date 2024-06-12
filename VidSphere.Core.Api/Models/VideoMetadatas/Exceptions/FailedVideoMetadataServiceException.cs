// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class FailedVideoMetadataServiceException : Xeption
    {
        public FailedVideoMetadataServiceException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
