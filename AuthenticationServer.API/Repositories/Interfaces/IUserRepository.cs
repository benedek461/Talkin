using AuthenticationServer.API.Models.Domain_Objects;
using System;
using System.Threading.Tasks;

namespace AuthenticationServer.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task<User> GetByUsername(string username);
        Task<User> GetByEmail(string email);
        Task<User> Create(User user);
    }
}
