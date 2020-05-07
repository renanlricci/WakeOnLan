using System.Net;

namespace WakeOnLan.CrossCutting.Exceptions
{
    public sealed class UserNotAuthenticatedException : CustomException
    {
        public UserNotAuthenticatedException() : base(
            HttpStatusCode.Unauthorized,
            "User not authenticated.",
            null)
        { }
    }
}
