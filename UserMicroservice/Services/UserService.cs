using UserMicroservice.Models;
using UserMicroservice.Repositories;

namespace UserMicroservice.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var existingUser = await _userRepository.FindByEmail(user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _userRepository.Create(user);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.Index();
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _userRepository.FindById(id);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await _userRepository.FindById(id);
        }

        public async Task UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            // Verifique se o usuário existe
            var existingUser = await _userRepository.FindById(user.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            await _userRepository.Update(user);
        }

        public async Task DeleteUser(int id)
        {
            // Verifique se o usuário existe
            var existingUser = await _userRepository.FindById(id);
            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            await _userRepository.Delete(id);
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            var authenticatedUser = await _userRepository.AuthenticateUser(email, password);

            if (authenticatedUser == null)
            {
                throw new UnauthorizedAccessException("Authentication failed.");
            }

            return authenticatedUser;
        }

    }
}
