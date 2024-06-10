// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by VidSphere Team
// --------------------------------------------------------

using System;

namespace VidSphere.Core.Api.Brokers.DateTimes
{
    public interface IDateTimebroker
    {
        DateTimeOffset GetCurrentDateTimeOffset();
    }
}
