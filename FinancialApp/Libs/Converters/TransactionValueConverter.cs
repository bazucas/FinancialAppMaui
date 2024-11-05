using FinancialApp.Models;
using System.Globalization;

namespace FinancialApp.Libs.Converters;

internal class TransactionValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var transaction = (Transaction)value;

        if (transaction is null) return null;

        return transaction.Type == TransactionType.Income ? transaction.Value.ToString("C") : $"-{transaction.Value:C}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}