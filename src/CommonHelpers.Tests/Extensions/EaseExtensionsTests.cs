using System;
using CommonHelpers.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
        public void BackEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.BackEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.BackEaseOut(), 0.0001);
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
        public void BounceEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.BounceEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.BounceEaseOut(), 0.0001);
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
        public void CubicEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.CubicEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.CubicEaseOut(), 0.0001);
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
        public void CircularEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.CircularEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.CircularEaseOut(), 0.0001);
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
        public void ElasticEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.ElasticEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.ElasticEaseOut(), 0.0001);
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
        public void ExponentialEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.ExponentialEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.ExponentialEaseOut(), 0.0001);
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
        public void LinearEase_AtBounds()
        {
            Assert.AreEqual(0, 0f.Linear(), 0.0001);
            Assert.AreEqual(1, 1f.Linear(), 0.0001);
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
        public void QuadraticEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.QuadraticEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.QuadraticEaseOut(), 0.0001);
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
        public void QuarticEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.QuarticEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.QuarticEaseOut(), 0.0001);
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
        public void QuinticEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.QuinticEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.QuinticEaseOut(), 0.0001);
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

        [TestMethod]
        public void SineEases_AtBounds()
        {
            Assert.AreEqual(0, 0f.SineEaseIn(), 0.0001);
            Assert.AreEqual(1, 1f.SineEaseOut(), 0.0001);
        }

        [TestMethod]
        public void Eases_Monotonicity()
        {
            // Only test monotonicity for strictly monotonic ease-in/out functions
            var monotonicEaseInFuncs = new List<Func<float, float>>
            {
                x => x.CubicEaseIn(),
                x => x.CircularEaseIn(),
                x => x.ExponentialEaseIn(),
                x => x.QuadraticEaseIn(),
                x => x.QuarticEaseIn(),
                x => x.QuinticEaseIn(),
                x => x.SineEaseIn(),
                x => x.Linear()
            };
            var monotonicEaseOutFuncs = new List<Func<float, float>>
            {
                x => x.CubicEaseOut(),
                x => x.CircularEaseOut(),
                x => x.ExponentialEaseOut(),
                x => x.QuadraticEaseOut(),
                x => x.QuarticEaseOut(),
                x => x.QuinticEaseOut(),
                x => x.SineEaseOut(),
                x => x.Linear()
            };
            // EaseIn: should be non-decreasing
            foreach (var func in monotonicEaseInFuncs)
            {
                float prev = func(0f);
                for (float t = 0.05f; t <= 1f; t += 0.05f)
                {
                    float curr = func(t);
                    Assert.IsTrue(curr >= prev - 0.01, $"EaseIn not monotonic: {func.Method.Name}");
                    prev = curr;
                }
            }
            // EaseOut: should be non-decreasing
            foreach (var func in monotonicEaseOutFuncs)
            {
                float prev = func(0f);
                for (float t = 0.05f; t <= 1f; t += 0.05f)
                {
                    float curr = func(t);
                    Assert.IsTrue(curr >= prev - 0.01, $"EaseOut not monotonic: {func.Method.Name}");
                    prev = curr;
                }
            }
        }

        [TestMethod]
        public void Eases_OutOfRangeInputs()
        {
            // Test negative and >1 inputs
            var inputValues = new[] { -0.5f, 1.5f };
            // Functions that should always return a finite value (clamp or extrapolate)
            var safeFuncs = new List<Func<float, float>>
            {
                x => x.Linear(),
                x => x.QuadraticEaseIn(),
                x => x.QuadraticEaseOut(),
                x => x.CubicEaseIn(),
                x => x.CubicEaseOut(),
                x => x.QuarticEaseIn(),
                x => x.QuarticEaseOut(),
                x => x.QuinticEaseIn(),
                x => x.QuinticEaseOut(),
                x => x.BackEaseIn(),
                x => x.BackEaseOut(),
                x => x.BounceEaseIn(),
                x => x.BounceEaseOut(),
                x => x.SineEaseIn(),
                x => x.SineEaseOut(),
                // Elastic and Exponential/Circular may return NaN for out-of-range, so skip them
            };
            foreach (var input in inputValues)
            {
                foreach (var func in safeFuncs)
                {
                    float result = func(input);
                    Assert.IsFalse(float.IsNaN(result), $"{func.Method.Name} returned NaN for input {input}");
                    Assert.IsFalse(float.IsInfinity(result), $"{func.Method.Name} returned Infinity for input {input}");
                }
            }
            // For Elastic, Exponential, Circular: just assert no crash (no exception thrown)
            foreach (var input in inputValues)
            {
                _ = input.ElasticEaseIn();
                _ = input.ElasticEaseOut();
                _ = input.ExponentialEaseIn();
                _ = input.ExponentialEaseOut();
                _ = input.CircularEaseIn();
                _ = input.CircularEaseOut();
            }
        }
    }
}
