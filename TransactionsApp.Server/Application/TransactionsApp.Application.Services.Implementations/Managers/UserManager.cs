using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Managers;
using TransactionsApp.Application.Services.Repositories;
using TransactionsApp.Common.Services;
using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Application.Services.Implementations.Managers
{
    /// <summary>
    /// Defines the operations for managing users.
    /// </summary>
    public class UserManager : IUserManager
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper<AddUserDto, User> _userMapper;

        public UserManager(IRepository<User> userRepository, IMapper<AddUserDto, User> userMapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userMapper = userMapper ?? throw new ArgumentNullException(nameof(userMapper));
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
            var newUser = _userMapper.Map(dto);

            await _userRepository.AddAsync(newUser);

            return newUser;
        }
    }
}
