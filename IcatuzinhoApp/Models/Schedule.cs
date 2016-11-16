﻿using System;
using Newtonsoft.Json;
using PropertyChanged;
using SQLite;

namespace IcatuzinhoApp
{
    public class Schedule : EntityBase
    {
        public DateTimeOffset StartSchedule { get; set; }

        [JsonIgnore]
        [Ignore]
        public DateTime TimeSchedule { get; set; }

        public string Message { get; set; }

        [Ignore]
        [JsonIgnore]
        public string StatusAvatar { get; set; }

        [Ignore]
        [JsonIgnore]
        public string StatusDescription { get; set; }
    }
}

