using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Common.Services;
using TransactionsApp.Domain.Models.Entities;

namespace TransactionsApp.Application.Services.Implementations.Mappers
{
    /// <summary>
    /// Maps <see cref="AddUserDto"/> into <see cref="User"/>.
    /// </summary>
    public class UserMapper : IMapper<AddUserDto, User>
    {
        /// <summary>
        /// Maps <see cref="AddUserDto"/> into <see cref="User"/>.
        /// </summary>
        /// <param name="source">Object to map from.</param>
        /// <returns>Mapped user.</returns>
        public User Map(AddUserDto source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullNameHebrew = source.FullNameHebrew,
                FullNameEnglish = source.FullNameEnglish,
                DateOfBirth = source.DateOfBirth,
                UserIdentity = source.UserIdentity
            };

            return user;
        }
    }
}
