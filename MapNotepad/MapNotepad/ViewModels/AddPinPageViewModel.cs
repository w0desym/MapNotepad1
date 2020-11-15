using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using static MapNotepad.Constants;


namespace MapNotepad.ViewModels
{
    //сделать проверку на null пина когда CameraChanging не сработал(из-за этого вылетает на кнопку Add Pin)
    class AddPinPageViewModel : ViewModelBase
    {
        private readonly IPinService _pinService;
        private readonly IPermissionService _permissionService;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly IMapService _mapService;
        private readonly IUserDialogs _userDialogs;

        public AddPinPageViewModel(
            INavigationService navigationService,
            IPermissionService permissionService,
            IPinService pinService,
            IUserService userService,
            IMapService mapService,
            IUserDialogs userDialogs)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _permissionService = permissionService;
            _pinService = pinService;
            _userService = userService;
            _mapService = mapService;
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

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

        private string _pinCategory;
        public string PinCategory
        {
            get => _pinCategory;
            set => SetProperty(ref _pinCategory, value);
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

        private CameraPosition _cameraPosition;
        public CameraPosition CameraPosition
        {
            get => _cameraPosition;
            set => SetProperty(ref _cameraPosition, value);
        }

        private ObservableCollection<Pin> _pinsCollection;
        public ObservableCollection<Pin> PinsCollection
        {
            get => _pinsCollection;
            set => SetProperty(ref _pinsCollection, value);
        }

        private Pin _pin;
        public Pin Pin
        {
            get => _pin;
            set => SetProperty(ref _pin, value);
        }

        private bool _myLocationEnabled;
        public bool MyLocationEnabled
        {
            get => _myLocationEnabled;
            set => SetProperty(ref _myLocationEnabled, value);
        }

        private ICommand _cameraMovingCommand;
        public ICommand CameraMovingCommand => _cameraMovingCommand ??= new Command<CameraPosition>(OnCameraMovingCommand);

        private ICommand _savePinCommand;
        public ICommand SavePinCommand => _savePinCommand ??= new Command(OnSavePinCommandAsync);

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new Command(OnGoBackCommandAsync);

        #endregion

        #region -- INavigationAware implementation --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            AskLocationPermissionsAsync();

            Pin = new Pin();
            Pin.Label = Resources["PinDefaultName"];

            if (parameters.TryGetValue(nameof(PinInfo), out PinInfo pinInfo))//pin editing
            {
                PinLabel = pinInfo.Label;
                PinDescription = pinInfo.Description;
                PinLatitude = pinInfo.Latitude;
                PinLongitude = pinInfo.Longitude;
                PinCategory = pinInfo.Category;

                SetCamera(new CameraPosition(new Position(PinLatitude, PinLongitude), DefaultZoom));
            }
            else //pin adding
            {
                if (MyLocationEnabled)
                {
                    SetCameraOnUserLocationAsync();
                }
                else
                {
                    SetCameraOnLastPosition();
                }
            }

            Pin.Position = new Position(CameraPosition.Target.Latitude, CameraPosition.Target.Longitude);

            PinsCollection = new ObservableCollection<Pin> { Pin };
        }

        #endregion

        #region -- Private helpers --
        private void OnCameraMovingCommand(CameraPosition position)
        {
            PinLatitude = position.Target.Latitude;
            PinLongitude = position.Target.Longitude;

            if (Pin != null)
            {
                Pin.Position = new Position(PinLatitude, PinLongitude);
                PinsCollection = new ObservableCollection<Pin> { Pin };
            }
        }
        private async void OnSavePinCommandAsync()
        {
            var pinInfo = new PinInfo()
            {
                Label = PinLabel ?? Resources["PinDefaultName"],
                Latitude = PinLatitude,
                Longitude = PinLongitude,
                Description = PinDescription ?? string.Empty,
                Category = PinCategory ?? DefaultCategory,
                ImgPath = NotFavoriteImagePath,
                UserId = _userService.CurrentUserId
            };

            await _pinService.AddPinInfoAsync(pinInfo);
            await _navigationService.GoBackAsync();
        }
        private async void OnGoBackCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        private void SetCameraOnLastPosition()
        {
            var lastPos = _mapService.GetLastMapPosition();

            SetCamera(lastPos);
        }

        private async void SetCameraOnUserLocationAsync()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();

            if (location != null)
            {
                SetCamera(new CameraPosition(new Position(location.Latitude, location.Longitude), DefaultZoom));

                PinLatitude = location.Latitude;
                PinLongitude = location.Longitude;
            }
        }
        private void SetCamera(CameraPosition position)
        {
            CameraPosition = position;
        }
        private async void AskLocationPermissionsAsync()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                if (await _permissionService.CheckPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Denied
                || await _permissionService.CheckPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Disabled
                || await _permissionService.CheckPermissionAsync<Permissions.LocationWhenInUse>() == PermissionStatus.Restricted)
                {
                    await _userDialogs.AlertAsync(Resources["LocationPermissionMessage"]);
                }
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
