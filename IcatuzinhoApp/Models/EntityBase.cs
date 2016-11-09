using System;
using Realms;

namespace IcatuzinhoApp
{

    public class EntityBase : RealmObject
    {
        [ObjectId]
        public int Id { get; set; }

        public DateTimeOffset DtRegister { get; set;}

        public bool Status { get; set; }
    }
}

