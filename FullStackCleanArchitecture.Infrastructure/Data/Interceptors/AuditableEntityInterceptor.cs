﻿using FullStackCleanArchitecture.Application.Common.Interfaces;
using FullStackCleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FullStackCleanArchitecture.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly TimeProvider _dateTime;

    public AuditableEntityInterceptor(TimeProvider dateTime)
    {
        _dateTime = dateTime;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "User"; //fixed
                entry.Entity.CreatedDate = _dateTime.GetUtcNow();
                entry.Entity.IsDelete = false;
            } 
            else if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = "User"; //fixed
                entry.Entity.LastModifiedDate = _dateTime.GetUtcNow();
                entry.Entity.IsDelete = false;
            }
            else if(entry.State == EntityState.Deleted)
            {
                entry.Entity.LastModifiedBy = "User"; //fixed
                entry.Entity.LastModifiedDate = _dateTime.GetUtcNow();
                entry.Entity.IsDelete = true;
                entry.State = EntityState.Modified;
            }
            
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
        entry.References.Any(r => 
            r.TargetEntry != null && 
            r.TargetEntry.Metadata.IsOwned() && 
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
