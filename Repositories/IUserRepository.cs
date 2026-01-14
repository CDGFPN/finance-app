using FinanceApp.Models;

namespace FinanceApp.Repositories
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
