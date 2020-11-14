using Plugin.Settings.Abstractions;
using static MapNotepad.Constants;

namespace MapNotepad.Services
{
    class SettingsManager : ISettingsManager
    {
        private readonly ISettings _settings;
        public SettingsManager(ISettings settings)
        {
            _settings = settings;
        }
        public int CurrentUser
        {
            get => _settings.GetValueOrDefault(nameof(CurrentUser), -1);
            set => _settings.AddOrUpdateValue(nameof(CurrentUser), value);
        }
        public double Zoom
        {
            get => _settings.GetValueOrDefault(nameof(Zoom), DefaultZoom);
            set => _settings.AddOrUpdateValue(nameof(Zoom), value);
        }
        public double LastLatitude
        {
            get => _settings.GetValueOrDefault(nameof(LastLatitude), DefaultLatitude);
            set => _settings.AddOrUpdateValue(nameof(LastLatitude), value);
        }
        public double LastLongitude
        {
            get => _settings.GetValueOrDefault(nameof(LastLongitude), DefaultLongitude);
            set => _settings.AddOrUpdateValue(nameof(LastLongitude), value);
        }
    }
}
