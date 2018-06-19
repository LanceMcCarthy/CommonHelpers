using System;
using System.Collections.Generic;
using System.Linq;
using CommonHelpers.Models;

namespace CommonHelpers.Services
{
    public class SampleDataService
    {
        private readonly Random rand;

        public SampleDataService()
        {
            rand = new Random();
        }

        public IEnumerable<ChartDataPoint> GenerateCategoricalData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                Title = $"Category {i}",
                Value = rand.Next(0,i)
            });
        }

        public IEnumerable<ChartDataPoint> GenerateDateTimeDayData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                Title = $"Category {i}",
                Date = DateTime.Now.AddDays(-i),
                Value = rand.Next(0, i)
            });
        }

        public IEnumerable<ChartDataPoint> GenerateDateTimeMinuteData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                Title = $"Category {i}",
                Date = DateTime.Now.AddMinutes(-i),
                Value = rand.Next(0, i)
            });
        }

        public IEnumerable<ChartDataPoint> GenerateScatterPointData(int count = 5)
        {
            return Enumerable.Range(1, count).Select(i => new ChartDataPoint
            {
                XValue = rand.Next(0, i),
                YValue = rand.Next(0, i)
            });
        }

        public IEnumerable<Person> GeneratePeopleData(int count = 30)
        {
            return Enumerable.Range(1, count).Select(i => new Person
            {
                Name = $"Person 1",
                Age = i + 10,
                Gender = (GenderType)(int)rand.Next(0,3),
                DateOfBirth = DateTime.Now.AddYears(-i)
            });
        }
    }
}
