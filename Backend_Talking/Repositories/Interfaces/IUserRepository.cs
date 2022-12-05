using Backend_Talking.Models.Dtos;
using ChatAPI.Models;

namespace ChatAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetUsersAsync();
        Task<User?> UpdateAsync(UpdateUserDto updateUserDto);
    }
}