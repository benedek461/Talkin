using ChatAPI.Models;
using ChatAPI.Models.Dtos;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginDto.Email);

            if (user == null || user.Password != loginDto.Password)
            {
                return null;
            }
            return user;
        }
    }
}
