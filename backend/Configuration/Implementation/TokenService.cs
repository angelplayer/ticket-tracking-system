

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Configuration.Implementation
{
  public class TokenService : ITokenService
  {

    private readonly JwtIssuerOptions jwtOptions;

    public TokenService(IOptions<JwtIssuerOptions> options)
    {
      this.jwtOptions = options.Value;
    }

    public string CreateToken(string username, params string[] scopes)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var scopeClaim = new Claim[] {
        new Claim(ClaimTypes.NameIdentifier, username),
        new Claim("scope", string.Join(" ", scopes))
      };

      var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
      {
        Issuer = jwtOptions.Issuer,
        Audience = jwtOptions.Audience,
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = jwtOptions.SigningCredentials,
        Subject = new ClaimsIdentity(scopeClaim)
      });

      return tokenHandler.WriteToken(token);
    }
  }
}