using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IStationService : IBaseService<Station>
    {
        Task GetAllStations();
    }
}

