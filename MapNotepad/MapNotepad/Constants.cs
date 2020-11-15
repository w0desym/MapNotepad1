using MapNotepad.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapNotepad
{
    public static class Constants
    {
        public const string DefaultLanguage = "en";
        public const string DatabaseName = "MapNotepadDb.db";
        public const string NotFavoriteImagePath = "ic_notfav.png";
        public const string FavoriteImagePath = "ic_fav.png";
        public const string DefaultCategory = "#";
        public const double DefaultZoom = 15.0d;
        public const double DefaultLatitude = 41.889999;
        public const double DefaultLongitude = 12.489999;
    }
}
