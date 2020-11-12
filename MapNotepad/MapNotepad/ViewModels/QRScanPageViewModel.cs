using Acr.UserDialogs;
using MapNotepad.Models;
using MapNotepad.Services;
using MapNotepad.Views;
using Newtonsoft.Json;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace MapNotepad.ViewModels
{
    class QRScanPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPinService _pinService;
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;
        public QRScanPageViewModel(
            INavigationService navigationService,
            IPinService pinService,
            IUserService userService,
            IUserDialogs userDialogs) :
            base(navigationService)
        {
            _navigationService = navigationService;
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

        private ICommand _QRScanResultCommand;
        public ICommand QRScanResultCommand => _QRScanResultCommand ??= new Command(OnQRScanResultCommand);

        private ICommand _GoBackCommand;
        public ICommand GoBackCommand => _GoBackCommand ??= new Command(OnGoBackCommandCommand);

        private void OnGoBackCommandCommand()
        {
            _navigationService.GoBackAsync();
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

                    var answer = await _userDialogs.ConfirmAsync(new ConfirmConfig()
                        .SetTitle("Confirm adding pin")
                        .SetMessage($"{pinValue.Label}\n{pinValue.Latitude}\n{pinValue.Longitude}")
                        .UseYesNo());

                    if (answer)
                    {
                        await _pinService.SavePinInfoAsync(pinValue);
                        await _navigationService.GoBackAsync();
                    }
                }
                catch
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry", "QR is not valid", "OK");
                }
            }
        }

        #endregion
    }
}
