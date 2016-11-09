using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class StationTests
    {
        HttpClient _httpClient;
        Mock<IStationService> mockService;

        [SetUp]
        public void SetUp()
        {
            mockService = new Mock<IStationService>();
        }

        [Test]
        public async Task ReturnListStationsFromAPI()
        {
            var listStation = new List<Station>();
            var token = await Helpers.GenerateTokenAuthentication();

            _httpClient = Helpers.ReturnClient(token);

            var result = await _httpClient.GetAsync(Constants.StationServiceAddress);

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                listStation = JsonConvert.DeserializeObject<List<Station>>(stringJson);
            }

            CollectionAssert.IsNotEmpty(listStation);
            CollectionAssert.AllItemsAreNotNull(listStation);
        }

        [Test]
        public void PopulateStationTable()
        {
            var stations = new List<Station>
            {
                new Station{Id = 1, Latitude = -1, Longitude = 1, Order = 1 },
                new Station{Id = 2, Latitude = -2, Longitude = 2, Order = 2 },
                new Station{Id = 3, Latitude = -3, Longitude = 3, Order = 3 },
                new Station{Id = 4, Latitude = -4, Longitude = 4, Order = 4 },
                new Station{Id = 5, Latitude = -5, Longitude = 5, Order = 5 }
            };

            mockService.Setup(m => m.Insert(It.IsAny<List<Station>>())).Returns(true);
            var service = mockService.Object;
            var insert = service.Insert(stations);

            mockService.Verify(m => m.Insert(It.IsAny<List<Station>>()), Times.Once);

            Assert.IsTrue(insert);
        }
    }
}

