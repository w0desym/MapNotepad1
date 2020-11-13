using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services;
using MapNotepad.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace MapNotepad.ViewModels
{
    class TabsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public TabsPageViewModel(
            INavigationService navigationService,
            IUserService userService) :
            base(navigationService)
        {
            Title = "Map Notepad";
            _navigationService = navigationService;
            _userService = userService;
        }

        #region -- Public Properties --

        public ICommand _LogOutCommand;
        public ICommand LogOutCommand => _LogOutCommand ??= new Command(OnLogOutCommandAsync);

        public ICommand _ScanQRCommand;
        public ICommand ScanQRCommand => _ScanQRCommand ??= new Command(OnScanQRCommandAsync);

        #endregion

        #region -- Private Helpers --
        private async void OnLogOutCommandAsync()
        {
            _userService.Logout();
            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }
        private async void OnScanQRCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(QRScanPage)}", useModalNavigation: true);
        }
        #endregion
    }
}
