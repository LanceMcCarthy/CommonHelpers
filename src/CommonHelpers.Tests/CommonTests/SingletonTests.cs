using System;
using CommonHelpers.Common;
using CommonHelpers.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.CommonTests
{
    [TestClass]
    public class SingletonTests
    {
        [TestMethod]
        public void SingleUse()
        {
            //Arrange
            string name = "John Doe";
            double salary = 56000;
            bool married = true;

            //Act
            Singleton<TestEmployee>.Instance.Name = "John Doe";
            Singleton<TestEmployee>.Instance.Salary = 56000;
            Singleton<TestEmployee>.Instance.Married = true;
            
            //Assert
            Assert.IsTrue(name.Equals(Singleton<TestEmployee>.Instance.Name), "Name was not equal to expected value");
            Assert.IsTrue(salary.Equals(Singleton<TestEmployee>.Instance.Salary), "Salary was not equal to expected value");
            Assert.IsTrue(married.Equals(Singleton<TestEmployee>.Instance.Married), "Married was not equal to expected value");
        }

        [TestMethod]
        public void MultipleUse()
        {
            //Arrange
            string name = "John Doe";
            long earlierTicks = DateTime.Now.Ticks;

            //Act
            Singleton<TestEmployee>.Instance.Name = "John Doe";
            var lazyInstanceTicks = Singleton<SingletonTestService>.Instance.InstanceId;

            //Assert
            Assert.IsTrue(name.Equals(Singleton<TestEmployee>.Instance.Name), "Name was not equal to expected value");
            Assert.IsTrue(earlierTicks < lazyInstanceTicks, "Lazy instantiation Id should always be larger.");
        }
    }
}
