using ChatAPI.Models;

namespace ChatAPI.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        Task<int> CreateAsync();
        Task<Conversation?> GetByIdAsync(int id);
    }
}