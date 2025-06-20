using System;

namespace CommonHelpers.Structs;

public static class StructUtilities
{
    public static Func<Either<TValue, TFailure>, Either<TSuccess, TFailure>> Bind<TValue, TSuccess, TFailure>(Func<TValue, Either<TSuccess, TFailure>> map)
    {
        return input =>
        {
            return input.IsOk switch
            {
                true => map(input.Value),
                false => Either<TSuccess, TFailure>.Err(input.Error)
            };
        };
    }

    public static Either<TSuccess, TFailure> Then<TValue, TSuccess, TFailure>(this Either<TValue, TFailure> instance, Func<TValue, Either<TSuccess, TFailure>> map) => Bind(map)(instance);
}