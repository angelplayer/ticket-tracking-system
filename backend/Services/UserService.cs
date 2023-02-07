// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using Backend.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services
{
  public class UserService : IUserService
  {

    async Task<User> IUserService.GetUserById(string userid)
    {
      var user = await context.Users
            .SingleOrDefaultAsync(user => user.Username.ToString().SequenceEqual(userid));

      return user ?? throw new DomainException("user_not_found", DomainExceptionCode.NotFound);
    }

    public UserService(TTSContext context)
    {
      this.context = context;
    }

    private readonly TTSContext context;
  }
}
