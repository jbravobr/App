using PropertyChanged;
using Realms;

namespace IcatuzinhoApp
{
    [ImplementPropertyChanged]
    public class Transaction : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        [Indexed]
        public string Name { get; set; }

        public string DetailKey { get; set; }

        public string DetailName { get; set; }
    }
}

