using System;
using System.IO;
using IcatuzinhoApp.Droid;
using SQLite.Net;

[assembly: Xamarin.Forms.Dependency(typeof(SQLite_Droid))]
namespace IcatuzinhoApp.Droid
{
    public class SQLite_Droid : ISQLite
    {
        public SQLite.Net.SQLiteConnection GetConnection()
        {
            const string sqliteFilename = "Icatuzinho.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);

            // Cria a conexão
            var plat = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);

            return conn;
        }
    }
}

