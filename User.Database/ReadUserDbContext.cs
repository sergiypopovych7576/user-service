using Microsoft.EntityFrameworkCore;
using Services.Shared.Infrastructure.Contexts;

namespace User.Database
{
    public class ReadUserDbContext : UserDbContext, IReadDbContext
    {
        public ReadUserDbContext(DbContextOptions<ReadUserDbContext> options) : base(options)
        {
        }
    }
}
