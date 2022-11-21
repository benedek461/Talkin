using ChatAPI.Data;
using ChatAPI.Models;
using ChatAPI.Repositories.Interfaces;

namespace ChatAPI.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _chatDbContext;

        public MessageRepository(
            ChatDbContext chatDbContext)
        {
            _chatDbContext = chatDbContext;
        }

        public async Task<Message> CreateAsync(Message message)
        {
            await _chatDbContext.Messages.AddAsync(message);
            await _chatDbContext.SaveChangesAsync();

            return message;
        }
    }
}
