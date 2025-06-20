using System;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Extensions
{
    [TestClass]
    public class EaseExtensionsTests
    {
        [TestMethod]
        public void BackEases()
        {
            // Arrange
            var inputValue = 0.23f;
            
            // Act
            var easeIn = inputValue.BackEaseIn();
            var easeOut = inputValue.BackEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Back easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Back easeOut value is not correct");
        }

        [TestMethod]
        public void BounceEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.BounceEaseIn();
            var easeOut = inputValue.BounceEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Bounce easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Bounce easeOut value is not correct");
        }

        [TestMethod]
        public void CubicEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.CubicEaseIn();
            var easeOut = inputValue.CubicEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Cubic easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Cubic easeOut value is not correct");
        }

        [TestMethod]
        public void CircularEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.CircularEaseIn();
            var easeOut = inputValue.CircularEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Circular easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Circular easeOut value is not correct");
        }

        [TestMethod]
        public void ElasticEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.ElasticEaseIn();
            var easeOut = inputValue.ElasticEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Elastic easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Elastic easeOut value is not correct");
        }

        [TestMethod]
        public void ExponentialEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.ExponentialEaseIn();
            var easeOut = inputValue.ExponentialEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Exponential easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Exponential easeOut value is not correct");
        }

        [TestMethod]
        public void LinearEase()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var ease = inputValue.Linear();

            // Assert
            Assert.IsTrue(Math.Abs(ease - inputValue) < 0.01, "The Linear ease value is not correct");
        }

        [TestMethod]
        public void QuadraticEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.QuadraticEaseIn();
            var easeOut = inputValue.QuadraticEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Quadratic easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Quadratic easeOut value is not correct");
        }

        [TestMethod]
        public void QuarticEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.QuarticEaseIn();
            var easeOut = inputValue.QuarticEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Quartic easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Quartic easeOut value is not correct");
        }

        [TestMethod]
        public void QuinticEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.QuinticEaseIn();
            var easeOut = inputValue.QuinticEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Quintic easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Quintic easeOut value is not correct");
        }

        [TestMethod]
        public void SineEases()
        {
            // Arrange
            var inputValue = 0.23f;

            // Act
            var easeIn = inputValue.SineEaseIn();
            var easeOut = inputValue.SineEaseOut();

            // Assert
            Assert.IsTrue(easeIn < inputValue, "The Sine easeIn value is not correct");
            Assert.IsTrue(easeOut > inputValue, "The Sine easeOut value is not correct");
        }
    }
}
