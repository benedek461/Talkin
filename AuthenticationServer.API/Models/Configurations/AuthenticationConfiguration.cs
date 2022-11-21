namespace AuthenticationServer.API.Models.Configurations
{
    public class AuthenticationConfiguration
    {
        public string Key { get; set; }
        public int ExpirationMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
