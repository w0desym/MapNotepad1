using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public interface ISettingsManager
    {
        int CurrentUser { get; set; }
    }
}
