using FinanceApp.Models;

namespace FinanceApp.Repositories
{
    public class UserRepository
    {
        private const string FilePath = "users.txt";
        private readonly List<User> users = new();

        public UserRepository()
        {
            LoadFromFile();
        }

        public void Add(User user)
        {
            bool emailAlreadyExists = users.Any(existingUser =>
                existingUser.Email.Equals(user.Email, StringComparison.OrdinalIgnoreCase));

            if (emailAlreadyExists)
            {
                throw new InvalidOperationException("Já existe um usuário com este email.");
            }

            users.Add(user);
            SaveToFile();
        }

        public User? GetById(Guid id)
        {
            return users.FirstOrDefault(user => user.Id == id);
        }

        public User? GetByEmail(string email)
        {
            return users.FirstOrDefault(user =>
                user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public List<User> GetAll()
        {
            return new List<User>(users);
        }

        public void Update(User user)
        {
            User? existingUser = GetById(user.Id);

            if (existingUser == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            SaveToFile();
        }

        private void LoadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return;
            }

            using var fileReader = new StreamReader(FilePath);
            string? currentLine;

            while ((currentLine = fileReader.ReadLine()) != null)
            {
                string[] userFields = currentLine.Split(';');

                if (userFields.Length < 4)
                {
                    continue;
                }

                var user = new User
                {
                    Id = Guid.Parse(userFields[0]),
                    Name = userFields[1],
                    Email = userFields[2],
                    Password = userFields[3]
                };

                users.Add(user);
            }
        }

        private void SaveToFile()
        {
            using var fileWriter = new StreamWriter(FilePath, false);

            foreach (var user in users)
            {
                fileWriter.WriteLine($"{user.Id};{user.Name};{user.Email};{user.Password}");
            }
        }
    }
}
