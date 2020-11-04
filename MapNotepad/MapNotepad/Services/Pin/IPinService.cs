using MapNotepad.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MapNotepad
{
    public interface IPinService
    {
        List<PinInfo> GetPins();
        IEnumerable<PinInfo> GetPins(string searchQuery);
        int SavePinInfo(PinInfo item);
        int DeletePinInfo(PinInfo item);
    }
}
