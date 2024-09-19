using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationAPI.Controllers;
using AuthorizationAPI.Model;
using AuthorizationAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuthorizationAPI.Tests.Controller
{
	internal class AuthControllerTests
	{
		private Mock<IUserService> _userServiceMock;
		private AuthController _authController;

		[SetUp]
		public void Setup()
		{
			_userServiceMock = new Mock<IUserService>();
			_authController = new AuthController(_userServiceMock.Object);
		}

		[Test]
		public async Task Login_ShouldReturnOk_WhenUserIsAuthenticated()
		{
			// Arrange
			var request = new LoginRequestDto { Username = "testuser", Password = "password" };
			var user = new User { Username = "testuser" };
			var token = "jwt-token";

			_userServiceMock.Setup(us => us.AuthenticateUserAsync(request.Username, request.Password))
							.ReturnsAsync(user);
			_userServiceMock.Setup(us => us.GenerateJwtTokenAsync(user))
							.ReturnsAsync(token);

			// Act
			var result = await _authController.Login(request) as OkObjectResult;			

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);

			var response = result.Value as LoginResponseDto;
			Assert.IsNotNull(response);
			Assert.AreEqual(token, response.Token);
		}

		[Test]
		public async Task Login_ShouldReturnUnauthorized_WhenUserIsNotAuthenticated()
		{
			// Arrange
			var request = new LoginRequestDto { Username = "testuser", Password = "password" };

			_userServiceMock.Setup(us => us.AuthenticateUserAsync(request.Username, request.Password))
							.ReturnsAsync((User)null);

			// Act
			var result = await _authController.Login(request) as UnauthorizedResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(401, result.StatusCode);
		}

		[Test]
		public async Task Register_ShouldReturnOk_WhenUserIsCreated()
		{
			// Arrange
			var request = new RegisterRequestDto { Username = "newuser", Password = "newpassword" };
			var user = new User { Username = request.Username, Password = request.Password };

			_userServiceMock.Setup(us => us.CreateUserAsync(user))
							.Returns(Task.CompletedTask);

			// Act
			var result = await _authController.Register(request) as OkResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
		}
	}
}
