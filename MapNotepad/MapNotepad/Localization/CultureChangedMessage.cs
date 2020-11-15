using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MapNotepad.Localization
{
    public class CultureChangedMessage
    {
        public CultureChangedMessage(CultureInfo newCultureInfo)
        {
            NewCultureInfo = newCultureInfo;
        }

        public CultureChangedMessage(string lngName)
            : this(new CultureInfo(lngName))
        { }

        #region -- Public properties --

        public CultureInfo NewCultureInfo { get; private set; }

        #endregion

    }
}
