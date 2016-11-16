using System;
using PropertyChanged;
using SQLite;
using System.Collections.Generic;

namespace IcatuzinhoApp
{
    public class Transaction
    {
        public string Name { get; set; }

        public Dictionary<string,string> Details { get; set; }
    }
}

