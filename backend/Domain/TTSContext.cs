using Microsoft.EntityFrameworkCore;

namespace Backend.Domain
{
  public class TTSContext : DbContext
  {
    public TTSContext(DbContextOptions<TTSContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().ToTable("Users");
    }

  }
}