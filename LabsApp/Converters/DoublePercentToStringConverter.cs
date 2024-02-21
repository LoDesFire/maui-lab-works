using System.Globalization;

namespace LabsApp.Converters;

public class DoublePercentToStringConverter: IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return $"{(double?)value * 100:0.00}%";
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var str = (string?)value;
        return System.Convert.ToDouble(str?[..^1]);
    }
}