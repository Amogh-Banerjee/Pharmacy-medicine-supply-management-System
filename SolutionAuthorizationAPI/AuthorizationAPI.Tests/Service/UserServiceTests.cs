using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAPI.Model;
using AuthorizationAPI.Repository;
using AuthorizationAPI.Service;
using Microsoft.Extensions.Options;
using Moq;

namespace AuthorizationAPI.Tests.Service
{
	internal class UserServiceTests
	{
		private Mock<IUserRepository> _userRepositoryMock;
		private Mock<IOptions<JwtSettings>> _jwtSettingsOptionsMock;
		private Mock<JwtTokenGenerator> _jwtTokenGeneratorMock;
		private UserService _userService;

		[SetUp]
		public void SetUp()
		{
			_userRepositoryMock = new Mock<IUserRepository>();
			_jwtSettingsOptionsMock = new Mock<IOptions<JwtSettings>>();
			_jwtSettingsOptionsMock.Setup(opt => opt.Value).Returns(new JwtSettings());

			// Use the actual JwtTokenGenerator constructor with mocked IOptions<JwtSettings>
			var jwtSettings = new JwtSettings(); // Set properties as needed
			_jwtTokenGeneratorMock = new Mock<JwtTokenGenerator>(_jwtSettingsOptionsMock.Object);

			_userService = new UserService(_userRepositoryMock.Object, _jwtTokenGeneratorMock.Object);
		}

		[Test]
		public async Task AuthenticateUserAsync_UserNotFound_ReturnsNull()
		{
			// Arrange
			var username = "testuser";
			var password = "password";
			_userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(username)).ReturnsAsync((User)null);

			// Act
			var result = await _userService.AuthenticateUserAsync(username, password);

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public async Task AuthenticateUserAsync_PasswordDoesNotMatch_ReturnsNull()
		{
			// Arrange
			var username = "testuser";
			var password = "wrongpassword";
			var user = new User { Username = username, Password = BCrypt.Net.BCrypt.HashPassword("correctpassword") };
			_userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(username)).ReturnsAsync(user);

			// Act
			var result = await _userService.AuthenticateUserAsync(username, password);

			// Assert
			Assert.IsNull(result);
		}

		[Test]
		public async Task AuthenticateUserAsync_SuccessfulAuthentication_ReturnsUser()
		{
			// Arrange
			var username = "testuser";
			var password = "correctpassword";
			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
			var user = new User { Username = username, Password = hashedPassword };
			_userRepositoryMock.Setup(repo => repo.GetUserByUsernameAsync(username)).ReturnsAsync(user);

			// Act
			var result = await _userService.AuthenticateUserAsync(username, password);

			// Assert
			Assert.AreEqual(user, result);
		}

		[Test]
		public async Task GenerateJwtTokenAsync_ValidUser_ReturnsToken()
		{
			// Arrange
			var user = new User { Username = "testuser" };
			var token = "generated.jwt.token";
			_jwtTokenGeneratorMock.Setup(gen => gen.GenerateToken(user.Username)).Returns(token);

			// Act
			var result = await _userService.GenerateJwtTokenAsync(user);

			// Assert
			Assert.AreEqual(token, result);
		}

		[Test]
		public async Task CreateUserAsync_ValidUser_HashesPasswordAndCreatesUser()
		{
			// Arrange
			var user = new User { Username = "testuser", Password = "plainpassword" };
			var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
			_userRepositoryMock.Setup(repo => repo.CreateUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);

			// Act
			await _userService.CreateUserAsync(user);

			// Assert
			_userRepositoryMock.Verify(repo => repo.CreateUserAsync(It.Is<User>(u =>
				u.Username == user.Username && !string.IsNullOrEmpty(u.Password)
			)), Times.Once);

			// Additional assertion to verify password hash
			var userPassedToRepo = _userRepositoryMock.Invocations
				.FirstOrDefault()?.Arguments.FirstOrDefault() as User;

			Assert.IsNotNull(userPassedToRepo);
			Assert.IsTrue(BCrypt.Net.BCrypt.Verify("plainpassword", userPassedToRepo.Password));
		}


	}
}
