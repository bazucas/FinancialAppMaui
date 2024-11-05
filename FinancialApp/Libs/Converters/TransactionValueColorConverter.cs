using FinancialApp.Models;
using System.Globalization;

namespace FinancialApp.Libs.Converters;

internal class TransactionValueColorConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var transaction = (Transaction)value;

        if (transaction is null) return null;

        return transaction.Type == TransactionType.Income ? Color.FromArgb("#FF939E5A") : Colors.Red;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}