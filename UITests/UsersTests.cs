using NUnit.Framework;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class UsersTests
    {
        HttpClient _httpClient = Helpers.ReturnClient();
        private Mock<IUserService> mockService;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<IUserService>();
        }

        [Test]
        public async Task TryToAuthenticateWithFormsAuthentication()
        {
            try
            {
                var username = "teste@icatuseguros.com.br";
                var password = "Icatu123!";
                var isEncrypted = false;

                var client = new HttpClient
                {
                    BaseAddress = new Uri($"{Constants.BaseAddress}"),
                    Timeout = TimeSpan.FromSeconds(40)
                };

                var request = new HttpRequestMessage(HttpMethod.Post, $"{Constants.FormsAuthentication}");
                request.Content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username",username),
                    new KeyValuePair<string, string>("password",isEncrypted ?
                                                     Crypto.EncryptStringAES(password) :
                                                     password)
                });

                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var authenticationToken = JsonConvert.DeserializeObject<AuthenticationToken>(jsonString);

                    if (authenticationToken != null)
                        authenticationToken.SetExpirationTime();
                }

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Test]
        public async Task TryAuthenticateWithWrongUser()
        {
            var userEmail = "ramaral@icatuseguros.com.br";
            var userPassword = "123";
            var user = new User();

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync($"icatuzinhoapi/api/user/{userEmail}/{userPassword}");

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(stringJson);
                Assert.AreEqual(user.Email, userEmail);
            }
            else
                Assert.IsFalse(result.StatusCode == System.Net.HttpStatusCode.InternalServerError, "Erro na API");

        }

        [Test]
        public async Task TryAuthenticateWithRightUser()
        {
            var userEmail = "teste@icatuseguros.com.br";
            var userPassword = "Icatu123!";
            var user = new User();

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync($"icatuzinhoapi/api/user/{userEmail}/{userPassword}");

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(stringJson);
                Assert.AreEqual(user.Email, userEmail);
            }
            else
                Assert.IsTrue(result.StatusCode == System.Net.HttpStatusCode.InternalServerError, "Erro na API");

        }

        [Test]
        public void PopulateUserTable()
        {
            var user = new User { Id = 1, Email = "teste@icatuseguros.com.br", Name = "Usuário Teste" };

            mockService.Setup(m => m.InsertOrReplaceWithChildren(It.IsAny<User>())).Returns(true);

            var service = mockService.Object;
            var insert = service.InsertOrReplaceWithChildren(user);

            Assert.IsTrue(insert);
        }
    }
}

