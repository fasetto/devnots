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
        private readonly RegisterUserValidator registerUserValidator;
        private readonly UpdateUserValidator updateUserValidator;
        private readonly Mock<IUserRepository> userRepoMock;
        private readonly UserService userService;
        public UserService_Tests()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            mapper = mapperConfig.CreateMapper();

            registerUserValidator = new RegisterUserValidator();
            updateUserValidator   = new UpdateUserValidator();
            userRepoMock          = new Mock<IUserRepository>();

            userService = new UserService(
                mapper,
                userRepoMock.Object,
                registerUserValidator,
                updateUserValidator
            );
        }

        [Fact]
        public async Task RegisterAsync_WhenUserDetailsValid_ReturnsUserId()
        {
            // Arrange
            userRepoMock.Setup(x => x.CreateAsync(It.IsAny<User>()))
                .Returns(Task.FromResult("id"));

            var user = new RegisterUserRequest()
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

            var user = new RegisterUserRequest()
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
            var response = await userService.DeleteUserAsync(new DeleteUserRequest()
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
            var response = await userService.DeleteUserAsync(new DeleteUserRequest()
            {
                Id = "invalid-user-id",
            });

            // Assert
            Assert.NotNull(response.Error);
            Assert.Equal(404, response.Error.StatusCode);
        }
    }
}
