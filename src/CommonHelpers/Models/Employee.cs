using System;
using CommonHelpers.Common;

namespace CommonHelpers.Models
{
    public class Employee : BindableBase
    {
        private string name;
        private string position;
        private DateTime startDate;
        private double salary;
        private int vacationTotal;
        private int vacationUsed;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Position
        {
            get => position;
            set => SetProperty(ref position, value);
        }

        public DateTime StartDate
        {
            get => startDate;
            set => SetProperty(ref startDate, value);
        }

        public double Salary
        {
            get => salary;
            set => SetProperty(ref salary, value);
        }

        public int VacationTotal
        {
            get => vacationTotal;
            set => SetProperty(ref vacationTotal, value);
        }

        public int VacationUsed
        {
            get => vacationUsed;
            set => SetProperty(ref vacationUsed, value);
        }
    }
}
