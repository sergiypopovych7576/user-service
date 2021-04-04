using Microsoft.EntityFrameworkCore;
using Services.Shared.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Shared.Infrastructure
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IEntity, new()
    {
        private readonly IReadDbContext _context;
        private readonly DbSet<T> _set;

        public ReadRepository(IReadDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _set = _context.Set<T>();
        }

        public IQueryable<T> Get()
        {
            return _set;
        }

        public ValueTask<T> FindAsync(Guid id)
        {
            return _set.FindAsync(id);
        }
    }
}
