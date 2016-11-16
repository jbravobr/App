using NUnit.Framework;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class UsersTests
    {
        Mock<IBaseService<User>> mockService;
        static IUnityContainer Container = null;

        [SetUp]
        public void SetUp()
        {
            Container = new UnityContainer();
            Container.RegisterType(typeof(IBaseService<>), typeof(BaseService<>));
            Container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

        [Test]
        public async Task TryToAuthenticateWithFormsAuthentication()
        {
            try
            {
                var email = "pcorreia@icatuseguros.com.br";
                var password = "123456";

                var userService = Container.Resolve<IBaseService<User>>();
                var result = await userService.Login(email, password);

                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Test]
        public void PopulateUserTable()
        {
            var user = new User { Id = 1, Email = "teste@icatuseguros.com.br", Name = "Usuário Teste" };

            mockService.Setup(m => m.InsertOrReplaceWithChildren(It.IsAny<User>())).Verifiable();

            var service = mockService.Object;
            var insert = service.InsertOrReplaceWithChildren(user);
        }
    }
}

