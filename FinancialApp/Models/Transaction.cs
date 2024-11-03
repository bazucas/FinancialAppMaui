using LiteDB;

namespace FinancialApp.Models;

public class Transaction
{
    [BsonId]
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public decimal Value { get; set; }
}