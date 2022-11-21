using ChatAPI.Models;
using ChatAPI.Models.Dtos;

namespace ChatAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessageAsync(CreateMessageDto createMessageDto);
    }
}