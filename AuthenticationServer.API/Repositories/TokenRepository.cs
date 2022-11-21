using AuthenticationServer.API.Data;
using AuthenticationServer.API.Models.Domain_Objects;
using AuthenticationServer.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace AuthenticationServer.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        public readonly DatabaseContext _context;
        public TokenRepository(DatabaseContext context)
        {
            _context= context;
        }

        public async Task DeleteToken(Guid id)
        {
            Tokens token = await _context.Tokens.FirstOrDefaultAsync(x => x.userRefID == id);
            if (token != null)
            {
                _context.Tokens.Remove(token);
                await _context.SaveChangesAsync();
            }
        }

        public Task GetByToken(string token)
        {
            return _context.Tokens.FirstOrDefaultAsync(x => x.token == token);
        }

        public async Task StoreToken(Tokens tokens)
        {
            _context.Tokens.Add(tokens);
            await _context.SaveChangesAsync();
        }

        public Task GetTokenById(Guid id)
        {
            return _context.Tokens.FirstOrDefaultAsync(x => x.userRefID == id);
        }
    }
}
