using ChatAPI.Data;
using ChatAPI.Models;
using ChatAPI.Repositories.Interfaces;

namespace ChatAPI.Repositories
{
    public class ConversationUserRepository : IConversationUserRepository
    {
        private readonly ChatDbContext _chatDbContext;

        public ConversationUserRepository(
            ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public async Task<List<ConversationUser>> CreateConversationUsersAsync(List<ConversationUser> conversationUsers)
        {
            await _chatDbContext.ConversationUsers.AddRangeAsync(conversationUsers);
            await _chatDbContext.SaveChangesAsync();

            return conversationUsers;
        }
    }
}
