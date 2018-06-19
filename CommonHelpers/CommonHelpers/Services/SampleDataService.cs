using System;
using System.Collections.Generic;
using System.Linq;
using CommonHelpers.Models;

namespace CommonHelpers.Services
{
    public class SampleDataService
    {
        private readonly Random rand;
        private List<string> peopleNames;

        public SampleDataService()
        {
            rand = new Random();
            LoadSharedData();
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

        public IEnumerable<Person> GeneratePeopleData()
        {
            return Enumerable.Range(1, 43).Select(i => new Person
            {
                Name = peopleNames[i],
                Age = i + 10,
                Gender = (GenderType)(int)rand.Next(0,3),
                DateOfBirth = DateTime.Now.AddYears(-i)
            });
        }

        public IEnumerable<string> GeneratePeopleNames()
        {
            return new List<string>(peopleNames);
        }

        private void LoadSharedData()
        {
            peopleNames = new List<string>()
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
        }
    }
}