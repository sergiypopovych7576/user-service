using Microsoft.EntityFrameworkCore;
using Services.Shared.Infrastructure.Contexts;

namespace User.Database
{
    public class WriteUserDbContext : UserDbContext, IWriteDbContext
    {
        public WriteUserDbContext(DbContextOptions<WriteUserDbContext> options) : base(options)
        {
        }
    }
}
