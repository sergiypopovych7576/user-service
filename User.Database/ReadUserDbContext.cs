using Microsoft.EntityFrameworkCore;
using Services.Shared.Domain.Interfaces;

namespace User.Database
{
    public class ReadUserDbContext : UserDbContext, IReadDbContext
    {
        public ReadUserDbContext(DbContextOptions<ReadUserDbContext> options) : base(options)
        {
        }
    }
}
