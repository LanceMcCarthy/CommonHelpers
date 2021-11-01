using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using CommonHelpers.Models;

namespace Demo.Uwp.Converters
{
    public class VacationBalanceToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Employee emp)
            {
                var percentageUsed = (double)emp.VacationUsed / (double)emp.VacationTotal * 100;

                if (percentageUsed < 50)
                    return new SolidColorBrush(Colors.LimeGreen);

                if (percentageUsed < 75)
                    return new SolidColorBrush(Colors.Gold);

                if (percentageUsed < 50)
                    return new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
