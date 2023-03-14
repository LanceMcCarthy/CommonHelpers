using System.Globalization;
using System.Text.RegularExpressions;

namespace CommonHelpers.Maui.Converters;

/// <summary>
/// A converter that will return the first letter of each word passed to it. Most commonly used for getting the initials of a full name.
/// Example: John Doe will be returned as "JD"
/// </summary>
public class NameToInitialsConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var name = value as string;

        return string.IsNullOrEmpty(name) 
            ? "XX" 
            : new Regex(@"\s*([^\s])[^\s]*\s*").Replace(name, "$1" + " ").ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}