using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace IcatuzinhoApp
{
    public class LogException
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime DtCreation { get; set; }

        public string Exception { get; set; }

        public string InnerException { get; set; }

        public LogExceptionType Type { get; set; }

        [OneToOne]
        public Transaction Trasaction { get; set; }

        [ForeignKey(typeof(Transaction))]
        public int TrasactionId { get; set; }
    }
}

