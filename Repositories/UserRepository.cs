using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly FinanceDbContext context;

        public UserRepository(FinanceDbContext context)
        {
            this.context = context;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User? GetById(Guid id)
        {
            return context.Users.FirstOrDefault(user => user.Id == id);
        }

        public User? GetByEmail(string email)
        {
            return context.Users.FirstOrDefault(user =>
                user.Email.ToLower() == email.ToLower());
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public void Update(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
