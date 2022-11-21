using ChatAPI.Models;

namespace ChatAPI.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> CreateAsync(Message message);
    }
}