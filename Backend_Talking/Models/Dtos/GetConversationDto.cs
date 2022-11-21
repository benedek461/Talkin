using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ChatAPI.Models.Dtos
{
    public class GetConversationDto
    {
        public int Id { get; set; }
        public List<int> Partitioners { get; set; }
        public List<MessageDto> Messages { get; set; }
    }
}
