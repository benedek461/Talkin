using ChatAPI.Models;
using ChatAPI.Models.Dtos;

namespace ChatAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(CreateUserDto createUserDto);
        Task<bool> ValidateEmailAsync(string email);
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> ValidateUsernameAsync(string username);
        Task<User?> GetDomainUserAsync();
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto> GetUserAsync();
    }
}