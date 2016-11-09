using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Vehicle : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        public int Number { get; set; }

        public int SeatsTotal { get; set; }

        public int SeatsAvailable { get; set; }
    }
}

