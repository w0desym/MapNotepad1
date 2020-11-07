using MapNotepad.Extensions;
using MapNotepad.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    //сделать проверку на null пина когда CameraChanging не сработал(из-за этого вылетает на кнопку Add Pin)
    class AddPinPageViewModel : ViewModelBase
    {
        private readonly IPinService _pinService;
        private readonly IPermissionService _permissionService;
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public AddPinPageViewModel(
            INavigationService navigationService,
            IPermissionService permissionService,
            IPinService pinService,
            IUserService userService) :
            base(navigationService)
        {
            _navigationService = navigationService;
            _permissionService = permissionService;
            _pinService = pinService;
            _userService = userService;
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

        private ICommand _cameraMovingCommand;
        public ICommand CameraMovingCommand => _cameraMovingCommand ??= new Command<CameraPosition>(OnCameraMovingCommand);

        private ICommand _savePinCommand;
        public ICommand SavePinCommand => _savePinCommand ??= new Command(OnSavePinCommandAsync);

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new Command(OnGoBackCommandAsync);

        private ICommand _latitudeChangedCommand;
        public ICommand LatitudeChangedCommand => _latitudeChangedCommand ??= new Command<string>(OnLatitudeChangedCommandAsync);

        private ICommand _longitudeChangedCommand;
        public ICommand LongitudeChangedCommand => _longitudeChangedCommand ??= new Command<string>(OnLongitudeChangedCommandAsync);

        #endregion

        #region -- IInitialize implementation --
        public override async void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            //checking parameters
            if (parameters != null)
            {
                if (parameters.TryGetValue(nameof(PinInfo), out PinInfo pinInfo))
                {
                    PinLabel = pinInfo.Label;
                    PinDescription = pinInfo.Description;
                    PinLatitude = pinInfo.Latitude;
                    PinLongitude = pinInfo.Longitude;
                }
            }


            //pin editing
            if (PinLabel != null)
            {
                //getting pin's location
                CameraPosition = new CameraPosition(new Position(PinLatitude, PinLongitude), 15.0d);
            }
            //pin adding
            else
            {
                //getting user's location
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High));

                if (location != null)
                {
                    CameraPosition = new CameraPosition(new Position(location.Latitude, location.Longitude), 15.0d);
                }
                PinLatitude = location.Latitude;
                PinLongitude = location.Longitude;
            }

            //rework
            Pin = new Pin();
            Pin.Label = "New Pin";
            Pin.Position = new Position(CameraPosition.Target.Latitude, CameraPosition.Target.Longitude);

            PinsCollection = new ObservableCollection<Pin> { Pin };
        }

        #endregion

        #region -- INavigationAware implementation --   
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            _permissionService.RequestLocationPermissionAsync();
        }

        #endregion

        #region -- Private helpers --
        private void OnCameraMovingCommand(CameraPosition position)
        {
            PinLatitude = position.Target.Latitude;
            PinLongitude = position.Target.Longitude;

            Pin.Position = new Position(PinLatitude, PinLongitude);
            PinsCollection = new ObservableCollection<Pin> { Pin };
        }
        private async void OnSavePinCommandAsync()
        {
            var pinInfo = new PinInfo()
            {
                UserId = _userService.GetCurrentUser(),
                Label = PinLabel ?? "New Pin",
                Latitude = PinLatitude,
                Longitude = PinLongitude,
                Description = PinDescription ?? string.Empty,
                ImgPath = Constants.NotFavoriteImagePath
            };

            var answer = await _pinService.SavePinInfoAsync(pinInfo);
            if (answer != -1)
            {
                await _navigationService.GoBackAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", $"There is already pin with name {pinInfo.Label}", "OK");
                PinLabel = string.Empty;
            }
        }
        private async void OnGoBackCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }
        //rework
        private void OnLatitudeChangedCommandAsync(string value)
        {
            //if (value != null)
            //{
            //    if (Convert.ToDouble(value) < -85 || Convert.ToDouble(value) > 85)
            //    {
            //        Application.Current.MainPage.DisplayAlert("Sorry", "It doesn't look like appropriate latitude", "Cancel");
            //        PinLatitude = 0;
            //    }
            //    else
            //    {
            //        Pin.Position = new Position(PinLatitude, PinLongitude);
            //    }
            //}
        }
        //rework
        private void OnLongitudeChangedCommandAsync(string value)
        {
        //    if (value != null)
        //    {
        //        if (Convert.ToDouble(value) < -180 || Convert.ToDouble(value) > 180)
        //        {
        //            Application.Current.MainPage.DisplayAlert("Sorry", "It doesn't look like appropriate longitude", "Cancel");
        //            PinLongitude = 0;
        //        }
        //        else
        //        {
        //            Pin.Position = new Position(PinLatitude, PinLongitude);
        //        }
        //    }
        }

        #endregion
    }
}
