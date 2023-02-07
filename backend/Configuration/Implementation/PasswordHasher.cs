

using System.Security.Cryptography;
using System.Text;

namespace Backend.Configuration.Implementation
{
  public class PasswordHasher : IPasswordHasher
  {
    private readonly HMACSHA512 x = new(Encoding.UTF8.GetBytes("ASSETS_SERVER"));

    public void Dispose() => x.Dispose();

    public Task<byte[]> Hash(string passwordHashed, byte[] salt)
    {
      var bytes = Encoding.UTF8.GetBytes(passwordHashed);

      var allBytes = new byte[bytes.Length + salt.Length];
      Buffer.BlockCopy(bytes, 0, allBytes, 0, bytes.Length);
      Buffer.BlockCopy(salt, 0, allBytes, bytes.Length, salt.Length);

      return x.ComputeHashAsync(new MemoryStream(allBytes));
    }
  }
}
