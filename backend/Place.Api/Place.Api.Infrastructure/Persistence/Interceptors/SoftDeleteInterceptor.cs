namespace Place.Api.Infrastructure.Persistence.Interceptors;

using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc />
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        SoftDelete(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    /// <inheritdoc />
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        SoftDelete(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


    /// <summary>
    /// Applies soft delete logic to entities marked for deletion.
    /// </summary>
    /// <param name="context">The database context containing the entities to be soft deleted.</param>
    private static void SoftDelete(DbContext? context)
    {
        foreach (EntityEntry entry in context?.ChangeTracker.Entries()!)
        {
            if (entry is not { Entity: ISoftDeletableEntity deletable, State: EntityState.Deleted })
            {
                continue;
            }

            entry.State = EntityState.Modified;
            deletable.Deleted = true;
            deletable.DeletedOnUtc = DateTime.UtcNow;
        }
    }
}
