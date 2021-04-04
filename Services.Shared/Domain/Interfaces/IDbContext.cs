using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Shared.Domain.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        EntityEntry Entry([NotNull] object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
