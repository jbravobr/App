using System;
using PropertyChanged;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class AuthenticationCode : EntityBase
    {
        [Unique]
        public string Code { get; set; }

        [OneToOne]
        public User User { get; set; }

        [ForeignKey(typeof(User))]
        public int UserId { get; set; }
    }
}

