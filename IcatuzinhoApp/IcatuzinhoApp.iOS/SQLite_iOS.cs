using System;
using Xamarin.Forms;
using System.IO;
using SQLite.Net;

[assembly: Dependency(typeof(IcatuzinhoApp.iOS.SQLite_iOS))]

namespace IcatuzinhoApp.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            try
            {
                var sqliteFilename = "Icatuzinho.db3";
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
                var path = Path.Combine(libraryPath, sqliteFilename);

                // Create the connection
                var conn = new SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path);
                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
