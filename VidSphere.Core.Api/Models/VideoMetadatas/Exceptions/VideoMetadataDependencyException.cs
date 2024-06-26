﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataDependencyException :Xeption
    {
        public VideoMetadataDependencyException(string message, Xeption innerException)
            :base(message, innerException)
        { }
    }
}
