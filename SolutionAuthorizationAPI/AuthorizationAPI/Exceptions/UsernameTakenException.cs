namespace AuthorizationAPI.Exceptions
{
	public class UsernameTakenException: Exception
	{
		public UsernameTakenException(string message) : base(message) { }
	}
}
