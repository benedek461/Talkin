using AuthenticationServer.API.Models.Domain_Objects;
using System;
using System.Threading.Tasks;

namespace AuthenticationServer.API.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task StoreToken(Tokens tokens);
        Task DeleteToken(Guid id);
        Task GetByToken(string token);
        Task GetTokenById(Guid id);
    }
}
