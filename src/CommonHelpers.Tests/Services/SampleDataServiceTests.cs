using System.Linq;
using CommonHelpers.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonHelpers.Tests.Services
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
            var dataWithoutRealNames = service.GeneratePeopleData();
            var dataWithRealNames = service.GeneratePeopleData(true);

            // Assert
            Assert.IsNotNull(dataWithoutRealNames);
            Assert.IsNotNull(dataWithRealNames);

            Assert.IsTrue(dataWithoutRealNames.Any());
            Assert.IsTrue(dataWithRealNames.Any());
        }

        [TestMethod]
        public void GeneratePeopleNames()
        {
            // Act
            var data = service.GeneratePeopleNames();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void GenerateCategoryData()
        {
            // Act
            var data = service.GenerateCategoryData();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void GenerateSupplierData()
        {
            // Act
            var data = service.GenerateSupplierData();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void GenerateProductData()
        {
            // Act
            var data = service.GenerateProductData();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
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
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void GenerateDateTimeChartData()
        {
            // Act
            var data = service.GenerateDateTimeDayData();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void GenerateDateTimeMinuteChartData()
        {
            // Act
            var data = service.GenerateDateTimeMinuteData();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void GenerateScatterPointChartData()
        {
            // Act
            var data = service.GenerateScatterPointData();

            // Assert
            Assert.IsNotNull(data);
            Assert.IsTrue(data.Any());
        }

        [TestMethod]
        public void GenerateEmployeeData()
        {
            // Act
            var dataWithoutRealNames = service.GenerateEmployeeData(false);
            var dataWithRealNames = service.GenerateEmployeeData();

            // Assert
            Assert.IsNotNull(dataWithoutRealNames);
            Assert.IsNotNull(dataWithRealNames);

            Assert.IsTrue(dataWithoutRealNames.Any());
            Assert.IsTrue(dataWithRealNames.Any());
        }
    }
}
