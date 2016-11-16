using System;
using PropertyChanged;

namespace IcatuzinhoApp
{
    public class Itinerary : EntityBase
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Order { get; set; }
    }
}

