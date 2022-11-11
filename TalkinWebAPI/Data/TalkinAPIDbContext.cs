using TalkinWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TalkinWebAPI.Data
{
    public class TalkinAPIDbContext : DbContext
    {
        public TalkinAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<Message> Message { get; set; }
    }
}
