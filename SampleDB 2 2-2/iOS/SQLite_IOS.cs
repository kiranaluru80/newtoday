using System;
using System.IO;
using Xamarin.Forms;
using SampleDB.iOS;
using Foundation;

[assembly:Dependency(typeof(SQLite_IOS))]
namespace SampleDB.iOS
{
    public class SQLite_IOS: ISQLite
    {
        public string GetFilePath(String fileName)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Database");
            Console.WriteLine(libFolder);
            if(!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

           string dbPath = Path.Combine(libFolder, fileName);

			CopyDatabaseIfNotExists(dbPath);

			return dbPath;

        }

		private static void CopyDatabaseIfNotExists(string dbPath)
		{
			if (!File.Exists(dbPath))
			{
				var existingDb = NSBundle.MainBundle.PathForResource("JSSE", "db3");
				File.Copy(existingDb, dbPath);
			}
		}
    }
	
}
