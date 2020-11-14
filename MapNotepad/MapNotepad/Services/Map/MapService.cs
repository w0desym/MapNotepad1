using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.Services
{
    class MapService : IMapService
    {
        private readonly ISettingsManager _settingsManager;
        public MapService(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        public CameraPosition GetLastMapPosition()
        {
            return new CameraPosition(new Position(_settingsManager.LastLatitude, _settingsManager.LastLongitude), _settingsManager.Zoom);
        }
        public void SetLastMapPosition(CameraPosition cameraPosition)
        {
            _settingsManager.Zoom = cameraPosition.Zoom;
            _settingsManager.LastLatitude = cameraPosition.Target.Latitude;
            _settingsManager.LastLongitude = cameraPosition.Target.Longitude;
        }
    }
}
