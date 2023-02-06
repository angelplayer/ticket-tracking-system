

namespace Backend.Configuration
{
  public interface ITokenService
  {
    string CreateToken(string username, params string[] scopes);
  }
}