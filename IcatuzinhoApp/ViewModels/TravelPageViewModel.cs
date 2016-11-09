using System;
using System.Collections.Generic;
using System.Linq;

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
                var itens = _itineraryService.GetAll();

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
            try
            {
                var stations = _stationService.GetAll();

                if (stations != null && stations.Any())
                    return stations;

                return null;
            }
            catch (Exception ex)
            {
                base.SendToInsights(ex);
                return null;
            }
        }
    }
}

