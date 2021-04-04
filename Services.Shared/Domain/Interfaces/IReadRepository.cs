using System;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Shared.Domain.Interfaces
{
    public interface IReadRepository<T>
    {
        IQueryable<T> Get();
        ValueTask<T> FindAsync(Guid id);
    }
}
