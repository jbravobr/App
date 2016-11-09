using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Runtime.InteropServices;
using System.Security;

namespace IcatuzinhoApp.UITests
{
    [TestFixture]
    public class CheckinTests
    {
        HttpClient _httpClient = Helpers.ReturnClient();

        [Test]
        public async Task Try_Make_Checkin()
        {
            var checkinDone = false;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync($"icatuzinhoapi/api/travel/checkin/1/1");

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                checkinDone = JsonConvert.DeserializeObject<bool>(stringJson);
            }

            Assert.IsTrue(checkinDone);
        }

        [Test]
        public async Task Try_Make_Checkout()
        {
            var checkoutDone = false;

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var result = await _httpClient.GetAsync($"icatuzinhoapi/api/travel/checkout/1/1");

            if (result.IsSuccessStatusCode)
            {
                var stringJson = await result.Content.ReadAsStringAsync();
                checkoutDone = JsonConvert.DeserializeObject<bool>(stringJson);
            }

            Assert.IsTrue(checkoutDone);
        }
    }
}

