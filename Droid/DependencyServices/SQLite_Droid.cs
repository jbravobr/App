using System;
using System.IO;
using IcatuzinhoApp.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Droid))]
namespace IcatuzinhoApp.Droid
{
    public class SQLite_Droid : ISQLite
    {
        public SQLite.SQLiteConnection GetConnection()
        {
            const string sqliteFilename = "Icatuzinho.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);

            // Cria a conexão
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}

