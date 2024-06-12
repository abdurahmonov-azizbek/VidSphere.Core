// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class NullVideoMetadataException : Xeption
    {
        public NullVideoMetadataException(string message)
            :base(message)
        { }
    }
}
