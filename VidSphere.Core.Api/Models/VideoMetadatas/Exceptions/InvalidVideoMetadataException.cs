// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class InvalidVideoMetadataException : Xeption
    {
        public InvalidVideoMetadataException(string message)
            : base(message)
        { }
    }
}
