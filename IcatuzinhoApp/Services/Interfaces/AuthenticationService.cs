using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IAuthenticationCodeService : IBaseService<AuthenticationCode>
    {
        Task<AuthenticationToken> DoAuthentication(string username, string password, bool isEncrypted);
    }
}

