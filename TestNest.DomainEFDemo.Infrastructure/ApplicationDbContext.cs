using Microsoft.EntityFrameworkCore;
using TestNest.DomainEFDemo.Guests;

namespace TestNest.DomainEFDemo.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Guest> Guests { get; set; }
    public DbSet<Nationality> Nationality { get; set; }
    public DbSet<Identification> Identification { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}