

using Microsoft.EntityFrameworkCore;

namespace Backend.Domain
{
  public class TTSContext : DbContext
  {
    public TTSContext(DbContextOptions<TTSContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Ticket> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<User>().ToTable("Users");
      modelBuilder.Entity<Ticket>().ToTable("Tickets");
    }

  }
}
