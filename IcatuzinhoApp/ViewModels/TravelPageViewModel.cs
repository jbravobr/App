using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace IcatuzinhoApp
{
    public class TravelPageViewModel : BasePageViewModel
    {
        IItineraryService _itineraryService;
        IStationService _stationService;

        public TravelPageViewModel(IItineraryService itineraryService,
                                   IStationService stationService)
        {
            _itineraryService = itineraryService;
            _stationService = stationService;
        }

        public List<Itinerary> Get()
        {
            try
            {
                var itens = _itineraryService.GetAllWithChildren();

                if (itens != null && itens.Any())
                    return itens;

                return null;
            }
            catch (Exception ex)
            {
                base.SendToInsights(ex);
                return null;
            }
        }

        public List<Station> GetStations()
        {
            var stations = _stationService.GetAllWithChildren();

            if (stations != null && stations.Any())
                return stations;

            return null;
        }
    }
}

