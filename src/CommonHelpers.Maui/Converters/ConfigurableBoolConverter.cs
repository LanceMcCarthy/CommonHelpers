using System.Globalization;

namespace CommonHelpers.Maui.Converters;

/// <summary>
/// A converter that can return one of two predefined values based on the input's True/False value.
/// for example, here is an implementation that returns the opposite of the input bool.
/// 
/// ConfigurableBoolConverter x:Key="InvertBoolConverter"
///    x:TypeArguments="x:Boolean"
///    TrueResult="False"
///    FalseResult="True"
/// 
/// </summary>
/// <typeparam name="T">the TypeArgument of the input value.</typeparam>
public class ConfigurableBoolConverter<T> : IValueConverter
{
    public ConfigurableBoolConverter() { }

    public ConfigurableBoolConverter(T trueResult, T falseResult)
    {
        this.TrueResult = trueResult;
        this.FalseResult = falseResult;
    }

    public T TrueResult { get; set; }

    public T FalseResult { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(this.TrueResult == null || this.FalseResult == null)
        {
            return !(bool) value;
        }

        return value is true ? this.TrueResult : this.FalseResult;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(this.TrueResult == null || this.FalseResult == null)
        {
            return !(bool) value;
        }

        return value is T variable && EqualityComparer<T>.Default.Equals(variable, this.TrueResult);
    }
}