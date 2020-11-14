using MapNotepad.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    public interface IRepositoryService
    {
        Task<IEnumerable<T>> GetItemsAsync<T>() where T : ICommonModel, new();
        Task<int> TryInsertItemAsync<T>(T item) where T : ICommonModel, new();
        Task<int> UpdateItemAsync<T>(T item) where T : ICommonModel, new();
        Task<int> DeleteItemAsync<T>(T item) where T : ICommonModel, new();
    }
}
