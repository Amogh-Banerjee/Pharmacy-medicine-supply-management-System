using AuthorizationAPI.Model;

namespace AuthorizationAPI.Repository
{
	public interface IUserRepository
	{
		Task<User> GetUserByUsernameAsync(string username);
		Task CreateUserAsync(User user);
	}
}
