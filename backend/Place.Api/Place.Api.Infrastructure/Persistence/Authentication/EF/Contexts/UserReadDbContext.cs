namespace Place.Api.Infrastructure.Persistence.Authentication.EF.Contexts;

using Microsoft.EntityFrameworkCore;
using Models;

internal sealed class UserReadDbContext(DbContextOptions<UserReadDbContext> options) : DbContext(options)
{
    public DbSet<UserReadModel> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
