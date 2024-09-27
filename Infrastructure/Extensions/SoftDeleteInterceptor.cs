using Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Domain.Enums;

namespace Infrastructure.Extensions
{
    public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        private readonly IServiceProvider _serviceProvider;
        public SoftDeleteInterceptor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;
                var userContext = scopedServiceProvider.GetRequiredService<IUserContext>();

                string _userId = userContext.GetCurrentUser().Id;


                if (eventData.Context is null)
                {
                    return base.SavingChangesAsync(eventData, result, cancellationToken);
                }

                IEnumerable<EntityEntry<BaseEntity>> entries = eventData
                        .Context
                        .ChangeTracker
                        .Entries<BaseEntity>()
                        .Where(e => e.State == EntityState.Deleted);

                foreach (EntityEntry<BaseEntity> softDeletable in entries)
                {
                    softDeletable.State = EntityState.Modified;
                    softDeletable.Entity.Status = (Domain.Enums.Status)Status.Disabled;
                    softDeletable.Entity.LastUpdatedDate = DateTime.UtcNow;
                    softDeletable.Entity.LastUpdatedBy = _userId;
                }

                IEnumerable<EntityEntry<ApplicationUser>> entityEntries = eventData
                    .Context
                        .ChangeTracker
                        .Entries<ApplicationUser>()
                        .Where(e => e.State == EntityState.Deleted);


                foreach (EntityEntry<ApplicationUser> softDeletable in entityEntries)
                {
                    softDeletable.State = EntityState.Modified;
                    softDeletable.Entity.Status = (Domain.Enums.Status)Status.Disabled;
                    softDeletable.Entity.LastUpdatedDate = DateTime.UtcNow;
                }

                try
                {
                    return base.SavingChangesAsync(eventData, result, cancellationToken);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    throw;
                }
            }
        }

    }
}
