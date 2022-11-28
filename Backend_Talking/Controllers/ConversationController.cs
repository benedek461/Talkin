using ChatAPI.Exceptions;
using ChatAPI.Models.Dtos;
using ChatAPI.Models.Enumerables;
using ChatAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(
            IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CreateConversationDto>> CreateAsync(List<int> partnerIds)
        {
            try
            {
                return await _conversationService.CreateAsync(partnerIds);
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }

        
        [HttpGet]
        [Route("Ids")]
        [Authorize]
        public async Task<ActionResult<List<int>>> GetConversationIdsAsync([FromQuery]List<int> partnerIds)
        {
            try
            {
                return await _conversationService.GetConversationIdsByPartnerIdsAsync(partnerIds);
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }

        [HttpGet]
        [Route("{conversationId}")]
        [Authorize]
        public async Task<ActionResult<GetConversationDto>> GetConversationByIdAsync([FromRoute]int conversationId)
        {
            try
            {
                return await _conversationService.GetConversationByIdAsync(conversationId);
            }
            catch(ChatApiException ex)
            {
                return StatusCode(500, ((ErrorCodes)ex.ErrorCode).ToString());
            }
        }

        //[HttpGet]
        //[Authorize]
        //public async Task
    }
}
