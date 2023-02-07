

using Backend.Configuration;
using Backend.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.Auth
{

  // Request/Reply dto
  public record LoginRequest(string Username, string Password);
  public record LoginReplay(string Access_token, string Username);
  public record CreateUserRequest(string Username, string Password);



  [Route("api/[Controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

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
