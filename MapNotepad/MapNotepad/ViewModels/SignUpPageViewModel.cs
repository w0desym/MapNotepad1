using MapNotepad.Models;
using MapNotepad.Services;
using Prism.Navigation;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapNotepad.ViewModels
{
    class SignUpPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IUserService _userService;

        public SignUpPageViewModel(INavigationService navigationService,
            IUserService userService) :
            base(navigationService)
        {
            Title = "Sign Up";
            _navigationService = navigationService;
            _userService = userService;
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

        #region -- INavigationAware implementation --
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue(nameof(User), out User newUser))
            {
                Email = newUser.Email;
                Name = newUser.Name;
            }
        }

        #endregion

        #region -- Overrides --
        [Obsolete]
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsSignUpEnabled))
            {
                (SignUpCommand as Command).ChangeCanExecute();
            }
        }

        #endregion

        #region -- Private Helpers --
        private async void OnSignUpCommandAsync()
        {
            int id = -1;

            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password))
            {
                id = await _userService.RegisterAsync(Email, Name, Password);
            }
            if (id != -1)
            {
                var navParams = new NavigationParameters 
                {
                    {
                        nameof(Email), Email
                    },
                    {
                        nameof(Password), Password
                    }
                };

                await Application.Current.MainPage.DisplayAlert("", "Registration is successful", "OK");
                await _navigationService.GoBackAsync(navParams);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("", "User with such email already exists", "OK");
            }
        }

        private bool CanSignUp()
        {
            return IsSignUpEnabled;
        }

        #endregion
    }
}
