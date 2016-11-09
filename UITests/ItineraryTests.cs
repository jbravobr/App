using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class ItineraryTests
    {
        HttpClient _httpClient = Helpers.ReturnClient();
        private Mock<IItineraryService> mock;

        [SetUp]
        public void SetUp()
        {
            mock = new Mock<IItineraryService>();
        }

        [Test]
        public async Task GetItinerariesFromAPI()
        {
            var listItineraries = new List<Itinerary>();

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync($"icatuzinhoapi/api/itinerary/");

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                listItineraries = JsonConvert.DeserializeObject<List<Itinerary>>(stringJson);
            }

            CollectionAssert.AllItemsAreInstancesOfType(listItineraries, typeof(Itinerary));
            CollectionAssert.IsNotEmpty(listItineraries);
            CollectionAssert.AllItemsAreNotNull(listItineraries);
        }

        [Test]
        public void PopulateItineraryTable()
        {
            var itineraries = new List<Itinerary>
            {
                new Itinerary{Id = 1, Latitude = -1, Longitude = 1, Order = 1 },
                new Itinerary{Id = 2, Latitude = -2, Longitude = 2, Order = 2 },
                new Itinerary{Id = 3, Latitude = -3, Longitude = 3, Order = 3 },
                new Itinerary{Id = 4, Latitude = -4, Longitude = 4, Order = 4 },
                new Itinerary{Id = 5, Latitude = -5, Longitude = 5, Order = 5 }
            };

            mock.Setup(m => m.InsertOrReplaceAllWithChildren(It.IsAny<List<Itinerary>>())).Returns(true);
            var service = mock.Object;
            var insert = service.InsertOrReplaceAllWithChildren(itineraries);

            mock.Verify(m => m.InsertOrReplaceAllWithChildren(It.IsAny<List<Itinerary>>()), Times.Once);

            Assert.IsTrue(insert);
        }
    }
}

