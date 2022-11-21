using ChatAPI.Exceptions;
using ChatAPI.Models;
using ChatAPI.Models.Enumerables;
using ChatAPI.Repositories.Interfaces;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services
{
    public class ConversationUserService : IConversationUserService
    {
        private readonly IConversationUserRepository _conversationUserRepository;

        public ConversationUserService(
            IConversationUserRepository conversationUserRepository)
        {
            _conversationUserRepository = conversationUserRepository;
        }

        public async Task<List<ConversationUser>> CreateConversationUsersAsync(List<int> userIds, int conversationId)
        {
            try
            {
                return await _conversationUserRepository.CreateConversationUsersAsync(
                userIds.Select(x => new ConversationUser()
                {
                    ConversationId = conversationId,
                    UserId = x
                }).ToList());
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }
    }
}
