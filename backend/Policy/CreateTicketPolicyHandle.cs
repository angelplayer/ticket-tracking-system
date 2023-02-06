

using Backend.Domain;
using Backend.Features.Ticket;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Policy
{

  public class CreateTicketRequirement : IAuthorizationRequirement
  {
  }

  public class CreateTicketPolicyHandle : AuthorizationHandler<CreateTicketRequirement, CreateTicketRequest>
  {

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateTicketRequirement requirement, CreateTicketRequest request)
    {
      var user = await userService.GetUserById(context.User.Identity.Name);
			
			if(request.type == TicketType.Bug) {
				// Only QA can create bug ticket
				if(user.UserType == UserType.QA) {
					context.Succeed(requirement);
				}

			} else if(request.type == TicketType.FeatureRequest) 
			{
				// TODO: Implement in Phase II
			}
    }

    public CreateTicketPolicyHandle(IUserService userService)
    {
      this.userService = userService;
    }

    private IUserService userService;
  }
}