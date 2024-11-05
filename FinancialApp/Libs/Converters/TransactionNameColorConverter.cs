using System.Globalization;

namespace FinancialApp.Libs.Converters;

internal class TransactionNameColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is null) return "#FFF";

        var random = new Random();
        var color = $"#FF{random.Next(0x1000000):X6}";
        return Color.FromArgb(color);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}