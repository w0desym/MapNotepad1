using MapNotepad.Models;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
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
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private bool _isSignUpEnabled;
        public bool IsSignUpEnabled
        {
            get => _isSignUpEnabled;
            set => SetProperty(ref _isSignUpEnabled, value);
        }

        private ICommand _signUpCommand;
        public ICommand SignUpCommand => _signUpCommand ??= new Command(OnSignUpCommandAsync, CanSignUp);

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsSignUpEnabled))
            {
                (SignUpCommand as Command).ChangeCanExecute();
            }
        }
        private async void OnSignUpCommandAsync()
        {
            User newUser = new User()
            {
                Email = this.Email,
                Name = this.Name,
                Password = this.Password
            };

            int answer = await _authorizationService.RegisterAsync(newUser);
            if (answer != -1)
            {
                NavigationParameters navParams = new NavigationParameters { { "credentials", newUser } };

                await App.Current.MainPage.DisplayAlert("", "Registration is successful", "OK");
                await _navigationService.GoBackAsync(navParams);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "User with such email already exists", "OK");
            }
        }

        private bool CanSignUp()
        {
            return IsSignUpEnabled;
        }
    }
}
