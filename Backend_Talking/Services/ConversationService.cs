using AutoMapper;
using ChatAPI.Exceptions;
using ChatAPI.Models.Dtos;
using ChatAPI.Models.Enumerables;
using ChatAPI.Repositories.Interfaces;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IConversationUserService _conversationUserService;
        private readonly IUserService _userService;
        private readonly IHttpContextService _httpContextService;
        private readonly IMapper _mapper;

        public ConversationService(
            IConversationRepository conversationRepository,
            IConversationUserService conversationUserService,
            IUserService userService,
            IHttpContextService httpContextService,
            IMapper mapper)
        {
            _conversationRepository = conversationRepository;
            _conversationUserService = conversationUserService;
            _userService = userService;
            _httpContextService = httpContextService;
            _mapper = mapper;
        }

        public async Task<CreateConversationDto> CreateAsync(List<int> partnerIds)
        {
            try
            {
                var conversationId = await _conversationRepository.CreateAsync();
                partnerIds.Add(_httpContextService.UserId);

                await _conversationUserService.CreateConversationUsersAsync(partnerIds, conversationId);

                return new CreateConversationDto()
                {
                    Id = conversationId,
                    Partitioners = partnerIds
                };
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }

        public async Task<List<int>> GetConversationIdsByPartnerIdsAsync(List<int> partnerIds)
        {
            partnerIds.Add(_httpContextService.UserId);
            var user = await _userService.GetDomainUserAsync();

            return user.Conversations
                .Where(x => x.Partitioners.Select(y => y.Id).Except(partnerIds).ToList().Count == 0)
                .Select(x => x.Id)
                .ToList();
        }

        public async Task<GetConversationDto> GetConversationByIdAsync(int conversationId)
        {
            try
            {
                var conversation = await _conversationRepository.GetByIdAsync(conversationId);

                return _mapper.Map<GetConversationDto>(conversation);
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }

        public async Task<List<string>> GetPartnerIdsByIdAsync(int conversationId)
        {
            try
            {
                var conversation = await _conversationRepository.GetByIdAsync(conversationId);

                return conversation.Partitioners
                    .Where(x => x.Id != _httpContextService.UserId)
                    .Select(y => y.Id.ToString())
                    .ToList();
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }
    }
}
