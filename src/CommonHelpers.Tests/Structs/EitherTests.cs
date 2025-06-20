using System;
using CommonHelpers.Structs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Structs
{
    [TestClass]
    public class EitherTests
    {

        [TestMethod]
        public void CauseErrorFalse()
        {
            // Arrange
            var causeError = false;

            // Act
            var result = TryGetData(causeError);

            // Assert
            Assert.IsTrue(result.IsOk);
            Assert.AreEqual(true, result.Value);
        }

        [TestMethod]
        public void CauseErrorTrue()
        {
            // Arrange
            bool causeError = true;

            // Act
            var result = TryGetData(causeError);

            // Assert
            Assert.IsFalse(result.IsOk);
            Assert.IsInstanceOfType<InvalidOperationException>(result.Error);
        }

        private static Either<bool, Exception> TryGetData(bool causeError)
        {
            try
            {
                if (causeError)
                {
                    throw new InvalidOperationException();
                }
                return true;
            }
            catch (Exception e)
            {
                return e;
            }

        }
    }
}
