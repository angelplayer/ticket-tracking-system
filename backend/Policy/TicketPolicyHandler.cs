// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the MIT license.  See License.txt in the project root for license information.

using Backend.Domain;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Policy
{
  public class TicketPolicyRequirement : IAuthorizationRequirement
  {
    public UserType UserType { get; private set; }

    public TicketPolicyRequirement(UserType userType)
    {
      UserType = userType;
    }
  }

  public class TicketPolicyHandler : AuthorizationHandler<TicketPolicyRequirement>
  {
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TicketPolicyRequirement requirement)
    {
      var user = await userService.GetUserById(context.User.Identity.Name);

      if (user.UserType == requirement.UserType)
      {
        context.Succeed(requirement);
      }
    }


    public TicketPolicyHandler(IUserService userService)
    {
      this.userService = userService;
    }

    private readonly IUserService userService;
  }
}
