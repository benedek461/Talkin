using AutoMapper;
using ChatAPI.Exceptions;
using ChatAPI.Models;
using ChatAPI.Models.Dtos;
using ChatAPI.Models.Enumerables;
using ChatAPI.Repositories.Interfaces;
using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextService _httpContextService;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextService httpContextService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextService = httpContextService;
        }

        public async Task<User> RegisterUserAsync(CreateUserDto createUserDto)
        {
            try
            {
                return await _userRepository.CreateAsync(_mapper.Map<User>(createUserDto));
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }


        public async Task<bool> ValidateEmailAsync(string email)
        {
            if (await _userRepository.GetByEmailAsync(email) == null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateUsernameAsync(string username)
        {
            if (await _userRepository.GetByUsernameAsync(username) == null)
            {
                return true;
            }
            return false;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _userRepository.GetByUsernameAsync(username);
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _userRepository.GetByEmailAsync(email);
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }

        public async Task<User?> GetDomainUserAsync()
        {
            try
            {
                return await _userRepository.GetByIdAsync(_httpContextService.UserId);
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetUsersAsync();

                return users.Select(x => _mapper.Map<UserDto>(x)).ToList();
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }

        public async Task<UserDto> GetUserAsync()
        {
            try
            {
                return _mapper.Map<UserDto>(await _userRepository.GetByIdAsync(_httpContextService.UserId));
            }
            catch
            {
                throw new ChatApiException((int)ErrorCodes.DatabaseError);
            }
        }
    }
}
