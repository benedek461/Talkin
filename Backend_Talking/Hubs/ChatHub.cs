using ChatAPI.Models.Dtos;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;

        public ChatHub(
            IMessageService messageService,
            IConversationService conversationService)
        {
            _messageService = messageService;
            _conversationService = conversationService;
        }

        public async Task SendMessage(int conversationId, string message)
        {
            var createMessageDto = new CreateMessageDto()
            {
                ConversationId = conversationId,
                Text = message,
                SenderId = Int32.Parse(Context.UserIdentifier)
            };

            await _messageService.CreateMessageAsync(createMessageDto);

            var partnerIds = await _conversationService.GetPartnerIdsByIdAsync(conversationId);

            await Clients.Users(partnerIds)
                .SendAsync("ReceiveMessage", conversationId, Context.UserIdentifier, message);
        }
    }
}
