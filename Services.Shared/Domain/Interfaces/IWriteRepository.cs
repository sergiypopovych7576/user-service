using System;
using System.Threading.Tasks;

namespace Services.Shared.Domain.Interfaces
{
    public interface IWriteRepository<T>
    {
        ValueTask<T> Create(T entity);
        ValueTask<T> Update(T entity);
        ValueTask Delete(Guid id);
    }
}
