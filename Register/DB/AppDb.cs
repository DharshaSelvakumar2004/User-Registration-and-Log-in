using Microsoft.EntityFrameworkCore;
using Register.Entity;

namespace Register.DB
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb>options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
