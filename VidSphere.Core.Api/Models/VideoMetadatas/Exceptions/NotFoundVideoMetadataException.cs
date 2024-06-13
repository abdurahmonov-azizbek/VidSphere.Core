// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class NotFoundVideoMetadataException : Xeption
    {
        public NotFoundVideoMetadataException(string message)
            : base(message: message)
        { }
    }
}
