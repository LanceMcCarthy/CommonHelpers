using System;
using CommonHelpers.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Structs;

[TestClass]
public class EitherTests
{

    [TestMethod]
    public void CauseErrorFalse()
    {
        const bool causeError = false;
        var result = TryGetData(causeError);
        Assert.IsTrue(result.IsOk);
        Assert.IsTrue(result.Value);
    }

    [TestMethod]
    public void CauseErrorTrue()
    {
        const bool causeError = true;
        var result = TryGetData(causeError);
        Assert.IsFalse(result.IsOk);
        Assert.IsInstanceOfType<InvalidOperationException>(result.Error);
    }

    private static Either<bool, Exception> TryGetData(bool causeError)
    {
        try
        {
            return causeError ? throw new InvalidOperationException() : true;
        }
        catch (Exception e)
        {
            return e;
        }

    }
}