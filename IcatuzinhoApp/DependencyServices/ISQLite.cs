using System;
using SQLite;

namespace IcatuzinhoApp
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}

