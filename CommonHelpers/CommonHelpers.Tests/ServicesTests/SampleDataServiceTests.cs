using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.ServicesTests
{
    [TestClass]
    public class SampleDataServiceTests
    {
        [TestMethod]
        public void GeneratePeopleData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var people = service.GeneratePeopleData();

            // Assert
            Assert.IsNotNull(people);
        }

        [TestMethod]
        public void GeneratePeopleNames()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var names = service.GeneratePeopleNames();

            // Assert
            Assert.IsNotNull(names);
        }

        [TestMethod]
        public void GenerateProductData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var products = service.GenerateProductData();

            // Assert
            Assert.IsNotNull(products);
        }

        [TestMethod]
        public void GenerateCategoryData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var categories = service.GenerateCategoryData();

            // Assert
            Assert.IsNotNull(categories);
        }

        [TestMethod]
        public void GenerateSupplierData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var suppliers = service.GenerateSupplierData();

            // Assert
            Assert.IsNotNull(suppliers);
        }

        [TestMethod]
        public void GenerateCategoricalChartData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var categories = service.GenerateCategoricalData();

            // Assert
            Assert.IsNotNull(categories);
        }

        [TestMethod]
        public void GenerateDateTimeChartData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var data = service.GenerateDateTimeDayData();

            // Assert
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GenerateDateTimeMinuteChartData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var data = service.GenerateDateTimeMinuteData();

            // Assert
            Assert.IsNotNull(data);
        }

        [TestMethod]
        public void GenerateScatterPointChartData()
        {
            // Arrange
            var service = new SampleDataService();

            // Act
            var data = service.GenerateScatterPointData();

            // Assert
            Assert.IsNotNull(data);
        }
    }
}
