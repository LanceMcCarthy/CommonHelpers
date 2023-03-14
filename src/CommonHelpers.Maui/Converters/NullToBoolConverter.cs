﻿using System.Globalization;

namespace CommonHelpers.Maui.Converters;

public class NullToBoolConverter : IValueConverter
{
    public bool IsInverted { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value != null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}