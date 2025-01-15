using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Implementations.Mappers;

namespace TransactionsApp.Application.UnitTests
{
    [TestClass]
    public class UserMapperUnitTests
    {
        private UserMapper _userMapper;

        [TestInitialize]
        public void Initialize()
        {
            _userMapper = new UserMapper();
        }

        [TestMethod]
        public void Map_WithValidDto_ReturnsUser()
        {
            var dto = new AddUserDto
            {
                FullNameHebrew = "שלום כהן",
                FullNameEnglish = "Shalom Cohen",
                DateOfBirth = new DateTime(1990, 1, 1),
                UserIdentity = "123456789"
            };

            var user = _userMapper.Map(dto);

            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Id);
            Assert.AreEqual(dto.FullNameHebrew, user.FullNameHebrew);
            Assert.AreEqual(dto.FullNameEnglish, user.FullNameEnglish);
            Assert.AreEqual(dto.DateOfBirth, user.DateOfBirth);
            Assert.AreEqual(dto.UserIdentity, user.UserIdentity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Map_WithNullDto_ThrowsArgumentNullException()
        {
            _userMapper.Map(null);
        }
    }
}
