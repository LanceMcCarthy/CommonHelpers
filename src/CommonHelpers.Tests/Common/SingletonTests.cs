using CommonHelpers.Common;
using CommonHelpers.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonHelpers.Tests.Common;

[TestClass]
public class SingletonTests
{
    [TestMethod]
    public void SingleUse()
    {
        const string name = "John Doe";
        const double salary = 56000;
        const bool married = true;
        Singleton<TestEmployee>.Instance.Name = "John Doe";
        Singleton<TestEmployee>.Instance.Salary = 56000;
        Singleton<TestEmployee>.Instance.Married = true;
        Assert.IsTrue(name.Equals(Singleton<TestEmployee>.Instance.Name), "Name was not equal to expected value");
        Assert.IsTrue(salary.Equals(Singleton<TestEmployee>.Instance.Salary), "Salary was not equal to expected value");
        Assert.IsTrue(married.Equals(Singleton<TestEmployee>.Instance.Married), "Married was not equal to expected value");
    }

    [TestMethod]
    public void MultipleUse()
    {
        const string name = "John Doe";
        var earlierTicks = DateTime.Now.Ticks;
        Singleton<TestEmployee>.Instance.Name = "John Doe";
        var lazyInstanceTicks = Singleton<SingletonTestService>.Instance.InstanceId;
        Assert.IsTrue(name.Equals(Singleton<TestEmployee>.Instance.Name), "Name was not equal to expected value");
        Assert.IsTrue(earlierTicks < lazyInstanceTicks, "Lazy instantiation Id should always be larger.");
    }
}