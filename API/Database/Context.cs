using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
