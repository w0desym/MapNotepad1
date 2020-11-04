using MapNotepad.Controls;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.GoogleMaps;
using MapNotepad.Extensions;
using Xamarin.Essentials;
using MapNotepad.Models;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class MapPageViewModel : ViewModelBase
    {
        private readonly IPinService _pinService;

        public MapPageViewModel(
            INavigationService navigationService,
            IPinService pinService) :
            base(navigationService)
        {
            _pinService = pinService;
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

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ??= new Command(OnSearchCommand);

        private ICommand _pinCommand;
        public ICommand PinCommand => _pinCommand ??= new Command<Pin>(OnPinCommand);
        #endregion

        #region -- IInitialize implementation --
        public override void Initialize(INavigationParameters parameters)
        {
            LoadPinsCollection();
            UpdateCameraPositionAsync();
        }
        #endregion

        #region -- INavigationAware implementation --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            LoadPinsCollection();
            if (parameters.TryGetValue(nameof(Pin), out Pin pin))
            {
                SelectedPin = PinsCollection.FirstOrDefault(x => x.Label == pin.Label);
                AssignPinPopup(pin);
            }
            UpdateCameraPositionAsync();
        }
        #endregion

        #region -- Private helpers --
        private void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                PinsCollection = new ObservableCollection<Pin>(_pinService.GetPins(SearchQuery).Select(x => x.ToPin()));
            }
            else
            {
                LoadPinsCollection();
            }
        }
        private void OnPinCommand(Pin pin)
        {
            AssignPinPopup(pin);
        }

        private void AssignPinPopup(Pin pin)
        {
            var pinInfo = pin.ToPinInfo();

            PinLabel = pinInfo.Label;
            PinDescription = pinInfo.Description;
            PinLatitude = Math.Truncate(pinInfo.Latitude * 1000) / 1000;
            PinLongitude = Math.Truncate(pinInfo.Longitude * 1000) / 1000;
        }

        private void LoadPinsCollection()
        {
            PinsCollection = new ObservableCollection<Pin>(_pinService.GetPins().Where(x => x.IsFavorite == true).Select(x => x.ToPin()));
        }
        private async void UpdateCameraPositionAsync()
        {
            if (SelectedPin != null)
            {
                CameraPosition = new CameraPosition(SelectedPin.Position, 15.0d);
            }
            else
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.High));

                if (location != null)
                {
                    CameraPosition = new CameraPosition(new Position(location.Latitude, location.Longitude), 15.0d);
                }
            }

        }
        #endregion
    }
}
