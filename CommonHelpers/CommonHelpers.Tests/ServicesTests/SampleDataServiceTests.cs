using System.Linq;
using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.ServicesTests
{
    [TestClass]
    public class SampleDataServiceTests
    {
        private readonly SampleDataService service;

        public SampleDataServiceTests()
        {
            // Arrange
            service = new SampleDataService();
        }

        [TestMethod]
        public void GeneratePeopleData()
        {
            // Act
            var data = service.GeneratePeopleData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GeneratePeopleNames()
        {
            // Act
            var data = service.GeneratePeopleNames();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GenerateCategoryData()
        {
            // Act
            var data = service.GenerateCategoryData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GenerateSupplierData()
        {
            // Act
            var data = service.GenerateSupplierData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GenerateProductData()
        {
            // Act
            var data = service.GenerateProductData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void FindProductByCategory()
        {
            // Act
            var product = service.FindProductByCategory(2);

            // Assert
            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void FindProductBySupplier()
        {
            // Act
            var product = service.FindProductBySupplier(2);

            // Assert
            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void GenerateCategoricalChartData()
        {
            // Act
            var data = service.GenerateCategoricalData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GenerateDateTimeChartData()
        {
            // Act
            var data = service.GenerateDateTimeDayData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GenerateDateTimeMinuteChartData()
        {
            // Act
            var data = service.GenerateDateTimeMinuteData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void GenerateScatterPointChartData()
        {
            // Act
            var data = service.GenerateScatterPointData();

            // Assert
            var count = data.Count();
            Assert.IsTrue(count > 0);
        }
    }
}
