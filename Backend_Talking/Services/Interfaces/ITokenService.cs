using ChatAPI.Models;

namespace ChatAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}