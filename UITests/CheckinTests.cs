using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class CheckinTests
    {
        HttpClient _httpClient;

        [Test]
        public async Task TestDoCheckIn()
        {
            var checkinDone = false;
            var token = await Helpers.GenerateTokenAuthentication();
            _httpClient = Helpers.ReturnClient(token);

            var result = await _httpClient.GetAsync(Constants.TravelServiceCheckInAddress);

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                checkinDone = JsonConvert.DeserializeObject<bool>(stringJson);
            }

            Assert.IsTrue(checkinDone);
        }

        [Test]
        public async Task TestDoCheckOut()
        {
            var checkoutDone = false;
            var token = await Helpers.GenerateTokenAuthentication();
            _httpClient = Helpers.ReturnClient(token);

            var result = await _httpClient.GetAsync(Constants.TravelServiceCheckOutAddress);

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                checkoutDone = JsonConvert.DeserializeObject<bool>(stringJson);
            }

            Assert.IsTrue(checkoutDone);
        }
    }
}

