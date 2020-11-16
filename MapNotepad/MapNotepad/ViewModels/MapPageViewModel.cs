using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms.GoogleMaps;
using MapNotepad.Extensions;
using Xamarin.Essentials;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using MapNotepad.Services;
using Acr.UserDialogs;
using System.Threading.Tasks;
using static MapNotepad.Constants;

namespace MapNotepad.ViewModels
{
    class MapPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPinService _pinService;
        private readonly IPermissionService _permissionService;
        private readonly IUserDialogs _userDialogs;
        private readonly IMapService _mapService;

        public MapPageViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IPermissionService permissionService,
            IUserDialogs userDialogs,
            IMapService mapService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _pinService = pinService;
            _permissionService = permissionService;
            _userDialogs = userDialogs;
            _mapService = mapService;
        }

        #region -- Public properties --

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private ObservableCollection<Pin> _pinsCollection;
        public ObservableCollection<Pin> PinsCollection
        {
            get => _pinsCollection;
            set => SetProperty(ref _pinsCollection, value);
        }
        private CameraPosition _cameraPosition;
        public CameraPosition CameraPosition
        {
            get => _cameraPosition;
            set => SetProperty(ref _cameraPosition, value);
        }

        private Pin _selectedPin;
        public Pin SelectedPin
        {
            get => _selectedPin;
            set => SetProperty(ref _selectedPin, value);
        }

        private string _pinLabel;
        public string PinLabel
        {
            get => _pinLabel;
            set => SetProperty(ref _pinLabel, value);
        }

        private string _pinDescription;
        public string PinDescription
        {
            get => _pinDescription;
            set => SetProperty(ref _pinDescription, value);
        }

        private double _pinLatitude;
        public double PinLatitude
        {
            get => _pinLatitude;
            set => SetProperty(ref _pinLatitude, value);
        }

        private double _pinLongitude;
        public double PinLongitude
        {
            get => _pinLongitude;
            set => SetProperty(ref _pinLongitude, value);
        }

        private bool _myLocationEnabled;
        public bool MyLocationEnabled
        {
            get => _myLocationEnabled;
            set => SetProperty(ref _myLocationEnabled, value);
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ??= new Command(OnSearchCommand);

        private ICommand _pinCommand;
        public ICommand PinCommand => _pinCommand ??= new Command<Pin>(OnPinCommand);

        private ICommand _cameraChangedCommand;
        public ICommand CameraChangedCommand => _cameraChangedCommand ??= new Command<CameraPosition>(OnCameraChangedCommand);

        #endregion

        #region -- IInitialize implementation --

        public override async void Initialize(INavigationParameters parameters)
        {
            await LoadPinsCollectionAsync();

            SetCameraOnLastPosition();

            await AskLocationPermissionsAsync(); 
        }
        #endregion

        #region -- INavigationAware implementation --

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await LoadPinsCollectionAsync();

            if (parameters.TryGetValue(nameof(Pin), out Pin pin))
            {
                SelectedPin = PinsCollection.FirstOrDefault(x => x.Label == pin.Label) ?? SelectedPin;
                AssignPinPopup(pin);

                if (SelectedPin != null)
                {
                    SetCamera(new CameraPosition(SelectedPin.Position, DefaultZoom));
                }
            }

            await AskLocationPermissionsAsync();
        }

        #endregion

        #region -- Private helpers --
        private async void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var foundPins = await _pinService.GetPinsAsync(SearchQuery);
                PinsCollection = new ObservableCollection<Pin>(foundPins.Select(x => x.ToPin()));
            }
            else
            {
                await LoadPinsCollectionAsync();
            }
        }
        private void OnPinCommand(Pin pin)
        {
            AssignPinPopup(pin);
        }

        private void OnCameraChangedCommand(CameraPosition cameraPosition)
        {
            _mapService.SetLastMapPosition(cameraPosition);
        }

        private void AssignPinPopup(Pin pin)
        {
            var pinInfo = pin.ToPinInfo();

            PinLabel = pinInfo.Label;
            PinDescription = pinInfo.Description;
            PinLatitude = Math.Truncate(pinInfo.Latitude * 1000) / 1000;
            PinLongitude = Math.Truncate(pinInfo.Longitude * 1000) / 1000;
        }

        private async Task LoadPinsCollectionAsync()
        {
            var pins = await _pinService.GetPinsAsync();
            var favorites = pins.Where(x => x.IsFavorite);

            if (favorites != null)
            {
                PinsCollection = new ObservableCollection<Pin>(favorites.Select(x => x.ToPin()));
            }
        }

        private void SetCameraOnLastPosition()
        {
            SetCamera(_mapService.GetLastMapPosition());
        }

        private void SetCamera(CameraPosition position)
        {
            CameraPosition = position;
        }

        private async Task AskLocationPermissionsAsync()
        {
            if (Device.RuntimePlatform == Device.iOS
                && (await _permissionService.CheckPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Denied
                || await _permissionService.CheckPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Disabled
                || await _permissionService.CheckPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Restricted))
            {
                await _userDialogs.AlertAsync(Resources["LocationPermissionMessage"]);
            }
            else
            {
                if (!MyLocationEnabled)
                {
                    MyLocationEnabled = await _permissionService.RequestPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Granted;
                }
            }
        }
        #endregion
    }
}
