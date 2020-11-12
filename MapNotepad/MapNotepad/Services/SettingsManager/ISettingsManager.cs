﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad.Services
{
    public interface ISettingsManager
    {
        int CurrentUser { get; set; }
        double LastLatitude { get; set; }
        double LastLongitude { get; set; }
    }
}
