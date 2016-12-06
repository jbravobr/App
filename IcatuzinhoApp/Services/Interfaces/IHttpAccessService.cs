using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IHttpAccessService
    {
        HttpClient Init(string accessToken = null);
    }
}

