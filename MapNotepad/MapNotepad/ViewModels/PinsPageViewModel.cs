using Acr.UserDialogs;
using MapNotepad.Extensions;
using MapNotepad.Models;
using MapNotepad.Views;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Plugin.Popups;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class PinsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPinService _pinService;
        private readonly IUserDialogs _userDialogs;

        public PinsPageViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IUserDialogs userDialogs) :
            base(navigationService)
        {
            _userDialogs = userDialogs;
            _pinService = pinService;
            _navigationService = navigationService;
        }

        #region -- Public properties --

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private ObservableCollection<PinInfo> _pinsCollection;
        public ObservableCollection<PinInfo> PinsCollection
        {
            get => _pinsCollection;
            set => SetProperty(ref _pinsCollection, value);
        }

        private PinInfo _pinInfo;
        public PinInfo PinInfo
        {
            get => _pinInfo;
            set => SetProperty(ref _pinInfo, value);
        }

        private ImageSource _imageButtonSource;
        public ImageSource ImageButtonSource
        {
            get => _imageButtonSource;
            set => SetProperty(ref _imageButtonSource, value);
        }

        private ICommand _searchCommand;
        public ICommand SearchCommand => _searchCommand ??= new Command(OnSearchCommand);

        private ICommand _addPinCommand;
        public ICommand AddPinCommand => _addPinCommand ??= new Command(OnAddPinCommandAsync);

        private ICommand _editPinCommand;
        public ICommand EditPinCommand => _editPinCommand ??= new Command<PinInfo>(OnEditPinCommandAsync);

        private ICommand _deletePinCommand;
        public ICommand DeletePinCommand => _deletePinCommand ??= new Command<PinInfo>(OnDeletePinCommandAsync);

        private ICommand _pinCommand;
        public ICommand PinCommand => _pinCommand ??= new Command<PinInfo>(OnPinCommandAsync);

        private ICommand _imageButtonCommand;
        public ICommand ImageButtonCommand => _imageButtonCommand ??= new Command<PinInfo>(OnImageButtonCommand);

        #endregion

        #region -- IInitialize implementation
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            LoadPinsCollection();
        }
        #endregion

        #region -- INavigationAware implementation --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            LoadPinsCollection();
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            if (PinInfo != null)
            {
                var pin = PinInfo.ToPin();
                parameters.Add(nameof(Pin), pin);
            }
        }
        #endregion

        #region -- Private helpers --
        private async void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                var pins = await _pinService.GetPinsAsync(SearchQuery);
                PinsCollection = new ObservableCollection<PinInfo>(pins);
            }
            else
            {
                LoadPinsCollection();
            }
        }
        private async void OnAddPinCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(AddPinPage)}");
        }
        private async void OnEditPinCommandAsync(PinInfo pinInfo)
        {
            NavigationParameters navParams = new NavigationParameters
            {
                { nameof(PinInfo), pinInfo }
            };

            await _navigationService.NavigateAsync($"{nameof(AddPinPage)}", navParams);
        }
        private async void OnDeletePinCommandAsync(PinInfo pinInfo)
        {
            var answer = await _userDialogs.ConfirmAsync(new ConfirmConfig()
                .SetMessage($"Delete {pinInfo.Label}?")
                .UseYesNo());
            if (answer)
            {
                await _pinService.DeletePinInfoAsync(pinInfo);
                LoadPinsCollection();
            }
        }
        private async void OnPinCommandAsync(PinInfo pinInfo)
        {
            PinInfo = pinInfo;
            await _navigationService.SelectTabAsync($"{nameof(MapPage)}");
        }
        private async void OnImageButtonCommand(PinInfo pinInfo)
        {
            if (pinInfo.IsFavorite)
            {
                pinInfo.ImgPath = Constants.NotFavoriteImagePath;
            }
            else
            {
                pinInfo.ImgPath = Constants.FavoriteImagePath;
            }
            pinInfo.IsFavorite = !pinInfo.IsFavorite;

            await _pinService.SavePinInfoAsync(pinInfo);
            LoadPinsCollection();
        }


        private async void LoadPinsCollection()
        {
            var pins = await _pinService.GetPinsAsync();
            PinsCollection = new ObservableCollection<PinInfo>(pins);
        }
        #endregion
    }
}
