
using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
	public class UserService:  IUserService
	{

    async Task<User> IUserService.GetUserById(string userid)
    {
      var user = await context.Users
			.SingleOrDefaultAsync(user => user.Id.ToString().SequenceEqual(userid));

			return user ?? throw new DomainException("user_not_found", DomainExceptionCode.NotFound);
    }

    public UserService(TTSContext context) {
			this.context = context;
		}

		private  readonly TTSContext context;
	}
}