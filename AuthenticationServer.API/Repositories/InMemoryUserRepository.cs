using AuthenticationServer.API.Models.Domain_Objects;
using AuthenticationServer.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.API.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public Task<User> Create(User user)
        {
            user.user_ID = Guid.NewGuid();
            _users.Add(user);

            return Task.FromResult(user);
        }
        public Task<User> GetById(Guid id)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.user_ID == id));
        }
        public Task<User> GetByEmail(string email)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.email == email));
        }
        public Task<User> GetByUsername(string username)
        {
            return Task.FromResult(_users.FirstOrDefault(x => x.userName == username));
        }
    }
}
