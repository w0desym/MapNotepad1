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

        private ObservableCollection<Grouping<string, PinInfo>> _pinsCollection;
        public ObservableCollection<Grouping<string, PinInfo>> PinsCollection
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

        private ICommand _sharePinCommand;
        public ICommand SharePinCommand => _sharePinCommand ??= new Command<PinInfo>(OnSharePinCommandAsync);

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
            LoadPinsCollectionAsync();
        }
        #endregion

        #region -- INavigationAware implementation --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            LoadPinsCollectionAsync();
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

        private void OnPageTapCommand()
        {

        }

        private void OnSearchCommand()
        {
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                LoadPinsCollectionAsync(SearchQuery);
            }
            else
            {
                LoadPinsCollectionAsync();
            }
        }

        private async void OnAddPinCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(AddPinPage)}");
        }

        private async void OnSharePinCommandAsync(PinInfo pinInfo)
        {
            var navParams = new NavigationParameters { { nameof(PinInfo), pinInfo } };

            await _navigationService.NavigateAsync($"{nameof(QRCodePage)}", navParams, useModalNavigation: true);
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
                .UseYesNo());//vinesi v peremennuyu
            if (answer)
            {
                await _pinService.DeletePinInfoAsync(pinInfo);
                LoadPinsCollectionAsync();
            }
        }

        private async void OnPinCommandAsync(PinInfo pinInfo)
        {
            PinInfo = pinInfo;
            await _navigationService.SelectTabAsync($"{nameof(MapPage)}");
        }

        private async void OnImageButtonCommand(PinInfo pinInfo)
        {
            pinInfo.ImgPath = pinInfo.IsFavorite ? Constants.NotFavoriteImagePath : Constants.FavoriteImagePath;
            pinInfo.IsFavorite = !pinInfo.IsFavorite;

            await _pinService.SavePinInfoAsync(pinInfo);
            LoadPinsCollectionAsync();
        }

        private async void LoadPinsCollectionAsync(string searchQuery = null)
        {
            var pins = string.IsNullOrEmpty(searchQuery) ? await _pinService.GetPinsAsync() : await _pinService.GetPinsAsync(searchQuery);
            var groups = pins.GroupBy(p => p.Category).Select(g => new Grouping<string, PinInfo>(g.Key, g));

            PinsCollection = new ObservableCollection<Grouping<string, PinInfo>>(groups);
        }

        #endregion
    }
}

//var groups = new List<Grouping<PinInfo>>
//{
//    new Grouping<PinInfo>("Hui")
//    {
//        pins.First(),
//        pins.ElementAt(1)
//    },
//    new Grouping<PinInfo>("Pizda")
//    {
//        pins.ElementAt(3),
//        pins.ElementAt(2),
//        pins.ElementAt(4)
//    }
//};
