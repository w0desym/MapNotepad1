using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    public interface IRepositoryService
    {
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : ICommonModel, new();
        Task<int> TryInsertItemAsync<T>(T item) where T : ICommonModel, new();
        Task<int> UpdateItemAsync<T>(T item) where T : ICommonModel, new();
        Task<int> DeleteItemAsync<T>(T item) where T : ICommonModel, new();
    }
}
