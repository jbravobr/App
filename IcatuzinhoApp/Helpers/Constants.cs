using System;
namespace IcatuzinhoApp
{
    public static class Constants
    {
        public const string UserServiceAddress = "icatuzinhoapi/api/user/";
        public const string ScheduleServiceAddress = "icatuzinhoapi/api/schedule/";
        public const string StationServiceAddress = "icatuzinhoapi/api/station/";
        public const string ItineraryServiceAddress = "icatuzinhoapi/api/itinerary/";
        public const string TravelServiceAddress = "icatuzinhoapi/api/travel/";
        public const string TravelServiceCheckInAddress = "icatuzinhoapi/api/travel/checkin/";
        public const string TravelServiceCheckOutAddress = "icatuzinhoapi/api/travel/checkout/";
        public const string WeatherServiceAddress = "icatuzinhoapi/api/weather/";
        ///public const string WeatherAuthServiceAddress = "icatuzinhoapi/api/weatherAuth/";
        public const string TravelServiceAvailableSeats = "icatuzinhoapi/api/travel/availableseats/";
        public const string FormsAuthentication = "icatuzinhoapi/api/auth";

        public const string BaseAddress = "http://labdev.labdevmobile.com.br/";

        public const string MessageErroAuthentication = "Ops! Parece que você está com problemas na autorização de acesso tente novamente";
    }
}