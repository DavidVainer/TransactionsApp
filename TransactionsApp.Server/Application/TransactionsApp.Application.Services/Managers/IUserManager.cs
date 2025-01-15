using System.Linq.Expressions;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Application.Services.Managers
{
    /// <summary>
    /// Defines the operations for managing users.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Gets a user by its unique identity (e.g., Teudat Zehut, National ID).
        /// </summary>
        /// <param name="identity">User's identity</param>
        /// <returns>Found user.</returns>
        Task<User?> GetUserByIdentity(string identity);

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="dto">Data transfer object with data of a user to create.</param>
        /// <returns>The created user.</returns>
        Task<User> CreateUserAsync(AddUserDto dto);
    }
}
