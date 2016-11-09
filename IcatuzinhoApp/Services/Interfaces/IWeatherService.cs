using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IWeatherService : IBaseService<Weather>
    {
        Task GetWeather();
    }
}

