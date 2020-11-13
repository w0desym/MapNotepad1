using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services
{
    public interface IMapService
    {
        CameraPosition GetLastMapPosition();
        void SetLastMapPosition(CameraPosition cameraPosition);

    }
}
