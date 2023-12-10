namespace Place.Api.Infrastructure.Persistence.EF.Contexts;

using Authentication.Configurations;
using Microsoft.EntityFrameworkCore;
using Place.Api.Domain.Authentication;
using Place.Api.Infrastructure.Persistence.Interceptors;

public sealed class WriteDbContext(DbContextOptions<WriteDbContext> options,
        UpdateAuditableEntitiesInterceptor updateAuditableEntitiesInterceptor,
        SoftDeleteInterceptor softDeleteInterceptor)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("user");
        WriteConfiguration configuration = new WriteConfiguration();
        modelBuilder.ApplyConfiguration(configuration);

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
