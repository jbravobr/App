using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Acr.UserDialogs;
using System.Runtime.InteropServices.WindowsRuntime;

namespace IcatuzinhoApp
{
    public class DTO<T> where T : class
    {
        public async Task<T> ConvertSingleObjectFromJson(HttpContent content)
        {
            try
            {
                var stringJson = await content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<T>(stringJson));
            }
            catch (Exception ex)
            {
                new LogExceptionService().SubmitToInsights(ex);
                return null;
            }
        }

        public async Task<List<T>> ConvertCollectionObjectFromJson(HttpContent content)
        {
            try
            {
                var stringJson = await content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<List<T>>(stringJson));
            }
            catch (Exception ex)
            {
                new LogExceptionService().SubmitToInsights(ex);
                return null;
            }
        }

        public async Task<bool> ConvertSingleObjectFromJsonToBolean(HttpContent content)
        {
            try
            {
                var stringJson = await content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<bool>(stringJson));
            }
            catch (Exception ex)
            {
                new LogExceptionService().SubmitToInsights(ex);
                return false;
            }
        }
    }
}

