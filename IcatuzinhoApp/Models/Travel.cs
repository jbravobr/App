using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Travel : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        public Schedule Schedule { get; set; }

        public Driver Driver { get; set; }

        public Vehicle Vehicle { get; set; }

        public bool Status { get; set; }
    }
}

