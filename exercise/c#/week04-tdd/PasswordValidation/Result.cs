namespace PasswordValidation;
public sealed class Result<T>
{
    public IEnumerable<string?> Errors { get; }
    public bool IsSuccess { get; }

    private Result(bool isSuccess, IEnumerable<string?> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result<T> Failure(IEnumerable<string?> errors) => new(false, errors);

    public static Result<T> Success() => new(true, []);
}