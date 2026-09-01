using CommonHelpers.Common;
using CommonHelpers.Tests.TestHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CommonHelpers.Tests.Common;

[TestClass]
public class JsonHelperTests
{
    [TestMethod]
    public void SerializeItem()
    {
        var employee = new TestEmployee { Name = "John Doe", Salary = 56000, Married = true };

        var json = JsonHelper<TestEmployee>.Serialize(employee);

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
        const string json = "{\"name\":\"John Doe\",\"salary\":56000,\"married\":true}";

        var employee = JsonHelper<TestEmployee>.Deserialize(json);

        Assert.IsNotNull(employee);
        Assert.AreEqual("John Doe", employee.Name);
        Assert.AreEqual(56000, employee.Salary);
        Assert.IsTrue(employee.Married);
    }

    [TestMethod]
    public void RoundTrip_SerializeAndDeserialize_ReturnsEquivalentObject()
    {
        var original = new TestEmployee { Name = "Jane Smith", Salary = 75000, Married = false };

        var json = JsonHelper<TestEmployee>.Serialize(original);
        var result = JsonHelper<TestEmployee>.Deserialize(json);

        Assert.IsNotNull(result);
        Assert.AreEqual(original.Name, result.Name);
        Assert.AreEqual(original.Salary, result.Salary);
        Assert.AreEqual(original.Married, result.Married);
    }

    [TestMethod]
    public void Deserialize_InvalidJson_ThrowsException()
    {
        const string invalidJson = "not a json string";

        Assert.ThrowsExactly<System.Runtime.Serialization.SerializationException>(() =>
        {
            JsonHelper<TestEmployee>.Deserialize(invalidJson);
        });
    }

    [TestMethod]
    public void Serialize_NullObject_ThrowsException()
    {
        Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            JsonHelper<TestEmployee>.Serialize(null);
        });
    }
}