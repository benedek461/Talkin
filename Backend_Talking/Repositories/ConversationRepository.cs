using ChatAPI.Data;
using ChatAPI.Models;
using ChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ChatDbContext _chatDbContext;

        public ConversationRepository(
            ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public async Task<int> CreateAsync()
        {
            var conversation = new Conversation();

            await _chatDbContext.Conversations.AddAsync(conversation);
            await _chatDbContext.SaveChangesAsync();

            return conversation.Id;
        }

        public Task<Conversation?> GetByIdAsync(int id)
        {
            return _chatDbContext.Conversations
                .AsNoTracking()
                .Include(conv => conv.Partitioners)
                .Include(conv => conv.Messages)
                .FirstOrDefaultAsync(conv => conv.Id == id);
        }
    }
}
