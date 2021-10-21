using System;
using System.Threading.Tasks;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories.Base;

namespace AuthServer.Core.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        public Task<User> RegisterUserAsync(User user);
        public Task<User> FetchUserAsync(string username);
    }
}
