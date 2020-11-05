using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad
{
    public interface IPinService
    {
        Task<IEnumerable<PinInfo>> GetPinsAsync();
        Task<IEnumerable<PinInfo>> GetPinsAsync(string searchQuery);
        Task<int> SavePinInfoAsync(PinInfo item);
        Task<int> DeletePinInfoAsync(PinInfo item);
    }
}
