using ChatAPI.Models;

namespace ChatAPI.Services.Interfaces
{
    public interface IConversationUserService
    {
        Task<List<ConversationUser>> CreateConversationUsersAsync(List<int> userIds, int conversationId);
    }
}