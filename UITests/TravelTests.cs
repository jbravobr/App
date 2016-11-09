using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class TravelTest
    {
        HttpClient _httpClient;
        Mock<ITravelService> mockService;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<ITravelService>();
        }

        [Test]
        public async Task ReturnTravelsFromAPI()
        {
            var travel = new Travel();
            var token = await Helpers.GenerateTokenAuthentication();
            var travelId = 1;

            _httpClient = Helpers.ReturnClient(token);

            var result = await _httpClient.GetAsync($"{Constants.TravelServiceAddress}{travelId}");

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                travel = JsonConvert.DeserializeObject<Travel>(stringJson);
            }

            Assert.IsInstanceOf(typeof(Travel), travel);
            Assert.NotNull(travel);
        }

        [Test]
        public void PopulateTravelTable()
        {
            var travels = new List<Travel>
            {
                new Travel{
                    Id = 1, Driver = new Driver
                    {
                        Id = 1,
                        Name = "Motorista teste 1"
                    }, Schedule = new Schedule
                    {
                        Id = 1,
                        Message = "Mensagem Teste 1",
                        StartSchedule = DateTime.Now
                    }, Vehicle = new Vehicle
                    {
                        Id =1,
                        Number = 1,
                        SeatsAvailable = 20,
                        SeatsTotal = 20
                    }
                }
            };

            mockService.Setup(m => m.Insert(It.IsAny<List<Travel>>())).Returns(true);

            var service = mockService.Object;
            var insert = service.Insert(travels);

            mockService.Verify(m => m.Insert(It.IsAny<List<Travel>>()), Times.Once);

            Assert.IsTrue(insert);
        }
    }
}

