using Microsoft.EntityFrameworkCore;

namespace User.Database
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public virtual DbSet<Domain.Entities.User> Users { get; set; }
    }
}
