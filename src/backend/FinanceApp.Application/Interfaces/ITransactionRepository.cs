using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Interfaces
{
    public interface ITransactionRepository
    {
        void Save(Transaction transaction);
        List<Transaction> GetAll();
        Transaction? GetById(Guid id);
        List<Transaction> GetByUserId(Guid userId);
        void Update(Transaction transaction);
        void Delete(Guid id);
    }
}
