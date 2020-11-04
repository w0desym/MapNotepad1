using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MapNotepad;
using MapNotepad.Droid.Persistence;

using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]

namespace MapNotepad.Droid.Persistence
{
    public class SQLite_Android : ISQLiteDb
    {
        public SQLite_Android()
        {
        }

        #region ISQLite implementation

        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "SampleSQLites.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Return the database connection 
            return new SQLiteConnection(path); ;
        }
        #endregion

    }
}