using ChatAPI.Models.Dtos;

namespace ChatAPI.Services.Interfaces
{
    public interface IConversationService
    {
        Task<CreateConversationDto> CreateAsync(List<int> partnerIds);
        Task<List<int>> GetConversationIdsByPartnerIdsAsync(List<int> partnerIds);
        Task<GetConversationDto> GetConversationByIdAsync(int conversationId);
        Task<List<string>> GetPartnerIdsByIdAsync(int conversationId);
    }
}