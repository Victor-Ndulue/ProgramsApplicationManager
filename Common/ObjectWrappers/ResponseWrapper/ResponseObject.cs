namespace Common.ObjectWrappers.ResponseWrapper;

public class ResponseObject<T>
{
    public T? Data { get; init; }
    public int StatusCode { get; init; }
    public bool IsSuccessStatusCode { get; init; }
    public string? Message { get; init; }
    public DateTime DateTimeCreated { get; init; }
    public static ResponseObject<T> SuccessResponse(string? message = "Successful.", T? data = default, int statusCode = 200)
    {
        return new ResponseObject<T>()
        {
            StatusCode = statusCode,
            Data = data,
            IsSuccessStatusCode = true,
            Message = message,
            DateTimeCreated = DateTime.UtcNow
        };
    }
    public static ResponseObject<T> FailureResponse(string? message = "Request Failed", T? data = default, int statusCode = 400)
    {
        return new ResponseObject<T>()
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccessStatusCode = false,
            DateTimeCreated = DateTime.UtcNow,
            Message = message
        };
    }
    public static ResponseObject<T> ServerFailure(string? message = "Service temporary unavailable", T? data = default, int statusCode = 500)
    {
        return new ResponseObject<T>()
        {
            Data = data,
            StatusCode = statusCode,
            IsSuccessStatusCode = false,
            DateTimeCreated = DateTime.UtcNow,
            Message = message
        };
    }
}
