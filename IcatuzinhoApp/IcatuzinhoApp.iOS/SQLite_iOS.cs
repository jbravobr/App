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
            var sqliteFilename = "Icatuzinho.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);

<<<<<<< HEAD:IcatuzinhoApp/IcatuzinhoApp.iOS/SQLite_iOS.cs
                // Create the connection
                var conn = new SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), path);
                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
=======
            // Create the connection
            var plat = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
            var conn = new SQLite.Net.SQLiteConnection(plat, path);


            return conn;
>>>>>>> parent of 83fb4ec... Atualização de pacotes e refactoring:iOS/SQLite_iOS.cs
        }
    }
}
