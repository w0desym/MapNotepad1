using MapNotepad.Services;
using MapNotepad.Views;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class TabsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        public TabsPageViewModel(
            INavigationService navigationService,
            IUserService userService,
            IPermissionService permissionService) 
            : base(navigationService)
        {
            _navigationService = navigationService;
            _userService = userService;
            _permissionService = permissionService;
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
            if (await _permissionService.RequestPermissionAsync<Permissions.Camera>() == PermissionStatus.Granted)
            {
                await _navigationService.NavigateAsync($"{nameof(QRScanPage)}", useModalNavigation: true);
            }
        }
        #endregion
    }
}
