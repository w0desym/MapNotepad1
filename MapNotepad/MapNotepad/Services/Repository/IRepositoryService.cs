using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MapNotepad
{
    public interface IRepositoryService
    {
        List<T> GetItems<T>() where T : ICommonModel, new();
        int InsertItem<T>(T item) where T : ICommonModel;
        int UpdateItem<T>(T item) where T : ICommonModel;
        int DeleteItem<T>(T item) where T : ICommonModel;
    }
}
