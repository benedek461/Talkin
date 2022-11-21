using AuthenticationServer.API.Data;
using AuthenticationServer.API.Models.Domain_Objects;
using AuthenticationServer.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AuthenticationServer.API.Repositories
{
    public class DatabaseUserRepository : IUserRepository
    {
        public readonly DatabaseContext _context;

        public DatabaseUserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.user_ID == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.userName == username);
        }
    }
}
