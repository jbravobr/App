using System;
using Newtonsoft.Json;
using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Schedule : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        public DateTimeOffset StartSchedule { get; set; }

        public string Message { get; set; }

        [JsonIgnore]
        public string StatusAvatar { get; set; }

        [JsonIgnore]
        public string StatusDescription { get; set; }
    }
}

