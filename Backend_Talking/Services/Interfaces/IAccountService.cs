using ChatAPI.Models;
using ChatAPI.Models.Dtos;

namespace ChatAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<User?> LoginAsync(LoginDto loginDto);
    }
}