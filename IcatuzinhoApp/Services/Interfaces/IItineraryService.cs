using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IItineraryService : IBaseService<Itinerary>
    {
        Task GetAllItineraries();
    }
}

