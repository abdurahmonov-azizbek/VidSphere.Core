using Xeptions;

namespace VidSphere.Core.Api.Models.Photos.Exceptions
{
    public class InvalidPhotoExceptions : Xeption
    {
        public InvalidPhotoExceptions(string message)
            :base(message)
        { }
    }
}
