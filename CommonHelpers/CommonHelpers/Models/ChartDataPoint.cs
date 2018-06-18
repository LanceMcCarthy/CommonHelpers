using System;
using CommonHelpers.Common;

namespace CommonHelpers.Models
{
    public class ChartDataPoint : BindableBase
    {
        private string title;
        private double value;
        private DateTime date;
        private double xValue;
        private double yValue;

        // Used for Categorical series types

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public double Value
        {
            get => value;
            set => SetProperty(ref this.value, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        // Used for Scatter series types

        public double XValue
        {
            get => xValue;
            set => SetProperty(ref xValue, value);
        }

        public double YValue
        {
            get => yValue;
            set => SetProperty(ref yValue, value);
        }
    }
}