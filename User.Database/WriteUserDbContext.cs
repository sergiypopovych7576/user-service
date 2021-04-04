using Microsoft.EntityFrameworkCore;
using Services.Shared.Domain.Interfaces;

namespace User.Database
{
    public class WriteUserDbContext : UserDbContext, IWriteDbContext
    {
        public WriteUserDbContext(DbContextOptions<WriteUserDbContext> options) : base(options)
        {
        }
    }
}
