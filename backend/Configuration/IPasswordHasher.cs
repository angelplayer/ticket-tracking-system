
namespace Backend.Configuration
{
  public interface IPasswordHasher
  {
    Task<byte[]> Hash(string passwordHashed, byte[] salt);
    void Dispose();
  }
}