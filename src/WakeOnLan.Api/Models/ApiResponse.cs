namespace WakeOnLan.Api.Models
{
    public class ApiResponse
    {
        public object Data { get; }

        public bool Success { get; }

        public ApiResponse(object data)
        {
            Data = data;
            Success = true;
        }

        protected ApiResponse(
            object data,
            bool success)
        {
            Data = data;
            Success = success;
        }
    }
}
