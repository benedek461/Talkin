using ChatAPI.Models;
using ChatAPI.Models.Dtos;

namespace ChatAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(CreateUserDto createUserDto);
        Task<bool> ValidateEmailAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserAsync();
        Task<List<UserDto>> GetUsersAsync();
    }
}