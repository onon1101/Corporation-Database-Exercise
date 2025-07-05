public class Result<T>
{
    public T? Ok { get; }
    public string? Error { get; }
    public bool IsSuccess => Error == null;

    private Result(T? ok, string? error)
    {
        Ok = ok;
        Error = error;
    }

    public static Result<T> Success(T value) => new Result<T>(value, null);
    public static Result<T> Fail(string error) => new Result<T>(default, error);
}