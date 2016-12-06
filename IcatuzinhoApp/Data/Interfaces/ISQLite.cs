using System;
using SQLite.Net;

namespace IcatuzinhoApp
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}

