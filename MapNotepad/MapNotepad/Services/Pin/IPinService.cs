using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MapNotepad.Services
{
    public interface IPinService
    {
        Task<IEnumerable<PinInfo>> GetPinsAsync(string searchQuery = null);
        Task<int> UpdatePinInfoAsync(PinInfo pinInfo);
        Task AddPinInfoAsync(PinInfo pinInfo);
        Task<int> DeletePinInfoAsync(PinInfo pinInfo);
    }
}
