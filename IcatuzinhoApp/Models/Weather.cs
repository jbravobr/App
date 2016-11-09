using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Weather : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        public string Temp { get; set; }

        public string WeatherDesc { get; set; }

        public string Ico { get; set; }
    }
}

