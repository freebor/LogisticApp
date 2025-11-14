namespace LogisticAppManagement.Common
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;


        public static ApiResponse<T> SuccessfulResponse(T? data, string message= "Operation successful")
        {
            return new ApiResponse<T>
            {
                Message = message,
                Data = data,
                Errors = new List<string>(),
                Success = true
            };
        }

        public static ApiResponse<T> FailureResponse(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors ?? new List<string>()
            };
        }

        public static ApiResponse<T> FailureResponse(string message, string error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = new List<string> { error }
            };
        }
    }
}
