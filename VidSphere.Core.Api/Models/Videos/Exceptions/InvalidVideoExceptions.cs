using Xeptions;

namespace VidSphere.Core.Api.Models.Videos.Exceptions
{
    public class InvalidVideoExceptions : Xeption
    {
        public InvalidVideoExceptions(string message)
            :base(message)
        { }
    }
}
