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
        Task<int> TrySavePinInfoAsync(PinInfo pinInfo);
        Task SavePinInfoAsync(PinInfo pinInfo);
        Task<int> DeletePinInfoAsync(PinInfo pinInfo);
    }
}
