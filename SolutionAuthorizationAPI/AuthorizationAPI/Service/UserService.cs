using AuthorizationAPI.Exceptions;
using AuthorizationAPI.Model;
using AuthorizationAPI.Repository;

namespace AuthorizationAPI.Service
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly JwtTokenGenerator _jwtTokenGenerator;

		public UserService(IUserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
		{
			_userRepository = userRepository;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<User> AuthenticateUserAsync(string username, string password)
		{
			var user = await _userRepository.GetUserByUsernameAsync(username);
			if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
				return null;

			return user;
		}

		public async Task<string> GenerateJwtTokenAsync(User user)
		{
			return _jwtTokenGenerator.GenerateToken(user.Username);
		}

		public async Task CreateUserAsync(User user)
		{
			// Check if a user with the same username already exists
			var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);

			if (existingUser != null)
			{
				throw new UsernameTakenException("Username is already taken.");
			}

			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			await _userRepository.CreateUserAsync(user);
		}
	}
}
