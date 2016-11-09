using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Itinerary : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Order { get; set; }
    }
}

