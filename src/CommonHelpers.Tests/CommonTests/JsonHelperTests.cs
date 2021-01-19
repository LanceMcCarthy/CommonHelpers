using System;
using CommonHelpers.Common;
using CommonHelpers.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.CommonTests
{
    [TestClass]
    public class JsonHelperTests
    {
        [TestMethod]
        public void SerializeItem()
        {
            //Arrange
            var employee = new TestEmployee { Name = "John Doe", Salary = 56000, Married = true };

            //Act
            var json = JsonHelper<TestEmployee>.Serialize(employee);

            //Assert
            Assert.IsNotNull(json);
        }

        [TestMethod]
        public void DeserializeItem()
        {
            //Arrange
            var json  = @"{""employee"": { ""name"": ""John Doe"", ""salary"": 56000, ""married"": true } }";

            //Act
            var employee = JsonHelper<TestEmployee>.Deserialize(json);

            //Assert
            Assert.IsNotNull(employee);
        }
    }

    
}
