

using Backend.Configuration;
using Backend.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Auth
{

  // Request/Reply dto
  public record LoginRequest(string Username, string Password);
  public record LoginReplay(string Access_token, string Username);
  public record CreateUserRequest(string Username, string Password);
  public record TicketPermission(bool Create, bool Edit, bool Delete, bool Resolve); // Very simple permission
  public record MeReply(
    string Username,
    TicketPermission TicketPermission
  );



  [Route("api/[Controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
      var username = User.Identity.Name;
      var user = await context.Users.SingleOrDefaultAsync(user => user.Username.SequenceEqual(username));
      if (user != null)
      {
        TicketPermission permission = null;

        if(user.UserType == UserType.QA)
        {
          permission = new TicketPermission(true, true, true, false);
        } else if(user.UserType == UserType.RD)
        {
          permission = new TicketPermission(false, false, false, true);
        }

        // TODO: for user ADMIN and PM

        return Ok(new Replay(true, new MeReply(username, permission)));
      }


      var noUserPermission = new MeReply(
        username,
        TicketPermission: new TicketPermission(false, false, false, false)
      );

      return Ok(new Replay(true, noUserPermission));
    }


    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {


      var user = await context.Users.SingleOrDefaultAsync(user => user.Username.SequenceEqual(request.Username));
      if (user == null)
      {
        return Ok(ReplyBuilder.Error("err.user.notfound"));
      }

      if (!user.PasswordHash.SequenceEqual(await passwordHasher.Hash(request.Password, user.Salt)))
      {
        return Ok(ReplyBuilder.Error("err.user.notfound"));
      }

      var token = tokenService.CreateToken(user.Username, "api");

      return Ok(new Replay(true, new LoginReplay(token, user.Username)));
    }

    public AuthController(TTSContext context, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
      this.tokenService = tokenService;
      this.passwordHasher = passwordHasher;
      this.context = context;
    }

    private readonly IPasswordHasher passwordHasher;
    private readonly ITokenService tokenService;
    private readonly TTSContext context;

  }
}
