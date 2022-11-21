using AuthenticationServer.API.Models.Domain_Objects;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationServer.API.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tokens> Tokens { get; set; }

    }
}
