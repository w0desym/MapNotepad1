using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad
{
    public interface IMapService
    {
        Position GetLastMapPosition();
        void SetLastMapPosition(Position position);

    }
}
