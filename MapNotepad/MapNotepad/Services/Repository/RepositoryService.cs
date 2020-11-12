using MapNotepad.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using static MapNotepad.Constants;

namespace MapNotepad.Services
{
    class RepositoryService : IRepositoryService 
    {
        #region -- Public properties --

        private SQLiteAsyncConnection _database;
        public SQLiteAsyncConnection Database => _database ??= new SQLiteAsyncConnection(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName));

        #endregion

        public async Task<IEnumerable<T>> GetItemsAsync<T>() where T : ICommonModel, new()
        {
            await Database.CreateTableAsync<T>();

            return await Database.Table<T>().ToListAsync();
        }

        public async Task<int> TryInsertItemAsync<T>(T item) where T : ICommonModel, new()
        {
            await Database.CreateTableAsync<T>();

            int id;

            try
            {
                id = await Database.InsertAsync(item);
            }
            catch
            {
                id = -1;
            }

            return id;
        }

        public async Task<int> UpdateItemAsync<T>(T item) where T : ICommonModel, new()
        {
            await Database.CreateTableAsync<T>();

            return await Database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : ICommonModel, new()
        {
            await Database.CreateTableAsync<T>();

            return await Database.DeleteAsync(item);
        }

    }
}
