using ChatAPI.Models;

namespace ChatAPI.Repositories.Interfaces
{
    public interface IConversationUserRepository
    {
        Task<List<ConversationUser>> CreateConversationUsersAsync(List<ConversationUser> conversationUsers);
    }
}