using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services;
using MapNotepad.Views;
using Newtonsoft.Json;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace MapNotepad.ViewModels
{
    class QRScanPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPermissionService _permissionService;
        private readonly IPinService _pinService;
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;
        public QRScanPageViewModel(
            INavigationService navigationService,
            IPermissionService permissionService,
            IPinService pinService,
            IUserService userService,
            IUserDialogs userDialogs) :
            base(navigationService)
        {
            _navigationService = navigationService;
            _permissionService = permissionService;
            _pinService = pinService;
            _userService = userService;
            _userDialogs = userDialogs;
        }

        #region -- Public properties --

        private Result _result;
        public Result Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private bool _isScanning = true;
        public bool IsScanning
        {
            get => _isScanning;
            set => SetProperty(ref _isScanning, value);
        }

        private ICommand _QRScanResultCommand;
        public ICommand QRScanResultCommand => _QRScanResultCommand ??= new Command(OnQRScanResultCommand);

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ??= new Command(OnGoBackCommandCommand);

        private void OnGoBackCommandCommand()
        {
            _navigationService.GoBackAsync();
        }

        #endregion

        #region -- INavigationAware implementation --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            //try to do camera permission
        }

        #endregion

        #region -- Private helpers --

        private async void OnQRScanResultCommand()
        {
            if (Result != null)
            {
                try
                {
                    var pinValue = JsonConvert.DeserializeObject<PinInfo>(Result.Text);
                    pinValue.UserId = _userService.CurrentUserId;
                    pinValue.Id = 0;

                    await _pinService.AddPinInfoAsync(pinValue);
                    await _navigationService.GoBackAsync();

                }
                catch
                {
                    await _userDialogs.AlertAsync("QR is not valid", "Sorry", "OK");
                }
            }
        }

        #endregion
    }
}
