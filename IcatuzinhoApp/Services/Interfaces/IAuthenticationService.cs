using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IAuthenticationService : IBaseService<AuthenticationToken>
    {
        Task<bool> AuthenticationWithFormUrlEncoded(string username, string password, bool isEncrypted);
        Task<bool> RefreshToken();
    }
}

