using MapNotepad.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class QRCodePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public QRCodePageViewModel(
            INavigationService navigationService) 
            : base(navigationService)
        {
            _navigationService = navigationService;
        }

        #region -- Public properties --

        private string _qRCodeValue;
        public string QRCodeValue
        {
            get => _qRCodeValue;
            set => SetProperty(ref _qRCodeValue, value);
        }

        private ICommand _goBackCommand;
        public ICommand GoBackCommand => _goBackCommand ??= new Command(OnGoBackCommandAsync);

        #endregion

        #region -- INavigationAware implementation -- 

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(PinInfo), out PinInfo pinInfo))
            {
                QRCodeValue = JsonConvert.SerializeObject(pinInfo);
            }
        }

        #endregion

        #region -- Private helpers --

        private async void OnGoBackCommandAsync()
        {
            await _navigationService.GoBackAsync();
        }

        #endregion
    }
}
