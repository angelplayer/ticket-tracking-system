

using Backend.Configuration;
using Backend.Domain;

namespace DbSetup
{
  public static class DbSetup
  {

    public static async Task SetupAsync(TTSContext context, IPasswordHasher passwordHasher)
    {
      if (context.Users.Any())
      {
        return;
      }

      var salt = Guid.NewGuid().ToByteArray(); // 

      var qaElysia = new User() { Id = Guid.NewGuid(), Username = "elysia", UserType = UserType.QA, PasswordHash = await passwordHasher.Hash("123", salt), Salt = salt };
      var rdKevin = new User() { Id = Guid.NewGuid(), Username = "kevin", UserType = UserType.RD, PasswordHash = await passwordHasher.Hash("321", salt), Salt = salt };
      var pmEden = new User() { Id = Guid.NewGuid(), Username = "eden", UserType = UserType.PM, PasswordHash = await passwordHasher.Hash("456", salt), Salt = salt };

      context.Users.AddRange(qaElysia, rdKevin, pmEden);
      await context.SaveChangesAsync();

    }

  }
}
