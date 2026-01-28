using FinanceApp.Domain.Entities;

namespace FinanceApp.Application.Services
{
    public interface IUserService
    {
        User? CurrentUser { get; }
        void CreateUser(string name, string email, string password);
        User? GetUserByEmail(string email);
        bool Login(string email, string password);
        void Logout();
        void EditProfile(string newName, string newEmail, string newPassword);
    }
}
