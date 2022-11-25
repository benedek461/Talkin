namespace ChatAPI.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public List<int> ConversationIds { get; set; }
    }
}
