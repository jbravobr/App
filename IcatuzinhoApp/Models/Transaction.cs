using System;
using PropertyChanged;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Transaction
    {
        public string Name { get; set; }

        public Dictionary<string,string> Details { get; set; }
    }
}

