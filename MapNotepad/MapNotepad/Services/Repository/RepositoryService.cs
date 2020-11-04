using MapNotepad.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad
{
    class RepositoryService : IRepositoryService 
    {
        private readonly SQLiteConnection database;
        public RepositoryService()
        {
            database = new SQLiteConnection(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MapNotepadDb.db"));
            database.CreateTable<PinInfo>();
            database.CreateTable<User>();
            //database.DropTable<PinInfo>();
            //database.DropTable<User>();
        }
        public List<T> GetItems<T>() where T : ICommonModel, new()
        {
            return database.Table<T>().ToList();
        }
        public int InsertItem<T>(T item) where T : ICommonModel
        {
            int id;
            try
            {
                id = database.Insert(item);
            }
            catch
            {
                id = -1;
            }
            return id;
        }
        public int UpdateItem<T>(T item) where T : ICommonModel
        {
            return database.Update(item);
        }
        public int DeleteItem<T>(T item) where T : ICommonModel
        {
            return database.Delete(item);
        }

    }
}
