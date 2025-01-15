using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Managers;
using TransactionsApp.Application.Services.Repositories;
using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Application.Services.Implementations.Managers
{
    /// <summary>
    /// Defines the operations for managing users.
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _userRepository;

        public UserManager(IRepository<User> userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Gets a user by its unique identity (e.g., Teudat Zehut, National ID).
        /// </summary>
        /// <param name="identity">User's identity</param>
        /// <returns>Found user.</returns>
        public async Task<User?> GetUserByIdentity(string identity)
        {
            var matchingUsers = await _userRepository.FindAsync(u => u.UserIdentity == identity);
            var user = matchingUsers.FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="dto">Data transfer object with data of a user to create.</param>
        /// <returns>The created user.</returns>
        public async Task<User> CreateUserAsync(AddUserDto dto)
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FullNameHebrew = dto.FullNameHebrew,
                FullNameEnglish = dto.FullNameEnglish,
                DateOfBirth = dto.DateOfBirth,
                UserIdentity = dto.UserIdentity
            };

            await _userRepository.AddAsync(newUser);

            return newUser;
        }
    }
}
