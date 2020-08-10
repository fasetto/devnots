using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Mapping;
using DevNots.Application.Services;
using DevNots.Application.Validations;
using DevNots.Domain;
using Moq;
using Xunit;

namespace DevNots.Application.Tests
{
    public class UserService_Tests
    {
        private readonly IMapper mapper;
        private readonly UserValidator userValidator;
        private readonly Mock<IUserRepository> userRepoMock;
        private readonly UserService userService;
        public UserService_Tests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            mapper = mapperConfig.CreateMapper();

            userValidator = new UserValidator();
            userRepoMock  = new Mock<IUserRepository>();
            userService   = new UserService(userRepoMock.Object, mapper, userValidator);
        }

        [Fact]
        public async Task RegisterAsync_WhenUserDetailsValid_ReturnsUserId()
        {
            // Arrange
            userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult("id"));

            var user = new UserDto()
            {
                Email    = "serkanbircan21@yandex.com",
                Username = "fasetto",
                Password = "MyPassword"
            };

            // Act
            var response = await userService.RegisterAsync(user);
            var id = response.Result;

            // Assert
            Assert.NotEmpty(id);
        }

        [Theory]
        [InlineData("not_valid_email", "", "")]
        [InlineData("valid@email.com", "fasetto", "")]
        [InlineData("", "", "")]
        public async Task RegisterAsync_WhenUserDetailsInvalid_ReturnsErrorResponse(string email, string username, string password)
        {
            // Arrange
            userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult("id"));

            var user = new UserDto()
            {
                Email    = email,
                Username = username,
                Password = password,
            };

            // Act
            var response = await userService.RegisterAsync(user);

            // Assert
            userRepoMock.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Never());
            Assert.NotNull(response.Error);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserIdValid_NotReturnsAnyError()
        {
            // Arrange
            userRepoMock.Setup(x => x.RemoveAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            // Act
            var response = await userService.DeleteUserAsync(new DeleteUserDto()
            {
                Id = "valid-user-id",
            });

            // Assert
            Assert.Null(response.Error);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserIdInvalid_ReturnsNotFoundError()
        {
            // Arrange
            userRepoMock.Setup(x => x.RemoveAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            // Act
            var response = await userService.DeleteUserAsync(new DeleteUserDto()
            {
                Id = "invalid-user-id",
            });

            // Assert
            Assert.NotNull(response.Error);
            Assert.Equal(404, response.Error.StatusCode);
        }


        [Fact]
        public async Task GetUsersAsync_ReturnsListOfUsers()
        {
            // Arrange
            userRepoMock.Setup(x => x.PaginateAsync(1, 10))
                .Returns(Task.FromResult(new List<User>().AsEnumerable()));

            // Act
            var response = await userService.GetUsersAsync(new UserListDto()
            {
                Limit = 20,
            });

            // Assert
            Assert.Null(response.Error);
            Assert.NotNull(response.Result);

        }
    }
}
