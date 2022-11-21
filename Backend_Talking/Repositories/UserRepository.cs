using ChatAPI.Data;
using ChatAPI.Models;
using ChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _chatDbContext;

        public UserRepository(
            ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _chatDbContext.AddAsync(user);
            await _chatDbContext.SaveChangesAsync();

            return user;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _chatDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return _chatDbContext.Users
                .Include(user => user.Conversations)
                .ThenInclude(conv => conv.Partitioners)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _chatDbContext.Users
                .AsNoTracking()
                .Include(user => user.Conversations)
                .ToListAsync();
        }
    }
}
