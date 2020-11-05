using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad
{
    class MapService : IMapService
    {
        private readonly ISettingsManager _settingsManager;
        public MapService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public Position GetLastMapPosition()
        {
            return new Position(_settingsManager.LastLatitude, _settingsManager.LastLongitude);
        }
        public void SetLastMapPosition(Position position)
        {
            _settingsManager.LastLatitude = position.Latitude;
            _settingsManager.LastLongitude = position.Longitude;
        }
    }
}
