using System;
using SQLite.Net.Attributes;

namespace IcatuzinhoApp
{

    public class EntityBase
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string DtRegister { get; } = DateTime.Now.ToString("G");

        public bool Status { get; set; }
    }
}

