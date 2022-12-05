namespace Backend_Talking.Models.Dtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string? userName { get; set; }
        public string? Password { get; set; }
    }
}
