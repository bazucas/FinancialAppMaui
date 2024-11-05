using System.Globalization;

namespace FinancialApp.Libs.Converters;

internal class TransactionNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var transactionName = (string)value;

        if (transactionName is null) return string.Empty;

        return transactionName.ToUpper()[0];
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}