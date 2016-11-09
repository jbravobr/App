using System;
using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class LogException : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        public DateTimeOffset DtCreation { get; set; }

        public string Exception { get; set; }

        public string InnerException { get; set; }

        public Transaction Trasaction { get; set; }
    }
}

