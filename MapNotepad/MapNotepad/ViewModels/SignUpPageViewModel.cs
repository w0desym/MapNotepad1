using MapNotepad.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class SignUpPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IAuthorizationService _authorizationService;

        public SignUpPageViewModel(INavigationService navigationService,
            IAuthenticationService authenticationService,
            IAuthorizationService authorizationService) :
            base(navigationService)
        {
            Title = "Sign Up";
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _authorizationService = authorizationService;
        }

        #region -- Public properties --

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

        private string _confirmPassword;
        private string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new Command(OnSignUpCommandAsync);
        #endregion

        private async void OnSignUpCommandAsync()
        {
            int answer = _authenticationService.Authenticate(Email, Password);
            if (answer != 0)
            {
                await App.Current.MainPage.DisplayAlert("", "User with such credentials already exists", "OK");
            }
            else
            {
                User newUser = new User()
                {
                    Email = this.Email,
                    Password = this.Password
                };
                _authorizationService.Register(newUser);

                NavigationParameters navParams = new NavigationParameters { { "credentials", newUser } };

                await App.Current.MainPage.DisplayAlert("", "Registration is successful", "OK");
                await _navigationService.GoBackAsync(navParams);
            }
        }
    }
}
