using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetById(Guid id);
        User? GetByEmail(string email);
        List<User> GetAll();
        void Update(User user);
    }
}
