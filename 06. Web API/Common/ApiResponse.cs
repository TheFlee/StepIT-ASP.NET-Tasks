namespace _06._Web_API.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    //public long? ExecutionTimesMs { get; set; }

    public static ApiResponse<T> SuccessResponse
    (T? data, string message = "Operation executed successfully")
    {
        return new()
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

}
