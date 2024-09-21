using AuthorizationAPI.Exceptions;
using AuthorizationAPI.Model;
using AuthorizationAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService = userService;
		}

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var user = await _userService.AuthenticateUserAsync(request.Username, request.Password);
				if (user == null)
					return Unauthorized();

				var token = await _userService.GenerateJwtTokenAsync(user);
				return Ok(new LoginResponseDto { Token = token });
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, new { 
					message = "An error occurred while processing your request.", detail = ex.Message });
			}
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				var user = new User
				{
					Username = request.Username,
					Password = request.Password
				};

				await _userService.CreateUserAsync(user);
				return Ok();
			}
			catch (UsernameTakenException ex)
			{
				return Conflict(new { message = ex.Message }); // HTTP 409 Conflict
			}
			catch (Exception ex)
			{
				return StatusCode(500, new { message = "An error occurred", details = ex.Message }); // HTTP 500 Internal Server Error
			}

		}
	}
}
