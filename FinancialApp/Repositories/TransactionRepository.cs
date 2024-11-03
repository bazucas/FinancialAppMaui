using FinancialApp.Models;
using LiteDB;

namespace FinancialApp.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ILiteDatabase _database;
    private const string CollectionName = "transactions";

    public TransactionRepository(ILiteDatabase database)
    {
        _database = database;
    }

    public List<Transaction> GetAll()
    {
        return _database
            .GetCollection<Transaction>(CollectionName)
            .Query()
            .OrderByDescending(a => a.Date)
            .ToList();
    }

    public void Add(Transaction transaction)
    {
        var collection = _database.GetCollection<Transaction>(CollectionName);
        collection.Insert(transaction);
        collection.EnsureIndex(a => a.Date);
    }

    public void Update(Transaction transaction)
    {
        var collection = _database.GetCollection<Transaction>(CollectionName);
        collection.Update(transaction);
    }

    public void Delete(Transaction transaction)
    {
        var collection = _database.GetCollection<Transaction>(CollectionName);
        collection.Delete(transaction.Id);
    }
}
