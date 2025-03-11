using Microsoft.EntityFrameworkCore;
using Trade.Domain;

namespace Trade.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}