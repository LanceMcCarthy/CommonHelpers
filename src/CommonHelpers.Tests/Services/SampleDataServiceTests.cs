using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CommonHelpers.Tests.Services;

[TestClass]
public class SampleDataServiceTests
{
    private readonly SampleDataService service = new();

    // Arrange

    [TestMethod]
    public void GeneratePeopleData()
    {
        var dataWithoutRealNames = service.GeneratePeopleData();
        var dataWithRealNames = service.GeneratePeopleData(true);
        Assert.IsNotNull(dataWithoutRealNames);
        Assert.IsNotNull(dataWithRealNames);
        Assert.IsTrue(dataWithoutRealNames.Any());
        Assert.IsTrue(dataWithRealNames.Any());
    }

    [TestMethod]
    public void GeneratePeopleNames()
    {
        var data = service.GeneratePeopleNames();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void GenerateCategoryData()
    {
        var data = service.GenerateCategoryData();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void GenerateSupplierData()
    {
        var data = service.GenerateSupplierData();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void GenerateProductData()
    {
        var data = service.GenerateProductData();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void FindProductByCategory()
    {
        var product = service.FindProductByCategory(2);
        Assert.IsNotNull(product);
    }

    [TestMethod]
    public void FindProductBySupplier()
    {
        var product = service.FindProductBySupplier(2);
        Assert.IsNotNull(product);
    }

    [TestMethod]
    public void GenerateCategoricalChartData()
    {
        var data = service.GenerateCategoricalData();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void GenerateDateTimeChartData()
    {
        var data = service.GenerateDateTimeDayData();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void GenerateDateTimeMinuteChartData()
    {
        var data = service.GenerateDateTimeMinuteData();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void GenerateScatterPointChartData()
    {
        var data = service.GenerateScatterPointData();
        Assert.IsNotNull(data);
        Assert.IsTrue(data.Any());
    }

    [TestMethod]
    public void GenerateEmployeeData()
    {
        var dataWithoutRealNames = service.GenerateEmployeeData(false);
        var dataWithRealNames = service.GenerateEmployeeData();
        Assert.IsNotNull(dataWithoutRealNames);
        Assert.IsNotNull(dataWithRealNames);
        Assert.IsTrue(dataWithoutRealNames.Any());
        Assert.IsTrue(dataWithRealNames.Any());
    }
}