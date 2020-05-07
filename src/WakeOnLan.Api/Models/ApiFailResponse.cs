using System;

namespace WakeOnLan.Api.Models
{
    public sealed class ApiFailResponse : ApiResponse
    {
        public string Message { get; }

        public string StackTrace { get; }

        public ApiFailResponse(
            string message,
            string stackTrace) : base(null, false)
        {
            Message = message;
            StackTrace = stackTrace;
        }
        public ApiFailResponse(Exception exception) : base(null, false)
        {
            Message = exception.Message;
            StackTrace = exception.StackTrace;
        }
    }
}
