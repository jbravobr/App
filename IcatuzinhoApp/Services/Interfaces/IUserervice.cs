using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> Login(string email, string password);
        Task<bool> GetAuthenticatedUser();
    }
}

