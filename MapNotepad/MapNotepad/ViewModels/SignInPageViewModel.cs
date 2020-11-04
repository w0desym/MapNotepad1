using MapNotepad.Models;
using MapNotepad.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class SignInPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;

        public SignInPageViewModel(
            INavigationService navigationService,
            IAuthenticationService authenticationService,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            Title = "Sign In";
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        #region -- Public Properties --
        private string _email;
        private string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        private string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private ICommand _signInCommand;
        public ICommand SignInCommand => _signInCommand ??= new Command(OnSignInCommandAsync);

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new Command(OnSignUpCommandAsync);

        #endregion

        #region -- INavigationAware implementation --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var navMode = parameters.GetNavigationMode();
            if (navMode == 0)
            {
                var _parameters = parameters.GetValue<User>("credentials");
                if (_parameters != null)
                {
                    Email = _parameters.Email; 
                    Password = _parameters.Password;
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private async void OnSignInCommandAsync()
        {
            int id = _authenticationService.Authenticate(Email, Password);
            if (id != 0)
            {
                _authorizationService.Authorize(id);
                await _navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(TabsPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Whoops", "Something went wrong", "OK");
                Password = string.Empty;
            }
        }
        private async void OnSignUpCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }
        #endregion
    }
}
