using FinancialApp.Models;

namespace FinancialApp.Repositories;

public interface ITransactionRepository
{
    List<Transaction> GetAll();
    void Add(Transaction transaction);
    void Update(Transaction transaction);
    void Delete(Transaction transaction);
}