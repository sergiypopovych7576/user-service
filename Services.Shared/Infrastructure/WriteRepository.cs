using Microsoft.EntityFrameworkCore;
using Services.Shared.Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Services.Shared.Infrastructure
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntity, new()
    {
        private readonly IWriteDbContext _context;
        private readonly DbSet<T> _set;

        public WriteRepository(IWriteDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<T>();
        }

        public async ValueTask<T> Create(T entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async ValueTask Delete(Guid id)
        {
            T entity = await _set.FindAsync(id);

            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async ValueTask<T> Update(T entity)
        {
            T entityToUpdate = await _set.FindAsync(entity.Id);

            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);

            return entityToUpdate;
        }
    }
}
