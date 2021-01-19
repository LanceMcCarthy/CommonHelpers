using System;
using System.Collections.Generic;
using CommonHelpers.Extensions;
using CommonHelpers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.ExtensionsTests
{
    [TestClass]
    public class EnumExtensionsTests
    {
        [TestMethod]
        public void GetEnumAsList()
        {
            // Arrange
            int expectedCount = 7;
            List<DayOfWeek> days;

            // Act
            days = EnumExtenstions.GetEnumAsList<DayOfWeek>();
            var actualDayCount = days.Count;

            // Assert
            Assert.AreEqual(expectedCount, actualDayCount);
        }

        [TestMethod]
        public void GetDefaultValue()
        {
            // Arrange
            var expectedDefault = GenderType.Male;

            // Act
            var actualDefault = EnumExtenstions.GetEnumDefaultValue<GenderType>();

            // Assert
            Assert.AreEqual(expectedDefault, actualDefault);
        }
    }
}
