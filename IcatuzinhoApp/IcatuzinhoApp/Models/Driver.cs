using System;
using PropertyChanged;
using SQLiteNetExtensions.Attributes;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Driver : EntityBase
    {
        public string Name { get; set; }
    }
}

