using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Driver : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        [Indexed]
        public string Name { get; set; }
    }
}

