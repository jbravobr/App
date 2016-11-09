using System;
using System.ComponentModel;
using PropertyChanged;
using SQLite.Net.Attributes;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class User : EntityBase
    {
        public string Email { get; set; }

        public string Name { get; set; }

        [NotNull]
        public string Password { get; set; }
    }
}

