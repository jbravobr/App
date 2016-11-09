using System;
using PropertyChanged;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Weather : EntityBase
    {
        public string Temp { get; set; }

        public string WeatherDesc { get; set; }

        public string Ico { get; set; }
    }
}

