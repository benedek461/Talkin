using AutoMapper;
using ChatAPI.Exceptions;
using ChatAPI.Models;
using ChatAPI.Models.Dtos;
using ChatAPI.Models.Enumerables;
using ChatAPI.Repositories.Interfaces;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(
            IMessageRepository messageRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<Message> CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            try
            {
                return await _messageRepository.CreateAsync(_mapper.Map<Message>(createMessageDto));
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }
    }
}
