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
