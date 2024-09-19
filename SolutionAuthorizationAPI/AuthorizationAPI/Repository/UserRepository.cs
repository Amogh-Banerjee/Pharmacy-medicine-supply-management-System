using AuthorizationAPI.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AuthorizationAPI.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly IMongoCollection<User> _users;

		public UserRepository(IOptions<MongoDbSettings> settings, IMongoClient client)
		{
			var database = client.GetDatabase(settings.Value.DatabaseName);
			_users = database.GetCollection<User>(settings.Value.UserCollection);
		}

		public async Task<User> GetUserByUsernameAsync(string username)
		{
			return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
		}

		public async Task CreateUserAsync(User user)
		{
			await _users.InsertOneAsync(user);
		}
	}
}
