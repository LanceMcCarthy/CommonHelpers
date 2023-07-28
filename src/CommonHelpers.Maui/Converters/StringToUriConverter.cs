using System.Globalization;

namespace CommonHelpers.Maui.Converters;

public class StringToUriConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return new Uri($"http://{value}", UriKind.RelativeOrAbsolute);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Uri uri)
        {
            return uri.ToString();
        }

        throw new ArgumentException("The ConvertBack value was not a valid Uri.");
    }
}