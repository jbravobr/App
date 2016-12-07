using System;
using PropertyChanged;
using SQLiteNetExtensions.Attributes;

namespace IcatuzinhoApp
{
    public class Vehicle : EntityBase
    {
        public int Number { get; set; }

        public int SeatsTotal { get; set; }

        public int SeatsAvailable { get; set; }
    }
}

