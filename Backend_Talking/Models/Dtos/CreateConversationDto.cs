using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.Dtos
{
    public class CreateConversationDto
    {
        public int Id { get; set; }
        public List<int> Partitioners { get; set; }
    }
}
