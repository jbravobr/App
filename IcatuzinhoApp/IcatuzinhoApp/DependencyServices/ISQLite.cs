using System;
using SQLite;

namespace IcatuzinhoApp
{
    public interface ISQLite
    {
        SQLite.Net.SQLiteConnection GetConnection();
    }
}

