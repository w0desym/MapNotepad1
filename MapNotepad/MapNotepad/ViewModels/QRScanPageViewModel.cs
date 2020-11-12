using MapNotepad.Views;
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
        public QRScanPageViewModel(INavigationService navigationService) :
            base(navigationService)
        {
            _navigationService = navigationService;
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

        private void OnQRScanResultCommand()
        {
            var navParams = new NavigationParameters { { nameof(Result), Result } };

            _navigationService.GoBackAsync(navParams);
        }

        #endregion
    }
}
