using ChatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Data
{
    public class ChatDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationUser> ConversationUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

        public ChatDbContext(DbContextOptions options) :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Conversations)
                .WithMany(u => u.Partitioners)
                .UsingEntity<ConversationUser>(
                    j => j
                        .HasOne(uc => uc.Conversation)
                        .WithMany(c => c.ConversationUsers)
                        .HasForeignKey(uc => uc.ConversationId),
                    j => j
                        .HasOne(uc => uc.User)
                        .WithMany(u => u.ConversationUsers)
                        .HasForeignKey(uc => uc.UserId)
                );
        }
    }
}
