﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class AlreadyExistsVideoMetadataException : Xeption
    {
        public AlreadyExistsVideoMetadataException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
