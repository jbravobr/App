using NUnit.Framework;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Moq;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class UsersTests
    {
        HttpClient _httpClient;
        Mock<IUserService> mockService;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<IUserService>();
        }

        [Test]
        public async Task TryAuthenticateWithWrongUser()
        {
            var userEmail = "ramaral@icatuseguros.com.br";
            var userPassword = "123";
            var user = new User();
            var token = await Helpers.GenerateTokenAuthentication();

            _httpClient = Helpers.ReturnClient(token);

            var result = await _httpClient.GetAsync($"{Constants.UserServiceAddress}{userEmail}/{userPassword}");

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
            var token = await Helpers.GenerateTokenAuthentication();

            _httpClient = Helpers.ReturnClient(token);

            var result = await _httpClient.GetAsync($"{Constants.UserServiceAddress}{userEmail}/{userPassword}");

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

            mockService.Setup(m => m.Insert(It.IsIn<User>())).Returns(true);

            var service = mockService.Object;
            var insert = service.Insert(user);

            Assert.IsTrue(insert);
        }
    }
}

