using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace IcatuzinhoApp
{
    public class TravelPageViewModel : BasePageViewModel
    {
        IBaseService<Itinerary> _itineraryService;
        IBaseService<Station> _stationService;

        public TravelPageViewModel(IBaseService<Itinerary> itineraryService,
                                   IBaseService<Station> stationService)
        {
            _itineraryService = itineraryService;
            _stationService = stationService;
        }

        public async Task<List<Itinerary>> Get()
        {
            try
            {
                var itens = await _itineraryService.GetAllWithChildren();

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

        public async Task<List<Station>> GetStations()
        {
            var stations = await _stationService.GetAllWithChildren();

            if (stations != null && stations.Any())
                return stations;

            return null;
        }
    }
}