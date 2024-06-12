using System.Net.Mail;
using Xeptions;

namespace VidSphere.Core.Api.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataDependencyValidationException : Xeption
    {
        public VideoMetadataDependencyValidationException(string message, Xeption innerException)
            :base(message, innerException)
        { }
    }
}
