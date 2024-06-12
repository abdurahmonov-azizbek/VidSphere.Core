// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class LockedVideoMetadataException : Xeption
    {
        public LockedVideoMetadataException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
