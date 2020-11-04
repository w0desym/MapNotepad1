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
        private readonly ISettingsManager _settingsManager;
        public TabsPageViewModel(
            INavigationService navigationService,
            ISettingsManager settingsManager) :
            base(navigationService)
        {
            Title = "Map Notepad";
            _navigationService = navigationService;
            _settingsManager = settingsManager;
        }

        public ICommand _LogOutCommand;
        public ICommand LogOutCommand => _LogOutCommand ??= new Command(OnLogOutCommandAsync);

        private async void OnLogOutCommandAsync()
        {
            _settingsManager.CurrentUser = -1;
            await _navigationService.NavigateAsync($"/{nameof(SignInPage)}");
        }
    }
}
