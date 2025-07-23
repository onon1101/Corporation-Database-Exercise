namespace Api.Utils;

public class Result<T>
{
    public T? Payload { get; }

    public ErrorStatusCode? StatusCode { get; }
    public string? Error { get; }
    public bool IsSuccess => Error == null;

    private Result(T? ok, string? error, ErrorStatusCode? status)
    {
        Payload = ok;
        Error = error;
        StatusCode = status;
    }

    public static Result<T> Success(T value) => new Result<T>(value, null, 0);
    public static Result<T> Fail(string error, ErrorStatusCode statusCode) => new Result<T>(default, error, statusCode);
}