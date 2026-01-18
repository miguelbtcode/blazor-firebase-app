using Microsoft.EntityFrameworkCore;
using NetFirebase.Api.Models.Domain;

namespace NetFirebase.Api.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext() { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
}
