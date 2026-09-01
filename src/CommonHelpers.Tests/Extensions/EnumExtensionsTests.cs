using CommonHelpers.Extensions;
using CommonHelpers.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonHelpers.Tests.Extensions;

[TestClass]
public class EnumExtensionsTests
{
    [TestMethod]
    public void GetEnumAsList()
    {
        const int expectedCount = 7;
        var days = EnumExtenstions.GetEnumAsList<DayOfWeek>();
        var actualDayCount = days.Count;
        Assert.AreEqual(expectedCount, actualDayCount);
    }

    [TestMethod]
    public void GetDefaultValue()
    {
        const GenderType expectedDefault = GenderType.Male;
        var actualDefault = EnumExtenstions.GetEnumDefaultValue<GenderType>();
        Assert.AreEqual(expectedDefault, actualDefault);
    }
}