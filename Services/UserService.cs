using FinanceApp.Models;
using FinanceApp.Repositories;
using BCrypt.Net;

namespace FinanceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public User? CurrentUser { get; private set; }

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void CreateUser(string name, string email, string password)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            userRepository.Add(newUser);
        }

        public User? GetUserByEmail(string email)
        {
            return userRepository.GetByEmail(email);
        }

        public bool Login(string email, string password)
        {
            User? user = userRepository.GetByEmail(email);

            if (user == null)
            {
                return false;
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordValid)
            {
                return false;
            }

            CurrentUser = user;
            return true;
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public void EditProfile(string newName, string newEmail, string newPassword)
        {
            if (CurrentUser == null)
            {
                throw new InvalidOperationException("Você precisa estar logado para editar seu perfil.");
            }

            CurrentUser.Name = newName;
            CurrentUser.Email = newEmail;
            CurrentUser.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

            userRepository.Update(CurrentUser);
        }
    }
}
