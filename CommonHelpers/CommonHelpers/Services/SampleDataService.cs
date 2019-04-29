using System;
using System.Collections.Generic;
using System.Linq;
using CommonHelpers.Models;

namespace CommonHelpers.Services
{
    public class SampleDataService
    {
        #region Singleton Members

        private static SampleDataService _current;

        public static SampleDataService Current => _current ?? (_current = new SampleDataService());

        #endregion

        #region Instance Members

        private readonly Random _rand;

        public SampleDataService()
        {
            _rand = new Random();
        }

        #region Product, Category and Supplier Data

        public IEnumerable<Product> GenerateProductData(int count = 30)
        {
            return Enumerable.Range(1, count).Select(i => new Product
            {
                ProductId = i,
                ProductName = $"Product {i}",
                SupplierId = _rand.Next(1,5),
                CategoryId = _rand.Next(1,5),
                QuantityPerUnit = _rand.Next(0, i).ToString(),
                UnitPrice = Convert.ToDecimal(_rand.NextDouble() * 100),
                UnitsInStock = Convert.ToInt16(_rand.Next(1, 100)),
                UnitsOnOrder = Convert.ToInt16(_rand.Next(1,100)),
                ReorderLevel = Convert.ToInt16(_rand.Next(5, 30)),
                Discontinued = i % 5 == 0,
            });
        }

        public IEnumerable<Supplier> GenerateSupplierData()
        {
            return Enumerable.Range(1, 5).Select(i => new Supplier
            {
                SupplierId = i,
                SupplierName = $"Supplier {i}"
            });
        }

        public IEnumerable<Category> GenerateCategoryData()
        {
            return Enumerable.Range(1, 5).Select(i => new Category
            {
                CategoryId = i,
                CategoryName = $"Category {i}"
            });
        }

        public IEnumerable<Product> FindProductByCategory(int categoryId, int maximumResultCount = 30)
        {
            return GenerateProductData(maximumResultCount).Where(p => p.CategoryId == categoryId);
        }

        public IEnumerable<Product> FindProductBySupplier(int supplierId, int maximumResultCount = 30)
        {
            return GenerateProductData(maximumResultCount).Where(p => p.SupplierId == supplierId);
        }

        #endregion

        #region Chart Data

        public IEnumerable<ChartDataPoint> GenerateCategoricalData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                Title = $"Category {i}",
                Value = _rand.Next(0,i)
            });
        }

        public IEnumerable<ChartDataPoint> GenerateDateTimeDayData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                Title = $"Category {i}",
                Date = DateTime.Now.AddDays(-i),
                Value = _rand.Next(0, i)
            });
        }

        public IEnumerable<ChartDataPoint> GenerateDateTimeMinuteData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                Title = $"Category {i}",
                Date = DateTime.Now.AddMinutes(-i),
                Value = _rand.Next(0, i)
            });
        }

        public IEnumerable<ChartDataPoint> GenerateScatterPointData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                XValue = _rand.Next(0, i),
                YValue = _rand.Next(0, i)
            });
        }

        #endregion

        #region People and Employee Data

        public IEnumerable<Employee> GenerateEmployeeData(bool useSampleNames = true)
        {
            return Enumerable.Range(1, 43).Select(i => CreateEmployeeDetails(i, useSampleNames));
        }

        private Employee CreateEmployeeDetails(int seed, bool usePeopleNames)
        {
            var vacationTotal = _rand.Next(80, 120);

            var employee = new Employee
            {
                Name = usePeopleNames ? peopleNames[seed - 1] : $"Employee {seed}",
                StartDate = DateTime.Today.AddYears(-_rand.Next(1, 20)).AddDays(-_rand.Next(1, 350)),
                Position = roles[_rand.Next(0, roles.Length)],
                Salary = _rand.Next(50000, 125000),
                VacationTotal = vacationTotal,
                VacationUsed = vacationTotal - _rand.Next(40, 70)
            };

            return employee;
        }

        public IEnumerable<Person> GeneratePeopleData(bool useSampleNames = false)
        {
            return Enumerable.Range(1, 43).Select(i => new Person
            {
                Name = useSampleNames ? peopleNames[i - 1] : $"Person {i}",
                Age = _rand.Next(0, 100),
                Gender = i % 2 == 0 ? GenderType.Male : GenderType.Female,
                DateOfBirth = DateTime.Today.AddYears(-i)
            });
        }

        public IEnumerable<string> GeneratePeopleNames()
        {
            return new List<string>(peopleNames);
        }

        #endregion

        #region Supplementary Data

        private readonly string[] roles =
        {
            "Developer",
            "Technical Support Engineer",
            "Sales Representative",
            "Sales Engineer",
            "Manager",
            "Customer Advocate",
            "IT Specialist",
            "CEO",
            "President",
        };

        private readonly string[] peopleNames = 
        {
            "Freda Curtis",
            "Jeffery Francis",
            "Eva Lawson",
            "Emmett Santos",
            "Theresa Bryan",
            "Jenny Fuller",
            "Terrell Norris",
            "Eric Wheeler",
            "Julius Clayton",
            "Alfredo Thornton",
            "Roberto Romero",
            "Orlando Mathis",
            "Eduardo Thomas",
            "Harry Douglas",
            "Parker Blanton",
            "Leanne Motton",
            "Shanti Osborn",
            "Merry Lasker",
            "Jess Doyon",
            "Kizzie Arjona",
            "Augusta Hentz",
            "Tasha Trial",
            "Fredda Boger",
            "Megan Mowery",
            "Hong Telesco",
            "Inez Landi",
            "Taina Cordray",
            "Shantel Jarrell",
            "Soo Heidt",
            "Rayford Mahon",
            "Jenny Omarah",
            "Denita Dalke",
            "Nida Carty",
            "Sharolyn Lambson",
            "Niki Samaniego",
            "Rudy Jankowski",
            "Matha Whobrey",
            "Jessi Knouse",
            "Vena Rieser",
            "Roosevelt Boyce",
            "Kristan Swiney",
            "Lauretta Pozo",
            "Jarvis Victorine",
            "Dane Gabor"
        };

        #endregion

        #endregion
    }
}