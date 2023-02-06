

using Backend.Domain;

namespace Backend.Services
{
	public interface IUserService
	{
		Task<User> GetUserById(string userid);
	}
}