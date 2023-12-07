namespace Place.Api.Infrastructure.Persistence.Authentication.EF.Contexts;

using Domain.Authentication;
using Interceptors;
using Microsoft.EntityFrameworkCore;

internal sealed class UserWriteDbContext(DbContextOptions<UserWriteDbContext> options,
        UpdateAuditableEntitiesInterceptor updateAuditableEntitiesInterceptor,
        SoftDeleteInterceptor softDeleteInterceptor)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .AddInterceptors(softDeleteInterceptor)
            .AddInterceptors(updateAuditableEntitiesInterceptor);

        base.OnConfiguring(optionsBuilder);
    }
}
