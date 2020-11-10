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
        Task<IEnumerable<PinInfo>> GetPinsAsync(string searchQuery = null);
        Task<int> TrySavePinInfoAsync(PinInfo pinInfo);
        Task<int> SavePinInfoAsync(PinInfo pinInfo, int count = 1);
        Task<int> DeletePinInfoAsync(PinInfo pinInfo);
    }
}
