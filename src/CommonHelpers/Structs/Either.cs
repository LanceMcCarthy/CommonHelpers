using System;

namespace CommonHelpers.Structs;

public readonly struct Either<TSuccess, TError>
{
    private readonly bool _success;

    private Either(TSuccess value, TError error, bool success)
    {
        Value = value;
        Error = error;
        _success = success;
    }

    public readonly TSuccess Value;

    public readonly TError Error;

    public bool IsOk => _success;

    public static Either<TSuccess, TError> Ok(TSuccess value) => new(value, default!, true);

    public static Either<TSuccess, TError> Err(TError error) => new(default!, error, false);

    public static implicit operator Either<TSuccess, TError>(TSuccess value) => new(value, default!, true);

    public static implicit operator Either<TSuccess, TError>(TError error) => new(default!, error, false);

    public TResult Match<TResult>(Func<TSuccess, TResult> success, Func<TError, TResult> failure) => _success ? success(Value) : failure(Error);

    public void Match(Action<TSuccess> success, Action<TError> failure) 
    {
        if (_success)
        {
            success(Value);
        }
        else
        {
            failure(Error);
        }
    }
}
