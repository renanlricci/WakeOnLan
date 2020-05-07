using System.Net;

namespace WakeOnLan.CrossCutting.Exceptions
{
    public abstract class CustomException : System.Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected CustomException(
            HttpStatusCode statusCode,
            string message,
            System.Exception exception) : base(message, exception)
        {
            StatusCode = statusCode;
        }
    }
}
