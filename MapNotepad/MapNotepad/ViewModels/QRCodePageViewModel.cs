using MapNotepad.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MapNotepad.ViewModels
{
    class QRCodePageViewModel : ViewModelBase
    {
        public QRCodePageViewModel(INavigationService navigationService) :
            base(navigationService)
        {

        }

        #region -- Public properties --

        private string _qRCodeValue;
        public string QRCodeValue
        {
            get => _qRCodeValue;
            set => SetProperty(ref _qRCodeValue, value);
        }

        #endregion

        #region -- INavigationAware implementation -- 

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(PinInfo), out PinInfo pinInfo))
            {
                QRCodeValue = pinInfo.Label + "\n" + pinInfo.Latitude + "\n" + pinInfo.Longitude + "\n" + pinInfo.Description;
            }
        }

        #endregion
    }
}
