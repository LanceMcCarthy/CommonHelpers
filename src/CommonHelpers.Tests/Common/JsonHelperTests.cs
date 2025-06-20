using CommonHelpers.Common;
using CommonHelpers.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonHelpers.Tests.Common
{
    [TestClass]
    public class JsonHelperTests
    {
        [TestMethod]
        public void SerializeItem()
        {
            // Arrange
            var employee = new TestEmployee { Name = "John Doe", Salary = 56000, Married = true };

            // Act
            var json = JsonHelper<TestEmployee>.Serialize(employee);

            // Assert
            Assert.IsNotNull(json);
            StringAssert.Contains(json, "name");
            StringAssert.Contains(json, "John Doe");
            StringAssert.Contains(json, "salary");
            StringAssert.Contains(json, "56000");
            StringAssert.Contains(json, "married");
            StringAssert.Contains(json, "true");
        }

        [TestMethod]
        public void DeserializeItem()
        {
            // Arrange
            var json = "{\"name\":\"John Doe\",\"salary\":56000,\"married\":true}";

            // Act
            var employee = JsonHelper<TestEmployee>.Deserialize(json);

            // Assert
            Assert.IsNotNull(employee);
            Assert.AreEqual("John Doe", employee.Name);
            Assert.AreEqual(56000, employee.Salary);
            Assert.IsTrue(employee.Married);
        }

        [TestMethod]
        public void RoundTrip_SerializeAndDeserialize_ReturnsEquivalentObject()
        {
            // Arrange
            var original = new TestEmployee { Name = "Jane Smith", Salary = 75000, Married = false };

            // Act
            var json = JsonHelper<TestEmployee>.Serialize(original);
            var result = JsonHelper<TestEmployee>.Deserialize(json);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(original.Name, result.Name);
            Assert.AreEqual(original.Salary, result.Salary);
            Assert.AreEqual(original.Married, result.Married);
        }

        [TestMethod]
        public void Deserialize_InvalidJson_ThrowsException()
        {
            // Arrange
            var invalidJson = "not a json string";

            // Act & Assert
            Assert.ThrowsException<System.Runtime.Serialization.SerializationException>(() =>
            {
                JsonHelper<TestEmployee>.Deserialize(invalidJson);
            });
        }

        [TestMethod]
        public void Serialize_NullObject_ThrowsException()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                JsonHelper<TestEmployee>.Serialize(null);
            });
        }
    }
}
