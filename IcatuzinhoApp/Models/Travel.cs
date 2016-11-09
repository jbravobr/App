using System;
using System.Collections.Generic;
using PropertyChanged;
using SQLiteNetExtensions.Attributes;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Travel : EntityBase
    {
        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public Schedule Schedule { get; set; }

        [ForeignKey(typeof(Schedule))]
        public int ScheduleId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public Driver Driver { get; set; }

        [ForeignKey(typeof(Driver))]
        public int DriverId { get; set; }

        [OneToOne(CascadeOperations = CascadeOperation.CascadeInsert | CascadeOperation.CascadeRead)]
        public Vehicle Vehicle { get; set; }

        [ForeignKey(typeof(Vehicle))]
        public int VehicleId { get; set; }
    }
}

