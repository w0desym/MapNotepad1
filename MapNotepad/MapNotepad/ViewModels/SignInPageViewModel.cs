using MapNotepad.Models;
using MapNotepad.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
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
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private ICommand _signInCommand;
        public ICommand SignInCommand => _signInCommand ??= new Command(OnSignInCommandAsync);

        private ICommand _signUpViaGoogleCommand;
        public ICommand SignUpViaGoogleCommand => _signUpViaGoogleCommand ??= new Command(OnSignUpViaGoogleCommandAsync);

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new Command(OnSignUpCommandAsync);

        #endregion

        #region -- INavigationAware implementation --

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            var navMode = parameters.GetNavigationMode();
            if (navMode == NavigationMode.Back)
            {
                if (parameters.TryGetValue(nameof(User), out User user))
                {
                    Email = user.Email; 
                    Password = user.Password;
                }
            }
        }

        #endregion

        #region -- Private helpers --

        private async void OnSignInCommandAsync()
        {
            int id = await _authenticationService.AuthenticateAsync(Email, Password);
            if (id != 0)
            {
                _authorizationService.Authorize(id);
                await _navigationService.NavigateAsync($"/{nameof(TabsPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Whoops", "Something went wrong", "OK");
                Password = string.Empty;
            }
        }

        private async void OnSignUpViaGoogleCommandAsync()
        {
            var user = await _authorizationService.LoginGoogleAsync();

            if (user.Email != null)
            {
                int id = await _authenticationService.AuthenticateAsync(user.Email);
                if (id != 0)
                {
                    _authorizationService.Authorize(id);
                    await _navigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(TabsPage)}");
                }
                else
                {
                    NavigationParameters navParams = new NavigationParameters { { $"{nameof(User)}", user } };
                    await _navigationService.NavigateAsync($"{nameof(SignUpPage)}", navParams);
                }
            }
            else
            {

            }
            _authorizationService.LogoutGoogle();
        }

        private async void OnSignUpCommandAsync()
        {
            await _navigationService.NavigateAsync($"{nameof(SignUpPage)}");
        }
        #endregion
    }
}
