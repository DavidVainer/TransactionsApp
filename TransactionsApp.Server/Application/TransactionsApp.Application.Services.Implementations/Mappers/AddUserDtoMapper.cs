using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Common.Services;

namespace TransactionsApp.Application.Services.Implementations.Mappers
{
    /// <summary>
    /// Maps <see cref="AddTransactionDto"/> into <see cref="AddUserDto"/>.
    /// </summary>
    public class AddUserDtoMapper : IMapper<AddTransactionDto, AddUserDto>
    {
        /// <summary>
        /// Maps <see cref="AddTransactionDto"/> into <see cref="AddUserDto"/>.
        /// </summary>
        /// <param name="source">Object to map from.</param>
        /// <returns>Mapped user.</returns>
        public AddUserDto Map(AddTransactionDto source)
        {
            ArgumentNullException.ThrowIfNull(source);

            var addUserDto = new AddUserDto
            {
                FullNameHebrew = source.FullNameHebrew,
                FullNameEnglish = source.FullNameEnglish,
                DateOfBirth = source.DateOfBirth,
                UserIdentity = source.UserIdentity
            };

            return addUserDto;
        }
    }
}
