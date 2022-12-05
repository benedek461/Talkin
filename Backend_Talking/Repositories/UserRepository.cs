using AutoMapper;
using Backend_Talking.Models.Dtos;
using ChatAPI.Data;
using ChatAPI.Models;
using ChatAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _chatDbContext;
        private readonly IMapper _mapper;

        public UserRepository(
            ChatDbContext chatDbContext,
            IMapper mapper)
        {
            _chatDbContext = chatDbContext;
            _mapper = mapper;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _chatDbContext.AddAsync(user);
            await _chatDbContext.SaveChangesAsync();

            return user;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            return _chatDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return _chatDbContext.Users
                .Include(user => user.Conversations)
                .ThenInclude(conv => conv.Partitioners)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return _chatDbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.userName == username);
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _chatDbContext.Users
                .AsNoTracking()
                .Include(user => user.Conversations)
                .ToListAsync();
        }

        public async Task<User?> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var user = await _chatDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == updateUserDto.Id);

            if (user != null) 
            {
                _mapper.Map(updateUserDto, user);

                await _chatDbContext.SaveChangesAsync();
            }

            return user;
        }
    }
}
