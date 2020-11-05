using MapNotepad.Models;
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

        public ICommand _LogOutCommand;
        public ICommand LogOutCommand => _LogOutCommand ??= new Command(OnLogOutCommandAsync);

        private async void OnLogOutCommandAsync()
        {
            _userService.SetCurrentUser(-1);
            await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignInPage)}");
        }
    }
}
