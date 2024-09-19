using AuthorizationAPI.Model;

namespace AuthorizationAPI.Service
{
	public interface IUserService
	{
		Task<User> AuthenticateUserAsync(string username, string password);
		Task<string> GenerateJwtTokenAsync(User user);
		Task CreateUserAsync(User user);
	}
}
