using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Practices.Unity;

namespace IcatuzinhoApp
{
    public class DTO<T> where T : class
    {
        ILogExceptionService _log;

        void ConfigureLogService()
        {
            _log = App._container.Resolve<ILogExceptionService>();
        }

        public async Task<T> ConvertSingleObjectFromJson(HttpContent content)
        {
            try
            {
                var stringJson = await content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<T>(stringJson));
            }
            catch (Exception ex)
            {
                _log.SubmitToInsights(ex);
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
                _log.SubmitToInsights(ex);
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
                _log.SubmitToInsights(ex);
                return false;
            }
        }
    }
}

